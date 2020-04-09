using System;
using System.Runtime.InteropServices;
using NumberOfActiveDisplays.NativeInterop.Types;

namespace NumberOfActiveDisplays.NativeInterop
{
    static class Methods
    {
        [DllImport("user32.dll")]
        public static extern ERROR DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_ADAPTER_NAME deviceName);

        [DllImport("user32.dll")]
        public static extern ERROR DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SOURCE_DEVICE_NAME deviceName);

        [DllImport("user32.dll")]
        public static extern ERROR DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);

        [DllImport("user32.dll")]
        public static extern ERROR GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS Flags, out uint numPathArrayElements, out uint numModeInfoArrayElements);

        [DllImport("user32.dll")]
        public static extern ERROR QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS flags, ref uint numPathArrayElements, [Out] DISPLAYCONFIG_PATH_INFO[] pathInfoArray, ref uint numModeInfoArrayElements, [Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray, IntPtr currentTopologyId);
    }
}
