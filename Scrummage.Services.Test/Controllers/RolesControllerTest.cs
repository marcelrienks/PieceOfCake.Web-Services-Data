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

        #region Get Roles

        [TestMethod]
        public void GetRoles_ShouldReturn_SingleElementRoleList()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(testRoles, new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var roles = controller.GetRoles().ToList();

            Assert.IsNotNull(roles);
            Assert.AreEqual(1, roles.Count);
            PerformCommonAsserts(testRoles.First(), roles.First());
        }

        [TestMethod]
        public void GetRoles_ShouldReturn_ExtendedRoleList()
        {
            var testRoles = new RoleFactory().WithExtendedList().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(testRoles, new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var roles = controller.GetRoles().ToList();

            Assert.IsNotNull(roles);
            Assert.AreEqual(2, roles.Count);
            foreach (var testRole in testRoles)
            {
                var role = roles.First(roleType => roleType.Id == testRole.Id);
                PerformCommonAsserts(testRole, role);
            }
        }

        [TestMethod]
        public void GetRole_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = AutoMapper.Mapper.Map(new RoleFactory().BuildList(), new List<Role>());

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.GetRole(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetRole_ShouldReturn_SingleRole()
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
        public void PutRole_ShouldReturn_BadRequestResult()
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
        public void PutRole_ShouldReturn_InvalidModel()
        {
            var key = "key";
            var errorMessage = "model is invalid";
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
        public void PutRole_ShouldReturn_NoContent()
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
        public void PostRole_ShouldReturn_InvalidModel()
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
        public void PostRole_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
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
        public void DeleteRole_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = null;

            var controller = new RolesController(_fakeUnitOfWork);
            var result = controller.DeleteRole(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteRole_ShouldReturn_OkNegotiatedContentResult()
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