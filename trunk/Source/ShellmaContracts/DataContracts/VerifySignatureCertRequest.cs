using System.Runtime.Serialization;

namespace Infotecs.ShellmaContracts.DataContracts
{
    /// <summary>
    ///     Запрос на проверку подписи.
    /// </summary>
    [DataContract]
    public class VerifySignatureCertRequest
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
        public string Certificate { get; set; }

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
            return string.Format("Data: {0}, Certificate: {1}, Signature: {2}", Data, Certificate, Signature);
        }
    }
}