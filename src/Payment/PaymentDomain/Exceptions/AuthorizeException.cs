﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string message) : base(message) {}
    }
}
