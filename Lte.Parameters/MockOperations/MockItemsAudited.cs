using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Moq;
using System.Linq;

namespace Lte.Parameters.MockOperations
{
    public static class MockItemsAudited
    {
        public static void SynchronizeAuditedValues<T, TRepository>(this Mock<TRepository> repository)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Count()).Returns(
                repository.Object.GetAll().Count());
            repository.Setup(x => x.GetAllList()).Returns(
                repository.Object.GetAll().ToList());
        }

        public static void MockAuditedItems<T, TRepository>(this Mock<TRepository> repository,
            IQueryable<T> items)
            where T : AuditedEntity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.GetAll()).Returns(items);
            repository.SynchronizeAuditedValues<T, TRepository>();
        }
    }
}
