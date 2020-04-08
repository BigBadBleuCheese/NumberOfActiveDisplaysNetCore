using System;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [Flags]
    enum DISPLAYCONFIG_PATH_TARGET_INFO_STATUSFLAGS : uint
    {
        IN_USE = 0x1,
        FORCIBLE = 0x2,
        FORCED_AVAILABILITY_BOOT = 0x4,
        FORCED_AVAILABILITY_PATH = 0x8,
        FORCED_AVAILABILITY_SYSTEM = 0x10
    }
}
