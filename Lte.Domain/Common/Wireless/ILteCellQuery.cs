namespace Lte.Domain.Common.Wireless
{
    public interface ILteCellQuery : IENodebId
    {
        byte SectorId { get; set; }
    }
}