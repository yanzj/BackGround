using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Maintainence;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Maintainence;

namespace Lte.Evaluations.DataService.College
{
    public class CheckingService
    {
        private readonly ICheckingBasicRepository _repository;
        private readonly ICheckingDetailsRepository _detailsRepository;

        public CheckingService(ICheckingBasicRepository repository, ICheckingDetailsRepository detailsRepository)
        {
            _repository = repository;
            _detailsRepository = detailsRepository;
        }

        public IEnumerable<CheckingResultView> QueryResultViews(double west, double east, double south, double north)
        {
            var items = _repository.GetAllList(x =>
                x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north);
            if (!items.Any()) return new List<CheckingResultView>();
            var results = items.MapTo<List<CheckingResultView>>();
            results.ForEach(result =>
            {
                var detailses = _detailsRepository.GetAllList(x => x.CheckingFlowNumber == result.CheckingFlowNumber);
                result.CheckingDetailsViews = detailses.Any()
                    ? detailses.MapTo<IEnumerable<CheckingDetailsView>>()
                    : new List<CheckingDetailsView>();
            });
            return results;
        }
    }
}
