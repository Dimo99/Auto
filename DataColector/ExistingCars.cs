using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DataColector
{
    public class ExistingCars
    {
        private ConcurrentDictionary<string, byte> Cars { get; set; } = new ConcurrentDictionary<string, byte>();

        public bool Contains(string url)
        {
            return Cars.ContainsKey(url);
        }

        public bool Add(string url)
        {
            return Cars.TryAdd(url, 0);
        }

        public void Load(List<string> urls)
        {
            foreach (string car in urls)
            {
                Cars.TryAdd(car, 0);
            }
        }
    }
}
