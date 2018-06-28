using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Parameters.Entities.Dt;

namespace Lte.Evaluations.DataService.Dt
{
    public static class DtFileReaderQuery
    {
        public static List<FileRecord4GCsv> GetFileRecord4GByHuawei(this StreamReader reader)
        {
            var huaweiInfos = CsvContext.Read<FileRecord4GHuawei>(reader, CsvFileDescription.CommaDescription).ToList();
            return huaweiInfos.FirstOrDefault(x => x.Rsrp != null) == null
                ? null
                : huaweiInfos.MapTo<List<FileRecord4GCsv>>();
        }

        public static List<FileRecord4GCsv> GetFileRecord4GByZte(this StreamReader reader)
        {
            try
            {
                var zteInfos = CsvContext.Read<FileRecord4GZte>(reader, CsvFileDescription.CommaDescription).ToList();
                return zteInfos.FirstOrDefault(x => x.Rsrp != null) == null
                    ? null
                    : zteInfos.MapTo<List<FileRecord4GCsv>>();
            }
            catch
            {
                return null;
            }
        }

        public static List<FileRecord4GCsv> GetFileRecord4GCsvs(this StreamReader reader)
        {
            try
            {
                var infos = CsvContext.Read<FileRecord4GCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                return infos.FirstOrDefault(x => x.Rsrp != null) == null ? null : infos;
            }
            catch
            {
                return null;
            }
        }

    }
}
