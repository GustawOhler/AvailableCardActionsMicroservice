using AvailableCardActions.Api.Domain;

namespace AvailableCardActions.Api.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<SystemAction>> GetAuthorizedActions(string userId, string cardNumber);
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
    }
}