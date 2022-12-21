using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentDomain.Exceptions;
using PaymentEngine.Commands;
using PaymentEngine.Identity;
using PaymentEngine.Queries;
using System.Linq.Expressions;

namespace BankAppPaymentAPI.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IPermissionAccess _permissionAccess;

        public BankAccountController(ISender mediator, IPermissionAccess permissionAccess)
        {
            _mediator = mediator;
            _permissionAccess = permissionAccess;
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AddAccountCommand addAccountCommand)
        {
            var result = await _mediator.Send(addAccountCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var userId = _permissionAccess.GetUserId();

            if (userId == Guid.Empty)
                throw new AuthorizeException("User is not authorized");

            var result = await _mediator.Send(new GetAccountsByUserQuery() { UserId = userId});

            return Ok(result);
        }
    }
}
