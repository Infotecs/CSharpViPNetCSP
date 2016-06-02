// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.Shellma.Cors;
using Infotecs.ShellmaContracts.DataContracts;

namespace Infotecs.Shellma
{
    /// <summary>
    ///     Представляет команды работы со службой.
    /// </summary>
    [ServiceContract]
    public interface IShellmaServiceProvider
    {
        /// <summary>
        ///     Подсчет хэша.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Хэш.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        string ComputeHash(string data);

        /// <summary>
        ///     Конвертирует строку в Hex представление
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        string ConvertToHex(string data);

        /// <summary>
        ///     Экспорт открытого ключа.
        /// </summary>
        /// <returns>Открытый ключ.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        string ExportPublicKey();

        /// <summary>
        ///     Подпись хэша.
        /// </summary>
        /// <param name="hash">Хэш.</param>
        /// <returns>Подпись хэша.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        string SignHash(string hash);

        /// <summary>
        ///     Проверка подписи.
        /// </summary>
        /// <param name="request">Данные.</param>
        /// <returns>True - провека прошла успешно, иначе False.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        bool VerifySignature(VerifySignatureRequest request);

        /// <summary>
        ///     Экспорт сертификата из контейнера.
        /// </summary>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        string ExportCertificate();


        /// <summary>
        ///     Проверка подписи.
        /// </summary>
        /// <param name="request">Данные.</param>
        /// <returns>True - провека прошла успешно, иначе False.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [CorsEnabled]
        bool VerifyCertificate(VerifySignatureCertRequest request);
    }
}
