using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Grid.Mvc.Ajax.GridExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using VSMMvcTDD.Entities;
using VSMMvcTDD.Models;
using VSMMvcTDD.Services;

namespace VSMMvcTDD.App_Start
{
    public class SimpleInjectorSetup
    {
        public static void Setup()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.Register<IContactService, ContactService>();
            container.Register<IAjaxGridFactory, AjaxGridFactory>();

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}