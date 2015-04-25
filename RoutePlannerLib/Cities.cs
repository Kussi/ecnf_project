using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.Globalization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> cities;

        public int Count 
        {
            get { return cities.Count; }
        }

        public Cities() { cities = new List<City>(); }

        public City this[int index]
        {
            get
            {
                if (index < 0 || index > this.cities.Count - 1) return null;
                return this.cities[index]; 
            }
            set { this.cities.Insert(index, value); }
        }


        public int ReadCities(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                IEnumerable<City> c = reader.GetSplittedLines('\t').Select(line => new City(line[0].Trim(), 
                                                                            line[1].Trim(),
                                                                            int.Parse(line[2], CultureInfo.InvariantCulture),
                                                                            double.Parse(line[3], CultureInfo.InvariantCulture), 
                                                                            double.Parse(line[4], CultureInfo.InvariantCulture)))
                                                                        .ToList();
                cities.AddRange(c);
                return c.Count();
            }

            /*
            // old version lab 4
            
            int count = 0;
            
            using (TextReader reader = new StreamReader(filename))
            {
                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                foreach (string[] line in citiesAsStrings)
                {
                    cities.Add(new City(line[0].Trim(),
                        line[1].Trim(),
                        int.Parse(line[2], CultureInfo.InvariantCulture),
                        double.Parse(line[3], CultureInfo.InvariantCulture),
                        double.Parse(line[4], CultureInfo.InvariantCulture)));
                    count++;
                }
            }
            return count;
             */
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            var neighbours = this.cities.Where(n => n.Location.Distance(location) <= distance).ToList();
            /*
            //old implementation
            List<City> neighbours = new List<City>();
            
            foreach(City city in this.cities)
            {
               
                if((city.Location).Distance(location) <= distance)
                {
                    neighbours.Add(city);
                }
                
            }*/

            return neighbours;
        }


        public City FindCity(string cityName)
        {
            try
            {
                var res = cities.Where(p => String.Compare(p.Name, cityName, true) == 0).Single();
                if (res != null) return res;
            }
            catch (Exception) { }
            return default(City);

            //return Array.Find(cities.ToArray(), p => String.Compare(p.Name, cityName, true) == 0);
        }

        /*public City FindCity(string cityName)
        {
            return Array.Find(cities.ToArray(), delegate(City city)
            {
                return String.Compare(city.Name, cityName, true) == 0;
            });
        }*/

        /// <summary>
        /// Find all cities between 2 cities 
        /// </summary>
        /// <param name="from">source city</param>
        /// <param name="to">target city</param>
        /// <returns>list of cities</returns>
        public List<City> FindCitiesBetween(string fromCity, string toCity)
        {
            var foundCities = new List<City>();
            var from = FindCity(fromCity);
            var to = FindCity(toCity);
            
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // rename the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(cities.FindAll(c =>
                c.Location.Latitude > minLat && c.Location.Latitude < maxLat
                        && c.Location.Longitude > minLon && c.Location.Longitude < maxLon));

            foundCities.Add(to);
            return foundCities;
        }
    }
}
