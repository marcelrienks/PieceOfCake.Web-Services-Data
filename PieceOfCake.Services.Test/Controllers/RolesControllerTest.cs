using Microsoft.VisualStudio.TestTools.UnitTesting;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Controllers;
using PieceOfCake.Services.Representors;
using PieceOfCake.Services.Test.DataAccess;
using PieceOfCake.Services.Test.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;

namespace PieceOfCake.Services.Test.Controllers
{
    /// <summary>
    /// This is the Unit test class for the Roles Controller.
    /// 
    /// It implements all tests using a Fake Unit of Work, Fake Repository, and Fake DB Context
    /// and Dependancy injection to prevent any connections to the DB
    /// 
    /// And also implements all tests using the Microsoft Fakes Framework to prevent any connections to teh DB
    /// </summary>
    [TestClass]
    public class RolesControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public RolesControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
            AutoMapperConfig.ConfigureMappings();
        }

        #region Fake Repository using Dependancy Injection

        #region Get Roles

        [TestMethod]
        public void GetRoles_Frepo_ShouldReturn_EmptyList()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = null;

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.GetRoles();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetRoles_Frepo_ShouldReturn_SingleElementRoleList()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(testRoles, new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.GetRoles().ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            PerformCommonAsserts(testRoles.First(), result.First());
        }

        [TestMethod]
        public void GetRoles_Frepo_ShouldReturn_ExtendedRoleList()
        {
            var testRoles = new RoleFactory().WithExtendedList().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(testRoles, new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.GetRoles().ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            foreach (var testRole in testRoles)
            {
                var role = result.First(roleType => roleType.Id == testRole.Id);
                PerformCommonAsserts(testRole, role);
            }
        }

        [TestMethod]
        public void GetRole_Frepo_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(new RoleFactory().BuildList(), new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.GetRole(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetRole_Frepo_ShouldReturn_SingleRole()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(testRoles, new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.GetRole(testRoles.First().Id) as OkNegotiatedContentResult<RoleRepresentor>;

            Assert.IsNotNull(result);
            PerformCommonAsserts(testRoles.First(), result.Content);
        }

        #endregion

        #region Put Role

        [TestMethod]
        public void PutRole_Frepo_ShouldReturn_BadRequestResult()
        {
            var testRole = new RoleFactory().Build();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).Model = AutoMapper.Mapper.Map(testRole, new Role());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.PutRole(9, testRole) as BadRequestErrorMessageResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual(result.Message, "The Role Id passed in the URL and Body, do not match.");
        }

        [TestMethod]
        public void PutRole_Frepo_ShouldReturn_InvalidModel()
        {
            const string key = "key";
            const string errorMessage = "model is invalid";
            var testRole = new RoleFactory().Build();

            var controller = new RolesController(_fakeUnitOfWork);
            controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
            var result = controller.PutRole(testRole.Id, testRole) as InvalidModelStateResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ModelState.ContainsKey(key));
            Assert.AreEqual(1, result.ModelState[key].Errors.Count());
            Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void PutRole_Frepo_ShouldReturn_NoContent()
        {
            var testRole = new RoleFactory().Build();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).Model = AutoMapper.Mapper.Map(testRole, new Role());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.PutRole(testRole.Id, testRole) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        #endregion

        #region Post Role

        [TestMethod]
        public void PostRole_Frepo_ShouldReturn_InvalidModel()
        {
            const string key = "key";
            const string errorMessage = "model is invalid";
            var testRole = new RoleFactory().Build();

            var controller = new RolesController(_fakeUnitOfWork);
            controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
            var result = controller.PostRole(testRole) as InvalidModelStateResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ModelState.ContainsKey(key));
            Assert.AreEqual(1, result.ModelState[key].Errors.Count());
            Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void PostRole_Frepo_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
        {
            var testRole = new RoleFactory().Build();

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.PostRole(testRole) as CreatedAtRouteNegotiatedContentResult<RoleRepresentor>;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues.ContainsKey("Id"));
            Assert.AreEqual(testRole.Id, result.RouteValues["Id"]);
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Delete Role

        [TestMethod]
        public void DeleteRole_Frepo_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = null;

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.DeleteRole(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteRole_Frepo_ShouldReturn_OkNegotiatedContentResult()
        {
            var testRoles = new RoleFactory().BuildList();

            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(testRoles, new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.DeleteRole(testRoles.First().Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<RoleRepresentor>));
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #endregion

        #region MS Fakes

        #region Get Roles

        [TestMethod]
        public void GetRoles_fakes_ShouldReturn_EmptyList()
        {
            var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
            {
                RoleRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<Role>
                {
                    All = () => null
                }
            };

            var controller = new RolesController(stubedUnitOfWork);
            var result = controller.GetRoles();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetRoles_fakes_ShouldReturn_SingleElementRoleList()
        {
            var roleRepresentors = new RoleFactory().BuildList();
            var testRoles = AutoMapper.Mapper.Map(roleRepresentors, new List<Role>());

            var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
            {
                RoleRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<Role>
                {
                    All = () => testRoles.AsQueryable()
                }
            };

            var controller = new RolesController(stubedUnitOfWork);
            var result = controller.GetRoles().ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            PerformCommonAsserts(roleRepresentors.First(), result.First());
        }

        [TestMethod]
        public void GetRoles_fakes_ShouldReturn_ExtendedRoleList()
        {
            var roleRepresentors = new RoleFactory().WithExtendedList().BuildList();
            var testRoles = AutoMapper.Mapper.Map(roleRepresentors, new List<Role>());

            var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
            {
                RoleRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<Role>
                {
                    All = () => testRoles.AsQueryable()
                }
            };

            var controller = new RolesController(stubedUnitOfWork);
            var result = controller.GetRoles().ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            foreach (var roleRepresentor in roleRepresentors)
            {
                var role = result.First(roleType => roleType.Id == roleRepresentor.Id);
                PerformCommonAsserts(roleRepresentor, role);
            }
        }

        [TestMethod]
        public void GetRole_fakes_ShouldReturn_NotFoundResult()
        {
            var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
            {
                RoleRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<Role>
                {
                    FindInt32 = id => null
                }
            };

            var controller = new RolesController(stubedUnitOfWork);
            var result = controller.GetRole(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetRole_fakes_ShouldReturn_SingleRole()
        {
            var roleRepresentor = new RoleFactory().Build();
            var testRole = AutoMapper.Mapper.Map(roleRepresentor, new Role());

            var stubedUnitOfWork = new Data.Interfaces.Fakes.StubIUnitOfWork
            {
                RoleRepositoryGet = () => new Data.Interfaces.Fakes.StubIRepository<Role>
                {
                    FindInt32 = id => testRole
                }
            };

            var controller = new RolesController(stubedUnitOfWork);
            var result = controller.GetRole(roleRepresentor.Id) as OkNegotiatedContentResult<RoleRepresentor>; ;

            Assert.IsNotNull(result);
            PerformCommonAsserts(roleRepresentor, result.Content);
        }

        #endregion
        //Todo: Complete MS Fake unit tests for Roles controller
        #region Put Role

        [TestMethod]
        public void PutRole_fakes_ShouldReturn_BadRequestResult()
        {

        }

        [TestMethod]
        public void PutRole_fakes_ShouldReturn_InvalidModel()
        {

        }

        [TestMethod]
        public void PutRole_fakes_ShouldReturn_NoContent()
        {

        }

        #endregion

        #region Post Role

        [TestMethod]
        public void PostRole_fakes_ShouldReturn_InvalidModel()
        {

        }

        [TestMethod]
        public void PostRole_fakes_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
        {

        }

        #endregion

        #region Delete Role

        [TestMethod]
        public void DeleteRole_fakes_ShouldReturn_NotFoundResult()
        {

        }

        [TestMethod]
        public void DeleteRole_fakes_ShouldReturn_OkNegotiatedContentResult()
        {

        }

        #endregion

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(RoleRepresentor expected, RoleRepresentor actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        #endregion
    }
}