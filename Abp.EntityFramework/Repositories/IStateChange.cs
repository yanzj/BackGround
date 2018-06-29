namespace Abp.EntityFramework.Repositories
{
    public interface IStateChange
    {
        string CurrentStateDescription { get; set; }

        string NextStateDescription { get; }
    }
}