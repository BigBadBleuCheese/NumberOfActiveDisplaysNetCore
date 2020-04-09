using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NumberOfActiveDisplays.NativeInterop;
using NumberOfActiveDisplays.NativeInterop.Types;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Console;

namespace NumberOfActiveDisplays
{
    class Program
    {
        static int Main(string[] args)
        {
            var mode = Mode.CountDisplays;

            foreach (var arg in args)
            {
                if (arg.StartsWith("-m:", StringComparison.OrdinalIgnoreCase) && Enum.TryParse<Mode>(arg.Substring(3), true, out var parsedAbbreviatedMode))
                    mode = parsedAbbreviatedMode;
                if (arg.StartsWith("--mode:", StringComparison.OrdinalIgnoreCase) && Enum.TryParse<Mode>(arg.Substring(7), true, out var parsedMode))
                    mode = parsedMode;
            }

            switch (Methods.GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, out var numPathArrayElements, out var numModeInfoArrayElements))
            {
                case ERROR.SUCCESS:
                    break;
                case ERROR.ACCESS_DENIED:
                    Error.WriteLine($"{nameof(Methods.GetDisplayConfigBufferSizes)} says the I do not have access to the console session");
                    return ExitCode.GetDisplayConfigBufferSizesAccessDenied;
                case ERROR.GEN_FAILURE:
                    Error.WriteLine($"{nameof(Methods.GetDisplayConfigBufferSizes)} says an unspecified error occurred (which is immensely helpful)");
                    return ExitCode.GetDisplayConfigBufferSizesGenFailure;
                case ERROR.INVALID_PARAMETER:
                    Error.WriteLine($"{nameof(Methods.GetDisplayConfigBufferSizes)} says the combination of parameters and flags I've specified is invalid");
                    return ExitCode.GetDisplayConfigBufferSizesInvalidParameter;
                case ERROR.NOT_SUPPORTED:
                    Error.WriteLine($"{nameof(Methods.GetDisplayConfigBufferSizes)} says the system is not running a graphics driver that was written according to the Windows Display Driver Model (WDDM), which it requires");
                    return ExitCode.GetDisplayConfigBufferSizesNotSupported;
                case ERROR unexpectedReturnValue:
                    Error.WriteLine($"{nameof(Methods.GetDisplayConfigBufferSizes)} returned unsuccessful result: {(int)unexpectedReturnValue}");
                    return ExitCode.GetDisplayConfigBufferSizesUnknownError;
            }

            return mode switch
            {
                Mode.GetDisplayDetails => GetDisplayDetails(numPathArrayElements, numModeInfoArrayElements),
                Mode.GetModeDetails => GetModeDetails(numPathArrayElements, numModeInfoArrayElements),
                _ => CountDisplays(numPathArrayElements)
            };
        }

        static int CountDisplays(uint numPathArrayElements)
        {
            WriteLine(numPathArrayElements);
            return ExitCode.Success;
        }

        static DISPLAYCONFIG_ADAPTER_NAME? GetAdapterName(LUID adapterId, uint id)
        {
            var deviceName = new DISPLAYCONFIG_ADAPTER_NAME();
            deviceName.header.adapterId = adapterId;
            deviceName.header.id = id;
            deviceName.header.size = (uint)Marshal.SizeOf(typeof(DISPLAYCONFIG_ADAPTER_NAME));
            deviceName.header.type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME;
            var error = Methods.DisplayConfigGetDeviceInfo(ref deviceName);
            if (error != ERROR.SUCCESS)
                return null;
            return deviceName;
        }

        static int GetDisplayDetails(uint numPathArrayElements, uint numModeInfoArrayElements)
        {
            switch (QueryDisplayConfig(numPathArrayElements, numModeInfoArrayElements, out var pathInfoArray, out _))
            {
                case ExitCode.Success:
                    WriteLine(JsonConvert.SerializeObject(pathInfoArray.Select(pathInfo => new
                    {
                        pathInfo,
                        sourceAdapterName = GetAdapterName(pathInfo.sourceInfo.adapterId, pathInfo.sourceInfo.id),
                        sourceDeviceName = GetSourceDeviceName(pathInfo.sourceInfo.adapterId, pathInfo.sourceInfo.id),
                        targetAdapterName = GetAdapterName(pathInfo.targetInfo.adapterId, pathInfo.targetInfo.id),
                        targetDeviceName = GetTargetDeviceName(pathInfo.targetInfo.adapterId, pathInfo.targetInfo.id)
                    }), Formatting.Indented, new StringEnumConverter()));
                    return ExitCode.Success;
                case int unsuccessfulReturnCode:
                    return unsuccessfulReturnCode;
            }
        }

        static int GetModeDetails(uint numPathArrayElements, uint numModeInfoArrayElements)
        {
            switch (QueryDisplayConfig(numPathArrayElements, numModeInfoArrayElements, out _, out var modeInfoArray))
            {
                case ExitCode.Success:
                    WriteLine(JsonConvert.SerializeObject(modeInfoArray.Select(modeInfo => new
                    {
                        modeInfo,
                        targetAdapterName = GetAdapterName(modeInfo.adapterId, modeInfo.id),
                        targetDeviceName = GetTargetDeviceName(modeInfo.adapterId, modeInfo.id)
                    }), Formatting.Indented, new StringEnumConverter()));
                    return ExitCode.Success;
                case int unsuccessfulReturnCode:
                    return unsuccessfulReturnCode;
            }
        }

        static DISPLAYCONFIG_SOURCE_DEVICE_NAME? GetSourceDeviceName(LUID adapterId, uint id)
        {
            var deviceName = new DISPLAYCONFIG_SOURCE_DEVICE_NAME();
            deviceName.header.adapterId = adapterId;
            deviceName.header.id = id;
            deviceName.header.size = (uint)Marshal.SizeOf(typeof(DISPLAYCONFIG_SOURCE_DEVICE_NAME));
            deviceName.header.type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME;
            var error = Methods.DisplayConfigGetDeviceInfo(ref deviceName);
            if (error != ERROR.SUCCESS)
                return null;
            return deviceName;
        }

        static DISPLAYCONFIG_TARGET_DEVICE_NAME? GetTargetDeviceName(LUID adapterId, uint id)
        {
            var deviceName = new DISPLAYCONFIG_TARGET_DEVICE_NAME();
            deviceName.header.adapterId = adapterId;
            deviceName.header.id = id;
            deviceName.header.size = (uint)Marshal.SizeOf(typeof(DISPLAYCONFIG_TARGET_DEVICE_NAME));
            deviceName.header.type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
            var error = Methods.DisplayConfigGetDeviceInfo(ref deviceName);
            if (error != ERROR.SUCCESS)
                return null;
            return deviceName;
        }

        static int QueryDisplayConfig(uint numPathArrayElements, uint numModeInfoArrayElements, out DISPLAYCONFIG_PATH_INFO[] pathInfoArray, out DISPLAYCONFIG_MODE_INFO[] modeInfoArray)
        {
            pathInfoArray = new DISPLAYCONFIG_PATH_INFO[numPathArrayElements];
            modeInfoArray = new DISPLAYCONFIG_MODE_INFO[numModeInfoArrayElements];
            switch (Methods.QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, ref numPathArrayElements, pathInfoArray, ref numModeInfoArrayElements, modeInfoArray, IntPtr.Zero))
            {
                case ERROR.SUCCESS:
                    return ExitCode.Success;
                case ERROR.ACCESS_DENIED:
                    Error.WriteLine($"{nameof(Methods.QueryDisplayConfig)} says the I do not have access to the console session");
                    return ExitCode.QueryDisplayConfigAccessDenied;
                case ERROR.GEN_FAILURE:
                    Error.WriteLine($"{nameof(Methods.QueryDisplayConfig)} says an unspecified error occurred (which is immensely helpful)");
                    return ExitCode.QueryDisplayConfigGenFailure;
                case ERROR.INSUFFICIENT_BUFFER:
                    Error.WriteLine($"{nameof(Methods.QueryDisplayConfig)} says the buffers I've provided are insufficient");
                    return ExitCode.QueryDisplayConfigInvalidParameter;
                case ERROR.INVALID_PARAMETER:
                    Error.WriteLine($"{nameof(Methods.QueryDisplayConfig)} says the combination of parameters and flags I've specified is invalid");
                    return ExitCode.QueryDisplayConfigInvalidParameter;
                case ERROR.NOT_SUPPORTED:
                    Error.WriteLine($"{nameof(Methods.QueryDisplayConfig)} says the system is not running a graphics driver that was written according to the Windows Display Driver Model (WDDM), which it requires");
                    return ExitCode.QueryDisplayConfigNotSupported;
                case ERROR unexpectedReturnValue:
                    Error.WriteLine($"{nameof(Methods.QueryDisplayConfig)} returned unsuccessful result: {(int)unexpectedReturnValue}");
                    return ExitCode.QueryDisplayConfigUnknownError;
            }
        }
    }
}
