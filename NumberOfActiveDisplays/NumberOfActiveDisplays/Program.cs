using static System.Console;

namespace NumberOfActiveDisplays
{
    using NativeInterop;
    using NativeInterop.Types;

    class Program
    {
        static int Main(string[] args)
        {
            var getDisplayConfigBufferSizesResult = Methods.GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, out var numPathArrayElements, out var numModeInfoArrayElements);
            if (getDisplayConfigBufferSizesResult != 0)
            {
                Error.WriteLine($"{nameof(Methods.GetDisplayConfigBufferSizes)} returned unsuccessful result: {getDisplayConfigBufferSizesResult}");
                return 1;
            }
            WriteLine(numPathArrayElements);
            return 0;
        }
    }
}
