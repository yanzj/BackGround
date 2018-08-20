using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourKpiService
    {
        private readonly IHourPrbRepository _prbRepository;

        private static Stack<HourPrb> HourPrbs { get; set; }

        public int HourPrbCount => HourPrbs.Count;

        public HourKpiService(IHourPrbRepository prbRepository)
        {
            _prbRepository = prbRepository;
            if (HourPrbs == null) HourPrbs = new Stack<HourPrb>();
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
