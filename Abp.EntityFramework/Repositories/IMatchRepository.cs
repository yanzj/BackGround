using Abp.Domain.Entities;

namespace Abp.EntityFramework.Repositories
{
    public interface IMatchRepository<out TEntity, in TExcel>
        where TEntity: Entity
    {
        TEntity Match(TExcel stat);
    }

    public interface IMatchKeyRepository<out TEntity, in TExcel>
        where TEntity : Entity
    {
        TEntity Match(TExcel stat, string key);
    }

    public interface IMatchRepository<TEntity>
        where TEntity : Entity
    {
        TEntity Match(TEntity stat);
    }
}
