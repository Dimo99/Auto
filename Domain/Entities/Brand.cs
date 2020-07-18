using System.Collections.Generic;

namespace Domain.Entities
{
    public class Brand
    {
        public Brand()
        {
            BrandKeys = new List<BrandKey>();
            Models = new List<Model>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BrandKey> BrandKeys { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
