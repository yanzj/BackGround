using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Geo;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class GridClusterService
    {
        private readonly IGridClusterRepository _repository;

        public GridClusterService(IGridClusterRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<GridClusterView> QueryClusterViews(string theme)
        {
            return
                _repository.GetAllList(x => x.Theme == theme)
                    .GroupBy(x => x.ClusterNumber)
                    .Select(g => new GridClusterView
                    {
                        ClusterNumber = g.Key,
                        Theme = theme,
                        GridPoints = g.Select(x => new GeoGridPoint
                        {
                            X = x.X,
                            Y = x.Y
                        })
                    });
        }

        public IEnumerable<GridClusterView> QueryClusterViews(string theme, double west, double east, double south, double north)
        {
            var westX = (int) ((west - 112)/0.00049);
            var eastX = Math.Ceiling((east - 112)/0.00049);
            var southY = (int) ((south - 22)/0.00045);
            var northY = Math.Ceiling((north - 22)/0.00045);
            return
                _repository.GetAllList(x => x.Theme == theme
                                            && x.X >= westX && x.X < eastX && x.Y >= southY && x.Y < northY)
                    .GroupBy(x => x.ClusterNumber)
                    .Where(g => g.Count() > 4)
                    .Select(g => new GridClusterView
                    {
                        ClusterNumber = g.Key,
                        Theme = theme,
                        GridPoints = g.Select(x => new GeoGridPoint
                        {
                            X = x.X,
                            Y = x.Y
                        })
                    });
        }

        public IEnumerable<GridClusterView> QueryClusterViews(double west, double east, double south, double north)
        {
            var westX = (int)((west - 112) / 0.00049);
            var eastX = Math.Ceiling((east - 112) / 0.00049);
            var southY = (int)((south - 22) / 0.00045);
            var northY = Math.Ceiling((north - 22) / 0.00045);
            return
                _repository.GetAllList(x => x.X >= westX && x.X < eastX && x.Y >= southY && x.Y < northY)
                    .GroupBy(x => x.ClusterNumber)
                    .Where(g => g.Count() > 4)
                    .Select(g => new GridClusterView
                    {
                        ClusterNumber = g.Key,
                        Theme = "",
                        GridPoints = g.Select(x => new GeoGridPoint
                        {
                            X = x.X,
                            Y = x.Y
                        })
                    });
        }

    }
}