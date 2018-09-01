using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Station;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace Lte.Evaluations.DataService.Basic
{
    public class StationInfoService
    {
        private readonly IStationDictionaryRepository _repository;
        private readonly IENodebBaseRepository _eNodebBaseRepository;

        public StationInfoService(IStationDictionaryRepository repository,
            IENodebBaseRepository eNodebBaseRepository)
        {
            _repository = repository;
            _eNodebBaseRepository = eNodebBaseRepository;
        }

        public bool UpdateStationPosition(string serialNum, double longtitute, double lattitute, string address)
        {
            var item = _repository.FirstOrDefault(x => x.StationNum == serialNum);
            if (item == null) return false;
            item.Longtitute = longtitute;
            item.Lattitute = lattitute;
            item.Address = address;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateStationBbuBasicInfo(string serialNum, string bbuHeapStation, string cAndLCoExist,
            string rruBufang, string electricFunctionDescription, string electricTypeDescription,
            string batteryTypeDescription)
        {
            var item = _repository.FirstOrDefault(x => x.StationNum == serialNum);
            if (item == null) return false;
            item.IsBbuHeapStation = bbuHeapStation == "是";
            item.IsCAndLCoExist = cAndLCoExist == "是";
            item.IsRruBufang = rruBufang == "是";
            item.ElectricFunction = electricFunctionDescription.GetEnumType<ElectricFunction>();
            item.ElectricType = electricTypeDescription.GetEnumType<ElectricType>();
            item.BatteryType = batteryTypeDescription.GetEnumType<BatteryType>();
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateStationRoomAndTowerInfo(string serialNum, string operatorUsageDescription,
            string stationRoomBelongDescription, string stationTowerBelongDescription,
            string stationSupplyBelongDescription, string towerTypeDescription, int totalPlatforms, double towerHeight)
        {

            var item = _repository.FirstOrDefault(x => x.StationNum == serialNum);
            if (item == null) return false;
            item.OperatorUsage = operatorUsageDescription.GetEnumType<OperatorUsage>();
            item.StationRoomBelong = stationRoomBelongDescription.GetEnumType<Operator>();
            item.StationTowerBelong = stationTowerBelongDescription.GetEnumType<Operator>();
            item.StationSupplyBelong = stationSupplyBelongDescription.GetEnumType<Operator>();
            item.TowerType = towerTypeDescription.GetEnumType<TowerType>();
            item.TotalPlatforms = totalPlatforms;
            item.TowerHeight = towerHeight;
            _repository.SaveChanges();
            return true;
        }
        
        public bool ResetBySerialNumber(string serialNumber)
        {
            var item = _repository.FirstOrDefault(x => x.StationNum == serialNumber && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public IEnumerable<StationDictionaryView> QueryAll()
        {
            return _repository.GetAllList(x => x.IsInUse).MapTo<IEnumerable<StationDictionaryView>>();
        }

        public IEnumerable<StationDictionaryView> QueryByDistrict(string district)
        {
            return
                _repository.GetAllList(x => x.StationDistrict == district && x.IsInUse)
                    .MapTo<IEnumerable<StationDictionaryView>>();
        }

        public IEnumerable<StationDictionaryView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south &&
                             x.Lattitute < north && x.IsInUse)
                    .MapTo<IEnumerable<StationDictionaryView>>();
        }

        public StationDictionaryView QueryOneByStationDistrictAndName(string district, string stationName)
        {
            return _repository.FirstOrDefault(x =>
                    x.ElementName.Contains(stationName) && x.StationDistrict == district && x.IsInUse)
                .MapTo<StationDictionaryView>();
        }
        
        public bool ResetOneByStationDistrictAndName(string district, string stationName)
        {
            var item = _repository.FirstOrDefault(x =>
                x.ElementName.Contains(stationName) && x.StationDistrict == district && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public StationDictionaryView QueryOneByStationName(string stationName)
        {
            return _repository.FirstOrDefault(x => x.ElementName.Contains(stationName) && x.IsInUse)
                .MapTo<StationDictionaryView>();
        }
        
        public bool ResetOneByStationName(string stationName)
        {
            var item = _repository.FirstOrDefault(x => x.ElementName.Contains(stationName) && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public StationDictionaryView QueryOneByENodebId(int eNodebId)
        {
            var eNodeb = _eNodebBaseRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            return eNodeb == null
                ? null
                : _repository.FirstOrDefault(x => x.StationNum == eNodeb.StationNum && x.IsInUse)
                    .MapTo<StationDictionaryView>();
        }
        
        public bool ResetOneByENodebId(int eNodebId)
        {
            var eNodeb = _eNodebBaseRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb ==null) return false;
            var item = _repository.FirstOrDefault(x => x.StationNum == eNodeb.StationNum && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public bool ResetAll()
        {
            var items = _repository.GetAll();
            foreach (var item in items)
            {
                item.IsInUse = false;
            }

            _repository.SaveChanges();
            return true;
        }
    }
}
