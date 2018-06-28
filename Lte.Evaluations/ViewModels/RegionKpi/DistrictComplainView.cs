namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class DistrictComplainView
    {
        public string District { get; set; }

        public int Complain2G { get; set; }

        public int Complain3G { get; set; }

        public int Complain4G { get; set; }

        public int ComplainAll => Complain2G + Complain3G + Complain4G;

        public int Demand2G { get; set; }

        public int Demand3G { get; set; }

        public int Demand4G { get; set; }

        public int DemandAll => Demand2G + Demand3G + Demand4G;

        public int Total => ComplainAll + DemandAll;
    }
}