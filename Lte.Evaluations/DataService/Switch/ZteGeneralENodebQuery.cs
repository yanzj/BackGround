using AutoMapper;

namespace Lte.Evaluations.DataService.Switch
{
    public abstract class ZteGeneralENodebQuery<TStat, TView> : IMongoQuery<TView>
        where TView : class
    {
        protected readonly int ENodebId;

        protected ZteGeneralENodebQuery(int eNodebId)
        {
            ENodebId = eNodebId;
        }

        protected abstract TStat QueryStat();

        public TView Query()
        {
            var ztePara = QueryStat();
            return ztePara == null ? null : Mapper.Map<TStat, TView>(ztePara);
        }
    }
}