using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Exceptions;
using AvailableCardActions.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace AvailableCardActions.Api.Services
{
    public class CardService : ICardService
    {
        private readonly IActionAuthorizator _authorizator;
        private readonly ILogger<CardService> _logger;

        public CardService(IActionAuthorizator authorizator, ILogger<CardService> logger)
        {
            _authorizator = authorizator;
            _logger = logger;
            _userCards = CreateSampleUserCards();
        }

        private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards;

        public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
        {
            _logger.LogInformation("Fetching card details for user {UserId} and card {CardNumber}", userId, cardNumber);

            // At this point, we would typically make an HTTP call to an external service
            // to fetch the data. For this example we use generated sample data.
            await Task.Delay(1000);
            if (!_userCards.TryGetValue(userId, out var cards)
            || !cards.TryGetValue(cardNumber, out var cardDetails))
            {
                _logger.LogWarning("No card details found for user {UserId} and card {CardNumber}", userId, cardNumber);
                return null;
            }
            _logger.LogDebug("Card details found for user {UserId} and card {CardNumber}", userId, cardNumber);
            return cardDetails;
        }

        public async Task<IEnumerable<SystemAction>> GetAuthorizedActions(string userId, string cardNumber)
        {
            _logger.LogInformation("Retrieving authorized actions for user {UserId} and card {CardNumber}", userId, cardNumber);
            var cardDetails = await GetCardDetails(userId, cardNumber);
            if (cardDetails == null)
            {
                _logger.LogWarning("Cannot retrieve actions because card details were missing for user {UserId} and card {CardNumber}", userId, cardNumber);
                throw new NotFoundException($"Card details not found for user {userId} and card {cardNumber}");
            }

            var authorizedActions = _authorizator.GetAuthorizedActions(cardDetails).ToList();
            _logger.LogInformation("Authorized {ActionCount} actions for user {UserId} and card {CardNumber}", authorizedActions.Count, userId, cardNumber);
            return authorizedActions;
        }

        private Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
        {
            var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
            for (var i = 1; i <= 3; i++)
            {
                var cards = new Dictionary<string, CardDetails>();
                var cardIndex = 1;
                foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
                {
                    foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                    {
                        var cardNumber = $"Card{i}{cardIndex}";
                        cards.Add(cardNumber,
                        new CardDetails(
                        CardNumber: cardNumber,
                        CardType: cardType,
                        CardStatus: cardStatus,
                        IsPinSet: cardIndex % 2 == 0));
                        cardIndex++;
                    }
                }
                var userId = $"User{i}";
                userCards.Add(userId, cards);

                _logger.LogDebug("For user {userId} added:", userId);
                foreach (var card in cards.Values)
                {
                    _logger.LogDebug("card: number {cardNumber}, type: {cardType}, status: {cardStatus}, isPinSet: {isPinSet}", card.CardNumber, card.CardType, card.CardStatus, card.IsPinSet);
                }
            }
            return userCards;
        }
    }
}
