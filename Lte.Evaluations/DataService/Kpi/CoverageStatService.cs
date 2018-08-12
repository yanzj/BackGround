using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Kpi
{
    public class CoverageStatService
    {
        private readonly ICoverageStatRepository _repository;

        private static Stack<CoverageStat> CoverageStats { get; set; }

        public CoverageStatService(ICoverageStatRepository repository)
        {
            _repository = repository;
            if (CoverageStats == null)
                CoverageStats = new Stack<CoverageStat>();
        }

        public void UploadStats(string path, string sheetName)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<CoverageStatExcel>(sheetName) select c).MapTo<List<CoverageStat>>();
            stats = stats.GroupBy(stat => new { stat.ENodebId, stat.SectorId }).Select(g => g.ArraySum()).ToList();
            var date = sheetName.GetSecondDateByString().ConvertToDateTime(DateTime.Today);
            foreach (var stat in stats)
            {
                stat.StatDate = date;
                CoverageStats.Push(stat);
            }
        }

        public bool DumpOneStat()
        {
            var stat = CoverageStats.Pop();
            if (stat == null) throw new NullReferenceException("coverage stat is null!");
            return _repository.ImportOne(stat) != null;
        }

        public int GetStatsToBeDump()
        {
            return CoverageStats.Count;
        }

        public void ClearStats()
        {
            CoverageStats.Clear();
        }

    }
}
