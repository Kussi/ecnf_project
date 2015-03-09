using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

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
            int count = 0;
            using (TextReader reader = new StreamReader(filename))
            {
                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                foreach(string[] line in citiesAsStrings)
                {
                    cities.Add(new City(line[0].Trim(), 
                        line[1].Trim(), 
                        int.Parse(line[2]),
                        double.Parse(line[3].Replace('.', ',')),
                        double.Parse(line[4].Replace('.', ','))));
                    count++;
                }
            }
            return count;
        }

        /*

        public int ReadCities(string filename)
        {
            int count = 0;
            TextReader reader;

            try
            {
                reader = new StreamReader(filename);
            } 
            catch(FileNotFoundException)
            {
                throw;
            }

            string line;


            while((line = reader.ReadLine()) != null)
            {
                line = line.Replace('.', ',');

                string[] splits = line.Split('\t');
                City city = new City(   splits[0], 
                                        splits[1], 
                                        Convert.ToInt32(splits[2]), 
                                        Convert.ToDouble(splits[3]), 
                                        Convert.ToDouble(splits[4]));


                this[this.cities.Count] = city;
                count++;
            }

            return count;
        }*/

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neighbours = new List<City>();

            foreach(City city in this.cities)
            {
               
                if((city.Location).Distance(location) <= distance)
                {
                    neighbours.Add(city);
                }
                
            }

            return neighbours;
        }


        public City FindCity(string cityName)
        {
            return Array.Find(cities.ToArray(), delegate(City city)
            {
                return String.Compare(city.Name, cityName, true) == 0;
            });
        }

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
