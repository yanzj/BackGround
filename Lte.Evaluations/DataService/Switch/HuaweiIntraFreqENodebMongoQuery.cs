using Lte.Parameters.Abstract.Switch;
using Lte.Parameters.Entities.Switch;

namespace Lte.Evaluations.DataService.Switch
{
    internal class HuaweiIntraFreqENodebMongoQuery : HuaweiENodebMongoQuery<IntraRatHoComm, ENodebIntraFreqHoView, IIntraRatHoCommRepository>
    {
        public HuaweiIntraFreqENodebMongoQuery(IIntraRatHoCommRepository repository, int eNodebId) : base(repository, eNodebId)
        {
        }
    }
}