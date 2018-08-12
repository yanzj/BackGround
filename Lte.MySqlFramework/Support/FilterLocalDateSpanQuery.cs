using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.MySqlFramework.Support
{
    public abstract class FilterLocalDateSpanQuery<TView, THuaweiRepository, TZteRepository, TZte, THuawei> :
        DateSpanQuery<TView, THuaweiRepository, TZteRepository>
        where TView : class, ILteCellQuery, IENodebName, new()
        where TZte : Entity, ILteCellQuery
        where THuawei : Entity, ILocalCellQuery
        where TZteRepository : IFilterTopRepository<TZte>
        where THuaweiRepository : IFilterTopRepository<THuawei>
    {
        protected FilterLocalDateSpanQuery(THuaweiRepository huaweiRepository, TZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository) :
            base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        public IEnumerable<TView> QueryDistrictViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var results = HuaweiCellRepository.QueryDistrictFlowViews<TView, TZte, THuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository, ENodebRepository);
            return results;
        }

    }
}
