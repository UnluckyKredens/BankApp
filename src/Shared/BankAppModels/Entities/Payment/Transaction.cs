using BankAppModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppModels.Entities.Payment
{
    public class Transaction : BaseEntity
    {
        public string SenderName { get; set; }
        public string SenderAccountNumber { get; set; }
        public string RecipentAccountNumber { get; set; }
        public string RecipentName { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }

    }
}
