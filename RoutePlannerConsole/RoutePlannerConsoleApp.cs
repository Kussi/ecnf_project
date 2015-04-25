using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            // version
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("Welcome to RoutePlanner (Version " + version + ")");
            Console.WriteLine("=========================================");
            Console.WriteLine();


            // testcode
            //Console.WriteLine("diverser Testcode");
            //Console.WriteLine("=========================================");
            //var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            //var bern = new WayPoint("Bern", 46.9546699, 7.39487);
            //var tripolis = new WayPoint("Tripolis", 32.8255927, 13.1133419);

            //Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);

            //Console.WriteLine();
            //Console.WriteLine("distance between tripolis and bern:");
            //Console.WriteLine(tripolis.Distance(bern));

            //Console.WriteLine();
            //Console.WriteLine(wayPoint.ToString());
            //Console.WriteLine("----------------------------------------");
            //Console.WriteLine();

            Console.WriteLine("Logger-Test");
            Console.WriteLine("=========================================");
            Cities cs = new Cities();
            Routes rs = new Routes(cs);
            cs.ReadCities("citiesTestDataLab2.txt");
            rs.ReadRoutes("linksTestDataLab3.txt");
            cs.ReadCities("exception");
            
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();


            // Garbage Collection
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            GC1();
            stopWatch.Stop();
            Console.WriteLine("RunTime of GC1: " + FormatTime(stopWatch));
            stopWatch.Reset();

            stopWatch.Start();
            GC2();
            stopWatch.Stop();
            Console.WriteLine("RunTime of GC2 " + FormatTime(stopWatch));

            Console.ReadLine();
        }

        public static string FormatTime(Stopwatch watch)
        {
            TimeSpan ts = watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            return elapsedTime;
        }

        public static void GC1() 
        {

            GC.Collect();
        }

        public static void GC2()
        {

            //StreamReader sr = new StreamReader("bla");
            //sr.Dispose();
        }
    }
}
