using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class EmergencyProcessRepository : EfRepositorySave<MySqlContext, EmergencyProcess>,
        IEmergencyProcessRepository
    {
        public EmergencyProcessRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public EmergencyProcess Match(EmergencyProcessDto stat)
        {
            var state = stat.ProcessStateDescription.GetEnumType<EmergencyState>();
            return FirstOrDefault(x => x.EmergencyId == stat.EmergencyId && x.ProcessState == state);
        }
        
        public List<EmergencyProcess> GetAllList(int emergencyId)
        {
            return GetAllList(x => x.EmergencyId == emergencyId);
        }
    }
}