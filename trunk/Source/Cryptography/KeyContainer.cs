using System;
using System.ComponentModel;
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
                using (KeyContext keyContext = container.GetUserKey())
                {
                    return keyContext.ExportPublicKey();
                }
            }
        }

        /// <summary>
        ///     Провекра наличия контейнера.
        /// </summary>
        /// <param name="keyContainerName">Название контейнера.</param>
        /// <returns>True - контейнер существует, иначе False.</returns>
        public static bool IsExist(string keyContainerName)
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
                using (KeyContext keyContext = container.ImportKey(null, publicKey, publicKey.Length, 0))
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

        private KeyContext ImportKey(KeyContext protectionKeyContext, byte[] keyData, int keyDataLength, int flags)
        {
            IntPtr protectionKeyHandler = IntPtr.Zero;

            if (protectionKeyContext != null)
            {
                protectionKeyHandler = protectionKeyContext.Handler;
            }

            IntPtr keyHandler = IntPtr.Zero;
            if (
                !CryptoApi.CryptImportKey(
                    cspHandler, keyData, keyDataLength, protectionKeyHandler, flags, ref keyHandler))
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
