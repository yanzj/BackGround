using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IEmergencyFiberWorkItemRepository
        : IRepository<EmergencyFiberWorkItem>,
            IMatchRepository<EmergencyFiberWorkItem>,
            ISaveChanges
    {
        List<EmergencyFiberWorkItem> GetAllList(int emergencyId);
    }
}