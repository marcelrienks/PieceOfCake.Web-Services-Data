using System.Web;
using System.Web.Optimization;

namespace Scrummage {
	public class BundleConfig {
		
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
									"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
									"~/Scripts/jquery.validate.unobtrusive*",
									"~/Scripts/jquery.validate*"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap-theme.css"));
		}
	}
}