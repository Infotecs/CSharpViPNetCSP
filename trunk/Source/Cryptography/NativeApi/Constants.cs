// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        public const int X509AsnEncoding = 0x00000001;
        public const int Pkcs7AsnEncoding = 0x00010000;

        public const int MyEncodingType = Pkcs7AsnEncoding | X509AsnEncoding;

        #region структуры для сертификатов

        [StructLayout(LayoutKind.Sequential)]
        internal struct CertPublicKeyInfo
        {
            public CryptAlgorithmIdentifier Algorithm;
            public CryptBitBlob PublicKey;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CertContext
        {
            public uint dwCertEncodingType;
            public IntPtr pbCertEncoded;
            public uint cbCertEncoded;
            public IntPtr pCertInfo;
            public IntPtr hCertStore;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct CryptoapiBlob //x86 - 8, x64 - 16
        {
            public Int32 cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CertInfo
        {
            public uint dwVersion; //4
            public CryptoapiBlob SerialNumber; //8 - 16
            public CryptAlgorithmIdentifier SignatureAlgorithm; //12 - 16
            public CryptoapiBlob Issuer; //8 - 16
            public System.Runtime.InteropServices.ComTypes.FILETIME NotBefore; //8
            public System.Runtime.InteropServices.ComTypes.FILETIME NotAfter; //8
            public CryptoapiBlob Subject; //8 - 16
            public CertPublicKeyInfo SubjectPublicKeyInfo; //24 - 40
            public CryptBitBlob IssuerUniqueId; //12 - 24
            public CryptBitBlob SubjectUniqueId; //12 - 24
            public uint cExtension; //4
            public IntPtr rgExtension; //4 - 8
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CryptAlgorithmIdentifier //12 - 16
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public String pszObjId; //4

            public CryptoapiBlob Parameters; //8
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CryptBitBlob //12-24
        {
            public Int32 cbData; //4
            public IntPtr pbData; //4-8
            public Int32 cUnusedBits; //4
        }

        #endregion
    }
}
