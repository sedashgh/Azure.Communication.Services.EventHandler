namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models
{
    /// <summary>
    /// Communication generic error model
    /// </summary>
    [Serializable]
    public class CommunicationError
    {
        /// <summary>
        /// Required. The error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Required. The error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The error target.
        /// </summary>
        public string? Target { get; set; }

        /// <summary>
        /// The inner error if any.
        /// </summary>
        public CommunicationError? InnerError { get; set; }

        /// <summary>
        /// Further details about specific errors that led to this error.
        /// </summary>
        public IEnumerable<CommunicationError>? Details { get; set; }
    }
}