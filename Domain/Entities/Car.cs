using Domain.Entities.Enums;
using System;

namespace Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public int SourceId { get; set; }

        public decimal? Price { get; set; }
        public FuelType? FuelType { get; set; }
        public Transmision? Transmision { get; set; }
        public int? Power { get; set; }
        public int? Millage { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public int? EngineDisplacement { get; set; }
        public NumberOfDoors? NumberOfDoors { get; set; }
        public string Color { get; set; }
        public string AdditionalInfo { get; set; }
        public string AdUrl { get; set; }

        public Safety SafetyInfo { get; set; }
        public string SafetyInfoString { get; set; }

        public Comfort ComfortInfo { get; set; }
        public string ComfortInfoString { get; set; }

        public OtherInfo OtherInfo { get; set; }
        public string OtherInfoString { get; set; }

        public Model Model { get; set; }
        public Source Source { get; set; }
    }
}
