using System.Collections.Generic;

namespace Domain.Entities
{
    public class Model
    {
        public Model()
        {
            SubModels = new List<Model>();
            ModelKeys = new List<ModelKey>();
            Cars = new List<Car>();
        }

        public int Id { get; set; }

        public int? ParentModelId { get; set; }
        public int BrandId { get; set; }

        public string Name { get; set; }

        public Brand Brand { get; set; }
        public Model ParentModel { get; set; }

        public ICollection<Model> SubModels { get; set; }
        public ICollection<ModelKey> ModelKeys { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
