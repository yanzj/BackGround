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
    public class StationAntennaService
    {
        private readonly IStationAntennaRepository _repository;

        public StationAntennaService(IStationAntennaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<StationAntennaView> QueryAll()
        {
            return _repository.GetAllList().MapTo<IEnumerable<StationAntennaView>>();
        }

        public IEnumerable<StationAntennaView> QueryByStationNum(string stationNum)
        {
            return _repository.GetAllList(x => x.StationNum == stationNum).MapTo<IEnumerable<StationAntennaView>>();
        }

        public IEnumerable<StationAntennaView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north)
                    .MapTo<IEnumerable<StationAntennaView>>();
        }
    }
}
