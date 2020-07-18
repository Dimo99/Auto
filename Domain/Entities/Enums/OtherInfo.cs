using System;

namespace Domain.Entities.Enums
{
    [Flags]
    public enum OtherInfo
    {
        None = 0,
        SevenPlaces = 1,
        TAXI = 2,
        AutoPilot = 4,
        BoardComputer = 8,
        Warranty = 16,
        RightHandDrive = 32,
        NavigationSystem = 64,
        PanoramicRoof = 128,
        Retro = 256,
        ServiceBook = 512,
        PowerSteering = 1024,
        Towbar = 2048,
        Multitronic = 4096,
        Tunning = 8192,
        Refrigeration = 16384
    }
}
