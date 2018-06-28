using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lte.Parameters.MockOperations
{
    public static class MockItemsRepository
    {
        public static void SynchronizeValues<T, TRepository>(this Mock<TRepository> repository)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Count()).Returns(
                repository.Object.GetAll().Count());
            repository.Setup(x => x.GetAllList()).Returns(
                repository.Object.GetAll().ToList());
        }

        public static void MockQueryItems<T, TRepository>(this Mock<TRepository> repository,
            IQueryable<T> items)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.GetAll()).Returns(items);
            repository.SynchronizeValues<T, TRepository>();
        }
        
        public static void MockRepositorySaveItems<T, TRepository>(
            this Mock<TRepository> repository)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Insert(It.IsAny<T>())).Callback<T>(
                e =>
                {
                    IEnumerable<T> btss = repository.Object.GetAll();
                    repository.Setup(x => x.GetAll()).Returns(
                        btss.Concat(new List<T> { e }).AsQueryable());
                    SynchronizeValues<T, TRepository>(repository);
                }).Returns<T>(e => e);
            repository.Setup(x => x.InsertAsync(It.IsAny<T>())).Callback<T>(e => repository.Object.Insert(e));
        }

        public static void MockRepositoryUpdateWorkItemCell<T, TRepository>(this Mock<TRepository> repository)
            where T : Entity, IWorkItemCell, new()
            where TRepository : class, IRepository<T>
        {
            Mapper.Initialize(cfg => cfg.CreateMap<T, T>());
            repository.Setup(x => x.Update(It.IsAny<T>())).Callback<T>(
                e =>
                {
                    var items = repository.Object.GetAll();
                    var item =
                        items.FirstOrDefault(
                            x =>
                                x.ENodebId == e.ENodebId && x.SectorId == e.SectorId &&
                                x.WorkItemNumber == e.WorkItemNumber);
                    if (item != null)
                        Mapper.Map(e, item);
                    SynchronizeValues<T, TRepository>(repository);
                }).Returns<T>(e => e);
            repository.Setup(x => x.UpdateAsync(It.IsAny<T>())).Callback<T>(e => repository.Object.Update(e));
        }

        public static void MockRepositoryDeleteItems<T, TRepository>(
            this Mock<TRepository> repository, IEnumerable<T> items)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            repository.Setup(x => x.Delete(It.Is<T>(e => e != null
                && items.FirstOrDefault(y => y == e) != null))
                ).Callback<T>(
                e =>
                {
                    repository.Setup(x => x.GetAll()).Returns(
                        items.Except(new List<T> { e }).AsQueryable());
                    SynchronizeValues<T, TRepository>(repository);
                });
        }

        public static void MockRepositoryDeleteItems<T, TRepository>(
            this Mock<TRepository> repository)
            where T : Entity
            where TRepository : class, IRepository<T>
        {
            if (repository.Object != null)
            {
                IEnumerable<T> items = repository.Object.GetAll();
                repository.Setup(x => x.Delete(It.Is<T>(e => e != null
                    && items.FirstOrDefault(y => y == e) != null))
                    ).Callback<T>(
                    e =>
                    {
                        repository.Setup(x => x.GetAll()).Returns(
                            items.Except(new List<T> { e }).AsQueryable());
                        SynchronizeValues<T, TRepository>(repository);
                    });
            }
        }

        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }
    }
}
