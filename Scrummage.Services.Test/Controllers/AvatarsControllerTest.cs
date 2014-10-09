using System.Net;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Services.Controllers;
using Scrummage.Services.Test.DataAccess;
using Scrummage.Services.Test.Factories;
using System.Collections.Generic;
using System.Linq;
using Scrummage.Services.ViewModels;

namespace Scrummage.Services.Test.Controllers
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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Data.Models.Avatar>());

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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Data.Models.Avatar>());

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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(new AvatarFactory().BuildList(), new List<Data.Models.Avatar>());

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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Data.Models.Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.GetAvatar(testAvatars.First().Id) as OkNegotiatedContentResult<Avatar>;

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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).Model = AutoMapper.Mapper.Map(testAvatar, new Data.Models.Avatar());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PutAvatar(9, testAvatar);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).Model = AutoMapper.Mapper.Map(testAvatar, new Data.Models.Avatar());

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
        public void PostAvatar_ShouldReturn_CreatedAtRouteNegotiatedContentResult()
        {
            var testAvatar = new AvatarFactory().Build();

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.PostAvatar(testAvatar) as CreatedAtRouteNegotiatedContentResult<Avatar>;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues.ContainsKey("Id"));
            Assert.AreEqual(testAvatar.Id, result.RouteValues["Id"]);
            Assert.IsTrue(((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).IsCreated);
            Assert.IsTrue(((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).IsSaved);
        }

        #endregion

        #region Delete Avatar

        [TestMethod]
        public void DeleteAvatar_ShouldReturn_NotFoundResult()
        {
            //'FakeUnitOfWork.AvatarRepository' must be cast to 'FakeRepository<Avatar>', as 'FakeRepository' exposes some properties that 'IRepository' does not
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = null;

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
            ((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).ModelList = AutoMapper.Mapper.Map(testAvatars, new List<Data.Models.Avatar>());

            var controller = new AvatarsController(_fakeUnitOfWork);
            var result = controller.DeleteAvatar(testAvatars.First().Id);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Avatar>));
            Assert.IsTrue(((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).IsDeleted);
            Assert.IsTrue(((FakeRepository<Data.Models.Avatar>)_fakeUnitOfWork.AvatarRepository).IsSaved);
        }

        #endregion

        #region Common Asserts

        private static void PerformCommonAsserts(Avatar expected, Avatar actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Image, actual.Image);
        }

        #endregion
    }
}
