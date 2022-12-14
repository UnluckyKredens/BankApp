using AuthDomain.Exceptions;
using AuthInfrastructure.Exceptions;
using AuthInfrastructure.Interfaces;
using BankAppModels.Informations.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;

namespace BankAppAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _service;

        public AuthController(IIdentityService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            registerDTO.Username = registerDTO.Username.ToLower();

            try
            {
                await _service.CreateUser(registerDTO);
            }
            catch(UsernameAlreadyTakenException exception)
            {
                return BadRequest(exception.Message);
            }

            return new JsonResult("Account has been created successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            string token;

            try
            {
                token = await _service.GetToken(loginDTO.Username, loginDTO.Password);
            }
            catch (WrongDataException exception)
            {
                return BadRequest(exception.Message);
            }

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return new JsonResult(token);
        }
    }
}
