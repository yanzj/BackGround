using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IEmergencyProcessRepository
        : IRepository<EmergencyProcess>,
            IMatchRepository<EmergencyProcess, EmergencyProcessDto>,
            ISaveChanges
    {
        List<EmergencyProcess> GetAllList(int emergencyId);
    }
}