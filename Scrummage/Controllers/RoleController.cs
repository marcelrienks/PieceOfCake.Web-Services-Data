using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Scrummage.DataAccess;
using Scrummage.DataAccess.Models;
using Scrummage.Interfaces;
using Scrummage.ViewModels;

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
            var roles = _unitOfWork.RoleRepository.All();
            var roleViewModels = roles.Select(role => (RoleViewModel)role).ToList();
            return View(roleViewModels);
        }

        // GET: /Role/Details/5
        public ActionResult Details(int id = 0)
        {
            var roleViewModel = (RoleViewModel)_unitOfWork.RoleRepository.Find(id);
            if (roleViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roleViewModel);
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RoleRepository.Create((Role)roleViewModel);
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        // GET: /Role/Edit/5
        public ActionResult Edit(int id = 0)
        {
            var roleViewModel = (RoleViewModel)_unitOfWork.RoleRepository.Find(id);
            if (roleViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roleViewModel);
        }

        // POST: /Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RoleRepository.Update((Role)roleViewModel);
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        // GET: /Role/Delete/5
        public ActionResult Delete(int id = 0)
        {
            var roleViewModel = (RoleViewModel)_unitOfWork.RoleRepository.Find(id);
            if (roleViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roleViewModel);
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
        //	var model = await _unitOfWork.ModelRepository.MethodAsync(id);
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