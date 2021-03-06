﻿using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IInterRatHoCommRepository : IRepository<InterRatHoComm, ObjectId>
    {
        InterRatHoComm GetRecent(int eNodebId);
    }
}
