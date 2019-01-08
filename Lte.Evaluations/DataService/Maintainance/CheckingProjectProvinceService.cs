using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.Evaluations.DataService.Maintainance
{
    public class CheckingProjectProvinceService
    {
        private readonly ICheckingProjectProvinceRepository _repository;

        public CheckingProjectProvinceService(ICheckingProjectProvinceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CheckingProjectProvinceView> QueryByContents(string keyword)
        {
            return _repository.GetAllList(x => x.ProjectName.Contains(keyword))
                .MapTo<IEnumerable<CheckingProjectProvinceView>>();
        }

        public IEnumerable<CheckingProjectProvinceView> QueryByBeginDate(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(x => x.BeginDate >= begin && x.BeginDate < end)
                .MapTo<IEnumerable<CheckingProjectProvinceView>>();
        }
    }
}
