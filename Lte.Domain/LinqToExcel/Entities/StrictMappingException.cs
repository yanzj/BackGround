using System;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class StrictMappingException : Exception
    {
        public StrictMappingException(string message)
            : base(message)
        { }

        public StrictMappingException(string formatMessage, params object[] args)
            : base(string.Format(formatMessage, args))
        { }
    }
}
