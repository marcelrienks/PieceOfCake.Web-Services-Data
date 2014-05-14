using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Scrummage.DataAccess;
using Scrummage.Interfaces;
using Scrummage.Models;

//todo: update Members controller (link to Avatar controller)
//todo: create Unit tests for Members controller (link to Avatar controller)
namespace Scrummage.Controllers {
	public class MemberController : Controller {

		#region Properties
		private readonly IUnitOfWork UnitOfWork;
		#endregion

		public MemberController() {
			UnitOfWork = new UnitOfWork();
		}

		public MemberController(IUnitOfWork unitOfWork) {
			UnitOfWork = unitOfWork;
		}

		#region Actions
		// GET: /Member/
		public ActionResult Index() {
			return View(UnitOfWork.MemberRepository.All());
		}

		// GET: /Member/Details/5
		public ActionResult Details(int id = 0) {
			var member = UnitOfWork.MemberRepository.Find(id);
			if (member == null) {
				return HttpNotFound();
			}
			return View(member);
		}

		// GET: /Member/Create
		public ActionResult Create() {
			//todo: this line in Membercontroller was auto generated, can it be used to link member to avatar
			//ViewBag.MemberId = new SelectList(db.Avatars, "AvatarId", "AvatarId");
			return View();
		}

		// POST: /Member/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Member member, HttpPostedFileBase file) {
			//If file was uploaded read bytes, else read bytes from default avatar
			byte[] bytes = null;
			if (file != null && file.ContentLength > 0) {
				bytes = new byte[file.ContentLength];
				file.InputStream.Read(bytes, 0, file.ContentLength);

			} else {
				bytes = System.IO.File.ReadAllBytes(ControllerContext.HttpContext.Server.MapPath(@"~\Images\default_avatar.jpg"));
			}

			//Create new Avatar model
			member.Avatar = new Avatar() {
				Image = bytes
			};

			//clear avatar model state error
			if (ModelState["Avatar"].Errors.Count == 1 && ModelState["Avatar"].Errors[0].ErrorMessage == "The Avatar field is required.") {
				ModelState["Avatar"].Errors.Clear();
			}

			//create model
			if (ModelState.IsValid) {
				UnitOfWork.MemberRepository.Create(member);
				UnitOfWork.MemberRepository.Save();
				return RedirectToAction("Index");
			}

			return View(member);
		}

		// GET: /Member/Edit/5
		public ActionResult Edit(int id = 0) {
			var member = UnitOfWork.MemberRepository.Find(id);
			if (member == null) {
				return HttpNotFound();
			}
			return View(member);
		}

		// POST: /Member/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Member member) {
			if (ModelState.IsValid) {
				UnitOfWork.MemberRepository.Update(member);
				UnitOfWork.MemberRepository.Save();
				return RedirectToAction("Index");
			}
			return View(member);
		}

		// GET: /Member/Delete/5
		public ActionResult Delete(int id = 0) {
			var member = UnitOfWork.MemberRepository.Find(id);
			if (member == null) {
				return HttpNotFound();
			}
			return View(member);
		}

		// POST: /Member/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			UnitOfWork.MemberRepository.Delete(id);
			UnitOfWork.MemberRepository.Save();
			return RedirectToAction("Index");
		}
		#endregion

		protected override void Dispose(bool disposing) {
			UnitOfWork.RoleRepository.Dispose();
			base.Dispose(disposing);
		}
	}
}