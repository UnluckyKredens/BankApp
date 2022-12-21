using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Identity
{
    public class PermissionAccess : IPermissionAccess
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public PermissionAccess(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetUserId()
        {
            if(_contextAccessor.HttpContext == null)
                return Guid.Empty;

            var auth = _contextAccessor.HttpContext.Request.Headers["authorization"];
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
