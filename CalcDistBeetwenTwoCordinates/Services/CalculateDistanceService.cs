using CalcDistBeetwenTwoCordinates.Exceptions;
using CalcDistBeetwenTwoCordinates.Interfaces;
using CalcDistBeetwenTwoCordinates.Modules.Queries;
using System;

namespace CalcDistBeetwenTwoCordinates.Services
{
    public class CalculateDistanceService : ICalculateDistanceService
    {
        private const double RAD = Math.PI / 180;
        private const double RADIUS_EARTH_MILES= 3959;
        private const double ML = 1.60934;


        public double CalculateDistanceByCords(GetDistanceQueryDefinition query)
        {
            if (query is null)
                throw new ApiException("Невалидный запрос", nameof(query));

            var radLat1 = query.FirstMarker.Lat * RAD;
            var radLat2 = query.SecondMarker.Lat * RAD;
            var dLat = (radLat2 - radLat1);
            var dLon = (query.SecondMarker.Lon - query.FirstMarker.Lon) * RAD;
            var a = (dLon) * Math.Cos((radLat1 + radLat2) / 2);
            var centralAngle = Math.Sqrt(a * a + dLat * dLat);

            return Math.Round(RADIUS_EARTH_MILES * centralAngle * ML, 1);
        }
    }
}
