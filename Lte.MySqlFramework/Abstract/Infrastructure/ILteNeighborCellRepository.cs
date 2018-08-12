using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface ILteNeighborCellRepository : IRepository<LteNeighborCell>
    {
    }
}