using System.Web;
using System.Web.Optimization;

namespace Xland
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-datepicker.js"
                ));

            // Page bundle
            bundles.Add(new ScriptBundle("~/bundles/front").Include(
                "~/Scripts/lib/lib-front.js",
                "~/Scripts/lib/classie.js",
                "~/Scripts/lib/selectFX.js",
                "~/Scripts/application-front.js"));

            // Admin bundle
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/lib/lib-admin.js",
                "~/Scripts/Application-admin.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/map").Include(
                "~/Scripts/map/mapstyle.js",
                "~/Scripts/map/mapSetup.js"
                ));

            
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.min.js"));


            bundles.Add(new StyleBundle("~/bundles/pluginCss").Include(
                "~/Content/pluginCss/font-awesome.min.css",
                "~/Content/pluginCss/cs-select.css",
                "~/Content/pluginCss/cs-skin-underline.css"
                
                ));

            bundles.Add(new LessBundle("~/Content/less").Include("~/Content/less/bootstrap.less"));

        }
    }
}