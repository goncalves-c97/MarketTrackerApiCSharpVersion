using System;

namespace MarketTracker.Entities
{
    public static class CoordinatesHelper
    {
        public static double GetDistanceBetweenPoints(double latA, double longA, double latB, double longB)
        {
            double latARadians = latA * Math.PI / 180;
            double longARadians = longA * Math.PI / 180;
            double latBRadians = latB * Math.PI / 180;
            double longBRadians = longB * Math.PI / 180;

            double cosLatA = Math.Cos(latARadians);
            double sinLatA = Math.Sin(latARadians);
            double cosLongA = Math.Cos(longARadians);
            double sinLongA = Math.Sin(longARadians);

            double cosLatB = Math.Cos(latBRadians);
            double sinLatB = Math.Sin(latBRadians);
            double cosLongB = Math.Cos(longBRadians);
            double sinLongB = Math.Sin(longBRadians);
            //RETURN(ACOS(COS(@lat1Radianos) * COS(@lng1Radianos) * COS(@lat2Radianos) * COS(@lng2Radianos) + COS(@lat1Radianos) * SIN(@lng1Radianos) * COS(@lat2Radianos) * SIN(@lng2Radianos) + SIN(@lat1Radianos) * SIN(@lat2Radianos)) * 6371) * 1.15
            double acos = Math.Acos(cosLatA * cosLongA * cosLatB * cosLongB + cosLatA * sinLongA * cosLatB * sinLongB + sinLatA * sinLatB);
            double distance = acos * 6371;// * 1.15;

            return distance;

            //double c = 90 - (longB);
            //double b = 90 - (latB);
            //double a = longA - latA;

            //double cos_a = Math.Cos(b) * Math.Cos(c) + Math.Sin(c) * Math.Sin(b) * Math.Cos(a);

            //double arc_cos = Math.Acos(cos_a);

            //double distancia = (40030 * arc_cos) / 360;

            //return distancia;
        }
    }
}
