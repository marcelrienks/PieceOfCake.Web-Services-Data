using AutoMapper;
using PieceOfCake.Data;
using PieceOfCake.Data.Interfaces;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Representors;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

//Todo: Investigate using auto code generation to create api's and api tests based on this controller as template
namespace PieceOfCake.Services.Controllers
{
    /// <summary>
    /// This is the Controller for the Roles model.
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
    public class RolesController : ApiController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public RolesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: api/RoleRepresentors
        public IEnumerable<RoleRepresentor> GetRoles()
        {
            var dbRoles = _unitOfWork.RoleRepository.All();
            var roles = Mapper.Map(dbRoles, new List<RoleRepresentor>());
            return roles;
        }

        // GET: api/RoleRepresentors/5
        [ResponseType(typeof(RoleRepresentor))]
        public IHttpActionResult GetRole(int id)
        {
            var dbRole = _unitOfWork.RoleRepository.Find(id);
            if (dbRole == null)
            {
                return NotFound();
            }

            var role = Mapper.Map(dbRole, new RoleRepresentor());
            return Ok(role);
        }

        // PUT: api/RoleRepresentors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(int id, RoleRepresentor role)
        {
            if (id != role.Id)
            {
                return BadRequest("The Role Id passed in the URL and Body, do not match.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dbRole = Mapper.Map(role, new Role());
                _unitOfWork.RoleRepository.Update(dbRole);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.RoleRepository.Exists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RoleRepresentors
        [ResponseType(typeof(RoleRepresentor))]
        public IHttpActionResult PostRole(RoleRepresentor role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRole = Mapper.Map(role, new Role());
            _unitOfWork.RoleRepository.Create(dbRole);
            Mapper.Map(dbRole, role);

            return CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
        }

        // DELETE: api/RoleRepresentors/5
        [ResponseType(typeof(RoleRepresentor))]
        public IHttpActionResult DeleteRole(int id)
        {
            var dbRole = _unitOfWork.RoleRepository.Find(id);
            if (dbRole == null)
            {
                return NotFound();
            }

            _unitOfWork.RoleRepository.Delete(dbRole);

            var role = Mapper.Map(dbRole, new RoleRepresentor());
            return Ok(role);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.RoleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}