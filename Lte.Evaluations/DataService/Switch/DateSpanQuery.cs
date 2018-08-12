using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.Evaluations.DataService.Switch
{
    public abstract class DateSpanQuery<T, THuaweiRepository, TZteRepository> 
        where T : class, new()
    {
        protected readonly THuaweiRepository HuaweiRepository;
        protected readonly TZteRepository ZteRepository;
        protected readonly IENodebRepository ENodebRepository;
        protected readonly ICellRepository HuaweiCellRepository;
        protected readonly ITownRepository TownRepository;

        protected DateSpanQuery(THuaweiRepository huaweiRepository, TZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository,
            ITownRepository townRepository)
        {
            HuaweiRepository = huaweiRepository;
            ZteRepository = zteRepository;
            ENodebRepository = eNodebRepository;
            HuaweiCellRepository = huaweiCellRepository;
            TownRepository = townRepository;
        }

        protected IDateSpanQuery<List<T>> ConstructQuery(int eNodebId, byte sectorId)
        {
            var eNodeb = ENodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb == null) return null;
            return eNodeb.Factory == "华为"
                ? GenerateHuaweiQuery(eNodebId, sectorId)
                : GenerateZteQuery(eNodebId, sectorId);
        }

        protected abstract IDateSpanQuery<List<T>> GenerateHuaweiQuery(int eNodebId, byte sectorId);

        protected abstract IDateSpanQuery<List<T>> GenerateZteQuery(int eNodebId, byte sectorId);

        public List<T> Query(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var query = ConstructQuery(eNodebId, sectorId);
            return query?.Query(begin, end);
        }

        public T QueryAverageView(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var query = ConstructQuery(eNodebId, sectorId);
            var list = query?.Query(begin, end);
            if (list == null) return null;
            return list.Any() ? list.Average() : null;
        }

    }
}