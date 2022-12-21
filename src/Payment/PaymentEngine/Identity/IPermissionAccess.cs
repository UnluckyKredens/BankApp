using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Identity
{
    public interface IPermissionAccess
    {
        public Guid GetUserId();
    }
}
