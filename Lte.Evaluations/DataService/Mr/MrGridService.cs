using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Mr
{
    public class MrGridService
    {
        private readonly IMrGridRepository _repository;
        private readonly ITownRepository _townRepository;

        public MrGridService(IMrGridRepository repository, ITownRepository townRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
        }

        public void UploadMrGrids(XmlDocument xml, string district, string fileName)
        {
            var candidateDescritions = new[] { "竞对总体", "移动竞对", "联通竞对" };
            var competeDescription = candidateDescritions.FirstOrDefault(fileName.Contains);
            var list = competeDescription == null
                ? MrGridXml.ReadGridXmls(xml, district ?? "禅城")
                : MrGridXml.ReadGridXmlsWithCompete(xml, district ?? "禅城", competeDescription);
            foreach (var item in list)
            {
                _repository.Insert(item.MapTo<MrGrid>());
            }
            _repository.SaveChanges();
        }

        public void UploadMrGrids(StreamReader reader, string fileName)
        {
            var xml = new XmlDocument();
            xml.Load(reader);
            var districts = _townRepository.GetAllList().Select(x => x.DistrictName).Distinct();
            var district = districts.FirstOrDefault(fileName.Contains);
            UploadMrGrids(xml, district, fileName);
        }

        public IEnumerable<MrCoverageGridView> QueryCoverageGridViews(DateTime initialDate, string district)
        {
            var stats =
                _repository.QueryDate(initialDate, (repository, beginDate, endDate) => repository.GetAllList(
                    x =>
                        x.StatDate >= beginDate && x.StatDate < endDate && x.District == district &&
                        x.Compete == AlarmCategory.Self));
            return stats.MapTo<IEnumerable<MrCoverageGridView>>();
        }

        public IEnumerable<MrCoverageGridView> QueryCoverageGridViews(DateTime initialDate,
            IEnumerable<List<GeoPoint>> boundaries, string district)
        {
            var stats =
                _repository.QueryDate(initialDate, (repository, beginDate, endDate) => repository.GetAllList(
                    x =>
                        x.StatDate >= beginDate && x.StatDate < endDate && x.District == district &&
                        x.Compete == AlarmCategory.Self)).Where(x =>
                {
                    var fields = x.Coordinates.GetSplittedFields(';')[0].GetSplittedFields(',');
                    var point = new GeoPoint(fields[0].ConvertToDouble(0), fields[1].ConvertToDouble(0));
                    return boundaries.Any(boundary => GeoMath.IsInPolygon(point, boundary));
                });
            return stats.MapTo<IEnumerable<MrCoverageGridView>>();
        }

        public IEnumerable<MrCompeteGridView> QueryCompeteGridViews(DateTime initialDate, string district,
            AlarmCategory? compete)
        {
            var stats =
                _repository.QueryDate(initialDate, (repository, beginDate, endDate) => repository.GetAllList(
                    x =>
                        x.StatDate >= beginDate && x.StatDate < endDate && x.District == district &&
                        x.Frequency == -1 && x.Compete == compete)).ToList();
            return stats.MapTo<IEnumerable<MrCompeteGridView>>();
        }

        public IEnumerable<MrCompeteGridView> QueryCompeteGridViews(DateTime initialDate, string district,
            AlarmCategory? compete, IEnumerable<List<GeoPoint>> boundaries)
        {
            var stats =
                _repository.QueryDate(initialDate, (repository, beginDate, endDate) => repository.GetAllList(
                    x =>
                        x.StatDate >= beginDate && x.StatDate < endDate && x.District == district &&
                        x.Frequency == -1 && x.Compete == compete)).Where(x =>
                {
                    var fields = x.Coordinates.GetSplittedFields(';')[0].GetSplittedFields(',');
                    var point = new GeoPoint(fields[0].ConvertToDouble(0), fields[1].ConvertToDouble(0));
                    return boundaries.Aggregate(false, (current, boundary) => current || GeoMath.IsInPolygon(point, boundary));
                });
            return stats.MapTo<IEnumerable<MrCompeteGridView>>();
        }
        
        public IEnumerable<MrCompeteGridView> QueryCompeteGridViews(DateTime initialDate, string district,
            string competeDescription)
        {
            var competeTuple =
                WirelessConstants.EnumDictionary["AlarmCategory"].FirstOrDefault(x => x.Item2 == competeDescription);
            var compete = (AlarmCategory?)competeTuple?.Item1;

            return QueryCompeteGridViews(initialDate, district, compete);
        }
    }
}