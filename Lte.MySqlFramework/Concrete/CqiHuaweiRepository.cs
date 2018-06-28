using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class CqiHuaweiRepository : EfRepositorySave<MySqlContext, CqiHuawei>, ICqiHuaweiRepository
    {
        public CqiHuaweiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CqiHuawei Match(CqiHuawei stat)
        {
            return FirstOrDefault(x =>
                x.StatTime == stat.StatTime && x.ENodebId == stat.ENodebId &&
                x.SectorId == stat.SectorId);
        }

        public List<CqiHuawei> FilterTopList(DateTime begin, DateTime end)
        {
            return GetAllList(
                x => x.StatTime >= begin && x.StatTime < end
                     && x.SingleCodeFullBandInPeriodicCqi0Times + x.SingleCodeFullBandPeriodicCqi0Times +
                     x.DoubleCodeSubCode0InPeriodicCqi0Times + x.DoubleCodeSubCode0PeriodicCqi0Times +
                     x.DoubleCodeSubCode1InPeriodicCqi0Times + x.DoubleCodeSubCode1PeriodicCqi0Times
                     + x.SingleCodeFullBandInPeriodicCqi1Times + x.SingleCodeFullBandPeriodicCqi1Times +
                     x.DoubleCodeSubCode0InPeriodicCqi1Times + x.DoubleCodeSubCode0PeriodicCqi1Times +
                     x.DoubleCodeSubCode1InPeriodicCqi1Times + x.DoubleCodeSubCode1PeriodicCqi1Times
                     + x.SingleCodeFullBandInPeriodicCqi2Times + x.SingleCodeFullBandPeriodicCqi2Times +
                     x.DoubleCodeSubCode0InPeriodicCqi2Times + x.DoubleCodeSubCode0PeriodicCqi2Times +
                     x.DoubleCodeSubCode1InPeriodicCqi2Times + x.DoubleCodeSubCode1PeriodicCqi2Times
                     + x.SingleCodeFullBandInPeriodicCqi3Times + x.SingleCodeFullBandPeriodicCqi3Times +
                     x.DoubleCodeSubCode0InPeriodicCqi3Times + x.DoubleCodeSubCode0PeriodicCqi3Times +
                     x.DoubleCodeSubCode1InPeriodicCqi3Times + x.DoubleCodeSubCode1PeriodicCqi3Times
                     + x.SingleCodeFullBandInPeriodicCqi4Times + x.SingleCodeFullBandPeriodicCqi4Times +
                     x.DoubleCodeSubCode0InPeriodicCqi4Times + x.DoubleCodeSubCode0PeriodicCqi4Times +
                     x.DoubleCodeSubCode1InPeriodicCqi4Times + x.DoubleCodeSubCode1PeriodicCqi4Times
                     + x.SingleCodeFullBandInPeriodicCqi5Times + x.SingleCodeFullBandPeriodicCqi5Times +
                     x.DoubleCodeSubCode0InPeriodicCqi5Times + x.DoubleCodeSubCode0PeriodicCqi5Times +
                     x.DoubleCodeSubCode1InPeriodicCqi5Times + x.DoubleCodeSubCode1PeriodicCqi5Times
                     + x.SingleCodeFullBandInPeriodicCqi6Times + x.SingleCodeFullBandPeriodicCqi6Times +
                     x.DoubleCodeSubCode0InPeriodicCqi6Times + x.DoubleCodeSubCode0PeriodicCqi6Times +
                     x.DoubleCodeSubCode1InPeriodicCqi6Times + x.DoubleCodeSubCode1PeriodicCqi6Times
                     > 3000000);
        }
    }
}