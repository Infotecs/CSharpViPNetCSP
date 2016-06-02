// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Infotecs.Cryptography.NativeApi;

namespace Infotecs.Cryptography
{
    /// <summary>
    ///     Контекст хэша.
    /// </summary>
    internal sealed class HashContext : IDisposable
    {
        private bool disposed;
        private IntPtr handler = IntPtr.Zero;

        /// <summary>
        ///     Создаёт экземпляр HashContext.
        /// </summary>
        /// <param name="hashContext">Дескриптор контекста.</param>
        internal HashContext(IntPtr hashContext)
        {
            handler = hashContext;
        }

        /// <summary>
        ///     Дескриптор контекста.
        /// </summary>
        public IntPtr Handler
        {
            get { return handler; }
        }

        /// <summary>
        ///     Добавляет данные в хэш контекст.
        /// </summary>
        /// <param name="data">Добаляемые данные.</param>
        /// <param name="flags">Дополнительные управляющие флаги.</param>
        public void AddData(byte[] data, int flags)
        {
            if (!CryptoApi.CryptHashData(Handler, data, data.Length, flags))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        ///     Возвращает хэш.
        /// </summary>
        /// <returns>Значение хэша.</returns>
        public byte[] GetValue()
        {
            int dataLenth = 0;
            if (!CryptoApi.CryptGetHashParam(Handler, Constants.HpHashValue, null, ref dataLenth, 0))
            {
                throw new Win32Exception();
            }
            if (dataLenth == 0)
            {
                throw new Win32Exception();
            }
            var result = new byte[dataLenth];
            if (!CryptoApi.CryptGetHashParam(Handler, Constants.HpHashValue, result, ref dataLenth, 0))
            {
                throw new Win32Exception();
            }
            return result;
        }

        /// <summary>
        ///     Устаналивает параметры хэша.
        /// </summary>
        /// <param name="parameterId">Идентификатор параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <param name="flags">Дополнительные управляющие флаги.</param>
        public void SetHashParameter(int parameterId, byte[] parameterValue, int flags)
        {
            if (!CryptoApi.CryptSetHashParam(Handler, parameterId, parameterValue, flags))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        ///     Создаёт подпись данных хэша.
        /// </summary>
        /// <param name="keyNumber">Тип ключа.</param>
        /// <param name="flags">Дополнительные управляющие флаги.</param>
        /// <returns>Результат операции.</returns>
        public byte[] SignHash(KeyNumber keyNumber, int flags)
        {
            int signatureSize = 0;
            if (!CryptoApi.CryptSignHash(handler, (int)keyNumber, null, flags, null, ref signatureSize))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var signature = new byte[signatureSize];
            if (!CryptoApi.CryptSignHash(handler, (int)keyNumber, null, flags, signature, ref signatureSize))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return signature;
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

            if (Handler == IntPtr.Zero)
            {
                return;
            }
            CryptoApi.CryptDestroyKey(Handler);
            handler = IntPtr.Zero;
            disposed = true;
        }
    }
}
