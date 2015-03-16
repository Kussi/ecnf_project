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
            var assembly = Assembly.GetExecutingAssembly();
            var readLine = stream.ReadLine();
            if (readLine == null)
                return null;

            var obj = assembly.CreateInstance(readLine.Split(' ')[2]);
            if (obj == null)
                return null;

            var type = obj.GetType();
            while ((readLine = stream.ReadLine()) != null && !readLine.Equals("End of instance"))
            {
                if (readLine.EndsWith("is a nested object..."))
                {
                    type.GetProperty(readLine.Split(' ')[0]).SetValue(obj, Next());
                }
                else
                {
                    var propertyValues = readLine.Split('=');
                    var property = type.GetProperty(propertyValues[0]);
                    if (property.PropertyType != typeof(String))
                    {
                        var invoker = Activator.CreateInstance(property.PropertyType);
                        var parser = property.PropertyType.GetMethod("Parse", new[] { typeof(String), typeof(CultureInfo) });
                        property.SetValue(obj, parser.Invoke(invoker, new object[] { propertyValues[1], CultureInfo.InvariantCulture }));
                    }
                    else
                    {
                        property.SetValue(obj, propertyValues[1].Replace("\"", ""));
                    }
                }
            }
            return obj;
        }
    }
}
