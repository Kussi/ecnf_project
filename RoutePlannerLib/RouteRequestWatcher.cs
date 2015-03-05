using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
    {
        Dictionary<string, int> cityCounter;

        public RouteRequestWatcher()
        {
            cityCounter = new Dictionary<string, int>();
        }

        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {

            if (cityCounter.ContainsKey(e.ToCity))
            {
                cityCounter[e.ToCity] += 1;
            }
            else
            {
                cityCounter.Add(e.ToCity, 1);
            }

            Console.WriteLine("Current Request State");
            Console.WriteLine("---------------------");

            foreach (var pair in cityCounter)
            {
                Console.WriteLine("ToCity: {0} has been requested {1} times", pair.Key, pair.Value);
            }

            Console.WriteLine();

        }

        public int GetCityRequests(string cityName)
        {
            if (cityCounter.ContainsKey(cityName))
            {
                return cityCounter[cityName];
            }
            return 0;
        }


    }
}
