using System;
using System.Runtime.InteropServices;

namespace Infotecs.Cryptography.NativeApi
{
    /// <summary>
    /// DllImport функций. 
    /// </summary>
    internal static class CryptoApi
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptAcquireContext(
            ref IntPtr hProv,
            string pszContainer,
            string pszProvider,
            int dwProvType,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptCreateHash(
            IntPtr hProv,
            int algid,
            IntPtr hKey,
            int dwFlags,
            ref IntPtr phHash
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptDestroyKey(
            IntPtr hKey
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptExportKey(
            IntPtr hKey,
            IntPtr hExpKey,
            int dwBlobType,
            int dwFlags,
            byte[] pbData,
            ref int pdwDataLen
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptGenKey(
            IntPtr hProv,
            int algid,
            int dwFlags,
            ref IntPtr phKey
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptGetHashParam(
            IntPtr hHash,
            int dwParam,
            byte[] pbData,
            ref int pdwDataLen,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptGetUserKey(
            IntPtr hProv,
            int dwKeySpec,
            ref IntPtr phUserKey
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptHashData(
            IntPtr hHash,
            byte[] pbData,
            int dwDataLen,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptImportKey(
            IntPtr hProv,
            byte[] pbData,
            int dwDataLen,
            IntPtr hPubKey,
            int dwFlags,
            ref IntPtr phKey
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptReleaseContext(
            IntPtr hProv,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptSetHashParam(
            IntPtr hHash,
            int dwParam,
            byte[] pbData,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptSetProvParam(
            IntPtr hProv,
            int dwParam,
            byte[] pbData,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptSignHash(
            IntPtr hHash,
            int dwKeySpec,
            string sDescription,
            int dwFlags,
            byte[] pbSignature,
            ref int pdwSigLen
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptVerifySignature(
            IntPtr hHash,
            byte[] pbSignature,
            int dwSigLen,
            IntPtr hPubKey,
            string sDescription,
            int dwFlags
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptGetKeyParam(
            IntPtr hKey,
            uint dwParam,
            byte[] pbData,
            ref uint pdwDataLen,
            uint dwFlags
            );

        [DllImport("crypt32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CertCreateCertificateContext(
            int dwCertEncodingType,
            byte[] pbCertEncoded,
            int cbCertEncoded
            );

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptImportPublicKeyInfo(
            IntPtr hCryptProv,
            Int32 dwCertEncodingType,
            IntPtr pInfo,
            ref IntPtr phKey
            );
    }
}
