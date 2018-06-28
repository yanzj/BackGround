using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Concrete
{
    public class FlowZteRepository : EfRepositorySave<MySqlContext, FlowZte>, IFlowZteRepository
    {
        public FlowZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public FlowZte Match(FlowZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }
    }

    public class RrcZteRepository : EfRepositorySave<MySqlContext, RrcZte>, IRrcZteRepository
    {
        public RrcZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public RrcZte Match(RrcZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }
    }

    public class PrbZteRepository : EfRepositorySave<MySqlContext, PrbZte>, IPrbZteRepository
    {
        public PrbZteRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public PrbZte Match(PrbZte stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }
    }

    public class FlowHuaweiRepository : EfRepositorySave<MySqlContext, FlowHuawei>, IFlowHuaweiRepository
    {
        public FlowHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public FlowHuawei Match(FlowHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }
    }

    public class RrcHuaweiRepository : EfRepositorySave<MySqlContext, RrcHuawei>, IRrcHuaweiRepository
    {
        public RrcHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public RrcHuawei Match(RrcHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }
    }

    public class PrbHuaweiRepository : EfRepositorySave<MySqlContext, PrbHuawei>, IPrbHuaweiRepository
    {
        public PrbHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public PrbHuawei Match(PrbHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.LocalCellId == stat.LocalCellId);
        }
    }

    public class TownFlowRepository : EfRepositorySave<MySqlContext, TownFlowStat>, ITownFlowRepository
    {
        public TownFlowRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownFlowStat Match(TownFlowStat stat)
        {
            return
                FirstOrDefault(
                    x =>
                        x.TownId == stat.TownId && x.StatTime == stat.StatTime &&
                        x.FrequencyBandType == stat.FrequencyBandType);
        }
    }

    public class TownRrcRepository : EfRepositorySave<MySqlContext, TownRrcStat>, ITownRrcRepository
    {
        public TownRrcRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class TownPrbRepository : EfRepositorySave<MySqlContext, TownPrbStat>, ITownPrbRepository
    {
        public TownPrbRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class InfrastructureRepository : EfRepositorySave<MySqlContext, InfrastructureInfo>, IInfrastructureRepository
    {
        public IEnumerable<int> GetCollegeInfrastructureIds(string collegeName, InfrastructureType type)
        {
            return GetAll().Where(x =>
                x.HotspotName == collegeName && x.InfrastructureType == type && x.HotspotType == HotspotType.College
                ).Select(x => x.InfrastructureId).ToList();
        }

        public IEnumerable<int> GetHotSpotInfrastructureIds(string name, InfrastructureType type, HotspotType hotspotType)
        {
            return GetAll().Where(x =>
                x.HotspotName == name && x.InfrastructureType == type && x.HotspotType == hotspotType
                ).Select(x => x.InfrastructureId).ToList();
        }
        
        public async Task InsertHotSpotCell(string hotSpotName, HotspotType hotspotType, int id)
        {
            var infrastructure = FirstOrDefault(x =>
                x.HotspotName == hotSpotName && x.HotspotType == hotspotType &&
                x.InfrastructureType == InfrastructureType.Cell && x.InfrastructureId == id);
            if (infrastructure == null)
            {
                await InsertAsync(new InfrastructureInfo
                {
                    HotspotName = hotSpotName,
                    HotspotType = hotspotType,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = id
                });
            }
        }

        public async Task InsertCollegeENodeb(string collegeName, int id)
        {
            var infrastructure = FirstOrDefault(x =>
                x.HotspotType == HotspotType.College && x.HotspotName == collegeName &&
                x.InfrastructureId == id && x.InfrastructureType == InfrastructureType.ENodeb);
            if (infrastructure == null)
            {
                await InsertAsync(new InfrastructureInfo
                {
                    HotspotName = collegeName,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = id
                });
            }
        }

        public async Task InsertCollegeBts(string collegeName, int id)
        {
            var infrastructure = FirstOrDefault(x =>
                x.HotspotType == HotspotType.College && x.HotspotName == collegeName &&
                x.InfrastructureId == id && x.InfrastructureType == InfrastructureType.CdmaBts);
            if (infrastructure == null)
            {
                await InsertAsync(new InfrastructureInfo
                {
                    HotspotName = collegeName,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.CdmaBts,
                    InfrastructureId = id
                });
            }
        }

        public InfrastructureRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class EFIndoorDistributionRepository
        : EfRepositorySave<MySqlContext, IndoorDistribution>, IIndoorDistributionRepository
    {
        public EFIndoorDistributionRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
