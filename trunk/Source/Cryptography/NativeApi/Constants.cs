using System;
using System.Runtime.InteropServices;

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

        // for setting Secure Channel certificate data (PCT1)
        public const int KpCertificate = 26;

        public const int X509_ASN_ENCODING = 0x00000001;
        public const int PKCS_7_ASN_ENCODING = 0x00010000;

        public const int MY_ENCODING_TYPE = PKCS_7_ASN_ENCODING | X509_ASN_ENCODING;

        #region структуры для сертификатов

        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_PUBLIC_KEY_INFO
        {
            public CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            public CRYPT_BIT_BLOB PublicKey;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CERT_CONTEXT
        {
            public uint dwCertEncodingType;
            public IntPtr pbCertEncoded;
            public uint cbCertEncoded;
            public IntPtr pCertInfo;
            public IntPtr hCertStore;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPTOAPI_BLOB //x86 - 8, x64 - 16
        {
            public Int32 cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_INFO
        {
            public uint dwVersion; //4
            public CRYPTOAPI_BLOB SerialNumber; //8 - 16
            public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm; //12 - 16
            public CRYPTOAPI_BLOB Issuer; //8 - 16
            public System.Runtime.InteropServices.ComTypes.FILETIME NotBefore; //8
            public System.Runtime.InteropServices.ComTypes.FILETIME NotAfter; //8
            public CRYPTOAPI_BLOB Subject; //8 - 16
            public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo; //24 - 40
            public CRYPT_BIT_BLOB IssuerUniqueId; //12 - 24
            public CRYPT_BIT_BLOB SubjectUniqueId; //12 - 24
            public uint cExtension; //4
            public IntPtr rgExtension; //4 - 8
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER //12 - 16
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public String pszObjId; //4

            public CRYPTOAPI_BLOB Parameters; //8
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_BIT_BLOB //12-24
        {
            public Int32 cbData; //4
            public IntPtr pbData; //4-8
            public Int32 cUnusedBits; //4
        }

        #endregion
    }
}
