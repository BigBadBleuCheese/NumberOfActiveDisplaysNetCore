using System.Diagnostics.CodeAnalysis;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name")]
    enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
    {
        DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,
        DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,
        DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 0xFFFFFFFF
    }
}
