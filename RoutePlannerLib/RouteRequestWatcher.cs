using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
    {
        Dictionary<string, int> counter;

        public RouteRequestWatcher()
        {
            counter = new Dictionary<string, int>();
        }

        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {

            try 
            {
                counter[e.ToCity]++;
            }
            catch
            {
                counter[e.ToCity] = 1;
            }

            Console.WriteLine("Current Request State");
            Console.WriteLine("---------------------");

            foreach (var pair in counter)
            {
                Console.WriteLine("ToCity: " + pair.Key + " has been requested " + pair.Value + " times");
            }

            Console.WriteLine();

        }

        public int GetCityRequests(string cityName)
        {
            return counter[cityName];
        }


    }
}
