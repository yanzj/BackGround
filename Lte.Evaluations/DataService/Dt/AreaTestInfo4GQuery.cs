using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Support;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestInfo4GQuery : AreaTestInfoQuery
    {
        private readonly IFileRecordRepository _repository;

        public AreaTestInfo4GQuery(IFileRecordRepository repository, ITownBoundaryRepository boundaryRepository) : base(boundaryRepository)
        {
            _repository = repository;
        }

        public override List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data4G = _repository.GetFileRecord4Gs(csvFileName);
            UpdateTownRecords(townIds, fileId, data4G, results);
            return results;
        }

        public override List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId)
        {
            var results = new List<AreaTestInfo>();
            var data4G = _repository.GetFileRecord4Gs(csvFileName);
            UpdateRoadRecords(townIds, fileId, data4G, results);
            return results;
        }
    }
}