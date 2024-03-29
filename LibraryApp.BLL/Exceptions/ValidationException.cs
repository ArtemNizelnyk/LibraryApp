﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryApp.BLL.Exceptions
{
    public class ValidationException: Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop)
            : base(message)
        {
            Property = prop;
        }
    }
}
