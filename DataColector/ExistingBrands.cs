using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DataColector
{
    public class ExistingBrands
    {
        private ConcurrentDictionary<string, byte> Brands { get; set; } = new ConcurrentDictionary<string, byte>();

        public bool Contains(string brand)
        {
            return Brands.ContainsKey(brand.ToLower());
        }

        public bool Add(string brand)
        {
            return Brands.TryAdd(brand.ToLower(), 0);
        }

        public void Load(List<string> brands)
        {
            foreach (string brand in brands)
            {
                Brands.TryAdd(brand.ToLower(), 0);
            }
        }
    }
}
