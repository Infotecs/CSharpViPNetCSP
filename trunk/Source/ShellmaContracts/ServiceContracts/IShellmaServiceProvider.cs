// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Infotecs.ShellmaContracts.DataContracts;

namespace Infotecs.ShellmaContracts.ServiceContracts
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
        string ComputeHash(string data);

        /// <summary>
        ///     Экспорт открытого ключа.
        /// </summary>
        /// <returns>Открытый ключ.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string ExportPublicKey();

        /// <summary>
        ///     Подпись хэша.
        /// </summary>
        /// <param name="hash">Хэш.</param>
        /// <returns>Подпись хэша.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string SignHash(string hash);

        /// <summary>
        ///     Проверка подписи.
        /// </summary>
        /// <param name="request">Данные.</param>
        /// <returns>True - провека прошла успешно, иначе False.</returns>
        [WebInvoke(Method = WebRequestMethods.Http.Post, ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool VerifySignature(VerifySignatureRequest request);
    }
}
