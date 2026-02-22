namespace System
{
    public class UnsupportedEnumValueException<T> : Exception
    {
        public UnsupportedEnumValueException(T value)
            : this(typeof(T).Name, value)
        {
        }

        public UnsupportedEnumValueException(string argumentName, T value)
            : base("Unsupported " + argumentName + " value: " + value)
        {
        }
    }
}