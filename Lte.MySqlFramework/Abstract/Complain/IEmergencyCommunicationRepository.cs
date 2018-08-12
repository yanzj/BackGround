using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IEmergencyCommunicationRepository 
        : IRepository<EmergencyCommunication>, 
        IMatchRepository<EmergencyCommunication, EmergencyCommunicationDto>, 
        IDateSpanRepository<EmergencyCommunication>,
        ISaveChanges
    {
    }
}
