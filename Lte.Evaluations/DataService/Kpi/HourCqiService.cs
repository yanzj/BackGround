using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourCqiService
    {
        private readonly IHourCqiRepository _usersRepository;
        private readonly IENodebRepository _eNodebRepository;

        private static Stack<HourCqi> HourCqis { get; set; }

        public int HourCqiCount => HourCqis.Count;

        public HourCqiService(IHourCqiRepository usersRepository, IENodebRepository eNodebRepository)
        {
            _usersRepository = usersRepository;
            _eNodebRepository = eNodebRepository;
            if (HourCqis == null) HourCqis = new Stack<HourCqi>();
        }
        
        public IEnumerable<HourCqiView> QueryHourCqiViews(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            var list = _usersRepository
                .GetAllList(x =>
                    x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId && x.SectorId == sectorId)
                .MapTo<List<HourCqiView>>();
            list.ForEach(stat => stat.ENodebName = eNodeb?.Name);
            return list;
        }

        public void UploadCqis(StreamReader reader)
        {
            var originCsvs = HourCqiCsv.ReadCsvs(reader);
            foreach (var csv in originCsvs)
            {
                HourCqis.Push(csv.MapTo<HourCqi>());
            }
        }

        public async Task<bool> DumpOneCqiStat()
        {
            var stat = HourCqis.Pop();
            if (stat != null)
            {
                await _usersRepository.ImportOneAsync(stat);
            }

            return true;
        }

        public void ClearCqiStats()
        {
            HourCqis.Clear();
        }

    }
}
