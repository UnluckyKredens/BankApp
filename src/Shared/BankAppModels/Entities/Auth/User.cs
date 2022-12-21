using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppModels.Entities.Auth
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Attributes { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
