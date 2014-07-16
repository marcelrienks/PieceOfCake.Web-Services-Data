using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Scrummage.Data;
using Scrummage.Data.Interfaces;
using Scrummage.Data.Models;

//Todo: verify of username, password on create of member without causing a post back
//Todo: add functionality to Member edit for Password and Avatar (including validation)
using Scrummage.Web.ViewModels;

namespace Scrummage.Web.Controllers
{
    public class MemberController : Controller
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public MemberController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: /Member/
        public ActionResult Index()
        {
            var members = _unitOfWork.MemberRepository.All();
            var memberViewModels = Mapper.Map(members, new List<MemberViewModel>());
            return View(memberViewModels);
        }

        // GET: /Member/Details/5
        public ActionResult Details(int id = 0)
        {
            var member = _unitOfWork.MemberRepository.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            var memberViewModel = Mapper.Map(member, new MemberViewModel());
            return View(memberViewModel);
        }

        // GET: /Member/Create
        public ActionResult Create()
        {
            //add roles to viewbag to populate the roles dropdown select
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View();
        }

        // POST: /Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberViewModel memberViewModel, HttpPostedFileBase file, FormCollection formCollection)
        {

            #region Clear Errors
            //Clear Avatar and Role errors, these models will be manually created from params passed
            if (ModelState["AvatarViewModel"] != null)
            {
                ModelState["AvatarViewModel"].Errors.Clear();
            }
            if (ModelState["RoleViewModels"] != null)
            {
                ModelState["RoleViewModels"].Errors.Clear();
            } 
            #endregion

            //create model
            if (ModelState.IsValid)
            {
                var member = Mapper.Map(memberViewModel, new Member());

                #region Avatar

                //If file was uploaded read bytes, else read bytes from default avatar
                byte[] bytes;
                if (file != null && file.ContentLength > 0)
                {
                    bytes = new byte[file.ContentLength];
                    file.InputStream.Read(bytes, 0, file.ContentLength);
                }
                else
                {
                    bytes = System.IO.File.ReadAllBytes(ControllerContext.HttpContext.Server.MapPath(@"~\Images\default_avatar.jpg"));
                }

                //Create new Avatar model
                member.Avatar = new Avatar
                {
                    Image = bytes
                };

                #endregion

                #region Roles

                //Create roleViewModels using comma delimited role string selected
                if (formCollection["roleSelect"] != null)
                {
                    var rolesTitles = formCollection["roleSelect"].Split(',');
                    var selectRoles = _unitOfWork.RoleRepository.Where(role => rolesTitles.Contains(role.Title));
                    member.Roles = selectRoles.ToList();
                }

                #endregion

                try
                {
                    _unitOfWork.MemberRepository.Create(member);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    #region Username Unique
                    //Check if username unique index was violated, and return friendly message
                    if (ex.InnerException.InnerException.Message.Contains(
                        string.Format(
                            "Cannot insert duplicate key row in object 'dbo.Members' with unique index 'IX_Username'. The duplicate key value is ({0}).",
                            memberViewModel.Username)))
                    {
                        ModelState["Username"].Errors.Add("Username is already in use.");
                    } 
                    #endregion
                }
            }

            //add roles to viewbag to populate the roles dropdown select if model state is invalid
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View(memberViewModel);
        }

        // GET: /Member/Edit/5
        public ActionResult Edit(int id = 0)
        {
            //find the member specified
            var member = _unitOfWork.MemberRepository.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            //map member to memberViewModel and return
            var memberViewModel = Mapper.Map(member, new MemberViewModel());

            //add roles to viewbag to populate the roles dropdown select
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());

            return View(memberViewModel);
        }

        // POST: /Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberViewModel memberViewModel, HttpPostedFileBase file, FormCollection formCollection)
        {
            #region Clear Errors
            //Clear Avatar and Role errors, these models will be manually created from params passed
            if (ModelState["AvatarViewModel"] != null)
            {
                ModelState["AvatarViewModel"].Errors.Clear();
            }
            if (ModelState["RoleViewModels"] != null)
            {
                ModelState["RoleViewModels"].Errors.Clear();
            }
            #endregion

            if (ModelState.IsValid)
            {
                var member = Mapper.Map(memberViewModel, new Member());

                #region Avatar

                //If file was uploaded read bytes, else read bytes from default avatar
                byte[] bytes;
                if (file != null && file.ContentLength > 0)
                {
                    bytes = new byte[file.ContentLength];
                    file.InputStream.Read(bytes, 0, file.ContentLength);
                }
                else
                {
                    bytes = _unitOfWork.AvatarRepository.Find(member.MemberId).Image;
                }

                //Create new Avatar model
                member.Avatar = new Avatar
                {
                    Image = bytes
                };

                #endregion

                #region Roles

                //Create roleViewModels using comma delimited role string selected
                if (formCollection["roleSelect"] != null)
                {
                    var rolesTitles = formCollection["roleSelect"].Split(',');
                    var selectRoles = _unitOfWork.RoleRepository.Where(role => rolesTitles.Contains(role.Title));
                    member.Roles = selectRoles.ToList();
                }

                #endregion

                try
                {
                    _unitOfWork.MemberRepository.Update(member);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    #region Username Unique
                    //Check if username unique index was violated, and return friendly message
                    if (ex.InnerException.InnerException.Message.Contains(
                        string.Format(
                            "Cannot insert duplicate key row in object 'dbo.Members' with unique index 'IX_Username'. The duplicate key value is ({0}).",
                            memberViewModel.Username)))
                    {
                        ModelState["Username"].Errors.Add("Username is already in use.");
                    }
                    #endregion
                }
            }

            //add roles to viewbag to populate the roles dropdown select if model state is invalid
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View(memberViewModel);
        }

        // GET: /Member/Delete/5
        public ActionResult Delete(int id = 0)
        {
            var member = _unitOfWork.MemberRepository.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            var memberViewModel = Mapper.Map(member, new MemberViewModel());
            return View(memberViewModel);
        }

        // POST: /Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var memeber = _unitOfWork.MemberRepository.Find(id);
            _unitOfWork.MemberRepository.Delete(memeber);
            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.RoleRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}