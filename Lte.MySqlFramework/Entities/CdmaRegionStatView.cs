using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(CdmaRegionStat))]
    public class CdmaRegionStatView
    {
        public string Region { get; set; }

        public double ErlangIncludingSwitch { get; set; }

        public double Drop2GRate { get; set; }

        public double CallSetupRate { get; set; }

        public double Ecio { get; set; }

        public double Utility2GRate { get; set; }

        public double Flow { get; set; }

        public double Erlang3G { get; set; }

        public double Drop3GRate { get; set; }

        public double ConnectionRate { get; set; }

        public double Ci { get; set; }

        public double LinkBusyRate { get; set; }

        public double DownSwitchRate { get; set; }

        public double Utility3GRate { get; set; }
    }
}