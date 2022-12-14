using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Exceptions
{
    public class WrongDataException : Exception
    {
        public WrongDataException(string message) : base(message)
        {

        }
    }
}
