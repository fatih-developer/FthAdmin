// code: fatih.unal date: 2025-04-21T10:01:51
using System;

namespace FthAdmin.Core.CrossCuttingConcerns.Exceptions
{
    /// <summary>
    /// İş kurallarına aykırı durumlarda fırlatılan özel exception türü.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
