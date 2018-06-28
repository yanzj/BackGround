namespace Lte.Domain.LinqToCsv
{
    public sealed class WrongFieldIndexException : LinqToCsvException
    {
        public WrongFieldIndexException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                "Line {0} has less fields then the FieldIndex value is indicating in type \"{1}\" ." +
                FileNameMessage(fileName), lineNbr, typeName))
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
        }
    }
}