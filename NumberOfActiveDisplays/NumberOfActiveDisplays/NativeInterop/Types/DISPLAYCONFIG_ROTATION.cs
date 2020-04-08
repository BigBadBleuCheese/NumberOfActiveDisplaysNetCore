﻿using System.Diagnostics.CodeAnalysis;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name")]
    enum DISPLAYCONFIG_ROTATION : uint
    {
        DISPLAYCONFIG_ROTATION_IDENTITY = 1,
        DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
        DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
        DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
        DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 0xFFFFFFFF
    }
}
