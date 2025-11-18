using AvailableCardActions.Api.Domain;

namespace AvailableCardActions.Api.Services
{
    public sealed record SystemActionRule(
        SystemAction Action,
        // Additional conditional to be met for the action to be authorized
        Func<CardDetails, bool>? Condition = null
    );
}