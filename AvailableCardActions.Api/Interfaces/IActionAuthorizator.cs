using AvailableCardActions.Api.Domain;

namespace AvailableCardActions.Api.Interfaces
{
    public interface IActionAuthorizator
    {
        IEnumerable<SystemAction> GetAuthorizedActions(CardDetails cardDetails);
    }
}