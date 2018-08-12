using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public class BtsQueryService
    {
        private readonly ITownRepository _townRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ITownBoundaryRepository _boundaryRepository;

        public BtsQueryService(ITownRepository townRepository, IBtsRepository btsRepository,
            ITownBoundaryRepository boundaryRepository)
        {
            _townRepository = townRepository;
            _btsRepository = btsRepository;
            _boundaryRepository = boundaryRepository;
        }

        public IEnumerable<CdmaBtsView> GetByTownNames(string city, string district, string town)
        {
            var townItem = _townRepository.GetAllList()
                .FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            var viewList = _btsRepository.GetAllList(x => x.TownId == townItem.Id).MapTo<List<CdmaBtsView>>();
            viewList.ForEach(x =>
            {
                x.CityName = city;
                x.DistrictName = district;
                x.TownName = town;
            });
            return townItem == null
                ? new List<CdmaBtsView>()
                : viewList;
        }

        public IEnumerable<CdmaBtsCluster> GetClustersByTownNames(string city, string district, string town)
        {
            var views = GetByTownNames(city, district, town).Where(x => x.IsInUse);
            return views.GroupBy(x => new
            {
                LongtituteGrid = (int) (x.Longtitute * 100000),
                LattituteGrid = (int) (x.Lattitute * 100000)
            }).Select(g => new CdmaBtsCluster
            {
                LongtituteGrid = g.Key.LongtituteGrid,
                LattituteGrid = g.Key.LattituteGrid,
                CdmaBtsViews = g
            });
        }

        public IEnumerable<CdmaBtsView> GetByTownArea(string city, string district, string town)
        {
            var list = new List<CdmaBtsView>();
            var townItem = _townRepository.GetAllList()
                .FirstOrDefault(x => x.CityName == city && x.DistrictName == district && x.TownName == town);
            if (townItem == null) return list;
            var boudary = _boundaryRepository.FirstOrDefault(x => x.TownId == townItem.Id);
            if (boudary == null) return list;
            foreach (var townEntity in _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district && x.TownName != town))
            {
                var views =
                    _btsRepository.GetAllList(x => x.TownId == townEntity.Id)
                        .Where(x => boudary.IsInTownRange(x))
                        .MapTo<List<CdmaBtsView>>();
                views.ForEach(x =>
                {
                    x.DistrictName = district;
                    x.TownName = townEntity.TownName;
                    x.TownId = townItem.Id;
                });
                list.AddRange(views);
            }
            return list;
        }

        public IEnumerable<CdmaBtsView> GetByGeneralName(string name)
        {
            var items =
                _btsRepository.GetAllList().Where(x => x.Name.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0).ToArray();
            if (items.Any()) return items.MapTo<IEnumerable<CdmaBtsView>>();
            var btsId = name.Trim().ConvertToInt(0);
            if (btsId > 0)
            {
                items = _btsRepository.GetAll().Where(x => x.BtsId == btsId).ToArray();
                if (items.Any()) return items.MapTo<IEnumerable<CdmaBtsView>>();
            }
            items =
                _btsRepository.GetAllList()
                    .Where(
                        x =>
                            x.Address.IndexOf(name.Trim(), StringComparison.Ordinal) >= 0)
                    .ToArray();
            return items.Any() ? items.MapTo<IEnumerable<CdmaBtsView>>() : null;
        }

        public CdmaBtsView GetByBtsId(int btsId)
        {
            var item = _btsRepository.GetByBtsId(btsId);
            if (item == null) return null;
            var view = item.MapTo<CdmaBtsView>();
            var town = _townRepository.Get(item.TownId);
            view.DistrictName = town.DistrictName;
            view.TownName = town.TownName;
            return view;
        }

        public IEnumerable<CdmaBtsView> QueryBtsViews(ENodebRangeContainer container)
        {
            var btss =
                _btsRepository.GetAllList(container.West, container.East, container.South, container.North)
                    .Where(x => x.IsInUse)
                    .ToList();
            var excludedBtss = from bts in btss
                join id in container.ExcludedIds on bts.BtsId equals id
                select bts;
            btss = btss.Except(excludedBtss).ToList();
            return btss.Any() ? btss.MapTo<IEnumerable<CdmaBtsView>>() : new List<CdmaBtsView>();
        }

        public CdmaBts UpdateTownInfo(int btsId, int townId)
        {
            var bts = _btsRepository.FirstOrDefault(x => x.BtsId == btsId);
            if (bts == null) return null;
            bts.TownId = townId;
            _btsRepository.SaveChanges();
            return bts;
        }
    }
}