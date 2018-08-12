using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Common.Types;
using Lte.MySqlFramework.Abstract.Test;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Evaluations.Policy
{
    public static class DtQuery
    {
        public static void UpdateRasterInfo<TStat>(this IRasterTestInfoRepository rasterTestInfoRepository,
            IEnumerable<TStat> stats, string tableName, string networkType)
            where TStat : IRasterNum
        {
            var rasterNumbers = stats.Select(x => x.RasterNum).Distinct();
            foreach (var rasterNumber in rasterNumbers)
            {
                var raster =
                    rasterTestInfoRepository.FirstOrDefault(x => x.RasterNum == rasterNumber && x.NetworkType == networkType);
                if (raster == null)
                {
                    rasterTestInfoRepository.Insert(new RasterTestInfo
                    {
                        RasterNum = rasterNumber,
                        NetworkType = networkType,
                        CsvFilesName = tableName
                    });
                    rasterTestInfoRepository.SaveChanges();
                }
                else if (!raster.CsvFilesName.Contains(tableName))
                {
                    raster.CsvFilesName += ";" + tableName;
                    rasterTestInfoRepository.SaveChanges();
                }
            }
        }

        public static void UpdateCsvFileInfo(this IDtFileInfoRepository dtFileInfoRepository,
            string tableName, DateTime testDate)
        {
            var csvFileInfo = dtFileInfoRepository.FirstOrDefault(x => x.CsvFileName == tableName + ".csv");
            if (csvFileInfo == null)
            {
                dtFileInfoRepository.Insert(new CsvFilesInfo
                {
                    CsvFileName = tableName + ".csv",
                    TestDate = testDate
                });
                dtFileInfoRepository.SaveChanges();
            }
        }

        public static string GetFileNameExisted(this IFileRecordRepository fileRecordRepository,
            string path, out bool fileExisted)
        {
            var fields = path.Replace(".csv", "").GetSplittedFields('\\');
            var tableName = fields[fields.Length - 1].DtFileNameEncode();
            var tableNames = fileRecordRepository.GetTables();
            fileExisted = tableNames.FirstOrDefault(x => x == tableName) != null;
            return tableName;
        }

    }
}