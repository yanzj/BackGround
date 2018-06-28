namespace Lte.Domain.LinqToCsv
{
    public sealed class MissingFieldIndexException : LinqToCsvException
    {
        public MissingFieldIndexException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                "Line {0} has more fields then there are fields or properties in type \"{1}\" with a FieldIndex." +
                FileNameMessage(fileName), lineNbr, typeName))
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
        }
    }
}