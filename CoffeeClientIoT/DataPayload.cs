using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeClientIoT
{
    public class DataPayload
    {
        public static int CupsOfCoffeeDispensed { get; set; }
        public string CoffeeTypeDispensed { get; set; }

        public DataPayload(string coffeetype)
        {
            CupsOfCoffeeDispensed++;
            CoffeeTypeDispensed = coffeetype;
        }
    }
}
