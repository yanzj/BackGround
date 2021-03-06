﻿using Abp.EntityFramework.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities.Maintainence;

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
            return _repository.GetAllList(x => x.IsInUse).MapTo<IEnumerable<IndoorDistributionView>>();
        }

        public IEnumerable<IndoorDistributionView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south &&
                             x.Lattitute < north && x.IsInUse)
                    .MapTo<IEnumerable<IndoorDistributionView>>();
        }

        public IndoorDistributionView QueryByRruNum(string rruNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum.Contains(rruNum) && x.IsInUse);
            return item?.MapTo<IndoorDistributionView>();
        }
        
        public bool ResetByRruNum(string rruNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.RruSerialNum.Contains(rruNum) && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public IndoorDistributionView QueryByCellNum(string cellNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.CellSerialNum.Contains(cellNum) && x.IsInUse);
            return item?.MapTo<IndoorDistributionView>();
        }
        
        public bool ResetByCellNum(string cellNum)
        {
            var item =
                _repository.FirstOrDefault(x => x.CellSerialNum.Contains(cellNum) && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public IEnumerable<IndoorDistributionView> QueryByStationNum(string stationNum)
        {
            var eNodebs = _eNodebBaseRepository.GetAllList(x => x.StationNum == stationNum);
            if (!eNodebs.Any()) return new List<IndoorDistributionView>();
            var cells = eNodebs.Select(x =>
                    _cellRepository.GetAllList(c => c.ENodebId == x.ENodebId && c.IndoorDistributionSerial != null))
                .Aggregate((a, b) => a.Concat(b).ToList());
            return cells.Any()
                ? cells.Select(c => _repository.GetAllList(x => x.CellSerialNum == c.CellSerialNum && x.IsInUse))
                    .Aggregate((a, b) => a.Concat(b).ToList()).MapTo<IEnumerable<IndoorDistributionView>>()
                : new List<IndoorDistributionView>();
        }
        
        public bool ResetBySerialNumber(string serialNumber)
        {
            var item = _repository.FirstOrDefault(x => x.IndoorSerialNum == serialNumber && x.IsInUse);
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

        public bool UpdatePositionInfo(string serialNumber, string address, double longtitute, double lattitute,
            string indoorCategoryDescription, string checkingAddress)
        {
            var item = _repository.FirstOrDefault(x => x.IndoorSerialNum == serialNumber);
            if (item == null) return false;
            item.Address = address;
            item.Longtitute = longtitute;
            item.Lattitute = lattitute;
            item.IndoorCategory = indoorCategoryDescription.GetEnumType<IndoorCategory>();
            item.CheckingAddress = checkingAddress;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateSystemInfo(string serialNumber, string indoorNetworkDescription, string hasCombiner,
            string combinerFunctionDescription, string oldCombinerDescription, string combinedWithOtherOperator,
            string distributionChannelDescription)
        {
            var item = _repository.FirstOrDefault(x => x.IndoorSerialNum == serialNumber);
            if (item == null) return false;
            item.IndoorNetwork = indoorNetworkDescription.GetEnumType<IndoorNetwork>();
            item.IsHasCombiner = hasCombiner == "是";
            item.CombinerFunction = combinerFunctionDescription.GetEnumType<CombinerFunction>();
            item.OldCombiner = oldCombinerDescription.GetEnumType<OldCombiner>();
            item.IsCombinedWithOtherOperator= combinedWithOtherOperator == "是";
            item.DistributionChannel = distributionChannelDescription.GetEnumType<DistributionChannel>();
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateBuildingInfo(string serialNumber, string buildingName, string buildingAddress,
            int totalFloors, string hasUnderGroundParker, string maintainor)
        {
            var item = _repository.FirstOrDefault(x => x.IndoorSerialNum == serialNumber);
            if (item == null) return false;
            item.BuildingName = buildingName;
            item.BuildingAddress = buildingAddress;
            item.TotalFloors = totalFloors;
            item.IsHasUnderGroundParker= hasUnderGroundParker == "是";
            item.Maintainor = maintainor;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateCoverageInfo(string serialNumber, string evenOddFloorCoverage, string lteFullCoverage,
            string liftLteFullCoverage, string undergroundFullCoverage)
        {
            var item = _repository.FirstOrDefault(x => x.IndoorSerialNum == serialNumber);
            if (item == null) return false;
            item.IsEvenOddFloorCoverage= evenOddFloorCoverage == "是";
            item.IsLteFullCoverage= lteFullCoverage == "是";
            item.IsLiftLteFullCoverage= liftLteFullCoverage == "是";
            item.IsUndergroundFullCoverage= undergroundFullCoverage == "是";
            _repository.SaveChanges();
            return true;
        }
    }
}
