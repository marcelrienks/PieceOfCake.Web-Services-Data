using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.DataAccess;
using Scrummage.ViewModels;

namespace Scrummage.Test {

	[TestClass]
	public class TestHarness {
		
		[TestMethod]
		public void Test() {
			var unitOfWork = new UnitOfWork();
			var roles = unitOfWork.RoleRepository.All();

			var roleModelViews = roles.Select(role => (RoleModelView)role).ToList();
			roleModelViews = roleModelViews;
		}
	}
}
