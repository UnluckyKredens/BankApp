using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthInfrastructure.Identity
{
    public interface IPermissionAccess
    {
        public Guid GetUserId();
    }
}
