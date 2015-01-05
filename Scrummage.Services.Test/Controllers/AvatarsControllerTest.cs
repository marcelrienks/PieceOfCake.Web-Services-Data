using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Controllers;
using PieceOfCake.Services.Representors;

//Todo: Update Post tests for Avatar
using PieceOfCake.Services.Test.DataAccess;
using PieceOfCake.Services.Test.Factories;

namespace PieceOfCake.Services.Test.Controllers
{
    [TestClass]
    public class AvatarsControllerTest
    {
        #region Properties

        private readonly FakeUnitOfWork _fakeUnitOfWork;

        #endregion

        public AvatarsControllerTest()
        {
            _fakeUnitOfWork = new FakeUnitOfWork();
            AutoMapperConfig.ConfigureMappings();
        }

        #region Get Avatars

        [TestMethod]
        public void GetAvatars_ShouldReturn_SingleElementAvatarList()
        {
            var testAvatars = new AvatarFactory().BuildList();
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var avatars = controller.GetAvatars().ToList();

            Assert.IsNotNull(avatars);
            Assert.AreEqual(1, avatars.Count);
            PerformCommonAsserts(testAvatars.First(), avatars.First());
        }

        [TestMethod]
        public void GetAvatars_ShouldReturn_ExtendedAvatarList()
        {
            var testAvatars = new AvatarFactory().WithExtendedList().BuildList();
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var avatars = controller.GetAvatars().ToList();

            Assert.IsNotNull(avatars);
            Assert.AreEqual(2, avatars.Count);
            foreach (var testAvatar in testAvatars)
            {
                var avatar = avatars.First(avatarType => avatarType.Id == testAvatar.Id);
                PerformCommonAsserts(testAvatar, avatar);
            }
        }

        [TestMethod]
        public void GetAvatar_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(new AvatarFactory().BuildList(), new List<Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.GetAvatar(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAvatar_ShouldReturn_SingleAvatar()
        {
            var testAvatars = new AvatarFactory().BuildList();
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.GetAvatar(testAvatars.First().Id) as OkNegotiatedContentResult<AvatarRepresentor>;

            Assert.IsNotNull(result);
            PerformCommonAsserts(testAvatars.First(), result.Content);
        }

        #endregion

        #region Put Avatar

        [TestMethod]
        public void PutAvatar_ShouldReturn_BadRequestResult()
        {
            var testAvatar = new AvatarFactory().Build();
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).Model = AutoMapper.Mapper.Map(testAvatar, new Avatar());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PutAvatar(9, testAvatar) as BadRequestErrorMessageResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual(result.Message, "The Avatar Id passed in the URL and Body, do not match.");
        }

        [TestMethod]
        public void PutAvatar_ShouldReturn_InvalidModel()
        {
            var key = "key";
            var errorMessage = "model is invalid";
            var testAvatar = new AvatarFactory().Build();

            var controller = new AvatarsController(_fakeUnitOfWork);
            controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
            var result = controller.PutAvatar(testAvatar.Id, testAvatar) as InvalidModelStateResult;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ModelState.ContainsKey(key));
            Assert.AreEqual(1, result.ModelState[key].Errors.Count());
            Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void PutAvatar_ShouldReturn_NoContent()
        {
            var testAvatar = new AvatarFactory().Build();
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).Model = AutoMapper.Mapper.Map(testAvatar, new Avatar());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PutAvatar(testAvatar.Id, testAvatar) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        #endregion

        #region Post Avatar

        [TestMethod]
        public void PostAvatar_ShouldReturn_InvalidModel()
        {
            const string key = "key";
            const string errorMessage = "model is invalid";
            var testAvatar = new AvatarFactory().Build();

            var controller = new AvatarsController(_fakeUnitOfWork);
            controller.ModelState.AddModelError(key, errorMessage); //Causes ModelState.IsValid to return false
            var result = controller.PostAvatar(testAvatar) as InvalidModelStateResult;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ModelState.ContainsKey(key));
            Assert.AreEqual(1, result.ModelState[key].Errors.Count());
            Assert.AreEqual(errorMessage, result.ModelState[key].Errors.First().ErrorMessage);
        }

        [TestMethod]
        public void PostAvatar_ShouldReturn_AvatarExistsError()
        {
            var testAvatars = new AvatarFactory().BuildList();
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PostAvatar(testAvatars[0]) as BadRequestErrorMessageResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual(result.Message, string.Format("An Avatar with Id: {0} already exists.", testAvatars[0].Id));
        }

        [TestMethod]
        public void PostAvatar_ShouldReturn_UserDoesNotExistError()
        {
            var testAvatar = new AvatarFactory().Build();

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PostAvatar(testAvatar) as BadRequestErrorMessageResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual(result.Message, string.Format("A User with Id: {0} does not exist. First create a user with Id: {0}, or supply another Id.", testAvatar.Id));
        }

        [TestMethod]
        public void PostAvatar_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
        {
            var id = 0;
            var testUsers = new UserFactory(id).BuildList();
            //'FakeUnitOfWork.UserRepository' must be cast to 'FakeRepository<User>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<User>)_fakeUnitOfWork.UserRepository).ModelList = AutoMapper.Mapper.Map(testUsers, new List<User>());

            var testAvatar = new AvatarFactory(id).Build();

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PostAvatar(testAvatar) as CreatedAtRouteNegotiatedContentResult<AvatarRepresentor>;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues.ContainsKey("Id"));
            Assert.AreEqual(testAvatar.Id, result.RouteValues["Id"]);
            Assert.IsTrue(((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).IsSaved);
        }

        #endregion

        #region Delete Avatar

        [TestMethod]
        public void DeleteAvatar_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = null;

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.DeleteAvatar(9);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteAvatar_ShouldReturn_OkNegotiatedContentResult()
        {
            var testAvatars = new AvatarFactory().BuildList();

            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.DeleteAvatar(testAvatars.First().Id);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<AvatarRepresentor>));
            Assert.IsTrue(((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<Avatar>)_fakeUnitOfWork.AvatarRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(AvatarRepresentor expected, AvatarRepresentor actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Image, actual.Image);
        }

        #endregion
    }
}
