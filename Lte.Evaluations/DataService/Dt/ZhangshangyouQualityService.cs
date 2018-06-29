using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouQualityService
    {
        private IZhangshangyouQualityRepository _repository;

        public ZhangshangyouQualityService(IZhangshangyouQualityRepository repository)
        {
            _repository = repository;
            if (Stats == null)
            {
                Stats = new Stack<ZhangshangyouQualityCsv>();
            }
        }

        private static Stack<ZhangshangyouQualityCsv> Stats { get; set; }

        public void UploadStats(StreamReader reader)
        {
            var stats = CsvContext.Read<ZhangshangyouQualityCsv>(reader, CsvFileDescription.CommaDescription)
                .ToList();
            foreach (var stat in stats)
            {
                Stats.Push(stat);
            }
        }

        public async Task<bool> DumpOneStat()
        {
            var stat = Stats.Pop();
            if (stat == null) throw new NullReferenceException("coverage stat is null!");
            await _repository
                .UpdateOne<IZhangshangyouQualityRepository, ZhangshangyouQuality, ZhangshangyouQualityCsv>(stat);
            return true;
        }

        public int GetStatsToBeDump()
        {
            return Stats.Count;
        }

        public void ClearStats()
        {
            Stats.Clear();
        }

    }
}
