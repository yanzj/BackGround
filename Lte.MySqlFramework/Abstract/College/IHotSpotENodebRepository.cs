﻿using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.College
{
    public interface IHotSpotENodebRepository : IRepository<HotSpotENodebId>, ISaveChanges
    {
        
    }
}