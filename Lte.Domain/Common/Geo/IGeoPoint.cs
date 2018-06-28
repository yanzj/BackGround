using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.Common.Geo
{
    public interface IGeoPointReadonly<out T>
    {
        T Longtitute { get; }

        T Lattitute { get; }
    }

    public interface IGeoPoint<T>
    {
        T Longtitute { get; set; }

        T Lattitute { get; set; }
    }
    
    public interface IGeoGridPoint<out T> : IGeoPointReadonly<T>
    {
        int X { get; set; }

        int Y { get; set; }
    }

    public interface IPn
    {
        double? Pn { get; set; }
    }
    
    public static class GeoPointOperations
    {
        public static IEnumerable<TGeoPoint> GetFoshanGeoPoints<TGeoPoint>(this IEnumerable<TGeoPoint> source)
            where TGeoPoint : IGeoPoint<double?>
        {
            return source.Where(
                x =>
                    x.Longtitute != null && x.Lattitute != null && x.Longtitute > 111 && x.Longtitute < 114 &&
                    x.Lattitute > 21 && x.Lattitute < 24);
        }
    }

    public interface ICoverage
    {
        bool IsCoverage();

        bool IsValid();
    }
}
