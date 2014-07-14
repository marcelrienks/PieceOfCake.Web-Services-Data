using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Controllers;
using Scrummage.Data.Models;
using Scrummage.Test.DataAccess;
using Scrummage.Test.Factories;
using Scrummage.Test.Factories.ModelFactories;
using Scrummage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Scrummage.Test.Controllers
{
    [TestClass]
    public class MemberControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public MemberControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
            AutoMapperConfig.ConfigureMappings();
        }

        #region Index tests

        [TestMethod]
        public void TestSuccessfulIndexGet()
        {
            var testMembers = new MemberFactory().BuildList();
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var memberViewModels = ((IEnumerable<MemberViewModel>)result.Model).ToList();
            Assert.AreEqual(1, memberViewModels.Count);
            PerformCommonAsserts(testMembers.First(), memberViewModels.First());
        }

        #endregion

        #region Details tests

        [TestMethod]
        public void TestFailedDetailsGet()
        {
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = new MemberFactory().BuildList();

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Details(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDetailsGet()
        {
            var testMembers = new MemberFactory().BuildList();
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Details() as ViewResult;
            Assert.IsNotNull(result);

            var memberViewModel = (MemberViewModel)result.Model;
            PerformCommonAsserts(testMembers.First(), memberViewModel);
        }

        #endregion

        #region Create tests

        [TestMethod]
        public void TestSuccessfulCreateGet()
        {
            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void TestFailedCreatePost()
        {
            var testMember = new MemberFactory().Build();
            var testMemberViewModel = Mapper.Map(testMember, new MemberViewModel());

            var controller = new MemberController(_fakeUnitOfWork);
            controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
            var result = controller.Create(testMemberViewModel, null, new FormCollection
            {
                {"roleSelect", new RoleFactory().Build().Title}
            }) as ViewResult;
            Assert.IsNotNull(result);

            var memberViewModel = (MemberViewModel)result.Model;
            PerformCommonAsserts(testMember, memberViewModel);
        }

        [TestMethod]
        public void TestSuccessfulCreatePostWithAvatar()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var customHttpPostedFileBase = HttpPostedFileBaseFactory.CreateCustomHttpPostedFileBase();

            var testMember = new MemberFactory().Build();
            var testMemberViewModel = Mapper.Map(testMember, new MemberViewModel());

            //Convert role titles to comma delimited string
            var roleTitles = testRoles.Aggregate(String.Empty, (current, role) => current + role.Title + ", ").TrimEnd(", ".ToCharArray());

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Create(testMemberViewModel, customHttpPostedFileBase, new FormCollection
            {
                {"roleSelect", roleTitles}
            }) as ViewResult;

            Assert.IsNull(result);

            Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsSaved);
        }

        #endregion

        #region Edit tests

        [TestMethod]
        public void TestFailedEditGet()
        {
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = new MemberFactory().BuildList();

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Edit(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulEditGet()
        {
            var testMembers = new MemberFactory().BuildList();
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Edit() as ViewResult;
            Assert.IsNotNull(result);

            var memberViewModel = (MemberViewModel)result.Model;
            PerformCommonAsserts(testMembers.First(), memberViewModel);
        }

        //[TestMethod]
        //public void TestFailedEditPost()
        //{
        //    var testMember = new MemberFactory().Build();
        //    var testMemberViewModel = Mapper.Map(testMember, new MemberViewModel());

        //    var controller = new MemberController(_fakeUnitOfWork);
        //    controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
        //    var result = controller.Edit(testMemberViewModel) as ViewResult;
        //    Assert.IsNotNull(result);

        //    var memberViewModel = (MemberViewModel)result.Model;
        //    Assert.AreSame(testMemberViewModel, memberViewModel);
        //}

        //[TestMethod]
        //public void TestSuccessfulEditPost()
        //{
        //    var testMember = new MemberFactory().Build();
        //    var testMemberViewModel = Mapper.Map(testMember, new MemberViewModel());

        //    var controller = new MemberController(_fakeUnitOfWork);
        //    var result = controller.Edit(testMemberViewModel);
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

        //    Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsUpdated);
        //    Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsSaved);
        //}

        #endregion

        #region Delete tests

        [TestMethod]
        public void TestFailedDeleteGet()
        {
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = new MemberFactory().BuildList();

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Delete(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDeleteGet()
        {
            var testMembers = new MemberFactory().BuildList();
            //'FakeUnitOfWork.MemberRepository' must be cast to 'FakeRepository<Member>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).ModelList = testMembers;

            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.Delete() as ViewResult;
            Assert.IsNotNull(result);

            var memberViewModel = (MemberViewModel)result.Model;
            PerformCommonAsserts(testMembers.First(), memberViewModel);
        }

        [TestMethod]
        public void TestSuccessfulDeletePost()
        {
            var controller = new MemberController(_fakeUnitOfWork);
            var result = controller.DeleteConfirmed(0);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<Member>)_fakeUnitOfWork.MemberRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(Member member, MemberViewModel memberViewModel)
        {
            Assert.AreEqual(member.Email, memberViewModel.Email);
            Assert.AreEqual(member.MemberId, memberViewModel.MemberId);
            Assert.AreEqual(member.Name, memberViewModel.Name);
            Assert.AreEqual(member.Password, memberViewModel.Password);
            Assert.AreEqual(member.ShortName, memberViewModel.ShortName);
            Assert.AreEqual(member.Username, memberViewModel.Username);
            Assert.AreEqual(member.Avatar.MemberId, memberViewModel.AvatarViewModel.MemberId);
            Assert.AreEqual(member.Avatar.Image, memberViewModel.AvatarViewModel.Image);
            Assert.AreEqual(member.Roles.Count, memberViewModel.RoleViewModels.Count);
            Assert.AreEqual(member.Roles.First().RoleId, memberViewModel.RoleViewModels.First().RoleId);
            Assert.AreEqual(member.Roles.First().Title, memberViewModel.RoleViewModels.First().Title);
        }

        #endregion
    }
}
