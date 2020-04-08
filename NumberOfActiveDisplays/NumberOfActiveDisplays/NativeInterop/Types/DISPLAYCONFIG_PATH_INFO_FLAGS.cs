using System;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [Flags]
    enum DISPLAYCONFIG_PATH_INFO_FLAGS : uint
    {
        ACTIVE = 0x1,
        SUPPORT_VIRTUAL_MODE = 0x8
    }
}
