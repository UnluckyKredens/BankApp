using BankAppModels.Informations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthInfrastructure.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetToken(string username, string password);
        Task CreateUser(RegisterDTO registerDTO);
    }
}
