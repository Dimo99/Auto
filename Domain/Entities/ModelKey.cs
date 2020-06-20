namespace Domain.Entities
{
    public class ModelKey
    {
        public int ModelId { get; set; }
        public int SourceId { get; set; }
        public string Key { get; set; }

        public Model Model { get; set; }
        public Source Source { get; set; }
    }
}
