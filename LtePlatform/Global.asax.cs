﻿using LtePlatform.Areas.HelpPage;
using LtePlatform.Models;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LtePlatform
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.SetDocumentationProvider(new DocProvider());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver();
            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsMessageHandler());
            AutoMapperWebConfiguration.Configure();
        }
    }
}
