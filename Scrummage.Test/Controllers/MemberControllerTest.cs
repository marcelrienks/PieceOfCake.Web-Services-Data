using System;
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
	public class MemberControllerTest {

		#region Properties
		private readonly FakeUnitOfWork _fakeUnitOfWork = new FakeUnitOfWork();
		#endregion

		#region Index tests
		[TestMethod]
		public void TestSuccessfulIndexGet() {
			var testMembers = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers; //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Index() as ViewResult;
			Assert.IsNotNull(result);

			var members = ((IEnumerable<Member>)result.Model).ToList();
			Assert.AreEqual(1, members.Count);
			Assert.AreEqual(testMembers[0], members[0]);
		}
		#endregion

		#region Details tests
		[TestMethod]
		public void TestFailedDetailsGet() {
			var testMembers = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Details(9);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void TestSuccessfulDetailsGet() {
			var testMembers = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Details() as ViewResult;
			Assert.IsNotNull(result);

			var member = ((Member)result.Model);
			Assert.AreSame(testMembers[0], member);
		}
		#endregion

		#region Create tests
		[TestMethod]
		public void TestSuccessfulCreateGet() {
			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Create() as ViewResult;
			Assert.IsNotNull(result);

			var role = ((Member)result.Model);
			Assert.IsNull(role);
		}

		[TestMethod]
		public void TestFailedCreatePost() {
			var testMember = MemberFactory.CreateDefaultMember();

			var controller = new MemberController(_fakeUnitOfWork);
			controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
			var result = controller.Create(testMember, null, new FormCollection {
				{"roleSelect", RoleFactory.CreateDefaultRole().Title}
			}) as ViewResult;
			Assert.IsNotNull(result);

			var member = ((Member)result.Model);
			Assert.AreSame(testMember, member);
		}

		[TestMethod]
		public void TestSuccessfulCreatePostWithAvatar() {
			var testRoles = RoleFactory.CreateExtendedRoleList();
			((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles; //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not

			var testMember = MemberFactory.CreateDefaultMember();
			var customHttpPostedFileBase = HttpPostedFileBaseFactory.CreateCustomHttpPostedFileBase();

			//Convert role titles to comma delimited string
			var roleTitles = testRoles.Aggregate(String.Empty, (current, role) => current + role.Title + ", ");

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Create(testMember, customHttpPostedFileBase, new FormCollection {
				{"roleSelect", roleTitles}
			}) as ViewResult;

			Assert.IsNull(result);

			Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsCreated);
			Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsSaved);
		}
		#endregion

		#region Edit tests
		[TestMethod]
		public void TestFailedEditGet() {
			var testMember = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMember;

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Edit(9);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void TestSuccessfulEditGet() {
			var testMembers = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Edit() as ViewResult;
			Assert.IsNotNull(result);

			var member = ((Member)result.Model);
			Assert.AreSame(testMembers[0], member);
		}

		[TestMethod]
		public void TestFailedEditPost() {
			var testMember = MemberFactory.CreateDefaultMember();

			var controller = new MemberController(_fakeUnitOfWork);
			controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
			var result = controller.Edit(testMember) as ViewResult;
			Assert.IsNotNull(result);

			var member = ((Member)result.Model);
			Assert.AreSame(testMember, member);
		}

		[TestMethod]
		public void TestSuccessfulEditPost() {
			var testMember = MemberFactory.CreateDefaultMember();

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Edit(testMember);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsUpdated);
			Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsSaved);
		}
		#endregion

		#region Delete tests
		[TestMethod]
		public void TestFailedDeleteGet() {
			var testMember = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMember;

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Delete(9);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void TestSuccessfulDeleteGet() {
			var testMember = MemberFactory.CreateDefaultMemberList();
			((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMember;

			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.Delete() as ViewResult;
			Assert.IsNotNull(result);

			var member = ((Member)result.Model);
			Assert.AreSame(testMember[0], member);
		}

		[TestMethod]
		public void TestSuccessfulDeletePost() {
			var controller = new MemberController(_fakeUnitOfWork);
			var result = controller.DeleteConfirmed(0);
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsDeleted);
			Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsSaved);
		}
		#endregion
	}
}
