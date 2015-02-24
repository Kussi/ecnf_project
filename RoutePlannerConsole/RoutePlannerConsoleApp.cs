﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("Welcome to RoutePlanner (Version " + version + ")");

            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            var bern = new WayPoint("Bern", 46.9546699, 7.39487);
            var tripolis = new WayPoint("Tripolis", 32.8255927, 13.1133419);
            //Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);

            Cities cs = new Cities();
            City c1 = new City("Möhlin", "CH", 10, 10.1, 11.12);

            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(cs.ReadCities("citiesTestDataLab2.txt"));

            Console.WriteLine(cs.Count);


            Console.WriteLine();


            Console.WriteLine(tripolis.Distance(bern));

            Console.ReadLine();
        }
    }
}
