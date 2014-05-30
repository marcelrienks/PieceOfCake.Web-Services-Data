using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Controllers;
using Scrummage.DataAccess.Models;
using Scrummage.Test.DataAccess;
using Scrummage.Test.Factories;

namespace Scrummage.Test.Controllers {

	[TestClass]
	public class RoleControllerTest {

		#region Properties
		private readonly FakeUnitOfWork _fakeUnitOfWork = new FakeUnitOfWork();
		#endregion

		#region Index tests
		[TestMethod]
		public void TestSuccessfulIndexGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			//'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Index() as ViewResult;
			Assert.IsNotNull(result);

			var roles = ((IEnumerable<Role>)result.Model).ToList();
			Assert.AreEqual(1, roles.Count);
			Assert.AreSame(testRoles[0], roles[0]);
		}
		#endregion

		#region Details tests
		[TestMethod]
		public void TestFailedDetailsGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Details(9);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void TestSuccessfulDetailsGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Details() as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Role)result.Model);
			Assert.AreSame(testRoles[0], role);
		}
		#endregion

		#region Create tests
		[TestMethod]
		public void TestSuccessfulCreateGet() {
			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Create() as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Role)result.Model);
			Assert.IsNull(role);
		}

		[TestMethod]
		public void TestFailedCreatePost() {
			var testRole = RoleFactory.CreateDefaultRole();

			var controller = new RoleController(_fakeUnitOfWork);
			controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
			var result = controller.Create(testRole) as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Role)result.Model);
			Assert.AreSame(testRole, role);
		}

		[TestMethod]
		public void TestSuccessfulCreatePost() {
			var testRole = RoleFactory.CreateDefaultRole();

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Create(testRole);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsCreated);
			Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
		}
		#endregion

		#region Edit tests
		[TestMethod]
		public void TestFailedEditGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Edit(9);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void TestSuccessfulEditGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Edit() as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Role)result.Model);
			Assert.AreSame(testRoles[0], role);
		}

		[TestMethod]
		public void TestFailedEditPost() {
			var testRole = RoleFactory.CreateDefaultRole();

			var controller = new RoleController(_fakeUnitOfWork);
			controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
			var result = controller.Edit(testRole) as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Role)result.Model);
			Assert.AreSame(testRole, role);
		}

		[TestMethod]
		public void TestSuccessfulEditPost() {
			var testRole = RoleFactory.CreateDefaultRole();

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Edit(testRole);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsUpdated);
			Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
		}
		#endregion

		#region Delete tests
		[TestMethod]
		public void TestFailedDeleteGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Delete(9);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void TestSuccessfulDeleteGet() {
			var testRoles = RoleFactory.CreateDefaultRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.Delete() as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Role)result.Model);
			Assert.AreSame(testRoles[0], role);
		}

		[TestMethod]
		public void TestSuccessfulDeletePost() {
			var controller = new RoleController(_fakeUnitOfWork);
			var result = controller.DeleteConfirmed(0);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsDeleted);
			Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
		}
		#endregion
	}
}
