using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Common.Exceptions
{
    public class NotFoundEmployeeException : Exception
    {
        public NotFoundEmployeeException()
            : base()
        {
        }

        public NotFoundEmployeeException(string message)
            : base(message)
        {
        }

        public NotFoundEmployeeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundEmployeeException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundEmployeeException(string name, object key, object key2)
            : base($"Entity \"{name}\" ({key}, {key2}) was not found.")
        {
        }

        public NotFoundEmployeeException(string name, object key, object key2, object key3)
            : base($"Entity \"{name}\" ({key}, {key2}. {key3}) was not found.")
        {
        }

        public static NotFoundEmployeeException Create<T>(object key)
        {
            return new NotFoundEmployeeException(typeof(T).Name, key);
        }

    }
}
