namespace NumberOfActiveDisplays
{
    static class ExitCode
    {
        public const int Success = 0;
        public const int GetDisplayConfigBufferSizesAccessDenied = 1;
        public const int GetDisplayConfigBufferSizesGenFailure = 2;
        public const int GetDisplayConfigBufferSizesInvalidParameter = 3;
        public const int GetDisplayConfigBufferSizesNotSupported = 4;
        public const int GetDisplayConfigBufferSizesUnknownError = 5;
        public const int QueryDisplayConfigAccessDenied = 6;
        public const int QueryDisplayConfigGenFailure = 7;
        public const int QueryDisplayConfigInsufficientBuffer = 8;
        public const int QueryDisplayConfigInvalidParameter = 9;
        public const int QueryDisplayConfigNotSupported = 10;
        public const int QueryDisplayConfigUnknownError = 11;
    }
}
