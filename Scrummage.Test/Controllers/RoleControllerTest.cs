using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Controllers;
using Scrummage.DataAccess.Models;
using Scrummage.Test.DataAccess;
using Scrummage.Test.Factories;
using Scrummage.ViewModels;

namespace Scrummage.Test.Controllers
{
    [TestClass]
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
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModels = ((IEnumerable<RoleViewModel>)result.Model).ToList();
            Assert.AreEqual(1, roleViewModels.Count);
            PerformCommonAsserts(testRoles.First(), roleViewModels.First());
        }

        [TestMethod]
        public void TestSuccessfulExtendedIndexGet()
        {
            var testRoles = new RoleFactory().WithExtendedList().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModels = ((IEnumerable<RoleViewModel>)result.Model).ToList();
            Assert.AreEqual(2, roleViewModels.Count);

            foreach (var testRole in testRoles)
            {
                var roleViewModel = roleViewModels.First(rvm => rvm.RoleId == testRole.RoleId);
                PerformCommonAsserts(testRole, roleViewModel);
            }
        }

        #endregion

        #region Details tests

        [TestMethod]
        public void TestFailedDetailsGet()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = new RoleFactory().BuildList();

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Details(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDetailsGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Details() as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModel = (RoleViewModel)result.Model;
            PerformCommonAsserts(testRoles.First(), roleViewModel);
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
            var testRoleViewModel = Mapper.Map(testRole, new RoleViewModel());

            var controller = new RoleController(_fakeUnitOfWork);
            controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
            var result = controller.Create(testRoleViewModel) as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModel = (RoleViewModel)result.Model;
            Assert.AreSame(testRoleViewModel, roleViewModel);
        }

        [TestMethod]
        public void TestSuccessfulCreatePost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleViewModel = Mapper.Map(testRole, new RoleViewModel());

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Create(testRoleViewModel);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Edit tests

        [TestMethod]
        public void TestFailedEditGet()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = new RoleFactory().BuildList();

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Edit(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulEditGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Edit() as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModel = (RoleViewModel)result.Model;
            PerformCommonAsserts(testRoles.First(), roleViewModel);
        }

        [TestMethod]
        public void TestFailedEditPost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleViewModel = Mapper.Map(testRole, new RoleViewModel());

            var controller = new RoleController(_fakeUnitOfWork);
            controller.ModelState.AddModelError("key", "model is invalid"); //Causes ModelState.IsValid to return false
            var result = controller.Edit(testRoleViewModel) as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModel = result.Model;
            Assert.AreSame(testRoleViewModel, roleViewModel);
        }

        [TestMethod]
        public void TestSuccessfulEditPost()
        {
            var testRole = new RoleFactory().Build();
            var testRoleViewModel = Mapper.Map(testRole, new RoleViewModel());

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Edit(testRoleViewModel);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsUpdated);
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Delete tests

        [TestMethod]
        public void TestFailedDeleteGet()
        {
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = new RoleFactory().BuildList();

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Delete(9);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestSuccessfulDeleteGet()
        {
            var testRoles = new RoleFactory().BuildList();
            //'FakeUnitOfWork.RoleRepository' must be cast to 'FakeRepository<Role>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).ModelList = testRoles;

            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.Delete() as ViewResult;
            Assert.IsNotNull(result);

            var roleViewModel = (RoleViewModel)result.Model;
            PerformCommonAsserts(testRoles.First(), roleViewModel);
        }

        [TestMethod]
        public void TestSuccessfulDeletePost()
        {
            var controller = new RoleController(_fakeUnitOfWork);
            var result = controller.DeleteConfirmed(0);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<Role>)_fakeUnitOfWork.RoleRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(Role role, RoleViewModel roleViewModel)
        {
            Assert.AreEqual(role.RoleId, roleViewModel.RoleId);
            Assert.AreEqual(role.Title, roleViewModel.Title);
            Assert.AreEqual(role.Description, roleViewModel.Description);
        }

        #endregion
    }
}
