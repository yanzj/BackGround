using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfo2GQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordRepository _repository;

        public AreaTestInfo2GQuery(IFileRecordRepository repository, ITownBoundaryRepository boundaryRepository)
            : base(boundaryRepository)
        {
            _repository = repository;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data2G = _repository.GetFileRecord2Gs(csvFileName);
            UpdateTownRecords(townIds, fileId, data2G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data2G = _repository.GetFileRecord2Gs(csvFileName);
            UpdateRoadRecords(townIds, fileId, data2G, results);
            return results;
        }
    }
}