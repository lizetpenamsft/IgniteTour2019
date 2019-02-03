using CoordinateSharp;

namespace Ignite.API.Common.Coordintes
{
    public static class CoordinateExtensions
    {
        public static string LonLatToMGRS(double longitude, double latitude)
        {
            var coordinate = new Coordinate(latitude, longitude);
            var mgrs = coordinate.MGRS.ToString();
            return mgrs;
        }
    }
}