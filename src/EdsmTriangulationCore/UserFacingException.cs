namespace EdsmTriangulationCore
{
    public abstract class UserFacingException : Exception
    {
        public UserFacingException(string message) : base(message)
        { }
    }

    public class EdsmConnectionException : UserFacingException
    {
        public EdsmConnectionException(string message) : base(message)
        { }
    }

    public class EdsmNullResponseException : UserFacingException
    {
        public EdsmNullResponseException(string message) : base(message)
        { }
    }

    public class InputValidationException : UserFacingException
    {
        public InputValidationException(string message) : base(message)
        { }
    }
}
