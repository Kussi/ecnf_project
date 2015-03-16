using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            var algorithmName = Properties.Settings.Default.RouteAlgorithm;
            return Create(cities, algorithmName);
        }

        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            IRoutes newIRoutes = null;
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetType(algorithmClassName);

            if (type == null)
            {
                return null;
            }

            newIRoutes = (IRoutes)Activator.CreateInstance(type, cities);
            return newIRoutes as IRoutes;
        }
    } 
}
