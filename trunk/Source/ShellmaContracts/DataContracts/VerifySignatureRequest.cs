using System.Runtime.Serialization;

namespace Infotecs.ShellmaContracts.DataContracts
{
    /// <summary>
    ///     Запрос на проверку подписи.
    /// </summary>
    [DataContract]
    public class VerifySignatureRequest
    {
        /// <summary>
        ///     Данные.
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        ///     Открытый ключ.
        /// </summary>
        [DataMember]
        public string PublicKey { get; set; }

        /// <summary>
        ///     Подпись.
        /// </summary>
        [DataMember]
        public string Signature { get; set; }

        /// <summary>
        ///     Строковое представление.
        /// </summary>
        /// <returns>
        ///     Строковое представление.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Data: {0}, PublicKey: {1}, Signature: {2}", Data, PublicKey, Signature);
        }
    }
}
