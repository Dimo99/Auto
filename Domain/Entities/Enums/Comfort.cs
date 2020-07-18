using System;

namespace Domain.Entities.Enums
{
    [Flags]
    public enum Comfort
    {
        None = 0,
        DVDTV = 1,
        AlloyWheels = 2,
        ElMirrors = 4,
        ElSeats = 8,
        ElWindows = 16,
        AirConditioner = 32,
        Climatronic = 64,
        LeatherInterior = 128,
        MultifunctionSteeringWheel = 256,
        SeatHeating = 512,
        Stereo = 1024,
        Shibedah = 2048
    }
}
