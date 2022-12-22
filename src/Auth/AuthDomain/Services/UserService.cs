using AuthDomain.Interfaces;
using AuthInfrastructure.DTOS;
using AuthInfrastructure.Helper;
using BankAppAuthAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetUserAttributes(Guid userId)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == userId);
            return AttributesHelper.ExtractUserAttributes(user.Attributes);
        }

        public async Task RemoveUserAttributes(RemoveAttributesDTOS attributes, Guid userId)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == userId);
            user.Attributes = AttributesHelper.RemoveAttributes(attributes, user.Attributes);

            await _context.SaveChangesAsync();
        }

        public async Task SetUserAttributes(AddAttributeDTOS attributes, Guid userId)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == userId);
            user.Attributes = AttributesHelper.SetAttributes(attributes, user.Attributes);

            await _context.SaveChangesAsync();
        }
    }
}
