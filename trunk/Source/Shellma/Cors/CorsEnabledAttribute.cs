using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Infotecs.Shellma.Cors
{
    /// <summary>
    ///     Включает Cors.
    /// </summary>
    public sealed class CorsEnabledAttribute : Attribute, IOperationBehavior
    {
        /// <summary>
        ///     Добавить Binding.
        /// </summary>
        /// <param name="operationDescription">OperationDescription.</param>
        /// <param name="bindingParameters">BindingParameterCollection.</param>
        public void AddBindingParameters(
            OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        ///     Implements a modification or extension of the client across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="clientOperation">
        ///     The run-time object that exposes customization properties for the operation described by
        ///     <paramref name="operationDescription" />.
        /// </param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        /// <summary>
        ///     Implements a modification or extension of the service across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="dispatchOperation">
        ///     DispatchOperation.
        /// </param>
        public void ApplyDispatchBehavior(
            OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
        }

        /// <summary>
        ///     Implement to confirm that the operation meets some intended criteria.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}
