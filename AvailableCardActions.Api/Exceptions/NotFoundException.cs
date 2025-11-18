namespace AvailableCardActions.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message = null) : base(message)
        {
        }
    }
}