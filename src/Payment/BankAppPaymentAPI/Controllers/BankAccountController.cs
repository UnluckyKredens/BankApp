using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentEngine.Commands;
using PaymentEngine.Queries;
using System.Linq.Expressions;

namespace BankAppPaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly ISender _mediator;

        public BankAccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AddAccountCommand addAccountCommand)
        {
            var result = await _mediator.Send(addAccountCommand);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAccounts(Guid userId)
        {            
            var result = await _mediator.Send(new GetAccountsByUserQuery() { UserId = userId});

            return Ok(result);
        }
    }
}
