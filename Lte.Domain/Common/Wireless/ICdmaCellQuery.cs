namespace Lte.Domain.Common.Wireless
{
    public interface ICdmaCellQuery : IBtsIdQuery
    {
        byte SectorId { get; set; }
    }
}