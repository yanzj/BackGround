using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Concrete
{
    public class CdmaRegionStatRepository : EfRepositorySave<MySqlContext, CdmaRegionStat>, ICdmaRegionStatRepository
    {
        public List<CdmaRegionStat> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatDate >= begin && x.StatDate < end);
        }

        public async Task<List<CdmaRegionStat>> GetAllListAsync(DateTime begin, DateTime end)
        {
            return await GetAllListAsync(x => x.StatDate >= begin && x.StatDate < end);
        }

        public CdmaRegionStatRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CdmaRegionStat Match(CdmaRegionStatExcel stat)
        {
            return FirstOrDefault(x => x.Region == stat.Region && x.StatDate == stat.StatDate);
        }
    }

    public class TopDrop2GCellRepository : EfRepositorySave<MySqlContext, TopDrop2GCell>, ITopDrop2GCellRepository
    {
        public List<TopDrop2GCell> GetAllList(string city, DateTime begin, DateTime end)
        {
            return GetAll().Where(x => x.StatTime >= begin && x.StatTime < end && x.City == city).ToList();
        }
        
        public TopDrop2GCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TopDrop2GCell Match(TopDrop2GCellExcel stat)
        {
            var time = stat.StatDate.AddHours(stat.StatHour);
            return FirstOrDefault(x => x.BtsId == stat.BtsId && x.SectorId == stat.SectorId && x.StatTime == time);
        }
    }

    public class TopConnection3GRepository : EfRepositorySave<MySqlContext, TopConnection3GCell>, ITopConnection3GRepository
    {
        public List<TopConnection3GCell> GetAllList(string city, DateTime begin, DateTime end)
        {
            return GetAll().Where(x => x.StatTime >= begin && x.StatTime < end && x.City == city).ToList();
        }
        
        public TopConnection3GRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TopConnection3GCell Match(TopConnection3GCellExcel stat)
        {
            var time = stat.StatDate.AddHours(stat.StatHour);
            return FirstOrDefault(x => x.BtsId == stat.BtsId && x.SectorId == stat.SectorId && x.StatTime == time);
        }
    }

    public class TopConnection2GRepository : EfRepositorySave<MySqlContext, TopConnection2GCell>, ITopConnection2GRepository
    {
        public List<TopConnection2GCell> GetAllList(string city, DateTime begin, DateTime end)
        {
            return GetAll().Where(x => x.StatTime >= begin && x.StatTime < end && x.City == city).ToList();
        }

        public TopConnection2GRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TopConnection2GCell Match(TopConnection2GExcel stat)
        {
            var time = stat.StatDate.AddHours(stat.StatHour);
            return FirstOrDefault(x => x.BtsId == stat.BtsId && x.SectorId == stat.SectorId && x.StatTime == time);
        }
    }

    public class CdmaRruRepository : EfRepositorySave<MySqlContext, CdmaRru>, ICdmaRruRepository
    {
        public CdmaRruRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CdmaRru Match(CdmaCellExcel stat)
        {
            return Get(stat.BtsId, stat.SectorId);
        }
        
        public CdmaRru Get(int btsId, byte sectorId)
        {
            return FirstOrDefault(x => x.BtsId == btsId && x.SectorId == sectorId);
        }
    }

    public class LteRruRepository : EfRepositorySave<MySqlContext, LteRru>, ILteRruRepository
    {
        public LteRruRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public LteRru Match(CellExcel stat)
        {
            return Get(stat.ENodebId, stat.LocalSectorId);
        }
        
        public LteRru Get(int eNodebId, byte localSectorId)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId && x.LocalSectorId == localSectorId);
        }
    }

    public class AreaTestDateDateRepository : EfRepositorySave<MySqlContext, AreaTestDate>, IAreaTestDateRepository
    {
        public AreaTestDateDateRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class TownRepository : EfRepositoryBase<MySqlContext, Town>, ITownRepository
    {
        public TownRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IEnumerable<Town> QueryTowns(string city, string district, string town)
        {
            const string flag = "=All=";
            city = city ?? flag;
            district = district ?? flag;
            town = town ?? flag;
            return GetAllList(x =>
                (x.TownName == town || town.IndexOf(flag, StringComparison.Ordinal) >= 0)
                && (x.DistrictName == district || district.IndexOf(flag, StringComparison.Ordinal) >= 0)
                && (x.CityName == city || city.IndexOf(flag, StringComparison.Ordinal) >= 0));
        }
        
        public Town QueryTown(string district, string town)
        {
            return FirstOrDefault(x => x.DistrictName == district && x.TownName == town);
        }
        
        public IEnumerable<string> GetFoshanDistricts()
        {
            return new[] { "顺德", "南海", "禅城", "三水", "高明" };
        }
    }

    public class OptimizeOptimzeRegionRepository : EfRepositorySave<MySqlContext, OptimizeRegion>, IOptimzeRegionRepository
    {
        public OptimizeOptimzeRegionRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<OptimizeRegion> GetAllList(string city)
        {
            return GetAllList(x => x.City == city);
        }

        public async Task<List<OptimizeRegion>> GetAllListAsync(string city)
        {
            return await GetAllListAsync(x => x.City == city);
        }
    }

    public class LteNeighborCellRepository : EfRepositoryBase<MySqlContext, LteNeighborCell>, ILteNeighborCellRepository
    {
        public LteNeighborCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class NearestPciCellRepository : EfRepositorySave<MySqlContext, NearestPciCell>, INearestPciCellRepository
    {
        public List<NearestPciCell> GetAllList(int cellId, byte sectorId)
        {
            return GetAllList(x => x.CellId == cellId && x.SectorId == sectorId);
        }

        public NearestPciCell GetNearestPciCell(int cellId, byte sectorId, short pci)
        {
            return FirstOrDefault(x => x.CellId == cellId && x.SectorId == sectorId && x.Pci == pci);
        }
        
        public NearestPciCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
