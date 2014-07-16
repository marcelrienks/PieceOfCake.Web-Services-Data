using Scrummage.Data;
using Scrummage.Data.Interfaces;
using Scrummage.Data.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Scrummage.Services.Controllers
{
    public class MembersController : ApiController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public MembersController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: api/Members
        public IQueryable<Member> GetMembers()
        {
            return _unitOfWork.MemberRepository.All();
        }

        // GET: api/Members/5
        [ResponseType(typeof(Member))]
        public IHttpActionResult GetMember(int id)
        {
            var member = _unitOfWork.MemberRepository.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        // PUT: api/Members/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMember(int id, Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != member.MemberId)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.MemberRepository.Update(member);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.MemberRepository.Exists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Members
        [ResponseType(typeof(Member))]
        public IHttpActionResult PostMember(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.MemberRepository.Create(member);

            return CreatedAtRoute("DefaultApi", new { id = member.MemberId }, member);
        }

        // DELETE: api/Members/5
        [ResponseType(typeof(Member))]
        public IHttpActionResult DeleteMember(int id)
        {
            var member = _unitOfWork.MemberRepository.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            _unitOfWork.MemberRepository.Delete(member);

            return Ok(member);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.MemberRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}