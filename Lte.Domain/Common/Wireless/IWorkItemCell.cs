namespace Lte.Domain.Common.Wireless
{
    public interface IWorkItemCell : ILteCellQuery
    {
        string WorkItemNumber { get; set; }
    }
}