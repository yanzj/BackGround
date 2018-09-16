using Lte.Parameters.MockOperations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Evaluations.DataService.RegionKpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Support;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.TestService
{
    public class PreciseRegionStatTestService
    {
        private readonly Mock<ITownRepository> _townRepository;
        private readonly Mock<ITownPreciseCoverageRepository> _statRepository;

        public PreciseRegionStatTestService(Mock<ITownRepository> townRepository,
            Mock<ITownPreciseCoverageRepository> statRepository)
        {
            _townRepository = townRepository;
            _statRepository = statRepository;
        }

        public void ImportPreciseRecord(int townId, string statDate, int totalMrs, int firstNeighbors,
            int secondNeighbors, int thirdNeighbors)
        {
            _statRepository.MockQueryItems(new List<TownPreciseStat>
            {
                new TownPreciseStat
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
            var statList = statDates.Select((t, i) => new TownPreciseStat
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
            var statList = statDates.Select((t, i) => new TownPreciseStat
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
