using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Mr
{
    public class MonthKpiService
    {
        private readonly IMonthKpiRepository _repository;

        public MonthKpiService(IMonthKpiRepository repository) 
        {
            _repository = repository;
        }

        public IEnumerable<MonthKpiStat> QueryLastMonthKpiStats()
        {
            var result = new List<MonthKpiStat>();

            var end = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            while (!result.Any() && end > DateTime.Today.AddYears(-1))
            {
                var begin = end.AddMonths(-1);
                result = _repository.GetAllList(x => x.StatDate >= begin && x.StatDate < end);
                end = end.AddMonths(-1);
            }

            return result;
        }

        private Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<T>>>> QueryMonthTrend<T>(Func<MonthKpiStat, T> func)
        {
            var end = DateTime.Today;
            var begin = DateTime.Today.AddYears(-1);
            var result = _repository.GetAllList(x => x.StatDate >= begin && x.StatDate < end);
            var category = result.Select(x => x.StatDate).Distinct().Select(x => x.ToString("yyyy-MM"));
            return new Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<T>>>>(category,
                result.GroupBy(x => x.District).Select(x => new Tuple<string, IEnumerable<T>>(x.Key, x.Select(func))));
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QureyMonthDropTrend()
        {
            return QueryMonthTrend(x => x.Drop2GRate);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QureyMonthFlow4GTo3GTrend()
        {
            return QueryMonthTrend(x => x.FlowRate4GTo3G);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QureyMonthPreciseTrend()
        {
            return QueryMonthTrend(x => x.PreciseRate);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<int>>>> QueryTotalComplainsTrend()
        {
            return QueryMonthTrend(x => x.TotalComplains);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<int>>>> QueryYuejiComplainsTrend()
        {
            return QueryMonthTrend(x => x.YuejiComplains);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<int>>>> QueryGongxinComplainsTrend()
        {
            return QueryMonthTrend(x => x.GongxinYuejiComplains);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QueryAverageAlarmsTrend()
        {
            return QueryMonthTrend(x => x.AverageAlarms);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QueryAlarmWorkTrend()
        {
            return QueryMonthTrend(x => x.AlarmWorkItemRate);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QueryAlarmProcessTrend()
        {
            return QueryMonthTrend(x => x.AlarmProcessRate);
        }

        public Tuple<IEnumerable<string>, IEnumerable<Tuple<string, IEnumerable<double>>>> QueryMaintainProjectTrend()
        {
            return QueryMonthTrend(x => x.MaintainProjectRate);
        }
    }
}