using System.Web.Optimization;

namespace EStore.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content-css")
                .Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/styles/metronic-mandatory")
                .Include("~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                    "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                    "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                    "~/Content/assets/global/plugins/uniform/css/uniform.default.css",
                    "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"
                ));

            bundles.Add(new StyleBundle("~/styles/metronic-page-level")
                .Include("~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                    "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css",
                    "~/Content/assets/global/plugins/jqvmap/jqvmap/jqvmap.css",
                    "~/Content/assets/admin/pages/css/tasks.css",
                    "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.css"
                ));

            bundles.Add(new StyleBundle("~/styles/metronic-theme")
                .Include(
                "~/Content/assets/global/css/components-rounded.css",
                "~/Content/assets/global/css/plugins.css",
                "~/Content/assets/admin/layout2/css/layout.css",
                "~/Content/assets/admin/layout2/css/themes/dark.css",
                "~/Content/assets/admin/layout2/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/javascript/metronic-core-scripts")
                .Include(
                    "~/Content/assets/global/plugins/jquery.min.js",
                    "~/Content/assets/global/plugins/jquery-migrate.js",
                    "~/Content/assets/global/plugins/jquery-ui/jquery-ui.js",
                    "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                    "~/Content/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                    "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                    "~/Content/assets/global/plugins/jquery.blockui.min.js",
                    "~/Content/assets/global/plugins/jquery.cokie.min.js",
                    "~/Content/assets/global/plugins/uniform/jquery.uniform.min.js",
                    "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                    "~/Scripts/common/custom.ui.toastr.js",
                    "~/Content/assets/global/scripts/metronic.js",
                    "~/Content/assets/admin/layout2/scripts/layout.js",
                    "~/Content/assets/admin/layout2/scripts/demo.js",
                    "~/Content/assets/admin/pages/scripts/index.js"
                ));

            bundles.Add(new ScriptBundle("~/javascript/metronic-page-level-scripts")
                .Include(
                    "~/Content/assets/global/scripts/metronic.js",
                    "~/Content/assets/admin/layout2/scripts/layout.js",
                    "~/Content/assets/admin/layout2/scripts/demo.js",
                    "~/Content/assets/admin/pages/scripts/index.js",
                    "~/Content/assets/admin/pages/scripts/tasks.js",
                    "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.js"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}