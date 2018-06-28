namespace Lte.Domain.LinqToCsv
{
    public sealed class NameNotInTypeException : LinqToCsvException
    {
        public NameNotInTypeException(string typeName, string fieldName, string fileName) :
            base(string.Format(
                "The input file has column name \"{0}\" in the first record, but there is no field or property with that name in type \"{1}\"." +
                FileNameMessage(fileName), fieldName, typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
            Data["FileName"] = fileName;
        }
    }
}