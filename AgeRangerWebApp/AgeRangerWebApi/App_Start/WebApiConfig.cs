using AgeRangerWebApi.Implementation;
using AgeRangerWebApi.Interface;
using AgeRangerWebApi.Utilities;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AgeRangerWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //log4net setup
            log4net.Config.XmlConfigurator.Configure();

            //Custom exception error handler
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());

            //Resolve Dependencies
            var container = new UnityContainer();
            //register interfaces and their implementations specifying which constructor to instantiate
            container.RegisterType<IPersonService, PersonService>(new InjectionConstructor());

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
