using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace AuthInfrastructure.Identity
{
    public class PermissionAccess : IPermissionAccess
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionAccess(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            if (_httpContextAccessor.HttpContext == null)
                return Guid.Empty;

            var auth = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
            var token = auth.ToString().Split(" ")[1];

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var nameId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid");

            if (nameId == null)
                return Guid.Empty;

            return Guid.Parse(nameId.Value);
        }
    }
}
