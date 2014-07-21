using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Scrummage.Data;
using Scrummage.Data.Interfaces;
using Scrummage.Data.Models;

namespace Scrummage.Services.Controllers
{
    public class UserController : ApiController
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

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return _unitOfWork.UserRepository.All();
        }

        // GET: api/Users/5
        [ResponseType(typeof (User))]
        public IHttpActionResult GetUser(int id)
        {
            var user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.UserRepository.Update(user);
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
        [ResponseType(typeof (User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.UserRepository.Create(user);

            return CreatedAtRoute("DefaultApi", new {id = user.Id}, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof (User))]
        public IHttpActionResult DeleteUser(int id)
        {
            var user = _unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.Delete(user);

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