namespace Lte.Domain.LinqToCsv
{
    public sealed class TooManyDataFieldsException : LinqToCsvException
    {
        public TooManyDataFieldsException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                "Line {0} has more fields then are available in type \"{1}\"." +
                FileNameMessage(fileName), lineNbr, typeName))
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
        }
    }
}