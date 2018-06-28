namespace Lte.Domain.Common.Types
{
    public interface ICellAntenna<TDouble>
    {
        TDouble MTilt { get; set; }

        TDouble ETilt { get; set; }

        TDouble Azimuth { get; set; }

        TDouble Height { get; set; }
    }
}
