using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.MySqlFramework.Abstract;
using Remotion.Data.Linq.Utilities;

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouCoverageService
    {
        private IZhangshangyouCoverageRepository _repository;

        public ZhangshangyouCoverageService(IZhangshangyouCoverageRepository repository)
        {
            _repository = repository;
            if (Stats == null)
            {
                Stats = new Stack<ZhangshangyouCoverageCsv>();
            }
        }

        private static Stack<ZhangshangyouCoverageCsv> Stats { get; set; }

        public void UploadStats(StreamReader reader)
        {
            var stats = CsvContext.Read<ZhangshangyouCoverageCsv>(reader, CsvFileDescription.CommaDescription)
                .ToList();
            foreach (var stat in stats)
            {
                Stats.Push(stat);
            }
            if (Stats.Count == 0)
                throw new ArgumentEmptyException("aaaa");
        }

        public async Task<bool> DumpOneStat()
        {
            var stat = Stats.Pop();
            if (stat == null) throw new NullReferenceException("coverage stat is null!");
            await _repository
                .UpdateOne<IZhangshangyouCoverageRepository, ZhangshangyouCoverage, ZhangshangyouCoverageCsv>(stat);
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
