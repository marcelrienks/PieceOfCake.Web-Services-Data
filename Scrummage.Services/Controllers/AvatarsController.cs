using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using PieceOfCake.Data;
using PieceOfCake.Data.Interfaces;
using PieceOfCake.Data.Models;
using PieceOfCake.Services.Representors;

namespace PieceOfCake.Services.Controllers
{
    public class AvatarsController : ApiController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public AvatarsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AvatarsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Actions

        // GET: api/Avatars
        public IEnumerable<AvatarRepresentor> GetAvatars()
        {
            var dbAvatars = _unitOfWork.AvatarRepository.All();
            var avatars = AutoMapper.Mapper.Map(dbAvatars, new List<AvatarRepresentor>());
            return avatars;
        }

        // GET: api/Avatars/5
        [ResponseType(typeof(AvatarRepresentor))]
        public IHttpActionResult GetAvatar(int id)
        {
            var dbAvatar = _unitOfWork.AvatarRepository.Find(id);
            if (dbAvatar == null)
            {
                return NotFound();
            }

            var avatar = AutoMapper.Mapper.Map(dbAvatar, new AvatarRepresentor());
            return Ok(avatar);
        }

        // PUT: api/Avatars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAvatar(int id, AvatarRepresentor avatar)
        {
            if (id != avatar.Id)
            {
                return BadRequest("The Avatar Id passed in the URL and Body, do not match.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dbAvatar = AutoMapper.Mapper.Map(avatar, new Avatar());
                _unitOfWork.AvatarRepository.Update(dbAvatar);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.AvatarRepository.Exists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Avatars
        [ResponseType(typeof(AvatarRepresentor))]
        public IHttpActionResult PostAvatar(AvatarRepresentor avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Validate that an Avatar with supplied Id does not already exist, this is because Avatar does not have an auto increment Identity
            if (_unitOfWork.AvatarRepository.Exists(avatar.Id))
            {
                return BadRequest(string.Format("An Avatar with Id: {0} already exists.", avatar.Id));
            }

            //Validate that a user with an id matching the supplied Avatar Id exists, this is because there is a one to one relationship between User and Avatar
            if (!_unitOfWork.UserRepository.Exists(avatar.Id))
            {
                return BadRequest(string.Format("A User with Id: {0} does not exist. First create a user with Id: {0}, or supply another Id.", avatar.Id));
            }

            var dbAvatar = AutoMapper.Mapper.Map(avatar, new Avatar());
            _unitOfWork.AvatarRepository.Create(dbAvatar);
            AutoMapper.Mapper.Map(dbAvatar, avatar);

            return CreatedAtRoute("DefaultApi", new { id = avatar.Id }, avatar);
        }

        // DELETE: api/Avatars/5
        [ResponseType(typeof(AvatarRepresentor))]
        public IHttpActionResult DeleteAvatar(int id)
        {
            var dbAvatar = _unitOfWork.AvatarRepository.Find(id);
            if (dbAvatar == null)
            {
                return NotFound();
            }

            _unitOfWork.AvatarRepository.Delete(dbAvatar);

            var avatar = AutoMapper.Mapper.Map(dbAvatar, new AvatarRepresentor());
            return Ok(avatar);
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