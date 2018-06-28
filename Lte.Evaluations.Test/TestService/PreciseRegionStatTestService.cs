using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using Lte.Parameters.MockOperations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.TestService
{
    public class PreciseRegionStatTestService
    {
        private readonly Mock<ITownRepository> _townRepository;
        private readonly Mock<ITownPreciseCoverage4GStatRepository> _statRepository;

        public PreciseRegionStatTestService(Mock<ITownRepository> townRepository,
            Mock<ITownPreciseCoverage4GStatRepository> statRepository)
        {
            _townRepository = townRepository;
            _statRepository = statRepository;
        }

        public void ImportPreciseRecord(int townId, string statDate, int totalMrs, int firstNeighbors,
            int secondNeighbors, int thirdNeighbors)
        {
            _statRepository.MockQueryItems(new List<TownPreciseCoverage4GStat>
            {
                new TownPreciseCoverage4GStat
                {
                    TownId = townId,
                    StatTime = DateTime.Parse(statDate),
                    TotalMrs = totalMrs,
                    FirstNeighbors = firstNeighbors,
                    SecondNeighbors = secondNeighbors,
                    ThirdNeighbors = thirdNeighbors
                }
            }.AsQueryable());
        }

        public void ImportPreciseRecord(int townId, string[] statDates, int[] totalMrs, int[] firstNeighbors,
            int[] secondNeighbors, int[] thirdNeighbors)
        {
            var statList = statDates.Select((t, i) => new TownPreciseCoverage4GStat
            {
                TownId = townId,
                StatTime = DateTime.Parse(statDates[i]),
                TotalMrs = totalMrs[i],
                FirstNeighbors = firstNeighbors[i],
                SecondNeighbors = secondNeighbors[i],
                ThirdNeighbors = thirdNeighbors[i]
            }).ToList();
            _statRepository.MockQueryItems(statList.AsQueryable());
        }

        public void ImportPreciseRecord(int[] townIds, string[] statDates, int[] totalMrs, int[] firstNeighbors,
            int[] secondNeighbors, int[] thirdNeighbors)
        {
            var statList = statDates.Select((t, i) => new TownPreciseCoverage4GStat
            {
                TownId = townIds[i],
                StatTime = DateTime.Parse(statDates[i]),
                TotalMrs = totalMrs[i],
                FirstNeighbors = firstNeighbors[i],
                SecondNeighbors = secondNeighbors[i],
                ThirdNeighbors = thirdNeighbors[i]
            }).ToList();
            _statRepository.MockQueryItems(statList.AsQueryable());
        }

        public PreciseRegionDateView QueryLastDateStat(string initialDate, string city)
        {
            var service = new PreciseRegionStatService(_statRepository.Object, _townRepository.Object);
            return service.QueryLastDateStat(DateTime.Parse(initialDate), city);
        }
    }
}
