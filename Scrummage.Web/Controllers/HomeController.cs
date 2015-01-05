using System.Web.Mvc;

namespace PieceOfCake.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}