using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.MySqlFramework.Query
{
    public abstract class HuaweiDateSpanQuery<T, TView, THuaweiRepository> : IDateSpanQuery<List<TView>>
        where TView : class, ILteCellQuery
    {
        protected readonly THuaweiRepository HuaweiRepository;
        private readonly ICellRepository _huaweiCellRepository;
        protected readonly int ENodebId;
        private readonly byte _sectorId;

        protected HuaweiDateSpanQuery(THuaweiRepository huaweiRepository, ICellRepository huaweiCellRepository,
            int eNodebId, byte sectorId)
        {
            HuaweiRepository = huaweiRepository;
            _huaweiCellRepository = huaweiCellRepository;
            ENodebId = eNodebId;
            _sectorId = sectorId;
        }

        public List<TView> Query(DateTime begin, DateTime end)
        {
            var huaweiCell =
                _huaweiCellRepository.FirstOrDefault(x => x.ENodebId == ENodebId && x.SectorId == _sectorId);
            var localCellId = huaweiCell?.LocalSectorId ?? _sectorId;
            var views =
                Mapper.Map<List<T>, List<TView>>(QueryList(begin, end, localCellId));
            foreach (var view in views)
            {
                view.SectorId = _sectorId;
            }
            return views;
        }

        protected abstract List<T> QueryList(DateTime begin, DateTime end, byte localCellId);
    }
}