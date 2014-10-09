using Scrummage.Data;
using Scrummage.Data.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DbUser = Scrummage.Data.Models.User;
using VmUser = Scrummage.Services.ViewModels.User;

namespace Scrummage.Services.Controllers
{
    public class UsersController : ApiController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public UsersController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: api/Users
        public IEnumerable<VmUser> GetUsers()
        {
            var dbUser = _unitOfWork.UserRepository.All();
            var user = AutoMapper.Mapper.Map(dbUser, new List<VmUser>());
            return user;
        }

        // GET: api/Users/5
        [ResponseType(typeof(VmUser))]
        public IHttpActionResult GetUser(int id)
        {
            var dbUser = _unitOfWork.UserRepository.Find(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            var user = AutoMapper.Mapper.Map(dbUser, new VmUser());
            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, VmUser user)
        {
            if (id != user.Id)
            {
                return BadRequest("The User Id passed in the URL and Body, do not match.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dbUser = AutoMapper.Mapper.Map(user, new DbUser());
                _unitOfWork.UserRepository.Update(dbUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.UserRepository.Exists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(VmUser))]
        public IHttpActionResult PostUser(VmUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbUser = AutoMapper.Mapper.Map(user, new DbUser());
            _unitOfWork.UserRepository.Create(dbUser);
            AutoMapper.Mapper.Map(dbUser, user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(VmUser))]
        public IHttpActionResult DeleteUser(int id)
        {
            var dbUser = _unitOfWork.UserRepository.Find(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.Delete(dbUser);

            var user = AutoMapper.Mapper.Map(dbUser, new VmUser());
            return Ok(user);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.UserRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}