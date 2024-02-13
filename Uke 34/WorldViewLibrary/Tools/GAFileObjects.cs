using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldViewLibrary.Tools
{
    public class GAFileObjects<T>
    {
        public IEnumerable<T> GetLineObjects(string fileName, 
            Func<string, string[]> splitFunc, 
            Func<string[], T?> parse)
        {
            using StreamReader reader = new(fileName, Encoding.UTF8);
            string? header = reader.ReadLine();
            string? line = reader.ReadLine();
            while (line != null)
            {
                var arr = splitFunc(line);
                var T = parse(arr);
                if (T != null)
                    yield return T;

                line = reader.ReadLine();
            }

        }
    }
}
