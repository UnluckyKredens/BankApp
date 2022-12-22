using AuthDomain.Interfaces;
using AuthInfrastructure.DTOS;
using AuthInfrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAppAuthAPI.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPermissionAccess _permissionAccess;
        private readonly IUserService _userService;

        public UserController(IPermissionAccess permissionAccess, IUserService userService)
        {
            _permissionAccess = permissionAccess;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAttributes()
        {
            var userId = _permissionAccess.GetUserId();

            return Ok(await _userService.GetUserAttributes(userId));
        }

        [HttpPost]
        public async Task<IActionResult> SetAttributes(AddAttributeDTOS addAttributeDTOS)
        {
            var userId = _permissionAccess.GetUserId();

            try
            {
                await _userService.SetUserAttributes(addAttributeDTOS, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAttributes(RemoveAttributesDTOS removeAttributesDTOS)
        {
            var userId = _permissionAccess.GetUserId();

            try
            {
                await _userService.RemoveUserAttributes(removeAttributesDTOS, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
