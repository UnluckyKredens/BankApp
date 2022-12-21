using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain.Exceptions
{
    public class FailInsertException : Exception
    {
        public FailInsertException(string message) : base(message)
        {

        }

    }
}
