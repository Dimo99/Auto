using System.Collections.Generic;

namespace Domain.Entities
{
    public class Source
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BrandKey> BrandKeys { get; set; }
        public ICollection<ModelKey> ModelKeys { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
