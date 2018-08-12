using System.Collections.Generic;
using Moq;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.MockOperations;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeDistributionTestService
    {
        private readonly Mock<IInfrastructureRepository> _repository;

        public CollegeDistributionTestService(Mock<IInfrastructureRepository> repository)
        {
            _repository = repository;
        }

        public void MockOneLteDistribution(int id)
        {
            var infrastructures = new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-"+id,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.LteIndoor,
                    InfrastructureId = id
                }
            };
            _repository.MockQueryItems(infrastructures.AsQueryable());
        }

        public void MockOneCdmaDistribution(int id)
        {
            var infrastructures = new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-"+id,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.CdmaIndoor,
                    InfrastructureId = id
                }
            };
            _repository.MockQueryItems(infrastructures.AsQueryable());
        }
    }
}
