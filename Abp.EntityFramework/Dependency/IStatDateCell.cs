namespace Abp.EntityFramework.Dependency
{
    public interface IStatDateCell : IStatDate
    {
        string CellId { get; set; }
    }
}