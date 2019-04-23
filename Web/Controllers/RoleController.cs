using AutoMapper;
using Web.Interfaces;
using Web.Models;
using Web.Representer;
using Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers
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
            var serviceRoles = await _service.AllAsync(new Role());
            var roles = AutoMapper.Mapper.Map(serviceRoles, new List<RoleRepresenter>());
            return View(roles);
        }

        // GET: /Role/Details/5
        public async Task<ActionResult> Details(int id = 0)
        {
            var serviceRoles = await _service.GetAsync(id, new Role());
            if (serviceRoles == null)
            {
                return HttpNotFound();
            }
            var role = AutoMapper.Mapper.Map(serviceRoles, new RoleRepresenter());
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
        public async Task<ActionResult> Create(RoleRepresenter role)
        {
            if (ModelState.IsValid)
            {
                var serviceRole = AutoMapper.Mapper.Map(role, new Role());
                _service.CreateAsync(serviceRole);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: /Role/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
            var role = await _service.GetAsync(id, new Role());
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleViewModel = AutoMapper.Mapper.Map(role, new RoleRepresenter());
            return View(roleViewModel);
        }

        // POST: /Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleRepresenter role)
        {
            if (ModelState.IsValid)
            {
                var serviceRole = AutoMapper.Mapper.Map(role, new Role());
                _service.UpdateAsync(serviceRole);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: /Role/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            var serviceRole = await _service.GetAsync(id, new Role());
            if (serviceRole == null)
            {
                return HttpNotFound();
            }
            var role = AutoMapper.Mapper.Map(serviceRole, new RoleRepresenter());
            return View(role);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Todo: Add validation to prevent role from being deleted if it's assigned to a User
            var serviceRole = await _service.GetAsync(id, new Role());
            _service.DeleteAsync(id, new Role());
            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}