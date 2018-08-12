using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;

namespace Lte.Evaluations.DataService.Mr
{
    public class PreciseWorkItemService
    {
        private readonly IPreciseWorkItemCellRepository _repository;
        private readonly ICellPowerService _powerService;
        private readonly ICellRepository _cellRepository;

        public PreciseWorkItemService(IPreciseWorkItemCellRepository repository, ICellPowerService powerService,
            ICellRepository cellRepository)
        {
            _repository = repository;
            _powerService = powerService;
            _cellRepository = cellRepository;
        }

        public async Task UpdateAsync<TLteCellId>(IPreciseWorkItemDto<TLteCellId> container)
            where TLteCellId : class, ILteCellQuery, new()
        {
            foreach (var neighbor in container.Items)
            {
                var item = _repository.Get(container.WorkItemNumber, neighbor.ENodebId, neighbor.SectorId);
                if (item == null)
                {
                    item = neighbor.MapTo<PreciseWorkItemCell>();
                    item.WorkItemNumber = container.WorkItemNumber;
                    var cell = _cellRepository.GetBySectorId(neighbor.ENodebId, neighbor.SectorId);
                    if (cell != null) item.OriginalDownTilt = cell.MTilt + cell.ETilt;
                    var power = _powerService.Query(neighbor.ENodebId, neighbor.SectorId);
                    if (power != null) item.OriginalRsPower = power.RsPower;
                    await _repository.InsertAsync(item);
                }
                else
                {
                    Mapper.Map(neighbor, item);
                    await _repository.UpdateAsync(item);
                }
                _repository.SaveChanges();
            }
        }

        public void Update<TLteCellId>(IPreciseWorkItemDto<TLteCellId> container)
            where TLteCellId : class, ILteCellQuery, new()
        {
            foreach (var neighbor in container.Items)
            {
                var item = _repository.Get(container.WorkItemNumber, neighbor.ENodebId, neighbor.SectorId);
                if (item == null)
                {
                    item = neighbor.MapTo<PreciseWorkItemCell>();
                    item.WorkItemNumber = container.WorkItemNumber;
                    var cell = _cellRepository.GetBySectorId(neighbor.ENodebId, neighbor.SectorId);
                    if (cell != null) item.OriginalDownTilt = cell.MTilt + cell.ETilt;
                    var power = _powerService.Query(neighbor.ENodebId, neighbor.SectorId);
                    if (power != null) item.OriginalRsPower = power.RsPower;
                    _repository.Insert(item);
                }
                else
                {
                    Mapper.Map(neighbor, item);
                    _repository.Update(item);
                }
                _repository.SaveChanges();
            }
        }

        public PreciseWorkItemCell Query(string number, int eNodebId, byte sectorId)
        {
            return _repository.Get(number, eNodebId, sectorId);
        }

        public List<PreciseWorkItemCell> Query(string number)
        {
            return _repository.GetAllList(number);
        }
    }
}
