using Abp.EntityFramework.AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.DataService.Basic
{
    public class ConstructionInformationService
    {
        private readonly IConstructionInformationRepository _repository;
        private readonly IENodebBaseRepository _baseRepository;

        public ConstructionInformationService(IConstructionInformationRepository repository,
            IENodebBaseRepository baseRepository)
        {
            _repository = repository;
            _baseRepository = baseRepository;
        }

        public IEnumerable<ConstructionView> QueryAll()
        {
            return _repository.GetAllList().MapTo<IEnumerable<ConstructionView>>();
        }

        public IEnumerable<ConstructionView> QueryByStationNum(string stationNum)
        {
            var eNodebs = _baseRepository.GetAllList(x => x.StationNum == stationNum);
            return eNodebs.Any() ? eNodebs.Select(x => _repository.GetAllList(c => c.ENodebId == x.ENodebId))
                .Aggregate((a, b) => a.Concat(b).ToList()).MapTo<IEnumerable<ConstructionView>>()
                : new List<ConstructionView>();
        }

        public ConstructionView QueryByCellName(string cellName)
        {
            var item = _repository.FirstOrDefault(x => x.CellName == cellName);
            return item == null ? null : item.MapTo<ConstructionView>();
        }

        public ConstructionView QueryByCellNum(string cellNum)
        {
            var item = _repository.FirstOrDefault(x => x.CellSerialNum == cellNum);
            return item == null ? null : item.MapTo<ConstructionView>();
        }
    }
}
