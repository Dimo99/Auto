using Domain.Entities.Enums;
using System;

namespace Domain.Entities
{
    public class CarMainInfo
    {
        public int Id { get; set; }

        public decimal Price { get; set; }
        public Engine Engine { get; set; }
        public Transmision Transmision { get; set; }
        public int Millage { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int EngineDisplacement { get; set; }
    }
}
