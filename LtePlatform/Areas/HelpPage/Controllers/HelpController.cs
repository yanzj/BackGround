using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using LtePlatform.Models;

namespace LtePlatform.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; }

        public JsonResult ApiDescriptions(string theme)
        {
            var provider = Configuration.Services.GetDocumentationProvider();
            return Json(Configuration.Services.GetApiExplorer().ApiDescriptions.Select(api =>
            {
                var descriptor = api.ActionDescriptor.ControllerDescriptor;
                var groupAttribute = descriptor.GetCustomAttributes<ApiGroupAttribute>().FirstOrDefault();
                return new
                {
                    descriptor.ControllerName,
                    ControllerType = descriptor.ControllerType.ToString(),
                    Documentation = provider.GetDocumentation(descriptor),
                    GroupDoc = groupAttribute != null ? groupAttribute.Documentation : ""
                };
            }).Where(api => api.GroupDoc == theme).Distinct(),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApiMethod(string controllerName)
        {
            var description =
                Configuration.Services.GetApiExplorer()
                    .ApiDescriptions.Where(
                        api => api.ActionDescriptor.ControllerDescriptor.ControllerName == controllerName).ToList();
            var provider = Configuration.Services.GetDocumentationProvider();
            var modelGenerator = Configuration.GetModelDescriptionGenerator();
            var self = Configuration.Services.GetApiExplorer()
                .ApiDescriptions.FirstOrDefault(
                    api => api.ActionDescriptor.ControllerDescriptor.ControllerName == controllerName)?
                .ActionDescriptor?.ControllerDescriptor;
            return Json(new
            {
                FullPath = self?.ControllerType.ToString() ?? "",
                Documentation = self != null ? provider.GetDocumentation(self) : "",
                ActionList = description.Select(api => new
                {
                    FriendlyId = api.GetFriendlyId(),
                    MethodName = api.HttpMethod.Method,
                    RelativePath = api.RelativePath,
                    Documentation = api.Documentation,
                    ResponseName = api.GenerateResponseDescription(modelGenerator)?.Name,
                    ResponseDocumentation = provider.GetResponseDocumentation(api.ActionDescriptor)
                })
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ApiActionDoc(string apiId)
        {
            var description = Configuration.Services.GetApiExplorer().ApiDescriptions
                .FirstOrDefault(x => x.GetFriendlyId() == apiId);
            if (description == null) return null;
            var modelGenerator = Configuration.GetModelDescriptionGenerator();
            var sampleGenerator = Configuration.GetHelpPageSampleGenerator();
            var parametersDescriptions = description.GenerateUriParameters(modelGenerator);
            var requestModelDescription = description.GenerateRequestModelDescription(modelGenerator, sampleGenerator);
            var responseModel = description.GenerateResponseDescription(modelGenerator);
            var modelDescription = modelGenerator.GetOrCreateModelDescription(responseModel.ModelType);
            var responseDescription = responseModel.GetParameterDescriptions();
            return Json(new
            {
                ParameterDescriptions = parametersDescriptions.Select(x => new
                {
                    x.Name,
                    x.Documentation,
                    TypeDocumentation = x.TypeDescription.Documentation,
                    TypeName = x.TypeDescription.Name,
                    AnnotationDoc = x.Annotations.Select(an => an.Documentation)
                }),
                FromBodyModel = requestModelDescription == null
                    ? null
                    : new
                    {
                        requestModelDescription.Name,
                        Type = requestModelDescription.ModelType.ToString(),
                        requestModelDescription.Documentation,
                        requestModelDescription.ParameterDocumentation
                    },
                ResponseModel = new
                {
                    responseModel.Name,
                    Documentation =
                        Configuration.Services.GetDocumentationProvider()
                            .GetResponseDocumentation(description.ActionDescriptor),
                    responseModel.ParameterDocumentation,
                    ModelDescription = modelDescription?.ParameterDocumentation,
                    Descriptions = responseDescription?.Select(x => new
                    {
                        x.Name,
                        x.Documentation,
                        AnnotationDoc = x.Annotations.Select(an => an.Documentation),
                        TypeDescription = x.TypeDescription.Name
                    })
                }
            },
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApiDescriptions()
        {
            return Json(Configuration.Services.GetApiExplorer().ApiDescriptions.Select(x=> new 
            {
                x.Documentation,
                Id = x.GetFriendlyId()
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApiDetails(string apiId)
        {
            return Json(Configuration.GetHelpPageApiModel(apiId)?.UriParameters.Select(des=>new
            {
                des.Name,
                Annotations = des.Annotations.Select(x=>x.Documentation),
                des.Documentation,
                TypeDocumentation = des.TypeDescription.Documentation,
                TypeName = des.TypeDescription.Name,
                Type = des.TypeDescription.ModelType.ToString()
            }), 
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (string.IsNullOrEmpty(modelName)) return View(ErrorViewName);
            var modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
            ModelDescription modelDescription;
            return modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription)
                ? View(modelDescription)
                : View(ErrorViewName);
        }
    }
}