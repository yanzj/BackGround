using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouQualityService
    {
        private IZhangshangyouQualityRepository _repository;

        public ZhangshangyouQualityService(IZhangshangyouQualityRepository repository)
        {
            _repository = repository;
        }

        private Stack<ZhangshangyouQuality> Stats { get; set; }

        public void UploadStats(StreamReader reader)
        {
            try
            {
                var stats = CsvContext.Read<ZhangshangyouQualityCsv>(reader, CsvFileDescription.CommaDescription)
                    .ToList();
                foreach (var stat in stats)
                {
                    Stats.Push(stat.MapTo<ZhangshangyouQuality>());
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
