using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using AutoMapper;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Query
{
    public abstract class ZteDateSpanQuery<T, TView, TZteRepository> : IDateSpanQuery<List<TView>>
        where TView : class, ILteCellQuery
    {
        protected readonly TZteRepository ZteRepository;
        protected readonly int ENodebId;
        protected readonly byte SectorId;

        protected ZteDateSpanQuery(TZteRepository zteRepository, int eNodebId, byte sectorId)
        {
            ZteRepository = zteRepository;
            ENodebId = eNodebId;
            SectorId = sectorId;
        }

        public List<TView> Query(DateTime begin, DateTime end)
        {
            return Mapper.Map<List<T>, List<TView>>(QueryList(begin, end));
        }

        protected abstract List<T> QueryList(DateTime begin, DateTime end);
    }
}