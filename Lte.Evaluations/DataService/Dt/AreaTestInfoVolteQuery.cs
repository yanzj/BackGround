using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Abstract.Infrastructure;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfoVolteQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordRepository _repository;

        public AreaTestInfoVolteQuery(IFileRecordRepository repository, ITownBoundaryRepository boundaryRepository) : base(boundaryRepository)
        {
            _repository = repository;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data4G = _repository.GetFileRecordVoltes(csvFileName);
            UpdateTownRecords(townIds, fileId, data4G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data4G = _repository.GetFileRecordVoltes(csvFileName);
            UpdateRoadRecords(townIds, fileId, data4G, results);
            return results;
        }
    }
}
