namespace Lte.Domain.LinqToCsv
{
    public sealed class MissingCsvColumnAttributeException : LinqToCsvException
    {
        public MissingCsvColumnAttributeException(string typeName, string fieldName, string fileName) :
            base(string.Format(
                "Field \"{0}\" in type \"{1}\" does not have the CsvColumn attribute." +
                FileNameMessage(fileName), fieldName, typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
            Data["FileName"] = fileName;
        }
    }
}