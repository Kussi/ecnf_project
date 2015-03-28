using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter
    {
        private StringWriter stream;

        public SimpleObjectWriter(StringWriter stream)
        {
            this.stream = stream;
        }

        public void Next(object obj)
        {
            var type = obj.GetType();
            stream.WriteLine("Instance of {0}", type.FullName);
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType.FullName.StartsWith("Fhnw.Ecnf.RoutePlanner."))
                {
                    stream.WriteLine("{0} is a nested object...", prop.Name);
                    Next(prop.GetValue(obj));
                }
                else
                {
                    //ignore Index Property (from City class)
                    if (prop.Name == "Index") continue;

                    if (prop.GetValue(obj) is double)
                        stream.WriteLine("{0}={1}", prop.Name, ((double)prop.GetValue(obj)).ToString(CultureInfo.InvariantCulture));
                    else if (prop.GetValue(obj) is string)
                        stream.WriteLine("{0}=\"{1}\"", prop.Name, prop.GetValue(obj).ToString());
                    else
                        stream.WriteLine("{0}={1}", prop.Name, prop.GetValue(obj).ToString());
                }
            }
            stream.WriteLine("End of instance");
        }
    }
}
