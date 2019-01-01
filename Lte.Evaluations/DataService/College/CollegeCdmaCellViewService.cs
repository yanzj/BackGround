using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Entities.Infrastructure;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.College;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeCdmaCellViewService
    {
        private readonly IHotSpotCdmaCellRepository _repository;
        private readonly ICdmaCellRepository _cellRepository;
        private readonly IBtsRepository _btsRepository;

        public CollegeCdmaCellViewService(IHotSpotCdmaCellRepository repository, ICdmaCellRepository cellRepository,
            IBtsRepository btsRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _btsRepository = btsRepository;
        }

        public IEnumerable<CdmaCellView> GetViews(string collegeName)
        {
            var ids = _repository.GetAllList(
                x =>
                    x.HotspotType == HotspotType.College && x.InfrastructureType == InfrastructureType.CdmaCell &&
                    x.HotspotName == collegeName);
            var query =
                ids.Select(x => _cellRepository.GetBySectorId(x.BtsId, x.SectorId)).Where(cell => cell != null).ToList();
            return query.Any()
                ? query.Select(x => CellQueries.ConstructView(x, _btsRepository))
                : null;
        }

        public IEnumerable<SectorView> Query(string collegeName)
        {
            var ids =
                _repository.GetAllList(
                    x =>
                        x.HotspotType == HotspotType.College && x.InfrastructureType == InfrastructureType.CdmaCell &&
                        x.HotspotName == collegeName);
            var query =
                ids.Select(x => _cellRepository.GetBySectorId(x.BtsId, x.SectorId)).Where(cell => cell != null).ToList();
            return query.Any()
                ? Mapper.Map<IEnumerable<CdmaCellView>, IEnumerable<SectorView>>(
                    query.Select(x => CellQueries.ConstructView(x, _btsRepository)))
                : null;
        }

        public async Task<int> UpdateHotSpotCells(CollegeCellNamesContainer container)
        {
            foreach (var cell in from cellName in container.CellNames
                select cellName.GetSplittedFields('-')
                into fields
                where fields.Length > 1
                let bts = _btsRepository.GetByName(fields[0])
                where bts != null
                select _cellRepository.GetBySectorId(bts.BtsId, fields[1].ConvertToByte(0))
                into cell
                where cell != null
                select cell)
            {
                await _repository.InsertAsync(new HotSpotCdmaCellId
                {
                    BtsId = cell.BtsId,
                    SectorId = cell.SectorId,
                    HotspotType = HotspotType.College,
                    HotspotName = container.CollegeName
                });
            }
            return _repository.SaveChanges();
        }
    }
}