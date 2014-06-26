using System.Web.Mvc;
using Scrummage.DataAccess;
using Scrummage.Interfaces;
using Scrummage.Models;

namespace Scrummage.Controllers
{
    public class RoleController : Controller
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public RoleController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: /Role/
        public ActionResult Index()
        {
            return View(_unitOfWork.RoleRepository.All());
        }

        // GET: /Role/Details/5
        public ActionResult Details(int id = 0)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RoleRepository.Create(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: /Role/Edit/5
        public ActionResult Edit(int id = 0)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RoleRepository.Update(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: /Role/Delete/5
        public ActionResult Delete(int id = 0)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.RoleRepository.Delete(id);
            return RedirectToAction("Index");
        }

        //Async Example
        //Async Action Method, calling an Async Repository Method (See Repository and IRepository for following code)
        //public async System.Threading.Tasks.Task<ActionResult> MethodAsync(int id = 0) {
        //	var model = await UnitOfWork.ModelRepository.MethodAsync(id);
        //	if (model == null) {
        //		return HttpNotFound();
        //	}
        //	return View(model);
        //}

        #endregion

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.RoleRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}