using System.Web.Mvc;
using Scrummage.DataAccess;
using Scrummage.Interfaces;
using Scrummage.Models;

namespace Scrummage.Controllers {
	public class RoleController : Controller {

		#region Properties
		private readonly IUnitOfWork UnitOfWork;
		#endregion

		public RoleController() {
			UnitOfWork = new UnitOfWork();
		}

		public RoleController(IUnitOfWork unitOfWork) {
			UnitOfWork = unitOfWork;
		}

		#region Actions
		// GET: /Role/
		public ActionResult Index() {
			return View(UnitOfWork.RoleRepository.All());
		}

		// GET: /Role/Details/5
		public ActionResult Details(int id = 0) {
			var role = UnitOfWork.RoleRepository.Find(id);
			if (role == null) {
				return HttpNotFound();
			}
			return View(role);
		}

		//Todo: Testing
		public async System.Threading.Tasks.Task<ActionResult> DetailsAsync(int id = 0) {
			Role role = await UnitOfWork.RoleRepository.FindAsync(id);
			if (role == null) {
				return HttpNotFound();
			}
			return View("Details", role);
		}

		// GET: /Role/Create
		public ActionResult Create() {
			return View();
		}

		// POST: /Role/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Role role) {
			if (ModelState.IsValid) {
				UnitOfWork.RoleRepository.Create(role);
				UnitOfWork.RoleRepository.Save();
				return RedirectToAction("Index");
			}

			return View(role);
		}

		// GET: /Role/Edit/5
		public ActionResult Edit(int id = 0) {
			var role = UnitOfWork.RoleRepository.Find(id);
			if (role == null) {
				return HttpNotFound();
			}
			return View(role);
		}

		// POST: /Role/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Role role) {
			if (ModelState.IsValid) {
				UnitOfWork.RoleRepository.Update(role);
				UnitOfWork.RoleRepository.Save();
				return RedirectToAction("Index");
			}
			return View(role);
		}

		// GET: /Role/Delete/5
		public ActionResult Delete(int id = 0) {
			var role = UnitOfWork.RoleRepository.Find(id);
			if (role == null) {
				return HttpNotFound();
			}
			return View(role);
		}

		// POST: /Role/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			UnitOfWork.RoleRepository.Delete(id);
			UnitOfWork.RoleRepository.Save();
			return RedirectToAction("Index");
		}
		#endregion

		protected override void Dispose(bool disposing) {
			UnitOfWork.RoleRepository.Dispose();
			base.Dispose(disposing);
		}
	}
}