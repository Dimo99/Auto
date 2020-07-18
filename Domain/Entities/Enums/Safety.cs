using System;

namespace Domain.Entities.Enums
{
    [Flags]
    public enum Safety
    {
        None = 0,
        FourXFour = 1,
        ABS = 2,
        Airbag = 4,
        ASR = 8,
        ESP = 16,
        Alarm = 32,
        KeylessIgnition = 64,
        Armored = 128,
        Insurance = 256,
        Immobilizer = 512,
        XenonLights = 1024,
        Parktronic = 2048,
        StartStopSystem = 4096,
        HalogenLights = 8192,
        CentralLocking = 16384
    }
}
