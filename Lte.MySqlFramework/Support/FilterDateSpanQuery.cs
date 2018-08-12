using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.MySqlFramework.Support
{
    public abstract class FilterDateSpanQuery<TView, THuaweiRepository, TZteRepository, TZte, THuawei> :
        DateSpanQuery<TView, THuaweiRepository, TZteRepository> 
        where TView : class, ILteCellQuery, IENodebName, new()
        where TZte: Entity, ILteCellQuery
        where THuawei: Entity, ILteCellQuery
        where TZteRepository: IFilterTopRepository<TZte>
        where THuaweiRepository: IFilterTopRepository<THuawei>
    {
        protected FilterDateSpanQuery(THuaweiRepository huaweiRepository, TZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository) :
            base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        protected IEnumerable<TView> QueryDistrictViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var results = ENodebRepository.QueryDistrictCqiViews<TView, TZte, THuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository);
            return results;
        }
}
}
