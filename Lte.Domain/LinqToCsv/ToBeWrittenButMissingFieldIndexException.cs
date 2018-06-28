namespace Lte.Domain.LinqToCsv
{
    public sealed class ToBeWrittenButMissingFieldIndexException : LinqToCsvException
    {
        public ToBeWrittenButMissingFieldIndexException(string typeName, string fieldName) :
            base(string.Format(
                "Field or property \"{0}\" of type \"{1}\" will be written to a file, but does not have a FieldIndex. " +
                "This exception only happens for input files without column names in the first record.",
                fieldName, typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
        }
    }
}