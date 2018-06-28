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
    public class IndoorDistributionService
    {
        private readonly IIndoorDistributionRepository _repository;
        private readonly IENodebBaseRepository _eNodebBaseRepository;
        private readonly IConstructionInformationRepository _cellRepository;

        public IndoorDistributionService(IIndoorDistributionRepository repository,
            IENodebBaseRepository eNodebBaseRepository, IConstructionInformationRepository cellRepository)
        {
            _repository = repository;
            _eNodebBaseRepository = eNodebBaseRepository;
            _cellRepository = cellRepository;
        }

        public IEnumerable<IndoorDistributionView> QueryAll()
        {
            return _repository.GetAllList().MapTo<IEnumerable<IndoorDistributionView>>();
        }

        public IEnumerable<IndoorDistributionView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north)
                    .MapTo<IEnumerable<IndoorDistributionView>>();
        }

        public IndoorDistributionView QueryByRruNum(string rruNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum);
            return item == null ? null : item.MapTo<IndoorDistributionView>();
        }

        public IEnumerable<IndoorDistributionView> QueryByStationNum(string stationNum)
        {
            var eNodebs = _eNodebBaseRepository.GetAllList(x => x.StationNum == stationNum);
            if (!eNodebs.Any()) return new List<IndoorDistributionView>();
            var cells = eNodebs.Select(x =>
                    _cellRepository.GetAllList(c => c.ENodebId == x.ENodebId && c.IndoorDistributionSerial != null))
                .Aggregate((a, b) => a.Concat(b).ToList());
            return cells.Any()
                ? cells.Select(c => _repository.GetAllList(x => x.CellSerialNum == c.CellSerialNum))
                    .Aggregate((a, b) => a.Concat(b).ToList()).MapTo<IEnumerable<IndoorDistributionView>>()
                : new List<IndoorDistributionView>();
        }
    }
}
