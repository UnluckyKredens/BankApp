using BankAppModels.Entities.Payment;
using MediatR;
using PaymentEngine.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Queries
{
    public class GetAccountsByUserQuery: IRequest<List<BankAccount>>
    {
        public Guid UserId { get; set; }
    }
}
