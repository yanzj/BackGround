using System.Linq;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Station
{
    public interface IStationDictionaryRepository : IRepository<StationDictionary>,
        IMatchRepository<StationDictionary, StationDictionaryExcel>, ISaveChanges
    {
        
    }

    public static class StationDictionaryQuery
    {
        
        public static string CalculateStationTown(this IStationDictionaryRepository repository,ENodebBaseExcel excel)
        {
            var candidates = new[] {"石湾", "张槎", "祖庙"};
            if (candidates.FirstOrDefault(x => excel.StationTown == x) != null) return excel.StationTown;
            var candidates2 = new[]
            {
                "石湾", "张槎", "祖庙", "朝东", "桂城", "季华", "佛大", "澜石", "同济", "亲仁", "敦厚", "大富", "城西",
                "化纤", "中院", "吴勤"
            };
            var result = candidates2.FirstOrDefault(x => excel.ENodebName.Contains(x));
            if (result != null)
            {
                switch (result)
                {
                    case "石湾":
                    case "季华":
                    case "澜石":
                    case "中院":
                        return "石湾";
                    case "张槎":
                    case "佛大":
                    case "大富":
                    case "城西":
                        return "张槎";
                    default:
                        return "祖庙";
                }
            }
            var station = repository.FirstOrDefault(x => x.StationNum == excel.StationNum);
            if (station == null) return excel.StationTown;
            result = candidates.FirstOrDefault(x =>
                station.ElementName.Contains(x) || station.Address.Contains(x));
            return result ?? excel.StationTown;
        }

    }
}