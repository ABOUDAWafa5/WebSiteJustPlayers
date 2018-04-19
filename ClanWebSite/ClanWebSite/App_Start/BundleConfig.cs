using System.Web;
using System.Web.Optimization;

namespace ClanWebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/classie.js",
                      "~/Scripts/controls.js",
                      "~/Scripts/easing.js",
                      "~/Scripts/jquery.chocolat.js",
                      "~/Scripts/jquery.filterizr.js",
                      "~/Scripts/move-top.js",
                      "~/Scripts/responsiveslides.min.js",
                      "~/Scripts/SmoothScroll.min.js",
                       "~/Scripts/jarallax.js",
                       "~/Scripts/CustomScripts/mainPageCustom.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/tournaments").Include(                   
                    "~/Scripts/CustomScripts/tournaments.js"
                    ));
            


            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                   "~/Scripts/grid-custom.js"
              ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/chocolat.css",
                      "~/Content/site.css",
                      "~/Content/Navigation.css"));

            bundles.Add(new StyleBundle("~/Content/spinner").Include(
                "~/Content/spinner.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
