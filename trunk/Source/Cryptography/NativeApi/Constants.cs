namespace Infotecs.Cryptography.NativeApi
{
    /// <summary>
    ///     Константы для криптопровайдера.
    /// </summary>
    internal static class Constants
    {
        public const int CpcspHashId = AlgClassHash | AlgSidHashCpcsp;
        public const int CryptVerifycontext = -268435456;
        public const int DeleteKeySet = 0x00000010;
        public const int HpHashValue = 0x00000002;
        public const int NewKeySet = 0x00000008;
        public const int NteBadSignature = -2146893818;
        public const int PublicKeyBlob = 0x06;
        public const int SilentMode = 0x00000040;
        private const int AlgClassHash = 4 << 13;
        private const int AlgSidHashCpcsp = 30;
    }
}
