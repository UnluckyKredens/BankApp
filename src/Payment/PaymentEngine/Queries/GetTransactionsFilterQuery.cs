using BankAppModels.Entities.Payment;
using MediatR;
using PaymentDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Queries
{
    public class GetTransactionsFilterQuery: IRequest<IEnumerable<Transaction>>
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
        public string Searcher { get; set; }
        public Guid UserId { get; set; }
        public string AccountNumber { get; set; }
    }
}
