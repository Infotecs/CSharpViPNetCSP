using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Infotecs.Shellma.Cors
{
    /// <summary>
    ///     PreflightOperationInvoker.
    /// </summary>
    internal sealed class PreflightOperationInvoker : IOperationInvoker
    {
        private readonly List<string> allowedHttpMethods;
        private readonly string replyAction;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="replyAction">ReplyAction.</param>
        /// <param name="allowedHttpMethods">AllowedHttpMethods.</param>
        public PreflightOperationInvoker(string replyAction, List<string> allowedHttpMethods)
        {
            this.replyAction = replyAction;
            this.allowedHttpMethods = allowedHttpMethods;
        }

        /// <summary>
        ///     Gets a value that specifies whether the
        ///     <see
        ///         cref="M:System.ServiceModel.Dispatcher.IOperationInvoker.Invoke(System.Object,System.Object[],System.Object[]@)" />
        ///     or
        ///     <see
        ///         cref="M:System.ServiceModel.Dispatcher.IOperationInvoker.InvokeBegin(System.Object,System.Object[],System.AsyncCallback,System.Object)" />
        ///     method is called by the dispatcher.
        /// </summary>
        /// <returns>
        ///     true if the dispatcher invokes the synchronous operation; otherwise, false.
        /// </returns>
        public bool IsSynchronous
        {
            get { return true; }
        }

        /// <summary>
        ///     Returns an <see cref="T:System.Array" /> of parameter objects.
        /// </summary>
        /// <returns>
        ///     The parameters that are to be used as arguments to the operation.
        /// </returns>
        public object[] AllocateInputs()
        {
            return new object[1];
        }

        /// <summary>
        ///     Returns an object and a set of output objects from an instance and set of input objects.
        /// </summary>
        /// <returns>
        ///     The return value.
        /// </returns>
        /// <param name="instance">The object to be invoked.</param>
        /// <param name="inputs">The inputs to the method.</param>
        /// <param name="outputs">The outputs from the method.</param>
        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            var input = (Message)inputs[0];
            outputs = null;
            return HandlePreflight(input);
        }

        /// <summary>
        ///     An asynchronous implementation of the
        ///     <see
        ///         cref="M:System.ServiceModel.Dispatcher.IOperationInvoker.Invoke(System.Object,System.Object[],System.Object[]@)" />
        ///     method.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.IAsyncResult" /> used to complete the asynchronous call.
        /// </returns>
        /// <param name="instance">The object to be invoked.</param>
        /// <param name="inputs">The inputs to the method.</param>
        /// <param name="callback">The asynchronous callback object.</param>
        /// <param name="state">Associated state data.</param>
        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("Only synchronous invocation");
        }

        /// <summary>
        ///     The asynchronous end method.
        /// </summary>
        /// <returns>
        ///     The return value.
        /// </returns>
        /// <param name="instance">The object invoked.</param>
        /// <param name="outputs">The outputs from the method.</param>
        /// <param name="result">
        ///     The <see cref="T:System.IAsyncResult" /> object.
        /// </param>
        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            throw new NotSupportedException("Only synchronous invocation");
        }

        private Message HandlePreflight(Message input)
        {
            var httpRequest = (HttpRequestMessageProperty)input.Properties[HttpRequestMessageProperty.Name];
            string origin = httpRequest.Headers[CorsConstants.Origin];
            string requestMethod = httpRequest.Headers[CorsConstants.AccessControlRequestMethod];
            string requestHeaders = httpRequest.Headers[CorsConstants.AccessControlRequestHeaders];

            Message reply = Message.CreateMessage(MessageVersion.None, replyAction);
            var httpResponse = new HttpResponseMessageProperty();
            reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);

            httpResponse.SuppressEntityBody = true;
            httpResponse.StatusCode = HttpStatusCode.OK;
            if (origin != null)
            {
                httpResponse.Headers.Add(CorsConstants.AccessControlAllowOrigin, origin);
            }

            if (requestMethod != null && allowedHttpMethods.Contains(requestMethod))
            {
                httpResponse.Headers.Add(CorsConstants.AccessControlAllowMethods, string.Join(",", allowedHttpMethods));
            }

            if (requestHeaders != null)
            {
                httpResponse.Headers.Add(CorsConstants.AccessControlAllowHeaders, requestHeaders);
            }

            return reply;
        }
    }
}
