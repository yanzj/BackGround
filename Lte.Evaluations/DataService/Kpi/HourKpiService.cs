using System.Collections.Generic;
using System.IO;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourKpiService
    {
        private readonly IHourPrbRepository _prbRepository;

        private static Stack<HourPrb> HourPrbs { get; set; }

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

    }
}
