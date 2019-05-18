using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Models;
using Services.Controllers;
using Services.Representers;
using Services.Test.DataAccess;
using Services.Test.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Services.Test.Controllers
{
    /// <summary>
    /// This is the Unit test class for the Users Controller.
    /// 
    /// It implements all tests using Microsoft Fakes
    /// Tests have been duplicated to showcase the use of Stubs with dependancy injection and
    /// Shims to prevent any connections to the DB
    /// </summary>
    //[TestClass]
    public class UsersControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public UsersControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
        }

        // Note: this block no longer works, as i currently have VS2015 Prof, which does not support Fakes
        // But it still exists as a demonstration
        //#region Get Users using Microsoft Fakes

        //[TestMethod]
        //public void GetUsers_ShouldReturn_EmptyList()
        //{
        //    var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
        //    {
        //        UserRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<User>
        //        {
        //            All = () => null
        //        }
        //    };

        //    var controller = new UsersController(stubedUnitOfWork);
        //    var result = controller.GetUsers();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(0, result.Count());
        //}

        //[TestMethod]
        //public void GetUsers_ShouldReturn_SingleElementRoleList()
        //{
        //    var userRepresentors = new UserFactory().BuildList();
        //    var testUsers = AutoMapper.Mapper.Map(userRepresentors, new List<User>());

        //    var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
        //    {
        //        UserRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<User>
        //        {
        //            All = () => testUsers.AsQueryable()
        //        }
        //    };

        //    var controller = new UsersController(stubedUnitOfWork);
        //    var result = controller.GetUsers().ToList();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(1, result.Count);
        //    PerformCommonAsserts(userRepresentors.First(), result.First());
        //}

        //[TestMethod]
        //public void GetUsers_ShouldReturn_ExtendedRoleList()
        //{
        //    var userRepresentors = new UserFactory(0).WithExtendedList(1).BuildList();
        //    var testUsers = AutoMapper.Mapper.Map(userRepresentors, new List<User>());

        //    var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
        //    {
        //        UserRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<User>
        //        {
        //            All = () => testUsers.AsQueryable()
        //        }
        //    };

        //    var controller = new UsersController(stubedUnitOfWork);
        //    var result = controller.GetUsers().ToList();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count);
        //    foreach (var userRepresentor in userRepresentors)
        //    {
        //        var role = result.First(roleType => roleType.Id == userRepresentor.Id);
        //        PerformCommonAsserts(userRepresentor, role);
        //    }
        //}

        //[TestMethod]
        //public async Task GetUser_ShouldReturn_NotFoundResult()
        //{
        //    var taskCompletionSource = new TaskCompletionSource<User>();
        //    taskCompletionSource.SetResult(null);

        //    var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
        //    {
        //        UserRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<User>
        //        {
        //            FindAsyncInt32 = id => taskCompletionSource.Task
        //        }
        //    };

        //    var controller = new UsersController(stubedUnitOfWork);
        //    var result = await controller.GetUser(9);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public async Task GetUser_ShouldReturn_SingleRole()
        //{
        //    var userRepresentor = new UserFactory().Build();
        //    var user = AutoMapper.Mapper.Map(userRepresentor, new User());
        //    var taskCompletionSource = new TaskCompletionSource<User>();
        //    taskCompletionSource.SetResult(user);

        //    var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
        //    {
        //        UserRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<User>
        //        {
        //            FindAsyncInt32 = id => taskCompletionSource.Task
        //        }
        //    };

        //    var controller = new UsersController(stubedUnitOfWork);
        //    var result = await controller.GetUser(9) as OkNegotiatedContentResult<UserRepresentor>;

        //    Assert.IsNotNull(result);
        //    PerformCommonAsserts(userRepresentor, result.Content);
        //}

        //#endregion

        //#region Get Users

        //[TestMethod]
        //public void GetUsers_ShouldReturn_EmptyList()
        //{
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = null;

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = controller.GetUsers();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(0, result.Count());
        //}

        //[TestMethod]
        //public void GetUsers_ShouldReturn_SingleElementUserList()
        //{
        //    var userRepresentors = new UserFactory().BuildList();
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = global::AutoMapper.Mapper.Map(userRepresentors, new List<User>());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = controller.GetUsers().ToList();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(1, result.Count);
        //    PerformCommonAsserts(userRepresentors.First(), result.First());
        //}

        //[TestMethod]
        //public void GetUsers_ShouldReturn_ExtendedUserList()
        //{
        //    var userRepresentors = new UserFactory(0).WithExtendedList(1).BuildList();
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = global::AutoMapper.Mapper.Map(userRepresentors, new List<User>());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = controller.GetUsers().ToList();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, result.Count);
        //    foreach (var userRepresentor in userRepresentors)
        //    {
        //        var role = result.First(roleType => roleType.Id == userRepresentor.Id);
        //        PerformCommonAsserts(userRepresentor, role);
        //    }
        //}

        //[TestMethod]
        //public async Task GetUser_ShouldReturn_NotFoundResult()
        //{
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = global::AutoMapper.Mapper.Map(new UserFactory().BuildList(), new List<User>());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.GetUser(9);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public async Task GetUser_ShouldReturn_SingleUser()
        //{
        //    var userRepresentors = new UserFactory().BuildList();
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = global::AutoMapper.Mapper.Map(userRepresentors, new List<User>());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.GetUser(0) as OkNegotiatedContentResult<UserRepresenter>;

        //    Assert.IsNotNull(result);
        //    PerformCommonAsserts(userRepresentors.First(), result.Content);
        //}

        //#endregion

        //#region Put User

        //[TestMethod]
        //public async Task PutUser_ShouldReturn_BadRequestResult()
        //{
        //    var testUser = new UserFactory().Build();
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).Model = global::AutoMapper.Mapper.Map(testUser, new User());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.PutUser(9, testUser) as BadRequestErrorMessageResult;

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        //    Assert.AreEqual(result.Message, "The User Id passed in the URL and Body, do not match.");
        //}

        //[TestMethod]
        //public async Task PutUser_ShouldReturn_InvalidModel()
        //{
        //    var key = "key";
        //    var errorMessage = "model is invalid";
        //    var testUser = new UserFactory().Build();

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
        //    var result = await controller.PutUser(testUser.Id, testUser) as InvalidModelStateResult;

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.ModelState.ContainsKey(key));
        //    Assert.AreEqual(1, result.ModelState[key].Errors.Count());
        //    Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        //}

        //[TestMethod]
        //public async Task PutUser_ShouldReturn_NoContent()
        //{
        //    var testUser = new UserFactory().Build();
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).Model = global::AutoMapper.Mapper.Map(testUser, new User());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.PutUser(testUser.Id, testUser) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        //}

        //#endregion

        //#region Post User

        //[TestMethod]
        //public async Task PostUser_ShouldReturn_InvalidModel()
        //{
        //    const string key = "key";
        //    const string errorMessage = "model is invalid";
        //    var testUser = new UserFactory().Build();

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
        //    var result = await controller.PostUser(testUser) as InvalidModelStateResult;

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.ModelState.ContainsKey(key));
        //    Assert.AreEqual(1, result.ModelState[key].Errors.Count());
        //    Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        //}

        //[TestMethod]
        //public async Task PostUser_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
        //{
        //    var testUser = new UserFactory().Build();

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.PostUser(testUser) as CreatedAtRouteNegotiatedContentResult<UserRepresenter>;

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.RouteValues.ContainsKey("Id"));
        //    Assert.AreEqual(testUser.Id, result.RouteValues["Id"]);
        //    Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsCreated);
        //    Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsSaved);
        //}

        //#endregion

        //#region Delete User

        //[TestMethod]
        //public async Task DeleteUser_ShouldReturn_NotFoundResult()
        //{
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = null;

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.DeleteUser(9);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public async Task DeleteUser_ShouldReturn_OkNegotiatedContentResult()
        //{
        //    var testUsers = new UserFactory().BuildList();
        //    //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
        //    ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = global::AutoMapper.Mapper.Map(testUsers, new List<User>());

        //    var controller = new UsersController(_fakeUnitOfWork);
        //    var result = await controller.DeleteUser(testUsers.First().Id);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<UserRepresenter>));
        //    Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsDeleted);
        //    Assert.IsTrue(((FakeRepository<User>)_fakeUnitOfWork.UserRepository).IsSaved);
        //}

        //#endregion

        //#region Common Asserts

        //private static void PerformCommonAsserts(UserRepresenter expected, UserRepresenter actual)
        //{
        //    Assert.AreEqual(expected.Email, actual.Email);
        //    Assert.AreEqual(expected.Id, actual.Id);
        //    Assert.AreEqual(expected.Name, actual.Name);
        //    Assert.AreEqual(expected.Password, actual.Password);
        //    Assert.AreEqual(expected.Username, actual.Username);
        //    Assert.AreEqual(expected.RoleRepresentors.Count, actual.RoleRepresentors.Count);
        //    Assert.AreEqual(expected.RoleRepresentors.First().Id, actual.RoleRepresentors.First().Id);
        //    Assert.AreEqual(expected.RoleRepresentors.First().Title, actual.RoleRepresentors.First().Title);
        //}

        //#endregion

        //[TestMethod]
        //public void GetRole_fakes_ShouldReturn_SingleRole()
        //{
        //    var roleRepresentor = new RoleFactory().Build();
        //    var testRole = AutoMapper.Mapper.Map(roleRepresentor, new Role());

        //    var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
        //    {
        //        RoleRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<Role>
        //        {
        //            FindInt32 = id => testRole
        //        }
        //    };

        //    var controller = new RolesController(stubedUnitOfWork);
        //    var result = controller.GetRole(roleRepresentor.Id) as OkNegotiatedContentResult<RoleRepresentor>; ;

        //    Assert.IsNotNull(result);
        //    PerformCommonAsserts(roleRepresentor, result.Content);
        //}

        //#endregion
    }
}