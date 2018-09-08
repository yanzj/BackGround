namespace Lte.Domain.Common.Wireless
{
    public interface ILocalCellQuery : IENodebId
    {
        byte LocalCellId { get; set; }
    }
}