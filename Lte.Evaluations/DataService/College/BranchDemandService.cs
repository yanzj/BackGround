using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Complain;
using AutoMapper;
using Lte.MySqlFramework.Abstract.Complain;

namespace Lte.Evaluations.DataService.College
{
    public class BranchDemandService : IDateSpanService<BranchDemand>
    {
        private readonly IBranchDemandRepository _repository;

        public BranchDemandService(IBranchDemandRepository repository)
        {
            _repository = repository;
        }

        public List<BranchDemand> QueryItems(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(begin, end);
        }

        public async Task<int> QueryCount(DateTime begin, DateTime end)
        {
            return await _repository.CountAsync(x => x.BeginDate >= begin && x.BeginDate < end);
        }

        public List<BranchDemandDto> QueryList(DateTime begin, DateTime end)
        {
            var items = _repository.GetAllList(begin, end);
            return Mapper.Map<List<BranchDemand>, List<BranchDemandDto>>(items);
        }
    }
}