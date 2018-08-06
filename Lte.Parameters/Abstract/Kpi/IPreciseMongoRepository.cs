using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IPreciseMongoRepository : IRepository<PreciseMongo, ObjectId>
    {
        List<PreciseMongo> GetAllList(DateTime statDate);
    }
}