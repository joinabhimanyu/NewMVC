﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data;
using System.Data.Entity;

namespace NewMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //Database.SetInitializer<NewMVC.Models.CustomerDB>(new NewMVC.Models.CustomerDBInitializer());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DisplayModeProvider.Instance.Modes.Insert(0,
new DefaultDisplayMode("Phone")
{
    ContextCondition = (context => (
    (context.GetOverriddenUserAgent() != null) &&
    (
    (context.GetOverriddenUserAgent().IndexOf("iPhone",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent().IndexOf("iPod",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent()
    .IndexOf("Droid",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent().IndexOf("Blackberry",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent()
    .StartsWith("Blackberry",
    StringComparison.OrdinalIgnoreCase))
    )
    ))
});

            DisplayModeProvider.Instance.Modes.Insert(0,
new DefaultDisplayMode("Tablet")
{
    ContextCondition = (context => (
    (context.GetOverriddenUserAgent() != null) &&
    (
    (context.GetOverriddenUserAgent().IndexOf("iPad",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent().IndexOf("Playbook",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent()
    .IndexOf("Transformer",
    StringComparison.OrdinalIgnoreCase) >= 0) ||
    (context.GetOverriddenUserAgent().IndexOf("Xoom",
    StringComparison.OrdinalIgnoreCase) >= 0)
    )
    ))
});

        }
    }
}