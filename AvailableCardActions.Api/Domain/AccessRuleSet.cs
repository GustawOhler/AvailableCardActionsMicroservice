using AvailableCardActions.Api.Domain;

namespace AvailableCardActions.Api.Services
{
    public static class AccessRuleSet
    {
        public static readonly IReadOnlyDictionary<CardType, IEnumerable<SystemActionRule>> CardTypeRules = new Dictionary<CardType, IEnumerable<SystemActionRule>>()
        {
            {CardType.Prepaid, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action1),
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action6),
                new SystemActionRule(SystemAction.Action7),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
                new SystemActionRule(SystemAction.Action10),
                new SystemActionRule(SystemAction.Action11),
                new SystemActionRule(SystemAction.Action12),
                new SystemActionRule(SystemAction.Action13),
            }},
            {CardType.Debit, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action1),
                new SystemActionRule(SystemAction.Action2),
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action6),
                new SystemActionRule(SystemAction.Action7),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
                new SystemActionRule(SystemAction.Action10),
                new SystemActionRule(SystemAction.Action11),
                new SystemActionRule(SystemAction.Action12),
                new SystemActionRule(SystemAction.Action13),
            }},
            {CardType.Credit, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action1),
                new SystemActionRule(SystemAction.Action2),
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action6),
                new SystemActionRule(SystemAction.Action7),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
                new SystemActionRule(SystemAction.Action10),
                new SystemActionRule(SystemAction.Action11),
                new SystemActionRule(SystemAction.Action12),
                new SystemActionRule(SystemAction.Action13),
            }},
        };

        public static readonly IReadOnlyDictionary<CardStatus, IEnumerable<SystemActionRule>> CardStatusRules = new Dictionary<CardStatus, IEnumerable<SystemActionRule>>()
        {
            {CardStatus.Ordered, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action6, ctx=>ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action7, ctx=>!ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
                new SystemActionRule(SystemAction.Action10),
                new SystemActionRule(SystemAction.Action12),
                new SystemActionRule(SystemAction.Action13),
            }},
            {CardStatus.Inactive, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action2),
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action6, ctx=>ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action7, ctx=>!ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
                new SystemActionRule(SystemAction.Action10),
                new SystemActionRule(SystemAction.Action11),
                new SystemActionRule(SystemAction.Action12),
                new SystemActionRule(SystemAction.Action13),
            }},
            {CardStatus.Active, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action1),
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action6, ctx=>ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action7, ctx=>!ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
                new SystemActionRule(SystemAction.Action10),
                new SystemActionRule(SystemAction.Action11),
                new SystemActionRule(SystemAction.Action12),
                new SystemActionRule(SystemAction.Action13),
            }},
            {CardStatus.Restricted, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action9),
            }},
            {CardStatus.Blocked, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action6, ctx=>ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action7, ctx=>ctx.IsPinSet),
                new SystemActionRule(SystemAction.Action8),
                new SystemActionRule(SystemAction.Action9),
            }},
            {CardStatus.Expired, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action9),
            }},
            {CardStatus.Closed, new List<SystemActionRule>{
                new SystemActionRule(SystemAction.Action3),
                new SystemActionRule(SystemAction.Action4),
                new SystemActionRule(SystemAction.Action5),
                new SystemActionRule(SystemAction.Action9),
            }},
        };
    }
}