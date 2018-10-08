using System;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Infrastructure;
using AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.Evaluations.ViewModels.Precise
{
    [AutoMapFrom(typeof(Cell))]
    public class CellPreciseKpiView: IENodebName, ILteCellQuery
    {
        public string ENodebName { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int Frequency { get; set; }

        public double RsPower { get; set; }

        public double Height { get; set; }

        public double Azimuth { get; set; }

        [AutoMapPropertyResolve("IsOutdoor", typeof(Cell), typeof(OutdoorDescriptionTransform))]
        public string Indoor { get; set; }

        public double ETilt { get; set; }

        public double MTilt { get; set; }

        public double DownTilt => ETilt + MTilt;

        public double AntennaGain { get; set; }

        public double PreciseRate { get; set; } = 100.0;

        public static CellPreciseKpiView ConstructView(Cell cell, IENodebRepository repository)
        {
            var view = Mapper.Map<Cell, CellPreciseKpiView>(cell);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == cell.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }

        public void UpdateKpi(IPreciseCoverage4GRepository repository, DateTime begin, DateTime end)
        {
            var query = repository.GetAll().Where(x => x.StatTime >= begin && x.StatTime < end
                                                       && x.CellId == ENodebId && x.SectorId == SectorId).ToList();
            if (query.Count > 0)
            {
                var sum = query.Sum(x => x.TotalMrs);
                PreciseRate = sum == 0 ? 100 : 100 - (double)query.Sum(x => x.SecondNeighbors) / sum * 100;
            }
        }
    }
}