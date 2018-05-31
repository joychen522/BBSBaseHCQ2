using System.Web;
using System.Web.Optimization;

namespace HCQ2UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            //合并css
            bundles.Add(new StyleBundle("~/bundles/css/basecss").Include(
                "~/Resources/bootstrap/css/bootstrap.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/baseScript").Include(
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/bootstrap.min.js"
            ));

            //H+通用js合并打包
            bundles.Add(new ScriptBundle("~/bundles/mainScript").Include(
                "~/Resources/mainFrame/js/jquery.min.js",
                "~/Resources/mainFrame/js/bootstrap.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/echarts").Include(
                "~/Resources/mainFrame/js/bootstrap.min.js",
                 "~/Resources/mainFrame/js/content.min.js",
                 "~/Resources/mainFrame/js/echarts.min3.3.1.js",
                 "~/Resources/mainFrame/js/macarons.js"
            ));

            //主页js合并打包
            bundles.Add(new ScriptBundle("~/bundles/indexScript").Include(
                "~/Resources/mainFrame/js/plugins/metisMenu/jquery.metisMenu.js",
                "~/Resources/mainFrame/js/plugins/slimscroll/jquery.slimscroll.min.js",
                "~/Resources/mainFrame/js/hplus.min.js",
                "~/Resources/mainFrame/js/contabs.main.js",
                "~/Resources/mainFrame/js/plugins/pace/pace.min.js"
            ));
            //jqGrid表格JS
            bundles.Add(new ScriptBundle("~/bundles/JqGrid").Include(
                "~/Resources/mainFrame/js/plugins/peity/jquery.peity.min.js",
                "~/Resources/mainFrame/js/plugins/jqgrid/i18n/grid.locale-cnffe4.js",
                "~/Resources/mainFrame/js/plugins/jqgrid/jquery.jqGrid.minffe4.js",
                "~/Resources/mainFrame/js/content.min.js"));

            //树形菜单JS
            bundles.Add(new ScriptBundle("~/bundles/TreeViewItem").Include(
                "~/Resources/mainFrame/js/content.min.js",
                "~/Resources/mainFrame/js/plugins/treeview/v1.2.0/bootstrap-treeview.js",
                "~/Resources/mainFrame/js/demo/treeview-demo.min.js"));

            //bootstrap-table 表格JS
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                "~/Resources/mainFrame/js/plugins/bootstrap-table/bootstrap-table.min.js",
                "~/Resources/mainFrame/js/plugins/bootstrap-table/bootstrap-table-mobile.min.js",
                "~/Resources/mainFrame/js/plugins/bootstrap-table/locale/bootstrap-table-zh-CN.min.js"));

            //z-tree JS
            bundles.Add(new ScriptBundle("~/bundles/z-tree").Include(
                "~/Resources/mainFrame/js/plugins/zTree3/js/jquery.ztree.all.js",
                "~/Resources/mainFrame/js/plugins/zTree3/js/jquery.ztree.exhide.js"));

            BundleTable.EnableOptimizations = true;//开启合并
        }
    }
}
