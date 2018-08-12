using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.College
{
    public class VipDemandService : IDateSpanService<VipDemand>
    {
        private readonly IVipDemandRepository _repository;
        private readonly ITownRepository _townRepository;
        private readonly IVipProcessRepository _processRepository;

        public VipDemandService(IVipDemandRepository repository, ITownRepository townRepository,
            IVipProcessRepository processRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
            _processRepository = processRepository;
        }
        
        public List<VipDemandDto> Query(DateTime begin, DateTime end)
        {
            return _repository.Query<IVipDemandRepository, VipDemand, VipDemandDto>(_townRepository, begin, end);
        }

        public List<VipDemandDto> Query(string phoneNumber)
        {
            var towns = _townRepository.GetAllList();
            return _repository.Query<IVipDemandRepository, VipDemand, VipDemandDto>(
                towns, _repository.GetAllList(x => x.PhoneNumber == phoneNumber));
        }

        public List<VipDemandDto> Query(string district, string town, DateTime begin, DateTime end)
        {
            return _repository.Query<IVipDemandRepository, VipDemand, VipDemandDto>(_townRepository, district, town,
                begin, end);
        }

        public async Task<int> UpdateAsync(VipDemandDto dto)
        {
            dto.TownId =
                _townRepository.QueryTown(dto.District, dto.Town)?.Id ?? 1;
            return await _repository.UpdateOne<IVipDemandRepository, VipDemand, VipDemandDto>(dto);
        }

        public async Task<int> UpdateAsync(VipProcessDto dto)
        {
            return await _processRepository.UpdateOne<IVipProcessRepository, VipProcess, VipProcessDto>(dto);
        }
        
        public VipDemandDto QuerySingle(string serialNumber)
        {
            var result =
                Mapper.Map<VipDemand, VipDemandDto>(_repository.FirstOrDefault(x => x.SerialNumber == serialNumber));
            var town = _townRepository.Get(result.TownId);
            if (town != null)
            {
                result.District = town.DistrictName;
                result.Town = town.TownName;
            }
            return result;
        }
        
        public List<VipDemand> QueryItems(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(begin, end);
        }

        public async Task<int> QueryCount(DateTime begin, DateTime end)
        {
            return await _repository.CountAsync(x => x.BeginDate >= begin && x.BeginDate < end);
        }

        public IEnumerable<VipProcessDto> QueryProcess(string serialNumber)
        {
            var items = _processRepository.GetAllList(serialNumber);
            return Mapper.Map<List<VipProcess>, IEnumerable<VipProcessDto>>(items);
        } 
    }
}