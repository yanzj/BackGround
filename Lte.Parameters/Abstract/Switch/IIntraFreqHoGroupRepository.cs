﻿using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IIntraFreqHoGroupRepository : IRepository<IntraFreqHoGroup, ObjectId>
    {
        IntraFreqHoGroup GetRecent(int eNodebId, int localCellId);
    }
}
