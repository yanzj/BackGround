using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public static class CellQueries
    {

        public static CellRruView ConstructCellRruView(this Cell cell, IENodebRepository repository, ILteRruRepository rruRepository)
        {
            var view = Mapper.Map<Cell, CellRruView>(cell);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == cell.ENodebId);
            view.ENodebName = eNodeb?.Name;
            var rru =
                rruRepository.FirstOrDefault(x => x.ENodebId == cell.ENodebId && x.LocalSectorId == cell.LocalSectorId);
            rru?.MapTo(view);
            return view;
        }

        public static CellRruView ConstructCellRruView(this Cell cell, IENodebRepository repository, LteRru rru)
        {
            var view = Mapper.Map<Cell, CellRruView>(cell);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == cell.ENodebId);
            view.ENodebName = eNodeb?.Name;
            rru?.MapTo(view);
            return view;
        }

        public static CdmaCellView ConstructView(this CdmaCell cell, IBtsRepository repository)
        {
            var view = Mapper.Map<CdmaCell, CdmaCellView>(cell);
            var bts = repository.GetByBtsId(cell.BtsId);
            view.BtsName = bts?.Name;
            return view;
        }

        public static CdmaCompoundCellView ConstructView(this CdmaCell onexCell, CdmaCell evdoCell, IBtsRepository repository)
        {
            CdmaCompoundCellView view = null;
            if (onexCell != null)
            {
                view = Mapper.Map<CdmaCell, CdmaCompoundCellView>(onexCell);
                view.OnexFrequencyList = onexCell.FrequencyList;
                if (evdoCell != null) view.EvdoFrequencyList = evdoCell.FrequencyList;
            }
            else if (evdoCell != null)
            {
                view = Mapper.Map<CdmaCell, CdmaCompoundCellView>(evdoCell);
                view.EvdoFrequencyList = evdoCell.FrequencyList;
            }

            if (view != null)
            {
                var bts = repository.GetByBtsId(view.BtsId);
                view.BtsName = bts?.Name;
            }

            return view;
        }

        public static NearestPciCellView ConstructView(this NearestPciCell stat, IENodebRepository repository)
        {
            var view = Mapper.Map<NearestPciCell, NearestPciCellView>(stat);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.NearestCellId);
            view.NearestENodebName = eNodeb == null ? "Undefined" : eNodeb.Name;
            return view;
        }
    }
}