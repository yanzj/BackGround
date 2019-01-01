using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Station;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeCdmaDistributionService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly IIndoorDistributionRepository _indoorRepository;

        public CollegeCdmaDistributionService(IInfrastructureRepository repository,
            IIndoorDistributionRepository indoorRepository)
        {
            _repository = repository;
            _indoorRepository = indoorRepository;
        }

        public IEnumerable<IndoorDistribution> Query(string collegeName)
        {
            var ids = _repository.GetCollegeInfrastructureIds(collegeName, InfrastructureType.CdmaIndoor);
            var distributions = ids.Select(_indoorRepository.Get).Where(distribution => distribution != null).ToList();
            return distributions;
        }
    }
}