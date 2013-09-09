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
