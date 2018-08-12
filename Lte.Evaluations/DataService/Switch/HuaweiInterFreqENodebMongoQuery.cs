using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
    internal class HuaweiInterFreqENodebMongoQuery : HuaweiENodebMongoQuery<IntraRatHoComm, ENodebInterFreqHoView, IIntraRatHoCommRepository>
    {
        public HuaweiInterFreqENodebMongoQuery(IIntraRatHoCommRepository repository, int eNodebId)
            : base(repository, eNodebId)
        {
        }
    }
}