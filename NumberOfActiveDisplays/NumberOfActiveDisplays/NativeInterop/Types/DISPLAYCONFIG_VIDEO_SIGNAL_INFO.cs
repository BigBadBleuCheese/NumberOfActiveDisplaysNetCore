using System.Runtime.InteropServices;

namespace NumberOfActiveDisplays.NativeInterop.Types
{
    [StructLayout(LayoutKind.Sequential)]
    struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
    {
        public ulong pixelRate;
        public DISPLAYCONFIG_RATIONAL hSyncFreq;
        public DISPLAYCONFIG_RATIONAL vSyncFreq;
        public DISPLAYCONFIG_2DREGION activeSize;
        public DISPLAYCONFIG_2DREGION totalSize;
        public DISPLAYCONFIG_VIDEO_SIGNAL_INFO_VIDEOSTANDARD videoStandard;
        public DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
    }
}
