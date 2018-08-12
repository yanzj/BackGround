using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class DpiGridKpiService
    {
        private readonly IDpiGridKpiRepository _repository;

        public DpiGridKpiService(IDpiGridKpiRepository repository)
        {
            _repository = repository;
        }
        
        public DpiGridKpiDto QueryKpi(int x, int y)
        {
            return _repository.FirstOrDefault(r => r.X == x && r.Y == y).MapTo<DpiGridKpiDto>();
        }
    }
}