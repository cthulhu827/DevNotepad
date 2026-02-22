namespace System
{
    public class BusinessLogicException : AppException
    {
        public BusinessLogicException(string message, params object[] msgParams)
            : base(message, msgParams)
        {
        }

        public BusinessLogicException(string message)
            : base(message)
        {
        }
    }
}