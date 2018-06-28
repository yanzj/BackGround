using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Infrastructure;
using Lte.Parameters.Abstract.Kpi;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Lte.Domain.Common.Wireless;

namespace Lte.Evaluations.MockItems
{
    public static class MockItemsOperations
    {
        public static void MockGetId<TRepository, TEntity>(this Mock<TRepository> repository)
            where TRepository : class, IRepository<TEntity, int>
            where TEntity: class, IEntity<int>
        {
            repository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => repository.Object.GetAll().FirstOrDefault(x => x.Id == id));
        }

        public static void MockOperations(this Mock<IAlarmRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        repository.Object.GetAll().Where(x => x.HappenTime >= begin && x.HappenTime < end).ToList());

            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns<DateTime, DateTime, int>(
                    (begin, end, eNodebId) =>
                        repository.Object.GetAll()
                            .Where(x => x.HappenTime >= begin && x.HappenTime < end && x.ENodebId == eNodebId).ToList());

            repository.Setup(x => x.Count(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns<DateTime, DateTime, int>(
                    (begin, end, eNodebId) => repository.Object.GetAllList(begin, end, eNodebId).Count);
        }

        public static void MockOperations(this Mock<ICdmaCellRepository> repository)
        {
            repository.Setup(x => x.GetBySectorId(It.IsAny<int>(), It.IsAny<byte>()))
                .Returns<int, byte>(
                    (btsId, sectorId) =>
                        repository.Object.GetAll().FirstOrDefault(x => x.BtsId == btsId && x.SectorId == sectorId));
        }

        public static void MockOperation(this Mock<IBtsRepository> repository)
        {
            repository.Setup(x => x.GetByBtsId(It.IsAny<int>()))
                .Returns<int>(btsId => repository.Object.GetAll().FirstOrDefault(x => x.BtsId == btsId));
        }

        public static void MockOperation(this Mock<ICdmaRegionStatRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        repository.Object.GetAll().Where(x => x.StatDate >= begin && x.StatDate < end).ToList());

            repository.Setup(x => x.GetAllListAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        Task.Run(
                            () =>
                                repository.Object.GetAll().Where(x => x.StatDate >= begin && x.StatDate < end).ToList()));
        }

        public static void MockOperations(this Mock<ICellRepository> repository)
        {
            repository.Setup(x => x.GetBySectorId(It.IsAny<int>(), It.IsAny<byte>()))
                .Returns<int, byte>(
                    (eNodebId, sectorId) =>
                        repository.Object.GetAll().FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId));

            repository.Setup(x => x.GetAllInUseList())
                .Returns(repository.Object.GetAll().Where(x => x.IsInUse).ToList());
        }

        public static void MockOpertions(this Mock<ICollegeRepository> repository)
        {
            repository.Setup(x => x.GetByName(It.IsAny<string>())).Returns<string>(
                name => repository.Object.GetAll().FirstOrDefault(
                    x => x.Name == name));
        }

        public static void MockOperations(this Mock<ICollege3GTestRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) => repository.Object.GetAll().Where(x => x.TestTime > begin && x.TestTime <= end).ToList());
        }
        
        public static void MockOperations(this Mock<IENodebRepository> repository)
        {
            repository.Setup(x => x.FirstOrDefault(e => e.ENodebId == It.IsAny<int>()))
                .Returns<int>(eNodebId => repository.Object.GetAll().FirstOrDefault(x => x.ENodebId == eNodebId));
            
            repository.Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns<string>(name => repository.Object.GetAll().FirstOrDefault(x => x.Name == name));

            repository.Setup(x => x.GetAllInUseList())
                .Returns(repository.Object.GetAll().Where(x => x.IsInUse).ToList());
        }
        
        public static void MockOperations(this Mock<IInfrastructureRepository> repository)
        {
            repository.Setup(x => x.GetCollegeInfrastructureIds(It.IsAny<string>(), It.IsAny<InfrastructureType>()))
                .Returns<string, InfrastructureType>((collegeName, type) => repository.Object.GetAll().Where(x =>
                    x.HotspotName == collegeName && x.InfrastructureType == type)
                    .Select(x => x.InfrastructureId).ToList());
            
        }
        
        public static void MockOperations(this Mock<IPreciseCoverage4GRepository> repository)
        {
            repository.Setup(
                x => x.GetAllList(It.IsAny<int>(), It.IsAny<byte>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<int, int, DateTime, DateTime>(
                    (cellId, sectorId, begin, end) =>
                        repository.Object.GetAll()
                            .Where(
                                x =>
                                    x.CellId == cellId && x.SectorId == sectorId && x.StatTime >= begin &&
                                    x.StatTime < end)
                            .ToList());
        }

        public static void MockOperation(this Mock<IOptimzeRegionRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<string>()))
                .Returns<string>(city => repository.Object.GetAll().Where(x => x.City == city).ToList());

            repository.Setup(x => x.GetAllListAsync(It.IsAny<string>()))
                .Returns<string>(city => Task.Run(() =>
                    repository.Object.GetAll().Where(x => x.City == city).ToList()));
        }

        public static void MockOperation(this Mock<ITopDrop2GCellRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<string, DateTime, DateTime>((city, begin, end) =>
                    repository.Object.GetAll().Where(x => x.City == city && x.StatTime >= begin && x.StatTime < end).ToList());
        }
        
        public static void MockOperation(this Mock<IPreciseWorkItemCellRepository> repository)
        {
            repository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte>()))
                .Returns<string, int, byte>(
                    (number, eNodebId, sectorId) =>
                        repository.Object.GetAll()
                            .FirstOrDefault(
                                x => x.WorkItemNumber == number && x.ENodebId == eNodebId && x.SectorId == sectorId));
        }
        
    }
}
