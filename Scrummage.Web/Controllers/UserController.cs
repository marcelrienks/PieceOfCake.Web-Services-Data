using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PieceOfCake.Data;
using PieceOfCake.Data.Interfaces;
using PieceOfCake.Data.Models;

//Todo: verify of username, password on create of User without causing a post back
//Todo: add functionality to User edit for Password and Avatar (including validation)
using PieceOfCake.Web.ViewModels;

namespace PieceOfCake.Web.Controllers
{
    public class UserController : Controller
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public UserController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: /User/
        public ActionResult Index()
        {
            var users = _unitOfWork.UserRepository.All();
            var userViewModels = Mapper.Map(users, new List<UserViewModel>());
            return View(userViewModels);
        }

        // GET: /User/Details/5
        public ActionResult Details(int id = 0)
        {
            var user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = Mapper.Map(user, new UserViewModel());
            return View(userViewModel);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            //add roles to viewbag to populate the roles dropdown select
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel, HttpPostedFileBase file, FormCollection formCollection)
        {
            //Todo: Fix issue with Avatar not re uploading on post after validation failure

            #region Roles

            //Create roleViewModels using comma delimited role string selected
            if (formCollection["roleSelect"] != null)
            {
                var rolesTitles = formCollection["roleSelect"].Split(',');
                var selectRoles = _unitOfWork.RoleRepository.Where(role => rolesTitles.Contains(role.Title));
                userViewModel.RoleViewModels = Mapper.Map(selectRoles.ToList(), new List<RoleViewModel>());
            }

            #endregion

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
                bytes =
                    System.IO.File.ReadAllBytes(
                        ControllerContext.HttpContext.Server.MapPath(@"~\Images\default_avatar.jpg"));
            }

            //Create new Avatar model
            userViewModel.AvatarViewModel = new AvatarViewModel
            {
                Image = bytes
            };

            #endregion

            //create model
            if (ModelState.IsValid)
            {
                var user = Mapper.Map(userViewModel, new User());

                try
                {
                    _unitOfWork.UserRepository.Create(user);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    #region Username Unique

                    //Check if username unique index was violated, and return friendly message
                    if (ex.InnerException.InnerException.Message.Contains(
                        string.Format(
                            "Cannot insert duplicate key row in object 'dbo.Users' with unique index 'IX_Username'. The duplicate key value is ({0}).",
                            userViewModel.Username)))
                    {
                        ModelState["Username"].Errors.Add("Username is already in use.");
                    }

                    #endregion
                }
            }

            //add roles to viewbag to populate the roles dropdown select if model state is invalid
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View(userViewModel);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int id = 0)
        {
            //find the User specified
            var user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            //map User to UserViewModel and return
            var userViewModel = Mapper.Map(user, new UserViewModel());

            //add roles to viewbag to populate the roles dropdown select
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());

            return View(userViewModel);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel, HttpPostedFileBase file, FormCollection formCollection)
        {
            //Todo: Fix issue with Avatar not re uploading on post after validation failure

            #region Roles

            //Create roles using comma delimited role string selected
            if (formCollection["roleSelect"] != null)
            {
                var rolesTitles = formCollection["roleSelect"].Split(',');
                var selectRoles = _unitOfWork.RoleRepository.Where(role => rolesTitles.Contains(role.Title));
                userViewModel.RoleViewModels = Mapper.Map(selectRoles.ToList(), new List<RoleViewModel>());
            }

            #endregion

            #region Avatar

            //If file was uploaded read bytes, else read bytes from current avatar
            byte[] bytes;
            if (file != null && file.ContentLength > 0)
            {
                bytes = new byte[file.ContentLength];
                file.InputStream.Read(bytes, 0, file.ContentLength);
            }
            else
            {
                bytes = _unitOfWork.AvatarRepository.Find(userViewModel.Id).Image;
            }

            //Create new Avatar model
            userViewModel.AvatarViewModel = new AvatarViewModel
            {
                Image = bytes
            };

            #endregion

            //Clear model state and try validate after populating Roles and avatars
            ModelState.Clear();
            TryValidateModel(userViewModel);

            if (ModelState.IsValid)
            {
                var user = Mapper.Map(userViewModel, new User());

                try
                {
                    _unitOfWork.UserRepository.Update(user);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    #region Username Unique

                    //Check if username unique index was violated, and return friendly message
                    if (ex.InnerException.InnerException.Message.Contains(
                        string.Format(
                            "Cannot insert duplicate key row in object 'dbo.Users' with unique index 'IX_Username'. The duplicate key value is ({0}).",
                            userViewModel.Username)))
                    {
                        ModelState["Username"].Errors.Add("Username is already in use.");
                    }

                    #endregion
                }
            }

            //add roles to viewbag to populate the roles dropdown select if model state is invalid
            var roles = _unitOfWork.RoleRepository.All();
            ViewBag.RoleViewModels = Mapper.Map(roles, new List<RoleViewModel>());
            return View(userViewModel);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int id = 0)
        {
            var user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = Mapper.Map(user, new UserViewModel());
            return View(userViewModel);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _unitOfWork.UserRepository.Find(id);
            _unitOfWork.UserRepository.Delete(user);
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