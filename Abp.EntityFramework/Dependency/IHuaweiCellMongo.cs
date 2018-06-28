namespace Abp.EntityFramework.Dependency
{
    public interface IHuaweiCellMongo : IHuaweiMongo
    {
        int LocalCellId { get; set; }
    }
}