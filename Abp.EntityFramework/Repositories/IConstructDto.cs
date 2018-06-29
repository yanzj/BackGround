namespace Abp.EntityFramework.Repositories
{
    public interface IConstructDto<out TDto>
    {
        TDto Construct(string userName);
    }
}