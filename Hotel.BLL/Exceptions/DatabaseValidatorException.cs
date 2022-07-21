using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Exceptions
{
    public class DatabaseValidatorException : Exception
    {
        public DatabaseValidatorException()
        { }

        public DatabaseValidatorException(string message)
            : base(message)
        { }

        public DatabaseValidatorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
