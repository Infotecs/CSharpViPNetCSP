using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Infotecs.Cryptography.NativeApi;

namespace Infotecs.Cryptography
{
    /// <summary>
    ///     Класс представляет функциональность Infotecs криптопровайдера.
    /// </summary>
    public sealed class KeyContainer : IDisposable
    {
        private const int PpSignaturePin = 0x21;
        private const string ProviderName = "Infotecs Cryptographic Service Provider";
        private const int ProviderType = 2;

        private IntPtr cspHandler = IntPtr.Zero;
        private bool disposed;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        private KeyContainer()
        {
        }

        /// <summary>
        ///     Подсчет хэша.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Хэш.</returns>
        public static byte[] ComputeHash(byte[] data)
        {
            using (var container = new KeyContainer())
            {
                container.AcquireContext(null, ProviderName, ProviderType, Constants.CryptVerifycontext);
                using (HashContext hashContext = container.CreateHash(null, Constants.CpcspHashId, 0))
                {
                    hashContext.AddData(data, 0);
                    return hashContext.GetValue();
                }
            }
        }

        /// <summary>
        ///     Создать <see cref="KeyContainer" />.
        /// </summary>
        /// <param name="keyContainerName">Название ключевого контейнера.</param>
        /// <param name="keyNumber">Тип ключа.</param>
        /// <returns>
        ///     Экземпляр <see cref="KeyContainer" />.
        /// </returns>
        public static KeyContainer Create(string keyContainerName, KeyNumber keyNumber)
        {
            var container = new KeyContainer();
            container.AcquireContext(keyContainerName, ProviderName, ProviderType, Constants.NewKeySet);
            container.GenerateRandomKey(keyNumber);
            return container;
        }

        /// <summary>
        ///     Экспорт открытого ключа.
        /// </summary>
        /// <param name="keyContainerName">Название контейнера.</param>
        /// <returns>Открытый ключ.</returns>
        public static byte[] ExportPublicKey(string keyContainerName)
        {
            using (var container = new KeyContainer())
            {
                container.AcquireContext(keyContainerName, ProviderName, ProviderType, 0);
                return container.ExportPublicKey();
            }
        }

        /// <summary>
        ///     Получить сертификат для конкретного ключа
        /// </summary>
        /// <returns></returns>
        public static byte[] ExportCertificateData(string keyContainerName)
        {
            using (var container = new KeyContainer())
            {
                container.AcquireContext(keyContainerName, ProviderName, ProviderType, 0);
                return container.ExportCertificateData();
            }
        }

        /// <summary>
        ///     Провекра наличия контейнера.
        /// </summary>
        /// <param name="keyContainerName">Название контейнера.</param>
        /// <returns>True - контейнер существует, иначе False.</returns>
        public static bool Exist(string keyContainerName)
        {
            try
            {
                using (var container = new KeyContainer())
                {
                    container.AcquireContext(keyContainerName, ProviderName, ProviderType, Constants.SilentMode);
                    container.GetUserKey();
                    return true;
                }
            }
            catch (Win32Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Открыть существующий контейнер.
        /// </summary>
        /// <param name="keyContainerName">Название контейнера.</param>
        /// <param name="keycontainerPassword">Пароль ключевого контейнера.</param>
        /// <returns>
        ///     Экземпляр <see cref="KeyContainer" />.
        /// </returns>
        public static KeyContainer Open(string keyContainerName, string keycontainerPassword)
        {
            var container = new KeyContainer();
            container.AcquireContext(keyContainerName, ProviderName, ProviderType, 0);
            container.SetPassword(keycontainerPassword);
            return container;
        }

        /// <summary>
        ///     Удаление ключевого контейнера.
        /// </summary>
        /// <param name="keyContainerName">Название контейнера.</param>
        public static void Remove(string keyContainerName)
        {
            try
            {
                var container = new KeyContainer();
                container.AcquireContext(keyContainerName, ProviderName, ProviderType, Constants.DeleteKeySet);
            }
            catch (Win32Exception)
            {
            }
        }

        /// <summary>
        ///     Проверка подписи.
        /// </summary>
        /// <param name="signature">Подпись.</param>
        /// <param name="data">Данные.</param>
        /// <param name="publicKey">Открытый ключ.</param>
        /// <returns>True - провека прошла успешно, иначе False.</returns>
        public static bool VerifySignature(byte[] signature, byte[] data, byte[] publicKey)
        {
            using (var container = new KeyContainer())
            {
                container.AcquireContext(null, ProviderName, ProviderType, Constants.CryptVerifycontext);
                using (KeyContext keyContext = container.ImportKey(null, publicKey, 0))
                {
                    using (HashContext hashContext =
                        container.CreateHash(null, Constants.CpcspHashId, 0))
                    {
                        hashContext.AddData(data, 0);
                        return keyContext.VerifySignature(signature, hashContext, 0);
                    }
                }
            }
        }

        /// <summary>
        ///     Проверка подписи.
        /// </summary>
        /// <param name="signature">Подпись.</param>
        /// <param name="data">Данные.</param>
        /// <param name="certificateData">Сертификат.</param>
        /// <returns>True - провека прошла успешно, иначе False.</returns>
        public static bool VerifyCertificate(byte[] signature, byte[] data, byte[] certificateData)
        {
            using (var container = new KeyContainer())
            {
                container.AcquireContext(null, ProviderName, ProviderType, Constants.CryptVerifycontext);
                using (KeyContext keyContext = container.ImportSertificate(certificateData))
                {
                    using (HashContext hashContext =
                        container.CreateHash(null, Constants.CpcspHashId, 0))
                    {
                        hashContext.AddData(data, 0);
                        return keyContext.VerifySignature(signature, hashContext, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает открытый ключ сертификата
        /// </summary>
        /// <param name="certificateData">данные сертификата</param>
        /// <returns></returns>
        public static byte[] GetCertificatePublicKey(byte[] certificateData)
        {
            using (var container = new KeyContainer())
            {
                container.AcquireContext(null, ProviderName, ProviderType, Constants.CryptVerifycontext);
                using (KeyContext keyContext = container.ImportSertificate(certificateData))
                {
                    return keyContext.ExportPublicKey();
                }
            }
        }

        /// <summary>
        ///     Экспорт открытого ключа.
        /// </summary>
        /// <returns>Открытый ключ.</returns>
        public byte[] ExportPublicKey()
        {
            using (KeyContext keyContext = GetUserKey())
            {
                return keyContext.ExportPublicKey();
            }
        }

        /// <summary>
        ///     Получить сертификат для конкретного ключа
        /// </summary>
        /// <returns></returns>
        public byte[] ExportCertificateData()
        {
            using (KeyContext keyContext = GetUserKey())
            {
                var rawDataCertificate = keyContext.GetSertificateData();
                return rawDataCertificate;
            }
        }

        /// <summary>
        ///     Подпись хэша.
        /// </summary>
        /// <param name="hash">Хэш.</param>
        /// <param name="keyNumber">Тип ключа.</param>
        /// <returns>Подпись хэша.</returns>
        public byte[] SignHash(byte[] hash, KeyNumber keyNumber)
        {
            using (HashContext hashContext = CreateHash(null, Constants.CpcspHashId, 0))
            {
                hashContext.SetHashParameter(Constants.HpHashValue, hash, 0);
                return hashContext.SignHash(keyNumber, 0);
            }
        }

        /// <summary>
        ///     Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            if (cspHandler != IntPtr.Zero)
            {
                CryptoApi.CryptReleaseContext(cspHandler, 0);
                cspHandler = IntPtr.Zero;
            }
            disposed = true;
        }

        private void AcquireContext(string keyContainerName, string providerName, int providerType, int flags)
        {
            Dispose();

            if (!CryptoApi.CryptAcquireContext(ref cspHandler, keyContainerName, providerName, providerType, flags))
            {
                throw new Win32Exception();
            }
        }

        private HashContext CreateHash(KeyContext keyContext, int algid, int flags)
        {
            IntPtr hashHandler = IntPtr.Zero;
            IntPtr keyHandler = IntPtr.Zero;

            if (keyContext != null)
            {
                keyHandler = keyContext.Handler;
            }

            if (!CryptoApi.CryptCreateHash(cspHandler, algid, keyHandler, flags, ref hashHandler))
            {
                throw new Win32Exception();
            }

            var hashContext = new HashContext(hashHandler);
            return hashContext;
        }

        private KeyContext GenerateRandomKey(KeyNumber keyNumber, int flags = 0)
        {
            IntPtr keyPiarHandler = IntPtr.Zero;
            if (!CryptoApi.CryptGenKey(cspHandler, (int)keyNumber, flags, ref keyPiarHandler))
            {
                throw new Win32Exception();
            }

            var keyPairContext = new KeyContext(keyPiarHandler);
            return keyPairContext;
        }

        private KeyContext GetUserKey(int keySpec = 0)
        {
            IntPtr keyPiarHandler = IntPtr.Zero;
            if (!CryptoApi.CryptGetUserKey(cspHandler, keySpec, ref keyPiarHandler))
            {
                throw new Win32Exception();
            }

            var keyPairContext = new KeyContext(keyPiarHandler);
            return keyPairContext;
        }

        private KeyContext ImportKey(KeyContext protectionKeyContext, byte[] keyData, int flags)
        {
            IntPtr protectionKeyHandler = IntPtr.Zero;

            if (protectionKeyContext != null)
            {
                protectionKeyHandler = protectionKeyContext.Handler;
            }

            IntPtr keyHandler = IntPtr.Zero;
            if (!CryptoApi.CryptImportKey(cspHandler, keyData, keyData.Length,
                protectionKeyHandler, flags, ref keyHandler))
            {
                throw new Win32Exception();
            }

            var keyContext = new KeyContext(keyHandler);
            return keyContext;
        }

        private KeyContext ImportSertificate(byte[] certificateData)
        {
            // создаём объект сертификата
            var hCertContext = CryptoApi.CertCreateCertificateContext(
                Constants.MyEncodingType, certificateData, certificateData.Length);

            //Получаем указатель на SubjectPublicKeyInfo
            var certContextStruct = (Constants.CertContext)
                Marshal.PtrToStructure(hCertContext, typeof(Constants.CertContext));
            var pCertInfo = certContextStruct.pCertInfo;

            // магия. для x32 и x64 сборок структуры разных размеров
            var certInfoStruct = (Constants.CertInfo)Marshal.PtrToStructure(pCertInfo, typeof(Constants.CertInfo));
            IntPtr subjectPublicKeyInfo = Marshal.AllocHGlobal(Marshal.SizeOf(certInfoStruct.SubjectPublicKeyInfo));
            Marshal.StructureToPtr(certInfoStruct.SubjectPublicKeyInfo, subjectPublicKeyInfo, false);

            IntPtr keyHandler = IntPtr.Zero;
            if (!CryptoApi.CryptImportPublicKeyInfo(cspHandler, Constants.MyEncodingType,
                subjectPublicKeyInfo, ref keyHandler))
            {
                throw new Win32Exception();
            }

            var keyContext = new KeyContext(keyHandler);
            return keyContext;
        }

        private void SetPassword(string password)
        {
            byte[] pwdData = Encoding.ASCII.GetBytes(password);
            var pwdDataWithEndZero = new byte[pwdData.Length + 1];
            Array.Copy(pwdData, pwdDataWithEndZero, pwdData.Length);
            SetProviderParameter(PpSignaturePin, pwdDataWithEndZero);
        }

        private void SetProviderParameter(int parameterId, byte[] parameterValue)
        {
            if (!CryptoApi.CryptSetProvParam(cspHandler, parameterId, parameterValue, 0))
            {
                throw new Win32Exception();
            }
        }
    }
}
