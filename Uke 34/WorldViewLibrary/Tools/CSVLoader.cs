using System.Text;
using static WorldViewLibrary.Tools.Interfaces.ICVSLoader;

namespace WorldViewLibrary.Tools
{
    public class CSVLoader<T> : ICSVLoader<T>
    {
        public IEnumerable<T> LoadFromCSV(string fileName, Func<string, string[]> splitFunc, Func<string[], T?> parse)
        {
            using StreamReader reader = new(fileName, encoding: Encoding.UTF8);
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