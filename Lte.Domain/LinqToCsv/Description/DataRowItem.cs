namespace Lte.Domain.LinqToCsv.Description
{
    public class DataRowItem
    {
        public DataRowItem(string value, int lineNbr)
        {
            this.Value = value;
            this.LineNbr = lineNbr;
        }


        public int LineNbr { get; }


        public string Value { get; }
    }
}