using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BrandKey> BrandKeys { get; set; }
        public ICollection<Model> Models { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
