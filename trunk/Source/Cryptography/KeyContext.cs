using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Infotecs.Cryptography.NativeApi;

namespace Infotecs.Cryptography
{
    /// <summary>
    ///     Контекст ключей.
    /// </summary>
    internal sealed class KeyContext : IDisposable
    {
        private bool disposed;
        private IntPtr handler = IntPtr.Zero;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="keyHandler">Дескриптор контекста.</param>
        internal KeyContext(IntPtr keyHandler)
        {
            handler = keyHandler;
        }

        /// <summary>
        ///     Дескриптор контекста.
        /// </summary>
        public IntPtr Handler
        {
            get { return handler; }
        }

        /// <summary>
        ///     Экспорт открытого ключа.
        /// </summary>
        /// <param name="context">Контекст ключей.</param>
        /// <returns>Открытый ключ.</returns>
        public byte[] ExportPublicKey(KeyContext context = null)
        {
            byte[] result = ExportKey(context, Constants.PublicKeyBlob, 0);
            return result;
        }

        /// <summary>
        ///     Проверяеи ЭЦП данных.
        /// </summary>
        /// <param name="signature">Подпись (ЭЦП).</param>
        /// <param name="hashContext">Контекст хэша данных.</param>
        /// <param name="flags">Дополнительные управляющие флаги.</param>
        /// <returns>True, если ЭЦП корректна.</returns>
        public bool VerifySignature(byte[] signature, HashContext hashContext, int flags)
        {
            if (CryptoApi.CryptVerifySignature(hashContext.Handler, signature, signature.Length, handler, null, flags))
            {
                return true;
            }

            if (Marshal.GetLastWin32Error() == Constants.NteBadSignature)
            {
                return false;
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
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

            if (Handler != IntPtr.Zero)
            {
                CryptoApi.CryptDestroyKey(Handler);
                handler = IntPtr.Zero;
            }
            disposed = true;
        }

        private byte[] ExportKey(KeyContext protectionKeyContext, int blobType, int falgs)
        {
            IntPtr protectionKeyHandler = IntPtr.Zero;

            if (protectionKeyContext != null)
            {
                protectionKeyHandler = protectionKeyContext.Handler;
            }

            int exportBufferSize = 0;
            if (!CryptoApi.CryptExportKey(handler, protectionKeyHandler, blobType, falgs, null, ref exportBufferSize))
            {
                throw new Win32Exception();
            }

            var result = new byte[exportBufferSize];
            if (!CryptoApi.CryptExportKey(handler, protectionKeyHandler, blobType, falgs, result, ref exportBufferSize))
            {
                throw new Win32Exception();
            }
            return result;
        }
    }
}
