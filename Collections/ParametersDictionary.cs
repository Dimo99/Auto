using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class ParametersDictionary
    {
        private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

        public string this[string key]
        {
            get
            {
                if (!data.ContainsKey(key))
                {
                    throw new KeyNotFoundException();
                }

                return data[key][0];
            }

            set
            {
                if (!data.ContainsKey(key))
                {
                    data.Add(key, new List<string>() { value });
                }
                else
                {
                    data[key][0] = value;
                }
            }
        }

        public void Add(string key, IEnumerable<string> values)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, new List<string>());
            }

            data[key].AddRange(values);
        }

        public bool ContainsKey(string key)
        {
            return data.ContainsKey(key);
        }

        public string GetEncodedParameters()
        {
            IEnumerable<string> parametersToString =
                data.SelectMany(kvp => kvp.Value.Select(value =>
                    $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(value)}"));

            return string.Join("&", parametersToString);
        }
    }
}
