using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Mr
{
    public class LteNeighborCellService
    {
        private readonly ILteNeighborCellRepository _repository;

        public LteNeighborCellService(ILteNeighborCellRepository repository)
        {
            _repository = repository;
        }

        public List<LteNeighborCell> QueryCells(int cellId, byte sectorId)
        {
            return _repository.GetAllList(x => x.CellId == cellId && x.SectorId == sectorId);
        }
    }
}