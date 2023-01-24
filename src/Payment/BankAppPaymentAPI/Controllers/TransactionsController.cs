using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentDomain.Exceptions;
using PaymentEngine.Commands;
using PaymentEngine.Identity;
using PaymentEngine.Queries;

namespace BankAppPaymentAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IPermissionAccess _permissionAccess;

        public TransactionsController(ISender mediator, IPermissionAccess permissionAccess)
        {
            _mediator = mediator;
            _permissionAccess = permissionAccess;
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteTransaction(ExecuteTransactionCommand executeTransactionCommand)
        {
            var result = await _mediator.Send(executeTransactionCommand);

            return Ok(result);
        }

        [HttpPost("Filter")]
        public async Task<IActionResult> GetTransactions(GetTransactionsFilterQuery getTransactionsFilterQuery)
        {
            var userId = _permissionAccess.GetUserId();

            if (userId == Guid.Empty)
                throw new AuthorizeException("User is not authorized");

            getTransactionsFilterQuery.UserId = userId;
            var result = await _mediator.Send(getTransactionsFilterQuery);

            return Ok(result);
        }
    }
}
