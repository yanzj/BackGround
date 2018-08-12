using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Region;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public class PlanningQueryService
    {
        private readonly ITownRepository _townRepository;
        private readonly IPlanningSiteRepository _planningSiteRepository;

        public PlanningQueryService(ITownRepository townRepository, IPlanningSiteRepository planningSiteRepository)
        {
            _townRepository = townRepository;
            _planningSiteRepository = planningSiteRepository;
        }

        public IEnumerable<PlanningSiteView> QueryPlanningSiteViews(double west, double east, double south, double north)
        {
            var sites =
                _planningSiteRepository.GetAllList(
                    x => x.Longtitute >= west && x.Longtitute <= east && x.Lattitute >= south && x.Lattitute <= north);
            var views = sites.MapTo<List<PlanningSiteView>>();
            views.ForEach(view =>
            {
                var town = view.TownId <= 0 ? null : _townRepository.Get(view.TownId);
                if (town != null)
                {
                    view.District = town.DistrictName;
                    view.Town = town.TownName;
                }
            });
            return views;
        }

        public IEnumerable<PlanningSiteView> GetAllSites(bool isOpened)
        {
            var towns = _townRepository.GetAllList();
            var views = new List<PlanningSiteView>();
            foreach (
                var stats in
                towns.Select(
                    town =>
                        GetPlanningSites(town, isOpened)
                            .MapTo<List<PlanningSiteView>>()
                            .Select(x =>
                            {
                                x.District = town.DistrictName;
                                x.Town = town.TownName;
                                return x;
                            })))
            {
                views.AddRange(stats);
            }
            return views;

        }

        public IEnumerable<PlanningSiteView> GetENodebsByDistrict(string city, string district, bool? isOpened = null)
        {
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            var views = new List<PlanningSiteView>();
            foreach (
                var stats in
                towns.Select(
                    town =>
                        GetPlanningSites(town, isOpened)
                            .MapTo<List<PlanningSiteView>>()
                            .Select(x =>
                            {
                                x.District = district;
                                x.Town = town.TownName;
                                return x;
                            })))
            {
                views.AddRange(stats);
            }
            return views;
        }

        private List<PlanningSite> GetPlanningSites(Town town, bool? isOpened)
        {
            if (isOpened==null) return _planningSiteRepository.GetAllList(x => x.TownId == town.Id);
            if (isOpened == true)
                return _planningSiteRepository.GetAllList(x => x.TownId == town.Id && x.FinishedDate != null);
            return _planningSiteRepository.GetAllList(x => x.TownId == town.Id && x.FinishedDate == null);
        }
    }
}