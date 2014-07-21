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

        // GET: api/Roles
        public IQueryable<Role> GetRoles()
        {
            return _unitOfWork.RoleRepository.All();
        }

        // GET: api/Roles/5
        [ResponseType(typeof (Role))]
        public IHttpActionResult GetRole(int id)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.RoleRepository.Update(role);
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

        // POST: api/Roles
        [ResponseType(typeof (Role))]
        public IHttpActionResult PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.RoleRepository.Create(role);

            return CreatedAtRoute("DefaultApi", new {id = role.Id}, role);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof (Role))]
        public IHttpActionResult DeleteRole(int id)
        {
            var role = _unitOfWork.RoleRepository.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            _unitOfWork.RoleRepository.Delete(role);

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