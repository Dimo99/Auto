namespace Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int SourceId { get; set; }

        public string AdditionalInfo { get; set; }
        public string AdUrl { get; set; }

        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public Source Source { get; set; }

        public CarMainInfo MainInfo { get; set; }
        public CarComfort Comfort { get; set; }
        public CarSafety Safety { get; set; }
    }
}
