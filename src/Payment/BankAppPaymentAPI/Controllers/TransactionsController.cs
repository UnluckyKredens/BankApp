using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentEngine.Commands;
using PaymentEngine.Identity;

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
    }
}
