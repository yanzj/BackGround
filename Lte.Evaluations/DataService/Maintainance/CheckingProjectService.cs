using System;
using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.Evaluations.DataService.Maintainance
{
    public class CheckingProjectService
    {
        private readonly ICheckingProjectRepository _repository;

        public CheckingProjectService(ICheckingProjectRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CheckingProjectView> QueryByStationNumber(string stationNumber)
        {
            return _repository.GetAllList(x => x.StationSerialNumber == stationNumber)
                .MapTo<IEnumerable<CheckingProjectView>>();
        }

        public IEnumerable<CheckingProjectView> QueryByBeginDate(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(x => x.BeginDate >= begin && x.BeginDate < end)
                .MapTo<IEnumerable<CheckingProjectView>>();
        }
    }
}
