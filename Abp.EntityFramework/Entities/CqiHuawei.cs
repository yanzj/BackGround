using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(CqiHuaweiCsv))]
    public class CqiHuawei : Entity, ILteCellQuery, IStatTime
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        public int SingleCodeFullBandInPeriodicCqi0Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi1Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi2Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi3Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi4Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi5Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi6Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi7Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi8Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi9Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi10Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi11Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi12Times { get; set; }

        public int SingleCodeFullBandInPeriodicCqi13Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi14Times { get; set; }
        
        public int SingleCodeFullBandInPeriodicCqi15Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi0Times { get; set; }
        
        public int SingleCodeFullBandPeriodicCqi1Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi2Times { get; set; }
        
        public int SingleCodeFullBandPeriodicCqi3Times { get; set; }
        
        public int SingleCodeFullBandPeriodicCqi4Times { get; set; }
        
        public int SingleCodeFullBandPeriodicCqi5Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi6Times { get; set; }
        
        public int SingleCodeFullBandPeriodicCqi7Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi8Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi9Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi10Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi11Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi12Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi13Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi14Times { get; set; }

        public int SingleCodeFullBandPeriodicCqi15Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi0Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi1Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi2Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi3Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi4Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi5Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi6Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi7Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi8Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi9Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi10Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi11Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi12Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi13Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi14Times { get; set; }

        public int DoubleCodeSubCode0InPeriodicCqi15Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi0Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi1Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi2Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi3Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi4Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi5Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi6Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi7Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi8Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi9Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi10Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi11Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi12Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi13Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi14Times { get; set; }

        public int DoubleCodeSubCode0PeriodicCqi15Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi0Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi1Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi2Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi3Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi4Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi5Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi6Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi7Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi8Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi9Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi10Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi11Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi12Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi13Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi14Times { get; set; }

        public int DoubleCodeSubCode1InPeriodicCqi15Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi0Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi1Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi2Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi3Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi4Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi5Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi6Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi7Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi8Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi9Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi10Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi11Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi12Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi13Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi14Times { get; set; }

        public int DoubleCodeSubCode1PeriodicCqi15Times { get; set; }

        public int Cqi0Reports
            =>
                SingleCodeFullBandInPeriodicCqi0Times + SingleCodeFullBandPeriodicCqi0Times +
                DoubleCodeSubCode0InPeriodicCqi0Times + DoubleCodeSubCode0PeriodicCqi0Times +
                DoubleCodeSubCode1InPeriodicCqi0Times + DoubleCodeSubCode1PeriodicCqi0Times;

        public int Cqi1Reports
            =>
                SingleCodeFullBandInPeriodicCqi1Times + SingleCodeFullBandPeriodicCqi1Times +
                DoubleCodeSubCode0InPeriodicCqi1Times + DoubleCodeSubCode0PeriodicCqi1Times +
                DoubleCodeSubCode1InPeriodicCqi1Times + DoubleCodeSubCode1PeriodicCqi1Times;

        public int Cqi2Reports
            =>
                SingleCodeFullBandInPeriodicCqi2Times + SingleCodeFullBandPeriodicCqi2Times +
                DoubleCodeSubCode0InPeriodicCqi2Times + DoubleCodeSubCode0PeriodicCqi2Times +
                DoubleCodeSubCode1InPeriodicCqi2Times + DoubleCodeSubCode1PeriodicCqi2Times;

        public int Cqi3Reports
            =>
                SingleCodeFullBandInPeriodicCqi3Times + SingleCodeFullBandPeriodicCqi3Times +
                DoubleCodeSubCode0InPeriodicCqi3Times + DoubleCodeSubCode0PeriodicCqi3Times +
                DoubleCodeSubCode1InPeriodicCqi3Times + DoubleCodeSubCode1PeriodicCqi3Times;

        public int Cqi4Reports
            =>
                SingleCodeFullBandInPeriodicCqi4Times + SingleCodeFullBandPeriodicCqi4Times +
                DoubleCodeSubCode0InPeriodicCqi4Times + DoubleCodeSubCode0PeriodicCqi4Times +
                DoubleCodeSubCode1InPeriodicCqi4Times + DoubleCodeSubCode1PeriodicCqi4Times;

        public int Cqi5Reports
            =>
                SingleCodeFullBandInPeriodicCqi5Times + SingleCodeFullBandPeriodicCqi5Times +
                DoubleCodeSubCode0InPeriodicCqi5Times + DoubleCodeSubCode0PeriodicCqi5Times +
                DoubleCodeSubCode1InPeriodicCqi5Times + DoubleCodeSubCode1PeriodicCqi5Times;

        public int Cqi6Reports
            =>
                SingleCodeFullBandInPeriodicCqi6Times + SingleCodeFullBandPeriodicCqi6Times +
                DoubleCodeSubCode0InPeriodicCqi6Times + DoubleCodeSubCode0PeriodicCqi6Times +
                DoubleCodeSubCode1InPeriodicCqi6Times + DoubleCodeSubCode1PeriodicCqi6Times;

        public int Cqi7Reports
            =>
                SingleCodeFullBandInPeriodicCqi7Times + SingleCodeFullBandPeriodicCqi7Times +
                DoubleCodeSubCode0InPeriodicCqi7Times + DoubleCodeSubCode0PeriodicCqi7Times +
                DoubleCodeSubCode1InPeriodicCqi7Times + DoubleCodeSubCode1PeriodicCqi7Times;

        public int Cqi8Reports
            =>
                SingleCodeFullBandInPeriodicCqi8Times + SingleCodeFullBandPeriodicCqi8Times +
                DoubleCodeSubCode0InPeriodicCqi8Times + DoubleCodeSubCode0PeriodicCqi8Times +
                DoubleCodeSubCode1InPeriodicCqi8Times + DoubleCodeSubCode1PeriodicCqi8Times;

        public int Cqi9Reports
            =>
                SingleCodeFullBandInPeriodicCqi9Times + SingleCodeFullBandPeriodicCqi9Times +
                DoubleCodeSubCode0InPeriodicCqi9Times + DoubleCodeSubCode0PeriodicCqi9Times +
                DoubleCodeSubCode1InPeriodicCqi9Times + DoubleCodeSubCode1PeriodicCqi9Times;

        public int Cqi10Reports
            =>
                SingleCodeFullBandInPeriodicCqi10Times + SingleCodeFullBandPeriodicCqi10Times +
                DoubleCodeSubCode0InPeriodicCqi10Times + DoubleCodeSubCode0PeriodicCqi10Times +
                DoubleCodeSubCode1InPeriodicCqi10Times + DoubleCodeSubCode1PeriodicCqi10Times;

        public int Cqi11Reports
            =>
                SingleCodeFullBandInPeriodicCqi11Times + SingleCodeFullBandPeriodicCqi11Times +
                DoubleCodeSubCode0InPeriodicCqi11Times + DoubleCodeSubCode0PeriodicCqi11Times +
                DoubleCodeSubCode1InPeriodicCqi11Times + DoubleCodeSubCode1PeriodicCqi11Times;

        public int Cqi12Reports
            =>
                SingleCodeFullBandInPeriodicCqi12Times + SingleCodeFullBandPeriodicCqi12Times +
                DoubleCodeSubCode0InPeriodicCqi12Times + DoubleCodeSubCode0PeriodicCqi12Times +
                DoubleCodeSubCode1InPeriodicCqi12Times + DoubleCodeSubCode1PeriodicCqi12Times;

        public int Cqi13Reports
            =>
                SingleCodeFullBandInPeriodicCqi13Times + SingleCodeFullBandPeriodicCqi13Times +
                DoubleCodeSubCode0InPeriodicCqi13Times + DoubleCodeSubCode0PeriodicCqi13Times +
                DoubleCodeSubCode1InPeriodicCqi13Times + DoubleCodeSubCode1PeriodicCqi13Times;

        public int Cqi14Reports
            =>
                SingleCodeFullBandInPeriodicCqi14Times + SingleCodeFullBandPeriodicCqi14Times +
                DoubleCodeSubCode0InPeriodicCqi14Times + DoubleCodeSubCode0PeriodicCqi14Times +
                DoubleCodeSubCode1InPeriodicCqi14Times + DoubleCodeSubCode1PeriodicCqi14Times;

        public int Cqi15Reports
            =>
                SingleCodeFullBandInPeriodicCqi15Times + SingleCodeFullBandPeriodicCqi15Times +
                DoubleCodeSubCode0InPeriodicCqi15Times + DoubleCodeSubCode0PeriodicCqi15Times +
                DoubleCodeSubCode1InPeriodicCqi15Times + DoubleCodeSubCode1PeriodicCqi15Times;

    }
}