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
            string[] lines = reader.ReadToEnd().Split('\n');

            foreach (string line in lines)
            {
                yield return line.Split(separator);
            }
        }
    }
}
