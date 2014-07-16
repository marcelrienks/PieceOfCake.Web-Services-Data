using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Scrummage.Data;
using Scrummage.Data.Interfaces;
using Scrummage.Data.Models;
using Scrummage.Web.ViewModels;

namespace Scrummage.Web.Controllers
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
            var roleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View(roleViewModels);
        }

        // GET: /Role/Details/5
        public ActionResult Details(int id = 0)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleViewModel = Mapper.Map(role, new RoleViewModel());
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
                var role = Mapper.Map(roleViewModel, new Role());
                _unitOfWork.RoleRepository.Create(role);
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        // GET: /Role/Edit/5
        public ActionResult Edit(int id = 0)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleViewModel = Mapper.Map(role, new RoleViewModel());
            return View(roleViewModel);
        }

        // POST: /Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = Mapper.Map(roleViewModel, new Role());
                _unitOfWork.RoleRepository.Update(role);
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        // GET: /Role/Delete/5
        public ActionResult Delete(int id = 0)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleViewModel = Mapper.Map(role, new RoleViewModel());
            return View(roleViewModel);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Todo: Add validation to prevent role from being deleted if it's assigned to a member
            var role = _unitOfWork.RoleRepository.Find(id);
            _unitOfWork.RoleRepository.Delete(role);
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