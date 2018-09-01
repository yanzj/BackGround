using Abp.EntityFramework.AutoMapper;
using System.Collections.Generic;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Region;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities.Infrastructure;

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
            return _repository.GetAllList(x => x.IsInUse).MapTo<IEnumerable<StationAntennaView>>();
        }

        public IEnumerable<StationAntennaView> QueryByStationNum(string stationNum)
        {
            return _repository.GetAllList(x => x.StationNum == stationNum && x.IsInUse)
                .MapTo<IEnumerable<StationAntennaView>>();
        }

        public IEnumerable<StationAntennaView> QueryRange(double west, double east, double south, double north)
        {
            return
                _repository.GetAllList(
                        x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south &&
                             x.Lattitute < north && x.IsInUse)
                    .MapTo<IEnumerable<StationAntennaView>>();
        }

        public bool ResetBySerialNumber(string serialNumber)
        {
            var item = _repository.FirstOrDefault(x => x.AntennaNum == serialNumber && x.IsInUse);
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

        public bool UpdateNumericInfo(string serialNumber, double azimuth, double eTilt, double mTilt,
            byte antennaPorts, string antennaGain, double height)
        {
            var item = _repository.FirstOrDefault(x => x.AntennaNum == serialNumber);
            if (item == null) return false;
            item.Azimuth = azimuth;
            item.ETilt = eTilt;
            item.MTilt = mTilt;
            item.AntennaPorts = antennaPorts;
            item.AntennaGain = antennaGain;
            item.Height = height;
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateBasicInfo(string serialNumber, string commonAntennaWithCdma, string antennaFactoryDescription,
            string antennaModel, string integratedWithRru, string antennaBand, string antennaDirectionDescription,
            string antennaPolarDescription, string electricAdjustable)
        {
            var item = _repository.FirstOrDefault(x => x.AntennaNum == serialNumber);
            if (item == null) return false;
            item.IsCommonAntennaWithCdma = commonAntennaWithCdma == "是";
            item.AntennaFactory = antennaFactoryDescription.GetEnumType<AntennaFactory>();
            item.AntennaModel = antennaModel;
            item.IsIntegratedWithRru= integratedWithRru == "是";
            item.AntennaBand = antennaBand;
            item.AntennaDirection = antennaDirectionDescription.GetEnumType<AntennaDirection>();
            item.AntennaPolar = antennaPolarDescription.GetEnumType<AntennaPolar>();
            item.IsElectricAdjustable= electricAdjustable == "是";
            _repository.SaveChanges();
            return true;
        }

        public bool UpdatePositionInfo(string serialNumber, double longtitute, double lattitute, string antennaAddress,
            string antennaBeautyDescription, string hasTowerAmplifier)
        {
            var item = _repository.FirstOrDefault(x => x.AntennaNum == serialNumber);
            if (item == null) return false;
            item.Longtitute = longtitute;
            item.Lattitute = lattitute;
            item.AntennaAddress = antennaAddress;
            item.AntennaBeauty = antennaBeautyDescription.GetEnumType<AntennaBeauty>();
            item.IsHasTowerAmplifier= hasTowerAmplifier == "是";
            _repository.SaveChanges();
            return true;
        }

        public bool UpdateCoverageInfo(string serialNumber, string coverageAreaDescription,
            string coverageRoadDescription,
            string coverageHotspotDescription, string boundaryTypeDescription)
        {
            var item = _repository.FirstOrDefault(x => x.AntennaNum == serialNumber);
            if (item == null) return false;
            item.CoverageArea = coverageAreaDescription.GetEnumType<CoverageArea>();
            item.CoverageRoad = coverageRoadDescription.GetEnumType<CoverageRoad>();
            item.CoverageHotspot = coverageHotspotDescription.GetEnumType<CoverageHotspot>();
            item.BoundaryType = boundaryTypeDescription.GetEnumType<BoundaryType>();
            _repository.SaveChanges();
            return true;
        }
    }
}
