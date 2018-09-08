namespace Lte.Domain.Common.Wireless
{
    public interface ITown
    {
        string CityName { get; set; }

        string DistrictName { get; set; }

        string TownName { get; set; }
    }
}