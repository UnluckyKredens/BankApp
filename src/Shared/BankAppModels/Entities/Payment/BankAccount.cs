using BankAppModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppModels.Entities.Payment
{
    public class BankAccount : BaseEntity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Balance { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
