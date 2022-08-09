namespace HotelApp.BLL.Exceptions
{
    public class AuditSenderException : Exception
    {
        public AuditSenderException()
        { }

        public AuditSenderException(string message)
            : base(message)
        { }

        public AuditSenderException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
