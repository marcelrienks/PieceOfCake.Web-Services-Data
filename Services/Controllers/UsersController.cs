using AutoMapper;
using Data;
using Data.Interfaces;
using Data.Models;
using Services.Representers;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Services.Controllers
{
    /// <summary>
    /// This is the Controller for the Users model.
    /// 
    /// This is an example of a syncronous Rest API using ASP.Net Web Api
    /// 
    /// This Controller also uses Dependancy injection passing in a Unit of work
    /// which encapsulates a repository pattern. This allows a Fake Unit of Work, Fake Repository, and Fake DB Context
    /// to be passed in and allow unit testing to be done without hitting the DB.
    /// 
    /// This Controller is also Tested using Microsoft Fakes framework allowing methods to be stubbed preventing connection to the DB
    /// 
    /// </summary>
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

        // GET: api/UserRepresentors
        public IEnumerable<UserRepresenter> GetUsers()
        {
            var dbUser = _unitOfWork.UserRepository.All();
            var user = AutoMapper.Mapper.Map(dbUser, new List<UserRepresenter>());
            return user;
        }

        // GET: api/UserRepresentors/5
        [ResponseType(typeof(UserRepresenter))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var dbUser = await _unitOfWork.UserRepository.FindAsync(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            var user = AutoMapper.Mapper.Map(dbUser, new UserRepresenter());
            return Ok(user);
        }

        // PUT: api/UserRepresentors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, UserRepresenter user)
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
                var dbUser = AutoMapper.Mapper.Map(user, new User());
                await _unitOfWork.UserRepository.UpdateAsync(dbUser);
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

        // POST: api/UserRepresentors
        [ResponseType(typeof(UserRepresenter))]
        public async Task<IHttpActionResult> PostUser(UserRepresenter user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbUser = AutoMapper.Mapper.Map(user, new User());
            await _unitOfWork.UserRepository.CreateAsync(dbUser);
            AutoMapper.Mapper.Map(dbUser, user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/UserRepresentors/5
        [ResponseType(typeof(UserRepresenter))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            var dbUser = await _unitOfWork.UserRepository.FindAsync(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            await _unitOfWork.UserRepository.DeleteAsync(dbUser);

            var user = AutoMapper.Mapper.Map(dbUser, new UserRepresenter());
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