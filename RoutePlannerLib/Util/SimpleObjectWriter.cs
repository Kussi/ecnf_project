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
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.FullName.StartsWith("Fhnw.Ecnf.RoutePlanner."))
                {
                    stream.WriteLine("{0} is a nested object...", property.Name);
                    Next(property.GetValue(obj));
                }
                else
                {
                    //ignore Index Property (from City class)
                    if (property.Name == "Index") continue;

                    if (property.GetValue(obj) is double)
                        stream.WriteLine("{0}={1}", property.Name, ((double)property.GetValue(obj)).ToString(CultureInfo.InvariantCulture));
                    else if (property.GetValue(obj) is string)
                        stream.WriteLine("{0}=\"{1}\"", property.Name, property.GetValue(obj).ToString());
                    else
                        stream.WriteLine("{0}={1}", property.Name, property.GetValue(obj).ToString());
                }
            }
            stream.WriteLine("End of instance");
        }
    }
}
