using System;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [Flags]
    enum DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS_VALUE : uint
    {
        friendlyNameFromEdid = 0x1,
        friendlyNameForced = 0x2,
        edidIdsValid = 0x4,
    }
}
