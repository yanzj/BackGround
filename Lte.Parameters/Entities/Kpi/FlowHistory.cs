namespace Lte.Parameters.Entities.Kpi
{
    public class FlowHistory
    {
        public string DateString { get; set; }

        public int HuaweiItems { get; set; }

        public int HuaweiCqis { get; set; }

        public int HuaweiRssis { get; set; }

        public int ZteItems { get; set; }

        public int ZteCqis { get; set; }

        public int ZteRssis { get; set; }

        public int TownStats { get; set; }

        public int TownStats2100 { get; set; }

        public int TownStats1800 { get; set; }

        public int TownStats800VoLte { get; set; }
        
        public int TownRrcs { get; set; }

        public int TownQcis { get; set; }

        public int TownCqis { get; set; }

        public int TownPrbs { get; set; }

        public int TownDoubleFlows { get; set; }
    }
}