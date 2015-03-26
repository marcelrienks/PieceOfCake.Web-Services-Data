using System.Threading.Tasks;
using AutoMapper;
using PieceOfCake.Web.Interfaces;
using PieceOfCake.Web.Models;
using PieceOfCake.Web.Representer;
using PieceOfCake.Web.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PieceOfCake.Web.Controllers
{
    public class RoleController : Controller
    {
        #region Properties

        private readonly IService<Role> _service;

        #endregion

        public RoleController()
        {
            _service = new Service<Role>(new Client());
        }

        public RoleController(IService<Role> service)
        {
            _service = service;
        }

        #region Actions

        // GET: /Role/
        public async Task<ActionResult> Index()
        {
            var serviceRoles = await _service.AllAsync(Role.Resource());
            var roles = Mapper.Map(serviceRoles, new List<RoleRepresenter>());
            return View(roles);
        }

        //// GET: /Role/Details/5
        //public ActionResult Details(int id = 0)
        //{
        //    var serviceRoles = _unitOfWork.RoleRepository.Find(id);
        //    if (serviceRoles == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var role = Mapper.Map(serviceRoles, new RoleRepresenter());
        //    return View(role);
        //}

        //// GET: /Role/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /Role/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(RoleRepresenter role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var serviceRole = Mapper.Map(role, new Role());
        //        _unitOfWork.RoleRepository.Create(serviceRole);
        //        return RedirectToAction("Index");
        //    }

        //    return View(role);
        //}

        //// GET: /Role/Edit/5
        //public ActionResult Edit(int id = 0)
        //{
        //    var role = _unitOfWork.RoleRepository.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var roleViewModel = Mapper.Map(role, new RoleRepresenter());
        //    return View(roleViewModel);
        //}

        //// POST: /Role/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(RoleRepresenter role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var serviceRole = Mapper.Map(role, new Role());
        //        _unitOfWork.RoleRepository.Update(serviceRole);
        //        return RedirectToAction("Index");
        //    }
        //    return View(role);
        //}

        //// GET: /Role/Delete/5
        //public ActionResult Delete(int id = 0)
        //{
        //    var serviceRole = _unitOfWork.RoleRepository.Find(id);
        //    if (serviceRole == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var role = Mapper.Map(serviceRole, new RoleRepresenter());
        //    return View(role);
        //}

        //// POST: /Role/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    //Todo: Add validation to prevent role from being deleted if it's assigned to a User
        //    var serviceRole = _unitOfWork.RoleRepository.Find(id);
        //    _unitOfWork.RoleRepository.Delete(serviceRole);
        //    return RedirectToAction("Index");
        //}

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
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}