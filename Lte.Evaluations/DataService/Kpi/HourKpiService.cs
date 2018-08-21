using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourKpiService
    {
        private readonly IHourPrbRepository _prbRepository;
        private readonly ITownHourPrbRepository _townPrbRepository;

        private static Stack<HourPrb> HourPrbs { get; set; }

        public int HourPrbCount => HourPrbs.Count;

        public HourKpiService(IHourPrbRepository prbRepository, ITownHourPrbRepository townPrbRepository)
        {
            _prbRepository = prbRepository;
            _townPrbRepository = townPrbRepository;
            if (HourPrbs == null) HourPrbs = new Stack<HourPrb>();
        }


        public async Task<IEnumerable<HourKpiHistory>> GetHourHistories(DateTime begin, DateTime end)
        {
            var results = new List<HourKpiHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin;
                var endDate = begin.AddDays(1);
                var prbItems =
                    await _prbRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townPrbs =
                    await _townPrbRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate);
                results.Add(new HourKpiHistory
                {
                    DateString = begin.ToShortDateString(),
                    PrbItems = prbItems,
                    TownPrbs = townPrbs
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

        public void UploadPrbs(StreamReader reader)
        {
            var originCsvs = HourPrbCsv.ReadCsvs(reader);
            foreach (var csv in originCsvs)
            {
                HourPrbs.Push(csv.MapTo<HourPrb>());
            }
        }

        public async Task<bool> DumpOnePrbStat()
        {
            var stat = HourPrbs.Pop();
            if (stat != null)
            {
                await _prbRepository.ImportOneAsync(stat);
            }

            return true;
        }

        public void ClearPrbStats()
        {
            HourPrbs.Clear();
        }

    }
}
