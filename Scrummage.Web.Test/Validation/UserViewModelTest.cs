using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummage.Test.Factories.ViewModelFactories;

namespace Scrummage.Test.Validation
{
    [TestClass]
    public class UserViewModelTest
    {
        #region Properties

        private readonly TestModelStateController _testController;

        #endregion

        public UserViewModelTest()
        {
            _testController = new TestModelStateController();
        }

        [TestMethod]
        public void TestRequiredFields()
        {
            var User = new UserViewModelFactory().WithNullRequiredFields().Build();
            var result = _testController.TestTryValidateModel(User);

            var modelState = _testController.ModelState;

            Assert.IsFalse(result);
            Assert.AreEqual(6, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("Name"));
            Assert.IsTrue(modelState["Name"].Errors.Count == 1);
            Assert.AreEqual("The Name field is required.", modelState["Name"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("ShortName"));
            Assert.IsTrue(modelState["ShortName"].Errors.Count == 1);
            Assert.AreEqual("The Short Name field is required.", modelState["ShortName"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("Username"));
            Assert.IsTrue(modelState["Username"].Errors.Count == 1);
            Assert.AreEqual("The User Name field is required.", modelState["Username"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("Email"));
            Assert.IsTrue(modelState["Email"].Errors.Count == 1);
            Assert.AreEqual("The Email field is required.", modelState["Email"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("Password"));
            Assert.IsTrue(modelState["Password"].Errors.Count == 1);
            Assert.AreEqual("The Password field is required.", modelState["Password"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("ConfirmPassword"));
            Assert.IsTrue(modelState["ConfirmPassword"].Errors.Count == 1);
            Assert.AreEqual("The Confirm Password field is required.",
                modelState["ConfirmPassword"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestInvalidFields()
        {
            var User = new UserViewModelFactory().WithInvalidFields().Build();
            var result = _testController.TestTryValidateModel(User);

            var modelState = _testController.ModelState;

            Assert.IsFalse(result);
            Assert.AreEqual(5, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("Name"));
            Assert.IsTrue(modelState["Name"].Errors.Count == 1);
            Assert.AreEqual("The Name must be between 3 and 30 characters long.",
                modelState["Name"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("ShortName"));
            Assert.IsTrue(modelState["ShortName"].Errors.Count == 1);
            Assert.AreEqual("The Short Name must be between 2 and 3 characters long.",
                modelState["ShortName"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("Username"));
            Assert.IsTrue(modelState["Username"].Errors.Count == 1);
            Assert.AreEqual("The User Name must be between 3 and 30 characters long.",
                modelState["Username"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("Email"));
            Assert.IsTrue(modelState["Email"].Errors.Count == 1);
            Assert.AreEqual("The email address is invalid.", modelState["Email"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("ConfirmPassword"));
            Assert.IsTrue(modelState["ConfirmPassword"].Errors.Count == 1);
            Assert.AreEqual("The passwords do not match.", modelState["ConfirmPassword"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestValidUser()
        {
            var User = new UserViewModelFactory().Build();
            var result = _testController.TestTryValidateModel(User);

            var modelState = _testController.ModelState;

            Assert.IsTrue(result);
            Assert.AreEqual(0, modelState.Keys.Count);
        }
    }
}