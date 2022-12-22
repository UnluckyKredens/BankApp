using AuthInfrastructure.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Interfaces
{
    public interface IUserService
    {
        public Task<Dictionary<string, string>> GetUserAttributes(Guid userId);
        public Task SetUserAttributes(AddAttributeDTOS addAttributeDTOS, Guid userId);
        public Task RemoveUserAttributes(RemoveAttributesDTOS removeAttributesDTOS, Guid userId);
    }
}
