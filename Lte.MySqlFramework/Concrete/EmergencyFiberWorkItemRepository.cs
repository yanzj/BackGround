using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class EmergencyFiberWorkItemRepository : EfRepositorySave<MySqlContext, EmergencyFiberWorkItem>,
        IEmergencyFiberWorkItemRepository
    {
        public EmergencyFiberWorkItemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public EmergencyFiberWorkItem Match(EmergencyFiberWorkItem stat)
        {
            return FirstOrDefault(x => x.EmergencyId == stat.EmergencyId && x.WorkItemNumber == stat.WorkItemNumber);
        }
        
        public List<EmergencyFiberWorkItem> GetAllList(int emergencyId)
        {
            return GetAllList(x => x.EmergencyId == emergencyId);
        }
    }
}