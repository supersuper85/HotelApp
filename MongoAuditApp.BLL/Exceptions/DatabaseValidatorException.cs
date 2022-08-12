namespace MongoAuditApp.BLL.Exceptions
{
    public class DatabaseValidatorException : Exception
    {
        public DatabaseValidatorException()
        { }

        public DatabaseValidatorException(string message)
            : base(message)
        { }

        public DatabaseValidatorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
