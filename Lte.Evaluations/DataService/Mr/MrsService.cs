using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Mr
{
    public class MrsService
    {
        private readonly IMrsPhrRepository _phrRepository;
        private readonly IMrsTadvRsrpRepository _tadvRsrpRepository;
        private readonly IMrsRsrpRepository _rsrpRepository;
        private readonly IMrsSinrUlRepository _sinrUlRepository;
        private readonly IMrsTadvRepository _tadvRepository;
        private readonly IENodebRepository _eNodebRepository;

        public MrsService(IMrsPhrRepository phrRepository, IMrsTadvRsrpRepository tadvRsrpRepository,
            IMrsRsrpRepository rsrpRepository, IMrsSinrUlRepository sinrUlRepository, IMrsTadvRepository tadvRepository,
            IENodebRepository eNodebRepository)
        {
            _phrRepository = phrRepository;
            _tadvRsrpRepository = tadvRsrpRepository;
            _rsrpRepository = rsrpRepository;
            _sinrUlRepository = sinrUlRepository;
            _tadvRepository = tadvRepository;
            _eNodebRepository = eNodebRepository;
        }

        public MrsPhrStat QueryPhrStat(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _phrRepository.Get(eNodebId + "-" + sectorId, statDate);
        }

        public IEnumerable<MrsPhrStat> QueryPhrStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _phrRepository.GetList(eNodebId + "-" + sectorId, begin, end);
        } 

        public MrsTadvRsrpStat QueryTadvRsrpStat(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _tadvRsrpRepository.Get(eNodebId + "-" + sectorId, statDate);
        }

        public IEnumerable<MrsTadvRsrpStat> QueryTadvRsrpStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _tadvRsrpRepository.GetList(eNodebId + "-" + sectorId, begin, end);
        }

        public CellMrsRsrpDto QueryRsrpStat(int eNodebId, byte sectorId, DateTime statDate)
        {
            return CellMrsRsrpDto.ConstructView(_rsrpRepository.Get(eNodebId + "-" + sectorId, statDate),
                _eNodebRepository);
        }
        
        public IEnumerable<CellMrsRsrpDto> QueryRsrpStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            var list = _rsrpRepository.GetList(eNodebId + "-" + sectorId, begin, end).MapTo<List<CellMrsRsrpDto>>();
            list.ForEach(stat => stat.ENodebName = eNodeb?.Name);
            return list;
        }
        
        public IEnumerable<MrsRsrpStat> QueryMrsRsrpStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _rsrpRepository.GetList(eNodebId + "-" + sectorId, begin, end);
        }

        public IEnumerable<MrsRsrpStat> QueryDateSpanMrsRsrpStats(DateTime begin, DateTime end)
        {
            return _rsrpRepository.GetAllList(x => x.StatDate >= begin && x.StatDate < end);
        }

        public CellMrsSinrUlDto QuerySinrUlStat(int eNodebId, byte sectorId, DateTime statDate)
        {
            return CellMrsSinrUlDto.ConstructView(_sinrUlRepository.Get(eNodebId + "-" + sectorId, statDate),
                _eNodebRepository);
        }
        
        public IEnumerable<CellMrsSinrUlDto> QueryMrsSinrUlStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            var list = _sinrUlRepository.GetList(eNodebId + "-" + sectorId, begin, end).MapTo<List<CellMrsSinrUlDto>>();
            list.ForEach(stat => stat.ENodebName = eNodeb?.Name);
            return list;
        }

        public IEnumerable<MrsSinrUlStat> QuerySinrUlStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _sinrUlRepository.GetList(eNodebId + "-" + sectorId, begin, end);
        }
        
        public IEnumerable<MrsSinrUlStat> QueryDateSpanMrsSinrUlStats(DateTime begin, DateTime end)
        {
            return _sinrUlRepository.GetAllList(x => x.StatDate >= begin && x.StatDate < end);
        }

        public MrsTadvStat QueryTadvStat(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _tadvRepository.Get(eNodebId + "-" + sectorId, statDate);
        }

        public IEnumerable<CellMrsTadvDto> QueryMrsTadvStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            var list = _sinrUlRepository.GetList(eNodebId + "-" + sectorId, begin, end).MapTo<List<CellMrsTadvDto>>();
            list.ForEach(stat => stat.ENodebName = eNodeb?.Name);
            return list;
        }

        public IEnumerable<MrsTadvStat> QueryTadvStats(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _tadvRepository.GetList(eNodebId + "-" + sectorId, begin, end);
        }
        
        public IEnumerable<MrsTadvStat> QueryDateSpanMrsTadvStats(DateTime begin, DateTime end)
        {
            return _tadvRepository.GetAllList(x => x.StatDate >= begin && x.StatDate < end);
        }

    }
}
