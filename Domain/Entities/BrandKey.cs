namespace Domain.Entities
{
    public class BrandKey
    {
        public int BrandId { get; set; }
        public int SourceId { get; set; }
        public string Key { get; set; }

        public Brand Brand { get; set; }
        public Source Source { get; set; }
    }
}
