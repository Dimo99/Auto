using System.Collections.Generic;

namespace Dtos
{
    public class ModelSearchDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string BrandKey { get; set; }

        public List<string> ModelKeys { get; set; }
    }
}
