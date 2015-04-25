using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public static class TextReaderExtension
    {
        public static IEnumerable<string[]> GetSplittedLines(this TextReader reader, char separator)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                foreach (string part in line.Split('\n'))
                {
                    yield return part.Split(separator);
                }
            }
        }
    }
}
