namespace Lte.Domain.LinqToCsv
{
    public sealed class RequiredButMissingFieldIndexException : LinqToCsvException
    {
        public RequiredButMissingFieldIndexException(string typeName, string fieldName) :
            base(string.Format(
                "Field or property \"{0}\" of type \"{1}\" is required, but does not have a FieldIndex. " +
                "This exception only happens for files without column names in the first record.",
                fieldName, typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
        }
    }
}