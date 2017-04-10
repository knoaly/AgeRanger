using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using AgeRangerBO.Managers;
using System.Configuration;

namespace AgeRangerWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //register interfaces and their implementations specifying which constructor to instantiate
            var webUrl = ConfigurationManager.AppSettings["WebServiceUrl"];
            container.RegisterType<IPeopleManager, PeopleManager>(new InjectionConstructor(webUrl));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}