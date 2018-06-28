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
    public class StationRruService
    {
        private readonly IStationRruRepository _repository;

        public StationRruService(IStationRruRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<StationRruView> QueryAll()
        {
            return _repository.GetAllList().MapTo<IEnumerable<StationRruView>>();
        }

        public IEnumerable<StationRruView> QueryByStationNum(string stationNum)
        {
            return _repository.GetAllList(x => x.StationNum == stationNum).MapTo<IEnumerable<StationRruView>>();
        }

        public IEnumerable<StationRruView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north)
                    .MapTo<IEnumerable<StationRruView>>();
        }

        public StationRruView QueryBySerialNum(string serialNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.CellSerialNum == serialNum);
            return item == null ? null : item .MapTo<StationRruView>();
        }

        public StationRruView QueryByRruNum(string rruNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum);
            return item == null ? null : item.MapTo<StationRruView>();
        }
    }
}
