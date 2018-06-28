using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface IEmergencyCommunicationRepository 
        : IRepository<EmergencyCommunication>, 
        IMatchRepository<EmergencyCommunication, EmergencyCommunicationDto>, 
        IDateSpanQuery<EmergencyCommunication>,
        ISaveChanges
    {
    }
}
