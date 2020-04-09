using System.Runtime.InteropServices;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [StructLayout(LayoutKind.Sequential)]
    struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS
    {
        public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS_VALUE value;
    }
}
