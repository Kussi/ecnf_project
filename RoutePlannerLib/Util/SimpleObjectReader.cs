using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        private StringReader stream;

        public SimpleObjectReader(StringReader stream)
        {
            this.stream = stream;
        }

        public object Next()
        {
            var ass = Assembly.GetExecutingAssembly();
            string line = stream.ReadLine();
            if (line == null)
                return null;

            var obj = ass.CreateInstance(line.Split(' ')[2]);
            if (obj == null)
                return null;

            var type = obj.GetType();
            while ((line = stream.ReadLine()) != null && !line.Equals("End of instance"))
            {
                if (line.EndsWith("is a nested object..."))
                {
                    type.GetProperty(line.Split(' ')[0]).SetValue(obj, Next());
                }
                else
                {
                    string[] propVals = line.Split('=');
                    var prop = type.GetProperty(propVals[0]);
                    if (prop.PropertyType != typeof(String))
                    {
                        var inv = Activator.CreateInstance(prop.PropertyType);
                        var pars = prop.PropertyType.GetMethod("Parse", new[] { typeof(String), typeof(CultureInfo) });
                        prop.SetValue(obj, pars.Invoke(inv, new object[] { propVals[1], CultureInfo.InvariantCulture }));
                    }
                    else
                    {
                        prop.SetValue(obj, propVals[1].Replace("\"", ""));
                    }
                }
            }
            return obj;
        }
    }
}
