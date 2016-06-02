// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    /// <summary>
    ///     Контроллер криптографии.
    /// </summary>
    public sealed class CryptographyController : Controller
    {
        private ShellmaModel Model
        {
            get { return Session["ShellmaModel"] as ShellmaModel ?? new ShellmaModel(); }
            set { Session["ShellmaModel"] = value; }
        }

        /// <summary>
        ///     Показ страницы для вычисления хеша.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public ActionResult Hash()
        {
            return View(Model);
        }

        /// <summary>
        ///     Данные с вычисленным на клиенте хешем с переходом к подписи.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public ActionResult Hash(ShellmaModel model)
        {
            Model = model;
            return RedirectToAction("SignHash");
        }

        /// <summary>
        ///     Данные с подписанным на клиенте хешем с переходом к проверке подписи.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public ActionResult SignHash()
        {
            return View(Model);
        }

        /// <summary>
        ///     Данные с подписанным на клиенте хешем с переходом к проверке подписи.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public ActionResult SignHash(ShellmaModel model)
        {
            Model = model;
            return RedirectToAction("VerifySignature");
        }

        /// <summary>
        ///     Данные с подписанным на клиенте хешем с переходом к проверке подписи.
        /// </summary>
        /// <returns>Результат операции.</returns>
        public ActionResult VerifySignature()
        {
            return View(Model);
        }

        /// <summary>
        ///     Данные с подписанным на клиенте хешем с переходом к проверке подписи.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public ActionResult VerifySignature(ShellmaModel model)
        {
            Model = model;
            return View(model);
        }
    }
}
