using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace AvailableCardActions.Api.Services
{
    public class ActionAuthorizator : IActionAuthorizator
    {
        private readonly ILogger<ActionAuthorizator> _logger;

        public ActionAuthorizator(ILogger<ActionAuthorizator> logger)
        {
            _logger = logger;
        }

        public IEnumerable<SystemAction> GetAuthorizedActions(CardDetails cardDetails)
        {
            _logger.LogDebug("Evaluating authorized actions for card {CardNumber}", cardDetails.CardNumber);
            var authorizedActions = new HashSet<SystemAction>();

            if (AccessRuleSet.CardTypeRules.TryGetValue(cardDetails.CardType, out var typeRules))
            {
                _logger.LogTrace("Applying type rules for card {CardNumber} with type {CardType}", cardDetails.CardNumber, cardDetails.CardType);
                authorizedActions = GetActionsInRules(cardDetails, typeRules);
            }
            else
            {
                _logger.LogWarning("No card type rules found for card type {CardType}", cardDetails.CardType);
            }

            if (AccessRuleSet.CardStatusRules.TryGetValue(cardDetails.CardStatus, out var statusRules))
            {
                _logger.LogTrace("Applying status rules for card {CardNumber} with status {CardStatus}", cardDetails.CardNumber, cardDetails.CardStatus);
                var allowedActionsByStatus = GetActionsInRules(cardDetails, statusRules);
                authorizedActions.IntersectWith(allowedActionsByStatus);
            }
            else
            {
                _logger.LogWarning("No card status rules found for card status {CardStatus}", cardDetails.CardStatus);
            }

            _logger.LogInformation("Authorized {ActionCount} actions for card {CardNumber}", authorizedActions.Count, cardDetails.CardNumber);

            return authorizedActions;
        }

        private static HashSet<SystemAction> GetActionsInRules(CardDetails cardDetails, IEnumerable<SystemActionRule> statusRules)
        {
            var authorizedActions = new HashSet<SystemAction>();
            foreach (var rule in statusRules)
            {
                if (rule.Condition == null || rule.Condition(cardDetails))
                {
                    authorizedActions.Add(rule.Action);
                }
            }
            return authorizedActions;
        }
    }
}
