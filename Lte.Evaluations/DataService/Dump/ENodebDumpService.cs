using AutoMapper;
using Lte.Evaluations.DataService.Basic;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class ENodebDumpService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;

        public ENodebDumpService(IENodebRepository eNodebRepository, ITownRepository townRepository)
        {
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
        }

        public int DumpNewEnodebExcels(IEnumerable<ENodebExcel> infos)
        {
            var containers = (from info in infos
                join town in _townRepository.GetAllList()
                    on new {info.CityName, info.DistrictName, info.TownName} equals
                    new {town.CityName, town.DistrictName, town.TownName}
                select new ENodebExcelWithTownIdContainer
                {
                    ENodebExcel = info,
                    TownId = town.Id
                }).ToArray();

            if (!containers.Any()) return 0;
            var items =
                Mapper.Map<IEnumerable<ENodebExcelWithTownIdContainer>, List<ENodebWithTownIdContainer>>(containers);
            items.ForEach(x => { x.ENodeb.TownId = x.TownId; });

            var count = 0;
            foreach (var eNodeb in items.Select(x => x.ENodeb).ToList())
            {
                var item = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodeb.ENodebId);
                if (item == null)
                {
                    var result = _eNodebRepository.Insert(eNodeb);
                    if (result != null) count++;
                }
                else
                {
                    item.IsInUse = true;
                    item.Name = eNodeb.Name;
                    item.Address = eNodeb.Address;
                    item.Factory = eNodeb.Factory;
                    _eNodebRepository.SaveChanges();
                }
            }
            _eNodebRepository.SaveChanges();
            return count;
        }

        public bool DumpSingleENodebExcel(ENodebExcel info)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == info.ENodebId);
            if (eNodeb == null)
            {
                eNodeb = info.ConstructENodeb(_townRepository);
                var result = _eNodebRepository.Insert(eNodeb);
                if (result == null) return false;
                var item = BasicImportContainer.ENodebExcels.FirstOrDefault(x => x.ENodebId == info.ENodebId);
                if (item != null)
                {
                    BasicImportContainer.ENodebExcels.Remove(item);
                }
                _eNodebRepository.SaveChanges();
                return true;
            }
            eNodeb.IsInUse = true;
            _eNodebRepository.Update(eNodeb);
            _eNodebRepository.SaveChanges();
            return true;
        }

        public void VanishENodebs(ENodebIdsContainer container)
        {
            foreach (
                var eNodeb in
                container.ENodebIds.Select(eNodebId => _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId))
                    .Where(eNodeb => eNodeb != null))
            {
                eNodeb.IsInUse = false;
                _eNodebRepository.Update(eNodeb);
            }
            _eNodebRepository.SaveChanges();
        }
    }

    public class BtsDumpService
    {
        private readonly IBtsRepository _btsRepository;
        private readonly ITownRepository _townRepository;

        public BtsDumpService(IBtsRepository btsRepository, ITownRepository townRepository)
        {
            _btsRepository = btsRepository;
            _townRepository = townRepository;
        }

        public int DumpBtsExcels(IEnumerable<BtsExcel> infos)
        {
            var containers = (from info in infos
                              join town in _townRepository.GetAllList()
                                  on new { info.DistrictName, info.TownName } equals
                                  new { town.DistrictName, town.TownName }
                              select new BtsExcelWithTownIdContainer
                              {
                                  BtsExcel = info,
                                  TownId = town.Id
                              }).ToArray();

            if (!containers.Any()) return 0;
            var items =
                Mapper.Map<IEnumerable<BtsExcelWithTownIdContainer>, List<BtsWithTownIdContainer>>(containers);
            items.ForEach(x => { x.CdmaBts.TownId = x.TownId; });

            var count = 0;
            foreach (var bts in items.Select(x => x.CdmaBts).ToList())
            {
                if (_btsRepository.Insert(bts) != null)
                    count++;
            }
            _btsRepository.SaveChanges();
            return count;
        }

        public bool DumpSingleBtsExcel(BtsExcel info)
        {
            var bts = info.ConstructBts( _townRepository);
            if (bts == null)
            {
                bts = info.ConstructBts(_townRepository);
                var result = _btsRepository.Insert(bts);
                if (result == null) return false;
                var item = BasicImportService.BtsExcels.FirstOrDefault(x => x.BtsId == info.BtsId);
                if (item != null)
                {
                    BasicImportService.BtsExcels.Remove(item);
                }
                _btsRepository.SaveChanges();
                return true;
            }
            bts.IsInUse = true;
            _btsRepository.Update(bts);
            _btsRepository.SaveChanges();
            return true;
        }

        public void VanishBtss(ENodebIdsContainer container)
        {
            foreach (
                var bts in
                    container.ENodebIds.Select(btsId => _btsRepository.GetByBtsId(btsId))
                        .Where(bts => bts != null))
            {
                bts.IsInUse = false;
                _btsRepository.Update(bts);
            }
            _btsRepository.SaveChanges();
        }
    }
}
