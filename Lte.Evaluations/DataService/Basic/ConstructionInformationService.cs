﻿using Abp.EntityFramework.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.MySqlFramework.Abstract.Station;
using Lte.MySqlFramework.Entities.Infrastructure;

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
            return _repository.GetAllList(x => x.IsInUse).MapTo<IEnumerable<ConstructionView>>();
        }

        public IEnumerable<ConstructionView> QueryByStationNum(string stationNum)
        {
            var eNodebs = _baseRepository.GetAllList(x => x.StationNum == stationNum && x.IsInUse);
            return eNodebs.Any() ? eNodebs.Select(x => _repository.GetAllList(c => c.ENodebId == x.ENodebId))
                .Aggregate((a, b) => a.Concat(b).ToList()).MapTo<IEnumerable<ConstructionView>>()
                : new List<ConstructionView>();
        }

        public IEnumerable<ConstructionView> QueryByAntennaNum(string antennaNum)
        {
            return _repository.GetAllList(x => x.AntennaSerial == antennaNum && x.IsInUse)
                .MapTo<IEnumerable<ConstructionView>>();
        }

        public ConstructionView QueryByCellName(string cellName)
        {
            var item = _repository.FirstOrDefault(x => x.CellName == cellName && x.IsInUse);
            return item == null ? null : item.MapTo<ConstructionView>();
        }
        
        public bool ResetByCellName(string cellName)
        {
            var item = _repository.FirstOrDefault(x => x.CellName == cellName && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }

        public ConstructionView QueryByCellNum(string cellNum)
        {
            var item = _repository.FirstOrDefault(x => x.CellSerialNum == cellNum && x.IsInUse);
            return item == null ? null : item.MapTo<ConstructionView>();
        }
        
        public bool ResetByCellNum(string cellNum)
        {
            var item = _repository.FirstOrDefault(x => x.CellSerialNum == cellNum && x.IsInUse);
            if (item == null) return false;
            item.IsInUse = false;
            _repository.SaveChanges();
            return true;
        }
        
        public bool ResetBySerialNumber(string serialNumber)
        {
            var item = _repository.FirstOrDefault(x => x.CellSerialNum == serialNumber && x.IsInUse);
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

        public bool Update(string serialNumber, string eNodebFactoryDescription, string duplexingDescription,
            string cellCoverageDescription, string remoteTypeDescription, string antennaTypeDescription,
            string coAntennaWithOtherCells)
        {
            var item = _repository.FirstOrDefault(x => x.CellSerialNum == serialNumber);
            if (item == null) return false;
            item.ENodebFactory = eNodebFactoryDescription.GetEnumType<ENodebFactory>();
            item.Duplexing = duplexingDescription.GetEnumType<Duplexing>();
            item.CellCoverage = cellCoverageDescription.GetEnumType<CellCoverage>();
            item.RemoteType = remoteTypeDescription.GetEnumType<RemoteType>();
            item.AntennaType = antennaTypeDescription.GetEnumType<AntennaType>();
            item.IsCoAntennaWithOtherCells = coAntennaWithOtherCells == "是";
            item.IsInUse = true;
            _repository.SaveChanges();
            return true;
        }
    }
}
