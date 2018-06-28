using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Basic
{
    public class StationInfoService
    {
        private readonly IStationDictionaryRepository _repository;

        public StationInfoService(IStationDictionaryRepository repository)
        {
            _repository = repository;
        }

        public bool UpdateStationPosition(string serialNum, double longtitute, double lattitute, string address)
        {
            var item = _repository.FirstOrDefault(x => x.StationNum == serialNum);
            if (item == null) return false;
            item.Longtitute = longtitute;
            item.Lattitute = lattitute;
            item.Address = address;
            return true;
        }

        public IEnumerable<StationDictionaryView> QueryAll()
        {
            return _repository.GetAllList().MapTo<IEnumerable<StationDictionaryView>>();
        }

        public IEnumerable<StationDictionaryView> QueryByDistrict(string district)
        {
            return
                _repository.GetAllList(x => x.StationDistrict == district).MapTo<IEnumerable<StationDictionaryView>>();
        }

        public IEnumerable<StationDictionaryView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north)
                    .MapTo<IEnumerable<StationDictionaryView>>();
        }

        public StationDictionaryView QueryOneByStationName(string stationName)
        {
            return _repository.FirstOrDefault(x => x.ElementName.Contains(stationName)).MapTo<StationDictionaryView>();
        }
    }
}
