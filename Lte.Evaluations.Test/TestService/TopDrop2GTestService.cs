using Lte.MySqlFramework.Abstract;
using Lte.Parameters.MockOperations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;

namespace Lte.Evaluations.TestService
{
    public class TopDrop2GTestService
    {
        private readonly Mock<ITopDrop2GCellRepository> _repository;
        private readonly Mock<IBtsRepository> _btsRepository;
        private readonly Mock<IENodebRepository> _eNodebRepository;

        public TopDrop2GTestService(Mock<ITopDrop2GCellRepository> repository, Mock<IBtsRepository> btsRepository,
            Mock<IENodebRepository> eNodebRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
            _eNodebRepository = eNodebRepository;
        }

        public void ImportOneStat(int btsId, byte sectorId, int drops, int assignmentSuccess)
        {
            _repository.MockQueryItems(new List<TopDrop2GCell>
            {
                new TopDrop2GCell
                {
                    BtsId = btsId,
                    SectorId = sectorId,
                    Drops = drops,
                    TrafficAssignmentSuccess = assignmentSuccess,
                    City = "Foshan",
                    StatTime = DateTime.Parse("2015-1-1")
                }
            }.AsQueryable());
        }
    }
}
