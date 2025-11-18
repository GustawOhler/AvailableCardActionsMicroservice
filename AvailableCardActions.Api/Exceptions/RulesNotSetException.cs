namespace AvailableCardActions.Api.Exceptions
{
    public class RulesNotSetException : Exception
    {
        public RulesNotSetException(string? message = null) : base(message)
        {
        }
    }
}