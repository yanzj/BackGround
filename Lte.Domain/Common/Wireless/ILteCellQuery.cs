namespace Lte.Domain.Common.Wireless
{
    public interface ILteCellQuery : IENodebId
    {
        byte SectorId { get; set; }
    }

    public interface ILteCellReadOnly
    {
        int ENodebId { get; }

        byte SectorId { get; }
    }
}