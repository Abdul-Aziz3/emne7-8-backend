using System.Text;
using WorldViewLibrary.Tools.Interfaces;

namespace WorldViewLibrary.Tools
{
    public class CSVSplitter 
    {
        public string[] Split(string customerLine) 
        {
            return this.Split(customerLine, '\"');
        }
        public string[] Split(string customerLine, char forceReadChar)
        {
            StringBuilder sb = new();
            List<string> strings = new();

            bool _forceRead = false;
            foreach (var c in customerLine)
            {
                if (c.Equals(forceReadChar))
                {
                    _forceRead = !_forceRead;
                }

                if (_forceRead)
                {
                    sb.Append(c);
                    continue;
                }

                if (!c.Equals(','))
                {
                    sb.Append(c);
                    continue;
                }

                strings.Add(sb.ToString());
                sb = new StringBuilder();
            }

            strings.Add(sb.ToString());
            return strings.ToArray();
        }
    }
}