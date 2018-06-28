namespace Abp.EntityFramework.Dependency
{
    public interface IHuaweiNeighborMongo : IHuaweiCellMongo
    {
        int eNodeBId { get; set; }

        int CellId { get; set; }
    }
}