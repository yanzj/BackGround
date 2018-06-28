namespace Lte.Domain.LinqToCsv
{
    public sealed class TooManyNonCsvColumnDataFieldsException : LinqToCsvException
    {
        public TooManyNonCsvColumnDataFieldsException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                "Line {0} has more fields then there are fields or properties in type \"{1}\" with the CsvColumn attribute set." +
                FileNameMessage(fileName), lineNbr, typeName))
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
        }
    }
}