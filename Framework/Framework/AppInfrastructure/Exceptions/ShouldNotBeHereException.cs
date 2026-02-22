namespace System
{
    public class ShouldNotBeHereException : AppException
    {
        public ShouldNotBeHereException()
            : base("Unexpected code branch")
        {
        }
    }
}