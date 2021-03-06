﻿using Abp.EntityFramework.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Common.Wireless.Station;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities.Infrastructure;

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
            return _repository.GetAllList(x => x.IsInUse).MapTo<IEnumerable<StationRruView>>();
        }

        public IEnumerable<StationRruView> QueryByStationNum(string stationNum)
        {
            return _repository.GetAllList(x => x.StationNum == stationNum && x.IsInUse)
                .MapTo<IEnumerable<StationRruView>>();
        }

        public IEnumerable<StationRruView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south &&
                             x.Lattitute < north && x.IsInUse)
                    .MapTo<IEnumerable<StationRruView>>();
        }

        public IEnumerable<StationRruView> QueryBySerialNum(string serialNum)
        {
            var items =
                _repository.GetAllList(x => x.CellSerialNum.Contains(serialNum) && x.IsInUse);
            return items.MapTo<IEnumerable<StationRruView>>();
        }

        public StationRruView QueryByRruNum(string rruNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum && x.IsInUse);
            return item?.MapTo<StationRruView>();
        }
        
        public StationRruView QueryByENodebIdAndRackId(int eNodebId, int rackId)
        {
            var items = _repository.GetAllList(x => x.ENodebId == eNodebId && x.IsInUse);
            var item = items.FirstOrDefault(x => x.RackId == rackId);
            return item?.MapTo<StationRruView>();
        }
        
        public bool ResetByENodebIdAndRackId(int eNodebId, int rackId)
        {
            var items = _repository.GetAllList(x => x.ENodebId == eNodebId && x.IsInUse);
            var item = items.FirstOrDefault(x => x.RackId == rackId);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public bool ResetBySerialNumber(string serialNumber)
        {
            var item = _repository.FirstOrDefault(x => x.RruSerialNum == serialNumber && x.IsInUse);
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

        public bool UpdateBasicInfo(string rruNum, string eNodebFactoryDescription, string duplexingDescription,
            string electricSourceDescription, string antiThunder, int transmitPorts, int receivePorts)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum);
            if (item == null) return false;
            item.ENodebFactory = eNodebFactoryDescription.GetEnumType<ENodebFactory>();
            item.Duplexing = duplexingDescription.GetEnumType<Duplexing>();
            item.ElectricSource = electricSourceDescription.GetEnumType<ElectricSource>();
            item.IsAntiThunder = antiThunder == "是";
            item.TransmitPorts = transmitPorts;
            item.ReceivePorts = receivePorts;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateSourceInfo(string rruNum, string eNodebClassDescription, string indoorDistributionSerial,
            string indoorSource)
        {

            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum);
            if (item == null) return false;
            var isIndoorSource = indoorSource == "是";
            if (isIndoorSource && string.IsNullOrWhiteSpace(indoorDistributionSerial)) return false;
            item.ENodebClass = eNodebClassDescription.GetEnumType<ENodebClass>();
            item.IndoorDistributionSerial = indoorDistributionSerial;
            item.IsIndoorSource = isIndoorSource;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdatePositionInfo(string rruNum, double longtitute, double lattitute, string address,
            string position)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum);
            if (item == null) return false;
            item.Longtitute = longtitute;
            item.Lattitute = lattitute;
            item.Address = address;
            item.Position = position;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateShareInfo(string rruNum, string operatorUsageDescription, string shareFunctionDescription,
            string virtualRru)
        {

            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum == rruNum);
            if (item == null) return false;
            item.OperatorUsage = operatorUsageDescription.GetEnumType<OperatorUsage>();
            item.ShareFunction = shareFunctionDescription.GetEnumType<ShareFunction>();
            item.IsVirtualRru = virtualRru == "是";
            _repository.SaveChanges();
            return true;
        }
    }
}
