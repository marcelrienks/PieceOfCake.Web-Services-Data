using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Test.Factories.RepresenterFactories;

namespace Web.Test.Validation
{
    [TestClass]
    public class UserRepresenterTest
    {
        #region Properties

        private readonly TestModelStateController _testController; 
        
        #endregion

        public UserRepresenterTest()
        {
            _testController = new TestModelStateController();
        }

        [TestMethod]
        public void TestRequiredFields()
        {
            var user = new UserRepresenterFactory().WithNullRequiredFields().Build();
            var result = _testController.TestTryValidateModel(user);

            var modelState = _testController.ModelState;

            Assert.IsFalse(result);
            Assert.AreEqual(5, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("Name"));
            Assert.IsTrue(modelState["Name"].Errors.Count == 1);
            Assert.AreEqual("The Name field is required.", modelState["Name"].Errors[0].ErrorMessage);

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
            var user = new UserRepresenterFactory().WithInvalidFields().Build();
            var result = _testController.TestTryValidateModel(user);

            var modelState = _testController.ModelState;

            Assert.IsFalse(result);
            Assert.AreEqual(4, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("Name"));
            Assert.IsTrue(modelState["Name"].Errors.Count == 1);
            Assert.AreEqual("The Name must be between 3 and 30 characters long.",
                modelState["Name"].Errors[0].ErrorMessage);

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
            var user = new UserRepresenterFactory().Build();
            var result = _testController.TestTryValidateModel(user);

            var modelState = _testController.ModelState;

            Assert.IsTrue(result);
            Assert.AreEqual(0, modelState.Keys.Count);
        }
    }
}
