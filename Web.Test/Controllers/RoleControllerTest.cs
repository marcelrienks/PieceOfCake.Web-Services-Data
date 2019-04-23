using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Models;
using Services;
using Web.Controllers;
using Web.Test.DataAccess;
using Web.Test.Factories.ModelFactories;
using Web.Representer;

namespace Web.Test.Controllers
{
    //[TestClass]
    public class RoleControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public RoleControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
            AutoMapperConfig.ConfigureMappings();
        }

        #region Index tests

        [TestMethod]
        public void TestSuccessfulIndexGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var roles = ((IEnumerable<RoleRepresenter>)result.Model).ToList();
            Assert.AreEqual(1, roles.Count);
            PerformCommonAsserts(testRoles.First(), roles.First());
        }

        [TestMethod]
        public void TestSuccessfulExtendedIndexGet()
        {
            var testRoles = new RoleFactory().WithExtendedList().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var roles = ((IEnumerable<RoleRepresenter>)result.Model).ToList();
            Assert.AreEqual(2, roles.Count);

            foreach (var testRole in testRoles)
            {
                var roleViewModel = roles.First(rvm => rvm.Id == testRole.Id);
                PerformCommonAsserts(testRole, roleViewModel);
            }
        }

        #endregion

        #region Details tests

        [TestMethod]
        public void TestFailedDetailsGet()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = new RoleFactory().BuildList();

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Details(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDetailsGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Details() as ViewResult;
            Assert.IsNotNull(result);

            var role = (RoleRepresenter)result.Model;
            PerformCommonAsserts(testRoles.First(), role);
        }

        #endregion

        #region Create tests

        [TestMethod]
        public void TestSuccessfulCreateGet()
        {
            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void TestFailedCreatePost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleRepresenter = AutoMapper.Mapper.Map(testRole, new RoleRepresenter());

            var controller = new RoleController(_fakeUnitOfWork);
            controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
            var result = controller.Create(testRoleRepresenter) as ViewResult;
            Assert.IsNotNull(result);

            var role = (RoleRepresenter)result.Model;
            Assert.AreSame(testRoleRepresenter, role);
        }

        [TestMethod]
        public void TestSuccessfulCreatePost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleRepresenter = AutoMapper.Mapper.Map(testRole, new RoleRepresenter());

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Create(testRoleRepresenter);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Edit tests

        [TestMethod]
        public void TestFailedEditGet()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = new RoleFactory().BuildList();

            var controller = new RoleController(_fakeUnitOfWork);
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

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Edit() as ViewResult;
            Assert.IsNotNull(result);

            var role = (RoleRepresenter)result.Model;
            PerformCommonAsserts(testRoles.First(), role);
        }

        [TestMethod]
        public void TestFailedEditPost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleViewModel = AutoMapper.Mapper.Map(testRole, new RoleRepresenter());

            var controller = new RoleController(_fakeUnitOfWork);
            controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
            var result = controller.Edit(testRoleViewModel) as ViewResult;
            Assert.IsNotNull(result);

            var role = result.Model;
            Assert.AreSame(testRoleViewModel, role);
        }

        [TestMethod]
        public void TestSuccessfulEditPost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleRepresenter = AutoMapper.Mapper.Map(testRole, new RoleRepresenter());

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Edit(testRoleRepresenter);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).IsUpdated);
            Assert.IsTrue(((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Delete tests

        [TestMethod]
        public void TestFailedDeleteGet()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = new RoleFactory().BuildList();

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Delete(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDeleteGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Delete() as ViewResult;
            Assert.IsNotNull(result);

            var role = (RoleRepresenter)result.Model;
            PerformCommonAsserts(testRoles.First(), role);
        }

        [TestMethod]
        public void TestSuccessfulDeletePost()
        {
            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.DeleteConfirmed(0);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<Role>) _fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(Role expected, RoleRepresenter actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Description, actual.Description);
        }

        #endregion
    }
}