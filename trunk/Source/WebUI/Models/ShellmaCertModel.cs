namespace WebUI.Models
{
    /// <summary>
    ///     Модель данных для примера с сертификатом.
    /// </summary>
    public class ShellmaCertModel
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
        ///     Сертификат из контейнера с открытым ключём.
        /// </summary>
        public string Certificate { get; set; }

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