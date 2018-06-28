using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Description;

namespace LtePlatform.Models
{
    public static class HttpFileUploadService
    {
        public static string UploadKpiFile(this HttpPostedFileBase httpPostedFileBase)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads\\Kpi",
                    httpPostedFileBase.FileName);
            httpPostedFileBase.SaveAs(path);
            return path;
        }

        public static string UploadParametersFile(this HttpPostedFileBase httpPostedFileBase)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads\\Parameters",
                    httpPostedFileBase.FileName);
            httpPostedFileBase.SaveAs(path);
            return path;
        }

        public static string UrlEncode(this string text)
        {
            var urlEncode = HttpUtility.UrlEncode(text.Replace(" ", ""));
            if (urlEncode != null)
                return urlEncode.Replace("+", "%2B")
                    .Replace("(", "%28").Replace(")", "%29")
                    .Replace("[", "%5B").Replace("]", "%5D")
                    .Replace("-", "_");
            return "";
        }
        
    }

    public class FileResultMessage
    {
        public string Error { get; set; }
        public string File { get; set; }
    }

    public class HelpPageApiModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPageApiModel"/> class.
        /// </summary>
        public HelpPageApiModel()
        {
            SampleRequests = new Dictionary<MediaTypeHeaderValue, object>();
            SampleResponses = new Dictionary<MediaTypeHeaderValue, object>();
            ErrorMessages = new Collection<string>();
        }

        public ApiDescription ApiDescription { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ParameterDescription"/> collection that describes the URI parameters for the API.
        /// </summary>
        public Collection<ParameterDescription> UriParameters { get; set; }

        /// <summary>
        /// Gets or sets the documentation for the request.
        /// </summary>
        public string RequestDocumentation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ModelDescription"/> that describes the request body.
        /// </summary>
        public ModelDescription RequestModelDescription { get; set; }

        /// <summary>
        /// Gets the request body parameter descriptions.
        /// </summary>
        public IList<ParameterDescription> RequestBodyParameters => RequestModelDescription.GetParameterDescriptions();

        /// <summary>
        /// Gets or sets the <see cref="ModelDescription"/> that describes the resource.
        /// </summary>
        public ModelDescription ResourceDescription { get; set; }

        /// <summary>
        /// Gets the resource property descriptions.
        /// </summary>
        public IList<ParameterDescription> ResourceProperties => ResourceDescription.GetParameterDescriptions();

        /// <summary>
        /// Gets the sample requests associated with the API.
        /// </summary>
        public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

        /// <summary>
        /// Gets the sample responses associated with the API.
        /// </summary>
        public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

        /// <summary>
        /// Gets the error messages associated with this model.
        /// </summary>
        public Collection<string> ErrorMessages { get; private set; }

    }
}
