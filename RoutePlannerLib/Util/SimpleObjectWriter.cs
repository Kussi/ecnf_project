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
            if (stream == null) { }
            else if (obj == null) { }
            else 
            {
                stream.Write("Instance of {0}\r\n", obj.GetType().FullName);
                var properties = obj.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(obj);
                    var valueString = value as string;
                    var valueValType = value as ValueType;
                    if (prop.Name == "Index") continue;
                    if (valueString != null)
                    {
                        stream.Write("{0}=\"{1}\"\r\n", prop.Name, value);
                    }
                    else if (valueValType != null)
                    {
                        stream.Write("{0}={1}\r\n", prop.Name, Convert.ToString(value, CultureInfo.InvariantCulture.NumberFormat));
                    }
                    else {
                        stream.Write("{0} is a nested object...\r\n", prop.Name);
                        this.Next(value);
                    }
                }
                stream.Write("End of instance\r\n");
            }
        }
    }
}
