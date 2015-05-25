using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CertificateController : Controller
    {
        private ShellmaCertModel Model
        {
            get { return Session["ShellmaCertModel"] as ShellmaCertModel ?? new ShellmaCertModel(); }
            set { Session["ShellmaCertModel"] = value; }
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
        public ActionResult Hash(ShellmaCertModel model)
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
        public ActionResult SignHash(ShellmaCertModel model)
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
        public ActionResult VerifySignature(ShellmaCertModel model)
        {
            Model = model;
            return View(model);
        }
    }

}
