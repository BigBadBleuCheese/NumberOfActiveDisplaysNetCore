using System;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [Flags]
    enum DISPLAYCONFIG_PATH_SOURCE_INFO_STATUSFLAGS : uint
    {
        IN_USE = 0x1
    }
}
