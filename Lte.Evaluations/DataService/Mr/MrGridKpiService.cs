using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.MySqlFramework.Abstract;
using Lte.Domain.Regular;

namespace Lte.Evaluations.DataService.Mr
{
    public class MrGridKpiService
    {
        private readonly IMrGridKpiRepository _kpiRepository;

        public MrGridKpiService(IMrGridKpiRepository kpiRepository)
        {
            _kpiRepository = kpiRepository;
        }

        public IEnumerable<MrGridKpiDto> QueryKpiDtos(double west, double east, double south, double north)
        {
            var westX = (int)((west - 112) / 0.00049);
            var eastX = Math.Ceiling((east - 112) / 0.00049);
            var southY = (int)((south - 22) / 0.00045);
            var northY = Math.Ceiling((north - 22) / 0.00045);
            return _kpiRepository.GetAllList(x => x.X >= westX && x.X < eastX && x.Y >= southY && x.Y < northY)
                .MapTo<IEnumerable<MrGridKpiDto>>();
        }

        public IEnumerable<MrGridKpiDto> QueryKpiDtos(IEnumerable<GeoGridPoint> points)
        {
            var stats =
                points.Select(point => _kpiRepository.FirstOrDefault(t => t.X == point.X && t.Y == point.Y))
                    .Where(stat => stat != null)
                    .ToList();
            return stats.MapTo<IEnumerable<MrGridKpiDto>>();
        }

        public MrGridKpiDto QueryClusterKpi(IEnumerable<GeoGridPoint> points)
        {
            var stats =
                points.Select(point => _kpiRepository.FirstOrDefault(t => t.X == point.X && t.Y == point.Y))
                    .Where(stat => stat != null)
                    .ToList();
            var result = stats.Average().MapTo<MrGridKpiDto>();
            var filter = stats.Where(x => x.Rsrp < -110).ToList();
            if (filter.Any())
            {
                var distance = filter.Max(x => x.ShortestDistance);
                var candidate = filter.FirstOrDefault(x => x.ShortestDistance == distance);
                if (candidate != null)
                {
                    result.X = candidate.X;
                    result.Y = candidate.Y;
                    return result;
                }
            }
            filter = stats.Where(x => x.Rsrp < -105).ToList();
            if (filter.Any())
            {
                var distance = filter.Max(x => x.ShortestDistance);
                var candidate = filter.FirstOrDefault(x => x.ShortestDistance == distance);
                if (candidate != null)
                {
                    result.X = candidate.X;
                    result.Y = candidate.Y;
                    return result;
                }
            }
            filter = stats;
            if (filter.Any())
            {
                var distance = filter.Max(x => x.ShortestDistance);
                var candidate = filter.FirstOrDefault(x => x.ShortestDistance == distance);
                if (candidate != null)
                {
                    result.X = candidate.X;
                    result.Y = candidate.Y;
                    return result;
                }
            }
            return result;
        }
    }
}
