using Scrummage.Data;
using Scrummage.Data.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DbRole = Scrummage.Data.Models.Role;
using VmRole = Scrummage.Services.ViewModels.Role;

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
        public IEnumerable<VmRole> GetRoles()
        {
            var dbRoles = _unitOfWork.RoleRepository.All();
            var roles = AutoMapper.Mapper.Map(dbRoles, new List<VmRole>());
            return roles;
        }

        // GET: api/Roles/5
        [ResponseType(typeof(VmRole))]
        public IHttpActionResult GetRole(int id)
        {
            var dbRole = _unitOfWork.RoleRepository.Find(id);
            if (dbRole == null)
            {
                return NotFound();
            }

            var role = AutoMapper.Mapper.Map(dbRole, new VmRole());
            return Ok(role);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(int id, VmRole role)
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
                var dbRole = AutoMapper.Mapper.Map(role, new DbRole());
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

        // POST: api/Roles
        [ResponseType(typeof(VmRole))]
        public IHttpActionResult PostRole(VmRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRole = AutoMapper.Mapper.Map(role, new DbRole());
            _unitOfWork.RoleRepository.Create(dbRole);

            return CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(VmRole))]
        public IHttpActionResult DeleteRole(int id)
        {
            var dbRole = _unitOfWork.RoleRepository.Find(id);
            if (dbRole == null)
            {
                return NotFound();
            }

            _unitOfWork.RoleRepository.Delete(dbRole);

            var role = AutoMapper.Mapper.Map(dbRole, new VmRole());
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