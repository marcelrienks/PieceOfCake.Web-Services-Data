using AutoMapper;
using Web.Interfaces;
using Web.Models;
using Web.Representer;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;

//Todo: verify of username, password on create of User without causing a post back
//Todo: add functionality to User edit for Password and Avatar (including validation)
using Web.Services;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        #region Properties

        private readonly IService<User> _service;

        #endregion

        public UserController()
        {
            _service = new Service<User>(new Client());
        }

        public UserController(IService<User> service)
        {
            _service = service;
        }

        #region Actions

        //// GET: /User/
        //public ActionResult Index()
        //{
        //    var serviceUsers = _unitOfWork.UserRepository.All();
        //    var users = Mapper.Map(serviceUsers, new List<UserRepresenter>());
        //    return View(users);
        //}

        //// GET: /User/Details/5
        //public ActionResult Details(int id = 0)
        //{
        //    var serviceUser = _unitOfWork.UserRepository.Find(id);
        //    if (serviceUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var user = Mapper.Map(serviceUser, new UserRepresenter());
        //    return View(user);
        //}

        //// GET: /User/Create
        //public ActionResult Create()
        //{
        //    //add roles to viewbag to populate the roles dropdown select
        //    var serviceRoles = _unitOfWork.RoleRepository.All();
        //    ViewBag.Roles = Mapper.Map(serviceRoles, new List<UserRepresenter>());
        //    return View();
        //}

        //// POST: /User/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(UserRepresenter user, HttpPostedFileBase file, FormCollection formCollection)
        //{

        //    #region Roles

        //    //Create roleRepresenters using comma delimited role string selected
        //    if (formCollection["roleSelect"] != null)
        //    {
        //        var rolesTitles = formCollection["roleSelect"].Split(',');
        //        var selectRoles = _unitOfWork.RoleRepository.Where(role => rolesTitles.Contains(role.Title));
        //        user.RoleRepresenters = Mapper.Map(selectRoles.ToList(), new List<RoleRepresenter>());
        //    }

        //    #endregion

        //    //create model
        //    if (ModelState.IsValid)
        //    {
        //        var serviceUser = Mapper.Map(user, new User());

        //        try
        //        {
        //            _unitOfWork.UserRepository.Create(serviceUser);
        //            return RedirectToAction("Index");
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            #region Username Unique

        //            //Check if username unique index was violated, and return friendly message
        //            if (ex.InnerException.InnerException.Message.Contains(
        //                string.Format(
        //                    "Cannot insert duplicate key row in object 'dbo.Users' with unique index 'IX_Username'. The duplicate key value is ({0}).",
        //                    user.Username)))
        //            {
        //                ModelState["Username"].Errors.Add("Username is already in use.");
        //            }

        //            #endregion
        //        }
        //    }

        //    //add roles to viewbag to populate the roles dropdown select if model state is invalid
        //    var serviceRoles = _unitOfWork.RoleRepository.All();
        //    ViewBag.Roles = Mapper.Map(serviceRoles, new List<RoleRepresenter>());
        //    return View(user);
        //}

        //// GET: /User/Edit/5
        //public ActionResult Edit(int id = 0)
        //{
        //    //find the User specified
        //    var serviceUser = _unitOfWork.UserRepository.Find(id);
        //    if (serviceUser == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    //map User to UserViewModel and return
        //    var user = Mapper.Map(serviceUser, new UserRepresenter());

        //    //add roles to viewbag to populate the roles dropdown select
        //    var serviceRoles = _unitOfWork.RoleRepository.All();
        //    ViewBag.Roles = Mapper.Map(serviceRoles, new List<RoleRepresenter>());
        //    return View(user);
        //}

        //// POST: /User/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(UserRepresenter user, HttpPostedFileBase file, FormCollection formCollection)
        //{

        //    #region Roles

        //    //Create roleRepresenters using comma delimited role string selected
        //    if (formCollection["roleSelect"] != null)
        //    {
        //        var rolesTitles = formCollection["roleSelect"].Split(',');
        //        var selectRoles = _unitOfWork.RoleRepository.Where(role => rolesTitles.Contains(role.Title));
        //        user.RoleRepresenters = Mapper.Map(selectRoles.ToList(), new List<RoleRepresenter>());
        //    }

        //    #endregion

        //    //Clear model state and try validate after populating Roles and avatars
        //    ModelState.Clear();
        //    TryValidateModel(user);

        //    if (ModelState.IsValid)
        //    {
        //        var serviceUsers = Mapper.Map(user, new User());

        //        try
        //        {
        //            _unitOfWork.UserRepository.Update(serviceUsers);
        //            return RedirectToAction("Index");
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            #region Username Unique

        //            //Check if username unique index was violated, and return friendly message
        //            if (ex.InnerException.InnerException.Message.Contains(
        //                string.Format(
        //                    "Cannot insert duplicate key row in object 'dbo.Users' with unique index 'IX_Username'. The duplicate key value is ({0}).",
        //                    user.Username)))
        //            {
        //                ModelState["Username"].Errors.Add("Username is already in use.");
        //            }

        //            #endregion
        //        }
        //    }

        //    //add roles to viewbag to populate the roles dropdown select if model state is invalid
        //    var serviceRoles = _unitOfWork.RoleRepository.All();
        //    ViewBag.Roles = Mapper.Map(serviceRoles, new List<RoleRepresenter>());
        //    return View(user);
        //}

        //// GET: /User/Delete/5
        //public ActionResult Delete(int id = 0)
        //{
        //    var serviceUsers = _unitOfWork.UserRepository.Find(id);
        //    if (serviceUsers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var user = Mapper.Map(serviceUsers, new UserRepresenter());
        //    return View(user);
        //}

        //// POST: /User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var serviceUsers = _unitOfWork.UserRepository.Find(id);
        //    _unitOfWork.UserRepository.Delete(serviceUsers);
        //    return RedirectToAction("Index");
        //}

        #endregion

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}