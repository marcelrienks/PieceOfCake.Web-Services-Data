using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Controllers;
using PieceOfCake.Services.Representors;
using PieceOfCake.Services.Test.DataAccess;
using PieceOfCake.Services.Test.Factories;

namespace PieceOfCake.Services.Test.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public UsersControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
            AutoMapperConfig.ConfigureMappings();
        }

        #region Get Users

        [TestMethod]
        public void GetUsers_ShouldReturn_SingleElementRoleList()
        {
            var testUsers = new UserFactory(0).BuildList();

            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = AutoMapper.Mapper.Map(testUsers, new List<User>());

            var controller = new UsersController(_fakeUnitOfWork);
            var users = controller.GetUsers().ToList();

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
            PerformCommonAsserts(testUsers.First(), users.First());
        }

        [TestMethod]
        public void GetUsers_ShouldReturn_ExtendedRoleList()
        {
            var testUsers = new UserFactory(0).WithExtendedList(1).BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = AutoMapper.Mapper.Map(testUsers, new List<User>());

            var controller = new UsersController(_fakeUnitOfWork);
            var roles = controller.GetUsers().ToList();

            Assert.IsNotNull(roles);
            Assert.AreEqual(2, roles.Count);
            foreach (var testRole in testUsers)
            {
                var role = roles.First(roleType => roleType.Id == testRole.Id);
                PerformCommonAsserts(testRole, role);
            }
        }

        [TestMethod]
        public void GetUser_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = AutoMapper.Mapper.Map(new UserFactory().BuildList(), new List<User>());

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.GetUser(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetUser_ShouldReturn_SingleRole()
        {
            var testUsers = new UserFactory().BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = AutoMapper.Mapper.Map(testUsers, new List<User>());

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.GetUser(testUsers.First().Id) as OkNegotiatedContentResult<UserRepresentor>;

            Assert.IsNotNull(result);
            PerformCommonAsserts(testUsers.First(), result.Content);
        }

        #endregion

        #region Put User

        [TestMethod]
        public void PutUser_ShouldReturn_BadRequestResult()
        {
            var testUser = new UserFactory().Build();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).Model = AutoMapper.Mapper.Map(testUser, new User());

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.PutUser(9, testUser) as BadRequestErrorMessageResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual(result.Message, "The User Id passed in the URL and Body, do not match.");
        }

        [TestMethod]
        public void PutUser_ShouldReturn_InvalidModel()
        {
            var key = "key";
            var errorMessage = "model is invalid";
            var testUser = new UserFactory().Build();

            var controller = new UsersController(_fakeUnitOfWork);
            controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
            var result = controller.PutUser(testUser.Id, testUser) as InvalidModelStateResult;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ModelState.ContainsKey(key));
            Assert.AreEqual(1, result.ModelState[key].Errors.Count());
            Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void PutUser_ShouldReturn_NoContent()
        {
            var testUser = new UserFactory().Build();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).Model = AutoMapper.Mapper.Map(testUser, new User());

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.PutUser(testUser.Id, testUser) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        #endregion

        #region Post User

        [TestMethod]
        public void PostUser_ShouldReturn_InvalidModel()
        {
            var key = "key";
            var errorMessage = "model is invalid";
            var testUser = new UserFactory().Build();

            var controller = new UsersController(_fakeUnitOfWork);
            controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
            var result = controller.PostUser(testUser) as InvalidModelStateResult;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ModelState.ContainsKey(key));
            Assert.AreEqual(1, result.ModelState[key].Errors.Count());
            Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void PostUser_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
        {
            var testUser = new UserFactory().Build();

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.PostUser(testUser) as CreatedAtRouteNegotiatedContentResult<UserRepresentor>;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues.ContainsKey("Id"));
            Assert.AreEqual(testUser.Id, result.RouteValues["Id"]);
            Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsSaved);
        }

        #endregion

        #region Delete User

        [TestMethod]
        public void DeleteUser_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = null;

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.DeleteUser(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteUser_ShouldReturn_OkNegotiatedContentResult()
        {
            var testUsers = new UserFactory().BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = AutoMapper.Mapper.Map(testUsers, new List<User>());

            var controller = new UsersController(_fakeUnitOfWork);
            var result = controller.DeleteUser(testUsers.First().Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<UserRepresentor>));
            Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(UserRepresentor expected, UserRepresentor actual)
        {
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.ShortName, actual.ShortName);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Avatar.Id, actual.Avatar.Id);
            Assert.AreEqual(expected.Avatar.Image, actual.Avatar.Image);
            Assert.AreEqual(expected.Roles.Count, actual.Roles.Count);
            Assert.AreEqual(expected.Roles.First().Id, actual.Roles.First().Id);
            Assert.AreEqual(expected.Roles.First().Title, actual.Roles.First().Title);
        }

        #endregion
    }
}