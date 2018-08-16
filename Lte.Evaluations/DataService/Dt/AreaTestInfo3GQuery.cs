using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Support;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfo3GQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordRepository _repository;

        public AreaTestInfo3GQuery(IFileRecordRepository repository, ITownBoundaryRepository boundaryRepository) : base(boundaryRepository)
        {
            _repository = repository;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data3G = _repository.GetFileRecord3Gs(csvFileName);
            UpdateTownRecords(townIds, fileId, data3G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data3G = _repository.GetFileRecord3Gs(csvFileName);
            UpdateRoadRecords(townIds, fileId, data3G, results);
            return results;
        }
    }
}