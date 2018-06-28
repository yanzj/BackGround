using System;

namespace Lte.Domain.LinqToCsv
{
    [Serializable]
    public class LinqToCsvException : Exception
    {
        protected LinqToCsvException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected LinqToCsvException(string message)
            : base(message)
        {
        }

        protected static string FileNameMessage(string fileName)
        {
            return ((fileName == null) ? "" : " Reading file \"" + fileName + "\".");
        }
    }
}
