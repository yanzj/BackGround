using AutoMapper;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.ViewModels.Precise
{
    public static class PreciseViewsQuery
    {
        public static Precise4GSector ConstructSector(this Precise4GView view, ICellRepository repository)
        {
            var sector = Mapper.Map<Precise4GView, Precise4GSector>(view);
            var cell = repository.GetBySectorId(view.CellId, view.SectorId);
            if (cell == null)
            {
                sector.Height = -1;
            }
            else
            {
                Mapper.Map(cell, sector);
                sector.DownTilt = cell.MTilt + cell.ETilt;
            }
            return sector;
        }
    }
}