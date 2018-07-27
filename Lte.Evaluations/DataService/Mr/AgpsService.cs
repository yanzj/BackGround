using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Channel;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Channel;

namespace Lte.Evaluations.DataService.Mr
{
    public class AgpsService
    {
        private readonly ITelecomAgpsRepository _telecomAgpsRepository;
        private readonly IMobileAgpsRepository _mobileAgpsRepository;
        private readonly IUnicomAgpsRepository _unicomAgpsRepository;
        private readonly IAgisDtPointRepository _agisDtPointRepository;

        public AgpsService(ITelecomAgpsRepository telecomAgpsRepository, IMobileAgpsRepository mobileAgpsRepository,
            IUnicomAgpsRepository unicomAgpsRepository, IAgisDtPointRepository agisDtPointRepository)
        {
            _telecomAgpsRepository = telecomAgpsRepository;
            _mobileAgpsRepository = mobileAgpsRepository;
            _unicomAgpsRepository = unicomAgpsRepository;
            _agisDtPointRepository = agisDtPointRepository;
        }

        public IEnumerable<AgpsCoverageView> QueryTelecomCoverageViews(DateTime begin, DateTime end,
            List<List<GeoPoint>> boundaries)
        {
            var range = new IntRangeContainer(boundaries);
            var stats =
                _telecomAgpsRepository.GetAllList(
                    x =>
                        x.StatDate >= begin && x.StatDate < end && x.X >= range.West && x.X < range.East &&
                        x.Y >= range.South && x.Y < range.North);
            return !stats.Any() ? new List<AgpsCoverageView>() : GenerateCoverageViews(boundaries, stats);
        }

        public int UpdateTelecomAgisPoint(AgpsCoverageView view, string district, string town)
        {
            var date = view.StatDate.Date;
            var point =
                _agisDtPointRepository.FirstOrDefault(
                    x => x.StatDate == date && x.X == view.X && x.Y == view.Y && x.Operator == district + town);
            if (point == null)
            {
                point = new AgisDtPoint
                {
                    X = view.X,
                    Y = view.Y,
                    Longtitute = view.Longtitute,
                    Lattitute = view.Lattitute,
                    StatDate = date,
                    TelecomRsrp = view.AverageRsrp,
                    TelecomRate100 = view.CoverageRate100,
                    TelecomRate105 = view.CoverageRate105,
                    TelecomRate110 = view.CoverageRate110,
                    Operator = district + town
                };
                _agisDtPointRepository.Insert(point);
            }
            else
            {
                point.TelecomRsrp = view.AverageRsrp;
                point.TelecomRate100 = view.CoverageRate100;
                point.TelecomRate105 = view.CoverageRate105;
                point.TelecomRate110 = view.CoverageRate110;
            }
            return _agisDtPointRepository.SaveChanges();
        }

        public int UpdateMobileAgisPoint(AgpsCoverageView view, string district, string town)
        {
            var date = view.StatDate.Date;
            var point =
                _agisDtPointRepository.FirstOrDefault(
                    x => x.StatDate == date && x.X == view.X && x.Y == view.Y && x.Operator == district + town);
            if (point == null)
            {
                point = new AgisDtPoint
                {
                    X = view.X,
                    Y = view.Y,
                    Longtitute = view.Longtitute,
                    Lattitute = view.Lattitute,
                    StatDate = date,
                    MobileRsrp = view.AverageRsrp,
                    MobileRate100 = view.CoverageRate100,
                    MobileRate105 = view.CoverageRate105,
                    MobileRate110 = view.CoverageRate110,
                    Operator = district + town
                };
                _agisDtPointRepository.Insert(point);
            }
            else
            {
                point.MobileRsrp = view.AverageRsrp;
                point.MobileRate100 = view.CoverageRate100;
                point.MobileRate105 = view.CoverageRate105;
                point.MobileRate110 = view.CoverageRate110;
            }
            return _agisDtPointRepository.SaveChanges();
        }

        public int UpdateUnicomAgisPoint(AgpsCoverageView view, string district, string town)
        {
            var date = view.StatDate.Date;
            var point =
                _agisDtPointRepository.FirstOrDefault(
                    x => x.StatDate == date && x.X == view.X && x.Y == view.Y && x.Operator == district + town);
            if (point == null)
            {
                point = new AgisDtPoint
                {
                    X = view.X,
                    Y = view.Y,
                    Longtitute = view.Longtitute,
                    Lattitute = view.Lattitute,
                    StatDate = date,
                    UnicomRsrp = view.AverageRsrp,
                    UnicomRate100 = view.CoverageRate100,
                    UnicomRate105 = view.CoverageRate105,
                    UnicomRate110 = view.CoverageRate110,
                    Operator = district + town
                };
                _agisDtPointRepository.Insert(point);
            }
            else
            {
                point.UnicomRsrp = view.AverageRsrp;
                point.UnicomRate100 = view.CoverageRate100;
                point.UnicomRate105 = view.CoverageRate105;
                point.UnicomRate110 = view.CoverageRate110;
            }
            return _agisDtPointRepository.SaveChanges();
        }

        public IEnumerable<AgpsCoverageView> QueryMobileCoverageViews(DateTime begin, DateTime end,
            List<List<GeoPoint>> boundaries)
        {
            var range = new IntRangeContainer(boundaries);
            var stats =
                _mobileAgpsRepository.GetAllList(
                    x =>
                        x.StatDate >= begin && x.StatDate < end && x.X >= range.West && x.X < range.East &&
                        x.Y >= range.South && x.Y < range.North);
            return !stats.Any() ? new List<AgpsCoverageView>() : GenerateCoverageViews(boundaries, stats);
        }

        public IEnumerable<AgpsCoverageView> QueryUnicomCoverageViews(DateTime begin, DateTime end,
            List<List<GeoPoint>> boundaries)
        {
            var range = new IntRangeContainer(boundaries);
            var stats =
                _unicomAgpsRepository.GetAllList(
                    x =>
                        x.StatDate >= begin && x.StatDate < end && x.X >= range.West && x.X < range.East &&
                        x.Y >= range.South && x.Y < range.North);
            return !stats.Any() ? new List<AgpsCoverageView>() : GenerateCoverageViews(boundaries, stats);
        }

        private static IEnumerable<AgpsCoverageView> GenerateCoverageViews(List<List<GeoPoint>> boundaries, List<AgpsMongo> stats)
        {
            var filterStats =
                stats.GroupBy(x => new {x.X, x.Y})
                    .Select(x => x.Average())
                    .Where(
                        x => boundaries.Any(boundary => GeoMath.IsInPolygon(new GeoPoint(x.Longtitute, x.Lattitute), boundary)));
            return filterStats.MapTo<List<AgpsCoverageView>>();
        }
        
    }
}