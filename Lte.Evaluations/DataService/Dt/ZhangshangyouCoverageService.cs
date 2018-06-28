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

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouCoverageService
    {
        private IZhangshangyouCoverageRepository _repository;

        public ZhangshangyouCoverageService(IZhangshangyouCoverageRepository repository)
        {
            _repository = repository;
        }

        private Stack<ZhangshangyouCoverage> Stats { get; set; }

        public void UploadStats(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<ZhangshangyouCoverageCsv>(reader, CsvFileDescription.CommaDescription)
                    .ToList();
                foreach (var stat in stats)
                {
                    Stats.Push(stat.MapTo<ZhangshangyouCoverage>());
                }
            }
            catch
            {
                // ignored
            }
        }

        public bool DumpOneStat()
        {
            var stat = Stats.Pop();
            if (stat == null) throw new NullReferenceException("coverage stat is null!");
            return _repository.ImportOne(stat) != null;
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
