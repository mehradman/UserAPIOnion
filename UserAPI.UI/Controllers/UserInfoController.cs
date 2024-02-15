using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Repo.Repository;
using UserAPI.Service.Models;
using UserAPI.Data.Entities;

namespace UserAPI.UI.Controllers
{
    [Route("api/users/{userId}/userinfo")]
    [ApiController]
    [Produces("application/json")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserAPIRepository userAPIRepository;
        private readonly IMapper mapper;

        public UserInfoController(IUserAPIRepository userAPIRepository, IMapper mapper)
        {
            this.userAPIRepository = userAPIRepository ?? throw new ArgumentNullException(nameof(userAPIRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region GET
        [HttpGet(Name = "GetUserInfo")]
        public async Task<ActionResult<IEnumerable<UserInfoDto>>> GetUserInfoAsync(int userId)
        {
            if (!await userAPIRepository.UserExistsAsync(userId)) return NotFound();

            var userInfo = await userAPIRepository.GetInfoForUserAsync(userId);
            if (userInfo == null) return NotFound();
            return Ok(mapper.Map<UserInfoDto>(userInfo));
        }
        #endregion
        #region POST
        [HttpPost]
        public async Task<ActionResult<UserInfoDto>> CreateUserInfo(int userId, UserInfoForCreationDto userInfo)
        {
            if (!await userAPIRepository.UserExistsAsync(userId)) return NotFound();

            var thisUserInfo = await userAPIRepository.GetInfoForUserAsync(userId);
            if (thisUserInfo != null) return BadRequest();

            var finalPoint = mapper.Map<UserInfo>(userInfo);

            await userAPIRepository.AddInfoForUserAsync(userId, finalPoint);
            await userAPIRepository.SaveChangesAsync();

            var createdPoint = mapper.Map<UserInfoDto>(finalPoint);

            return CreatedAtAction("GetUserInfo", new
            {
                userId = userId
            }, createdPoint);
        }
        #endregion
        #region PUT
        [HttpPut]
        public async Task<ActionResult> UpdateUserInfo(int userId, UserInfoForCreationDto userInfo)
        {
            if (!await userAPIRepository.UserExistsAsync(userId)) return NotFound();

            var user = await userAPIRepository.GetInfoForUserAsync(userId);
            if (user == null) return NotFound();

            mapper.Map(userInfo, user);
            await userAPIRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #region PATCH
        [HttpPatch]
        public async Task<ActionResult> PartialUpdateUserInfo(int userId, JsonPatchDocument<UserInfoForCreationDto> patchDocument)
        {
            if (!await userAPIRepository.UserExistsAsync(userId)) return NotFound();

            var user = await userAPIRepository.GetInfoForUserAsync(userId);
            if (user == null) return NotFound();

            var pointToPatch = mapper.Map<UserInfoForCreationDto>(user);

            patchDocument.ApplyTo(pointToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!TryValidateModel(pointToPatch)) return BadRequest(ModelState);

            mapper.Map(pointToPatch, user);
            await userAPIRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete]
        public async Task<ActionResult> DeleteUserInfo(int userId)
        {
            if (!await userAPIRepository.UserExistsAsync(userId)) return NotFound();

            var user = await userAPIRepository.GetInfoForUserAsync(userId);
            if (user == null) return NotFound();

            userAPIRepository.DeleteInfo(user);
            await userAPIRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
