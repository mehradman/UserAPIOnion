using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Data.Entities;
using UserAPI.Repo.Repository;
using UserAPI.Service.Models;

namespace UserAPI.UI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAPIRepository userAPIRepository;
        private readonly IMapper mapper;

        public UsersController(IUserAPIRepository userAPIRepository, IMapper mapper)
        {
            this.userAPIRepository = userAPIRepository ?? throw new ArgumentNullException(nameof(userAPIRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region GETALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithoutInfoDto>>> GetUsers()
        {
            var users = await userAPIRepository.GetUsersAsync();
            return Ok(mapper.Map<IEnumerable<UserWithoutInfoDto>>(users));
        }
        #endregion
        #region GET
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult> GetUser(int id, bool includeInfo)
        {
            var user = await userAPIRepository.GetUserAsync(id, includeInfo);
            if (user == null) return NotFound();

            if (includeInfo) return Ok(mapper.Map<UserDto>(user));
            return Ok(mapper.Map<UserWithoutInfoDto>(user));
        }
        #endregion
        #region POST
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto user)
        {
            var finalPoint = mapper.Map<User>(user);

            await userAPIRepository.AddUser(finalPoint);
            await userAPIRepository.SaveChangesAsync();

            var createdPoint = mapper.Map<UserWithoutInfoDto>(finalPoint);

            return CreatedAtAction("GetUser", new
            {
                Id = createdPoint.UserId,
                includeInfo = false
            }, createdPoint);
        }
        #endregion
        #region PUT
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(int userId, UserForCreationDto user)
        {
            var thisuser = await userAPIRepository.GetUserAsync(userId, false);
            if (user == null) return NotFound();

            mapper.Map(user, thisuser);
            await userAPIRepository.SaveChangesAsync();

            return NoContent();

        }
        #endregion
        #region PATCH
        [HttpPatch("{userId}")]
        public async Task<ActionResult> PartialUpdateUser(int userId, JsonPatchDocument<UserForCreationDto> patchDocument)
        {
            var user = await userAPIRepository.GetUserAsync(userId, false);
            if (user == null) return NotFound();

            var pointToPatch = mapper.Map<UserForCreationDto>(user);

            patchDocument.ApplyTo(pointToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!TryValidateModel(pointToPatch)) return BadRequest(ModelState);

            mapper.Map(pointToPatch, user);
            await userAPIRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var user = await userAPIRepository.GetUserAsync(userId, false);
            if (user == null) return NotFound();

            await userAPIRepository.DeleteUser(user);
            await userAPIRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
