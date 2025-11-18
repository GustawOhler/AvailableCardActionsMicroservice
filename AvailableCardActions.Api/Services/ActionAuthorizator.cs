using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Interfaces;

namespace AvailableCardActions.Api.Services
{
    public class ActionAuthorizator : IActionAuthorizator
    {
        public IEnumerable<SystemAction> GetAuthorizedActions(CardDetails cardDetails)
        {
            var authorizedActions = new HashSet<SystemAction>();

            if (AccessRuleSet.CardTypeRules.TryGetValue(cardDetails.CardType, out var typeRules))
            {
                authorizedActions = GetActionsInRules(cardDetails, typeRules);
            }

            if (AccessRuleSet.CardStatusRules.TryGetValue(cardDetails.CardStatus, out var statusRules))
            {
                var allowedActionsByStatus = GetActionsInRules(cardDetails, statusRules);
                authorizedActions.IntersectWith(allowedActionsByStatus);
            }

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