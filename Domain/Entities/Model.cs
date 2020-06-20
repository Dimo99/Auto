using System.Collections.Generic;

namespace Domain.Entities
{
    public class Model
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public int BrandId { get; set; }

        public string Name { get; set; }

        public Brand Brand { get; set; }
        public Model SeriesModel { get; set; }
        public ICollection<Model> SubModels { get; set; }
        public ICollection<ModelKey> ModelKeys { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
