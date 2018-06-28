namespace Lte.Domain.LinqToCsv
{
    public class CsvColumnAttributeRequiredException : LinqToCsvException
    {
        public CsvColumnAttributeRequiredException() :
            base(
                "CsvFileDescription.EnforceCsvColumnAttribute is false, but needs to be true because " +
                "CsvFileDescription.FirstLineHasColumnNames is false. See the description for CsvColumnAttributeRequiredException.")
        {
        }
    }
}