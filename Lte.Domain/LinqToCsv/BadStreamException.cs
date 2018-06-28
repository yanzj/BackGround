namespace Lte.Domain.LinqToCsv
{
    public class BadStreamException : LinqToCsvException
    {
        public BadStreamException() :
            base("Stream provided to Read is either null, or does not support Seek.")
        {
        }
    }
}