using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Station;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Maintainence;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class AlarmWorkItemService
    {
        private readonly IAlarmWorkItemRepository _repository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellRepository _cellRepository;

        public AlarmWorkItemService(IAlarmWorkItemRepository repository,
            IENodebRepository eNodebRepository, ICellRepository cellRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
        }

        public IEnumerable<AlarmWorkItemView> QueryByDateSpan(DateTime begin, DateTime end)
        {
            return
                _repository.GetAllList(x => x.HappenTime >= begin && x.HappenTime < end)
                    .MapTo<IEnumerable<AlarmWorkItemView>>();
        }

        public IEnumerable<AlarmWorkItemView> QueryByDateSpan(DateTime begin, DateTime end, string networkType)
        {
            var network = networkType.GetEnumType<NetworkType>();
            return
                _repository.GetAllList(x => x.HappenTime >= begin && x.HappenTime < end && x.NetworkType == network)
                    .MapTo<IEnumerable<AlarmWorkItemView>>();
        }

        public IEnumerable<ENodebAlarmWorkItemGroup> QueryENodebAlarmGroups(DateTime begin, DateTime end, int count)
        {
            var alarms =
                _repository.GetAllList(
                    x => x.HappenTime >= begin && x.HappenTime < end && x.NetworkType == NetworkType.With4G);
            var groups = alarms.Where(x => x.SectorId == 255).ToList().GroupBy(x => x.ENodebId);
            var eNodebs = _eNodebRepository.GetAllList();
            var results = groups.Select(g => new ENodebAlarmWorkItemGroup
            {
                ENodebId = g.Key,
                AlarmCounts = g.Count(),
                ItemList = g.Select(x => x.MapTo<AlarmWorkItemView>()).ToList()
            }).OrderByDescending(x => x.AlarmCounts).Take(count).ToList();
            results.ForEach(result =>
            {
                var eNodeb = eNodebs.FirstOrDefault(x => x.ENodebId == result.ENodebId);
                if (eNodeb != null) eNodeb.MapTo(result);
            });
            return results;
        }

        public IEnumerable<CellAlarmWorkItemGroup> QueryCellAlarmGroups(DateTime begin, DateTime end, int count)
        {
            var alarms =
                _repository.GetAllList(
                    x => x.HappenTime >= begin && x.HappenTime < end && x.NetworkType == NetworkType.With4G);
            var groups = alarms.Where(x => x.SectorId < 255).ToList().GroupBy(x => new
            {
                x.ENodebId,
                x.SectorId
            });
            var eNodebs = _eNodebRepository.GetAllList();
            var cells = _cellRepository.GetAllList();
            var results = groups.Select(g => new CellAlarmWorkItemGroup
            {
                ENodebId = g.Key.ENodebId,
                SectorId = g.Key.SectorId,
                AlarmCounts = g.Count(),
                ItemList = g.Select(x => x.MapTo<AlarmWorkItemView>()).ToList()
            }).OrderByDescending(x => x.AlarmCounts).Take(count).ToList();
            results.ForEach(result =>
            {
                var eNodeb = eNodebs.FirstOrDefault(x => x.ENodebId == result.ENodebId);
                if (eNodeb != null) result.ENodebName = eNodeb.Name;
                var cell = cells.FirstOrDefault(x => x.ENodebId == result.ENodebId && x.SectorId == result.SectorId);
                if (cell != null) cell.MapTo(result);
            });
            return results;
        }
    }
}
