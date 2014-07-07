using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Test.Factories.ViewModelFactories;
using System.Linq;

namespace Scrummage.Test.Validation
{
    [TestClass]
    public class RoleViewModelTest
    {
        #region Properties

        private readonly ModelStateTestController _testController; 
        
        #endregion

        public RoleViewModelTest()
        {
            _testController = new ModelStateTestController();
        }

        [TestMethod]
        public void TestRoleInvalidTitle()
        {
            var role = new RoleViewModelFactory().Build();
            var result = _testController.TestTryValidateModel(role);

            var modelState = _testController.ModelState;

            Assert.IsFalse(result);
            Assert.AreEqual(1, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("Title"));
            Assert.IsTrue(modelState["Title"].Errors.Count == 1);
            Assert.AreEqual("The Title field is required.", modelState["Title"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestRoleInvalidTitleDescription()
        {
            var role = new RoleViewModelFactory().WithInvalidTitleDescription().Build();
            var result = _testController.TestTryValidateModel(role);

            var modelState = _testController.ModelState;

            Assert.IsFalse(result);
            Assert.AreEqual(2, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("Title"));
            Assert.IsTrue(modelState["Title"].Errors.Count == 2);
            Assert.IsTrue(modelState["Title"].Errors.Any(x => x.ErrorMessage == "The Title must be between 3 and 30 characters long."));
            Assert.IsTrue(modelState["Title"].Errors.Any(x => x.ErrorMessage == "The Title field is required."));

            Assert.IsTrue(modelState.Keys.Contains("Description"));
            Assert.IsTrue(modelState["Description"].Errors.Count == 1);
            Assert.AreEqual("The Description must be between 3 and 180 characters long.", modelState["Description"].Errors[0].ErrorMessage);
        }
    }
}
