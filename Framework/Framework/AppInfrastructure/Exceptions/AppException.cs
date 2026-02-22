using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public class AppException : ApplicationException
    {
        public AppException(string message, params object[] msgParams)
            : base(string.Format(message, msgParams))
        {
        }

        public AppException(string message)
            : base(message)
        {
        }
    }
}
