using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TextReader reader;

            try
            {
                reader = new StreamReader(filename);
            } 
            catch(FileNotFoundException e)
            {
                return -1;
            }

            string line;


            while((line = reader.ReadLine()) != null)
            {
                line = line.Replace('.', ',');

                string[] splits = line.Split('\t');
                City city = new City(splits[0], splits[1], Convert.ToInt32(splits[2]), Convert.ToDouble(splits[3]), Convert.ToDouble(splits[4]));


                this[this.cities.Count] = city;
                count++;
            }

            return count;
        }

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
    }
}
