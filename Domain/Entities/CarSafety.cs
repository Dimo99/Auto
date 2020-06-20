namespace Domain.Entities
{
    public class CarSafety
    {
        public int Id { get; set; }

        public bool ABS { get; set; }
        public bool ESP { get; set; }
        public bool Airbag { get; set; }
        public bool HalogenHeadlights { get; set; }
        public bool Alarm { get; set; }
    }
}
