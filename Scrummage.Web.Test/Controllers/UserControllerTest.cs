using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Data.Models;
using Scrummage.Test.DataAccess;
using Scrummage.Test.Factories;
using Scrummage.Test.Factories.ModelFactories;
using Scrummage.Web;
using Scrummage.Web.Controllers;
using Scrummage.Web.ViewModels;

namespace Scrummage.Test.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public UserControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
            AutoMapperConfig.ConfigureMappings();
        }

        #region Index tests

        [TestMethod]
        public void TestSuccessfulIndexGet()
        {
            var testUsers = new UserFactory().BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = testUsers;

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var UserViewModels = ((IEnumerable<UserViewModel>) result.Model).ToList();
            Assert.AreEqual(1, UserViewModels.Count);
            PerformCommonAsserts(testUsers.First(), UserViewModels.First());
        }

        #endregion

        #region Details tests

        [TestMethod]
        public void TestFailedDetailsGet()
        {
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = new UserFactory().BuildList();

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Details(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDetailsGet()
        {
            var testUsers = new UserFactory().BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = testUsers;

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Details() as ViewResult;
            Assert.IsNotNull(result);

            var UserViewModel = (UserViewModel) result.Model;
            PerformCommonAsserts(testUsers.First(), UserViewModel);
        }

        #endregion

        #region Create tests

        [TestMethod]
        public void TestSuccessfulCreateGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void TestFailedCreatePost()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var customHttpPostedFileBase = HttpPostedFileBaseFactory.CreateCustomHttpPostedFileBase();

            var testUser = new UserFactory().Build();
            var testUserViewModel = Mapper.Map(testUser, new UserViewModel());

            var controller = new UserController(_fakeUnitOfWork);
            controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
            var result = controller.Create(testUserViewModel, customHttpPostedFileBase, new FormCollection
            {
                {"roleSelect", new RoleFactory().Build().Title}
            }) as ViewResult;
            Assert.IsNotNull(result);

            var UserViewModel = (UserViewModel) result.Model;
            PerformCommonAsserts(testUser, UserViewModel);
        }

        [TestMethod]
        public void TestSuccessfulCreatePostWithAvatar()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var customHttpPostedFileBase = HttpPostedFileBaseFactory.CreateCustomHttpPostedFileBase();

            var testUser = new UserFactory().Build();
            var testUserViewModel = Mapper.Map(testUser, new UserViewModel());

            //Convert role titles to comma delimited string
            var roleTitles =
                testRoles.Aggregate(String.Empty, (current, role) => current + role.Title + ", ")
                    .TrimEnd(", ".ToCharArray());

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Create(testUserViewModel, customHttpPostedFileBase, new FormCollection
            {
                {"roleSelect", roleTitles}
            }) as ViewResult;

            Assert.IsNull(result);

            Assert.IsTrue(((FakeRepository<User>) _fakeUnitOfWork.UserRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<User>) _fakeUnitOfWork.UserRepository).IsSaved);
        }

        #endregion

        #region Edit tests

        [TestMethod]
        public void TestFailedEditGet()
        {
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = new UserFactory().BuildList();

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Edit(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulEditGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var testUsers = new UserFactory().BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = testUsers;

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Edit() as ViewResult;
            Assert.IsNotNull(result);

            var UserViewModel = (UserViewModel) result.Model;
            PerformCommonAsserts(testUsers.First(), UserViewModel);
        }

        //[TestMethod]
        //public void TestFailedEditPost()
        //{
        //    var testUser = new UserFactory().Build();
        //    var testUserViewModel = Mapper.Map(testUser, new UserViewModel());

        //    var controller = new UserController(_fakeUnitOfWork);
        //    controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
        //    var result = controller.Edit(testUserViewModel) as ViewResult;
        //    Assert.IsNotNull(result);

        //    var UserViewModel = (UserViewModel)result.Model;
        //    Assert.AreSame(testUserViewModel, UserViewModel);
        //}

        //[TestMethod]
        //public void TestSuccessfulEditPost()
        //{
        //    var testUser = new UserFactory().Build();
        //    var testUserViewModel = Mapper.Map(testUser, new UserViewModel());

        //    var controller = new UserController(_fakeUnitOfWork);
        //    var result = controller.Edit(testUserViewModel);
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

        //    Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsUpdated);
        //    Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsSaved);
        //}

        #endregion

        #region Delete tests

        [TestMethod]
        public void TestFailedDeleteGet()
        {
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = new UserFactory().BuildList();

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Delete(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDeleteGet()
        {
            var testUsers = new UserFactory().BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>) _fakeUnitOfWork.UserRepository).ModelList = testUsers;

            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.Delete() as ViewResult;
            Assert.IsNotNull(result);

            var UserViewModel = (UserViewModel) result.Model;
            PerformCommonAsserts(testUsers.First(), UserViewModel);
        }

        [TestMethod]
        public void TestSuccessfulDeletePost()
        {
            var controller = new UserController(_fakeUnitOfWork);
            var result = controller.DeleteConfirmed(0);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<User>) _fakeUnitOfWork.UserRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<User>) _fakeUnitOfWork.UserRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(User User, UserViewModel UserViewModel)
        {
            Assert.AreEqual(User.Email, UserViewModel.Email);
            Assert.AreEqual(User.Id, UserViewModel.Id);
            Assert.AreEqual(User.Name, UserViewModel.Name);
            Assert.AreEqual(User.Password, UserViewModel.Password);
            Assert.AreEqual(User.ShortName, UserViewModel.ShortName);
            Assert.AreEqual(User.Username, UserViewModel.Username);
            Assert.AreEqual(User.Avatar.Id, UserViewModel.AvatarViewModel.Id);
            Assert.AreEqual(User.Avatar.Image, UserViewModel.AvatarViewModel.Image);
            Assert.AreEqual(User.Roles.Count, UserViewModel.RoleViewModels.Count);
            Assert.AreEqual(User.Roles.First().Id, UserViewModel.RoleViewModels.First().Id);
            Assert.AreEqual(User.Roles.First().Title, UserViewModel.RoleViewModels.First().Title);
        }

        #endregion
    }
}