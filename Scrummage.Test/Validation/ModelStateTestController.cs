using System.Web.Mvc;

namespace Scrummage.Test.Validation
{
    /// <summary>
    ///     This allows for the testing of expected errors from ModelState
    /// </summary>
    class ModelStateTestController : Controller
    {
        /// <summary>
        ///     Instantiate a new Controller Context
        /// </summary>
        public ModelStateTestController()
        {
            ControllerContext = new ControllerContext();
        }

        /// <summary>
        ///     Call TryValidateModel to force Model validation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool TestTryValidateModel(object model)
        {
            return TryValidateModel(model);
        }
    }
}
