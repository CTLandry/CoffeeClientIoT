using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeCommon
{
    public class CoffeeDispensedData
    {
        public double Latitude;
        public double Longitude;
        public CoffeeDescription.CoffeeType CoffeeType;

        public CoffeeDispensedData(CoffeeDescription.CoffeeType coffeeType, double deviceLatitude,
            double deviceLongitude)
        {
            Latitude = deviceLatitude;
            Longitude = deviceLongitude;
            CoffeeType = coffeeType;
        }
    }
}
