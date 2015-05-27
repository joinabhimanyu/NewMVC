using System.Web;
using System.Web.Optimization;

namespace NewMVC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            //Script Bundles

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/MobileJS").Include(
                        "~/Scripts/jquery.mobile-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/flat-ui").Include(
                        "~/Scripts/flat-ui.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js/frameworks").Include(
                        "~/bower_components/bootstrap/dist/js/bootstrap.js",
                        "~/bower_components/bootstrap/dist/js/npm.js",
                        "~/Scripts/alertify.js",
                        "~/Scripts/velocity.ui.js",
                        "~/Scripts/jquery.velocity.min.js",
                        "~/Scripts/sweet-alert.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-animate.min.js",
                        "~/Scripts/angular-aria.min.js",
                        "~/Scripts/angular-cookies.min.js",
                        "~/Scripts/angular-loader.min.js",
                        "~/Scripts/angular-messages.min.js",
                        "~/Scripts/angular-mocks.js",
                        "~/Scripts/angular-resource.min.js",
                        "~/Scripts/angular-sanitize.min.js",
                        "~/Scripts/angular-scenario.js",
                        "~/Scripts/angular-touch.min.js"));

            //Style Bundles

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/css/frameworks").Include(
                        "~/bower_components/bootstrap/dist/css/bootstrap.css",
                        "~/bower_components/bootstrap/dist/css/bootstrap-theme.css",
                        "~/Content/alertify.core.css",
                        "~/Content/alertify.default.css",
                        "~/Content/sweet-alert.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css/Animate").Include(
                        "~/Content/animate.css"));

            bundles.Add(new StyleBundle("~/Content/MobileCSS").Include(
                        "~/Content/jquery.mobile-{version}.css",
                        "~/Content/jquery.mobile.external-png-{version}.css",
                        "~/Content/jquery.mobile.icons-{version}.css",
                        "~/Content/jquery.mobile.inline-png-{version}.css",
                        "~/Content/jquery.mobile.inline-svg-{version}.css",
                        "~/Content/jquery.mobile.structure-{version}.css",
                        "~/Content/jquery.mobile.theme-{version}.css"
                ));

            bundles.Add(new StyleBundle("~/Content/flat-ui").Include(
                        "~/Content/flat-ui.min.css"));

            // Custom Bundles

            //BundleTable.EnableOptimizations = false;

#if DEBUG
            BundleTable.EnableOptimizations = true;
#else
                BundleTable.EnableOptimizations = true;
#endif



        }
    }
}