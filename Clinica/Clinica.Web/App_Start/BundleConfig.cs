using System.Web;
using System.Web.Optimization;

namespace Clinica.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts: General
            bundles.Add(new ScriptBundle("~/bundles/js/own/general").Include(
                "~/Scripts/common/Enums.js",
                "~/Scripts/common/Shared.js"
            ));

            //Bundle for CSS Main Plugins
            bundles.Add(new StyleBundle("~/Content/css/plugins/main").Include(
                "~/Content/plugins/bootstrap.css",
                "~/Content/plugins/bootstrap-datetimepicker.css",
                "~/Content/plugins/jquery.gritter.css"
            ));

            //Bundle for CSS OWN Main
            bundles.Add(new StyleBundle("~/Content/css/own/main").Include(
                "~/Content/custom/Site.css",
                "~/Content/custom/Responsive.css"
            ));

            //Bundle for JS Main Plugins
            bundles.Add(new ScriptBundle("~/bundles/js/main").Include(
                "~/Scripts/plugins/jquery-{version}.js",
                "~/Scripts/plugins/jquery.validate.js",
                "~/Scripts/plugins/jquery.validate.unobtrusive.js",
                "~/Scripts/plugins/jquery.unobtrusive-ajax.js",
                "~/Scripts/plugins/bootstrap.js",
                "~/Scripts/plugins/respond.js",
                "~/Scripts/plugins/spin.js",
                "~/Scripts/plugins/moment.js",
                "~/Scripts/plugins/bootstrap-datetimepicker.js",
                "~/Scripts/plugins/jquery.spin.js",
                "~/Scripts/plugins/jquery.gritter.js"
            ));

            //Scripts: Economy Main Login
            bundles.Add(new ScriptBundle("~/bundles/js/own/main/login").Include(
                "~/Scripts/login/LoginFunctions.js",
                "~/Scripts/login/Login.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/own/main/citas").Include(
                "~/Scripts/citas/CitasFunctions.js",
                "~/Scripts/citas/Citas.js"
            ));   
        }
    }
}
