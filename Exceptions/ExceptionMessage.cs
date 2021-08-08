namespace BasketService.Exceptions
{
    public class ExceptionMessage
    {
        public string errorCode { get; set; }
        public string errorDetail { get; set; }

        public ExceptionMessage(string code, string errorDetail)
        {
            this.errorCode = code;
            this.errorDetail = errorDetail;
        }
    }
}
