using System;

namespace BasketService.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public ExceptionMessage BusinessExceptionMessage { get; set; }
        public BusinessException(string exceptionCode, string exceptionMessage) : base(exceptionMessage)
        {
            BusinessExceptionMessage = new ExceptionMessage(exceptionCode, exceptionMessage);
        }
    }
}
