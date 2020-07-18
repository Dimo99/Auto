using Dtos;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DataColector
{
    public class ExistingModels
    {
        private ConcurrentDictionary<ModelDto, byte> Models { get; set; } = new ConcurrentDictionary<ModelDto, byte>();

        public bool Contains(ModelDto model)
        {
            return Models.ContainsKey(model);
        }

        public bool Add(ModelDto model)
        {
            return Models.TryAdd(model, 0);
        }

        public void Load(List<ModelDto> models)
        {
            foreach (ModelDto model in models)
            {
                Models.TryAdd(model, 0);
            }
        }
    }
}
