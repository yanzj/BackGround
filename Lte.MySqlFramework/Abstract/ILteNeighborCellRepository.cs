using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;

namespace Lte.MySqlFramework.Abstract
{
    public interface ILteNeighborCellRepository : IRepository<LteNeighborCell>
    {
    }
}