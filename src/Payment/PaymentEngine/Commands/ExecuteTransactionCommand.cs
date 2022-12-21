using BankAppModels.Entities.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Commands
{
    public class ExecuteTransactionCommand : IRequest<Transaction>
    {
        public string SenderAccountNumber { get; set; }
        public string RecipentName { get; set; }
        public string RecipentAccountNumber { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }
    }
}
