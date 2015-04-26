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
using System.Runtime.InteropServices;
using System.Runtime;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {

        private static Stopwatch stopWatch = new Stopwatch();
        private static int counter = 0;

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
            Console.WriteLine("Garbage Collection test");
            Console.WriteLine("=========================================");
            Console.WriteLine("Cleaning times with gcConcurrent enabled or disabled");
            Console.WriteLine("(has to be modified in the App.config manually)");
            for (int i = 0; i < 10; ++i)
            {
                createGarbage(1);
                removeGarbage();
            }


            // Erläuterung:
            // Mittels der Methode createGarbage erzeugen wir Daten, welche wir anschliessend
            // mit der removeGarbage-Methode wieder null setzen und mittels GC aufräumen. Die
            // Dauer, welche das Aufräumen in Anspruch nimmt, messen wir mit einer Stopwatch.
            // Dabei haben wir zwei Arten von Garbage Collectoren getestet: Concurrent
            // Collection enabled und disabled. (Muss im App.Config manuell angepasst werden)

            // Ergebnis
            //          enabled = true      enabled = false
            // 1.       5.04s               5.91s
            // 2.       4.99s               5.87s
            // 3.       5.17s               5.92s
            // 4.       5.42s               6.49s
            // 5.       4.82s               5.53s
            // 6.       4.98s               6.08s
            // 7.       5.27s               5.80s
            // 8.       5.01s               5.86s
            // 9.       5.11s               5.98s
            //10.       5.16s               6.25s
            // ---------------------------------------------
            // Schnitt: 5.097s              5.969s
            // =============================================

            // Damit ist in unserem Fall die Concurrent Collection (enabled) um ca 17% schneller

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

        // generiert 100 * <numberOfHundredMBs> MegaByte Speicher in Form eines int[][]
        public static int[][] createGarbage(int numberOfHundredMBs)
        {
            int[][] buf = new int[25 * 1024 * 1024 / 2][]; // 32B (int) * 25 * 1024 * 1024 = 100MB
            for (int i = 0; i < buf.Length; ++i)
            {
                buf[i] = new int[numberOfHundredMBs * 2]; // *2 und /2 für die bessere Verteilung
                for (int j = 0; j < buf[i].Length; ++j)
                {
                    buf[i][j] = (int)i;
                }
            }
            return buf;
        }

        public static void removeGarbage() 
        {
            stopWatch.Start();

            int[][] array = createGarbage(5);
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = null;
            }
            array = null;
            GC.Collect();

            stopWatch.Stop();
           
            Console.WriteLine("{0}. {1}", ++counter, FormatTime(stopWatch));
            stopWatch.Reset();
        }
    }
}
