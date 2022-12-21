using AuthDomain.Configs;
using AuthDomain.Exceptions;
using AuthDomain.Helper;
using AuthInfrastructure.Exceptions;
using AuthInfrastructure.Interfaces;
using BankAppAuthAPI.Data;
using BankAppModels.Entities;
using BankAppModels.Entities.Auth;
using BankAppModels.Informations.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace AuthInfrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly DataContext _context;
        private readonly TokenConfig _tokenConfig;

        public IdentityService(DataContext context, IOptions<TokenConfig> options)
        {
            _context = context;
            _tokenConfig = options.Value;
        }

        public async Task CreateUser(RegisterDTO registerDTO)
        {
            if (await _context.Users.AnyAsync(x => x.Username == registerDTO.Username))
                throw new UsernameAlreadyTakenException("This username is already taken.");

            EncryptHelper.CreatePasswordHashAndSalt(registerDTO.Password, out byte[] hash, out byte[] salt);
            var attributes = JsonSerializer.Serialize(new
            {
                registerDTO.FirstName,
                registerDTO.LastName,
                registerDTO.Email
            });

            var user = new User
            {
                Username = registerDTO.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                Attributes = attributes 
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetToken(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null || !EncryptHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new WrongDataException("Username or password is wrong!");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
