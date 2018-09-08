namespace Lte.Domain.Common.Wireless
{
    public interface ICityDistrictTown : IDistrictTown
    {
        string City { get; set; }
    }
}