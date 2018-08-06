using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.MockOperations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;

namespace Lte.Evaluations.TestService
{
    public class CdmaRegionStatTestService
    {
        private readonly Mock<IOptimzeRegionRepository> _regionRepository;
        private readonly Mock<ICdmaRegionStatRepository> _statRepository;

        public CdmaRegionStatTestService(Mock<IOptimzeRegionRepository> regionRepository,
            Mock<ICdmaRegionStatRepository> statRepository)
        {
            _regionRepository = regionRepository;
            _statRepository = statRepository;
        }

        public void ImportElangRecord(string region, string recordDate, double erlang)
        {
            _statRepository.MockQueryItems(new List<CdmaRegionStat>
            {
                new CdmaRegionStat
                {
                    Region = region,
                    StatDate = DateTime.Parse(recordDate),
                    ErlangIncludingSwitch = erlang
                }
            }.AsQueryable());
        }

        public void ImportElangRecords(string region, string[] recordDates, double[] erlangs)
        {
            var statList = recordDates.Select((t, i) => new CdmaRegionStat
            {
                Region = region,
                StatDate = DateTime.Parse(t),
                ErlangIncludingSwitch = erlangs[i]
            }).ToList();
            _statRepository.MockQueryItems(statList.AsQueryable());
        }

        public void ImportElangRecords(string[] regions, string recordDate, double[] erlangs)
        {
            var statList = regions.Select((t, i) => new CdmaRegionStat
            {
                Region = t,
                StatDate = DateTime.Parse(recordDate),
                ErlangIncludingSwitch = erlangs[i]
            }).ToList();
            _statRepository.MockQueryItems(statList.AsQueryable());
        }

        public void ImportElangRecords(string[] regions, string[] recordDates, double[] erlangs)
        {
            var statList = regions.Select((t, i) => new CdmaRegionStat
            {
                Region = t,
                StatDate = DateTime.Parse(recordDates[i]),
                ErlangIncludingSwitch = erlangs[i]
            }).ToList();
            _statRepository.MockQueryItems(statList.AsQueryable());
        }

        public void ImportDrop2Gs(string[] regions, string recordDate, int[] drop2GNums, int[] drop2GDems)
        {
            var statList = regions.Select((t, i) => new CdmaRegionStat
            {
                Region = t,
                StatDate = DateTime.Parse(recordDate),
                Drop2GNum = drop2GNums[i],
                Drop2GDem = drop2GDems[i]
            }).ToList();
            _statRepository.MockQueryItems(statList.AsQueryable());
        }

        public async Task<CdmaRegionDateView> QueryLastDateStat(string initialDate, string city)
        {
            var service = new CdmaRegionStatService(_regionRepository.Object, _statRepository.Object);
            return await service.QueryLastDateStat(DateTime.Parse(initialDate), city);
        }

        public async Task<CdmaRegionStatTrend> QueryDateTrend(string beginDate, string endDate, string city)
        {
            var service = new CdmaRegionStatService(_regionRepository.Object, _statRepository.Object);
            return await service.QueryStatTrend(DateTime.Parse(beginDate), DateTime.Parse(endDate), city);
        }
    }
}
