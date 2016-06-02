// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WebUI.Models
{
    /// <summary>
    ///     Модель данных.
    /// </summary>
    public class ShellmaModel
    {
        /// <summary>
        ///     Хеш.
        /// </summary>
        public string Hash { get; set; }
        /// <summary>
        ///     Хеш в Hex кодировке
        /// </summary>
        public string HashHex { get; set; }

        /// <summary>
        ///     Подпись верна.
        /// </summary>
        public bool? IsSignatureValid { get; set; }

        /// <summary>
        ///     Открытый ключ.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        ///     Подпись.
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        ///     Текст.
        /// </summary>
        public string Text { get; set; }
    }
}
