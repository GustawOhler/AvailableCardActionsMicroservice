namespace AvailableCardActions.Api.Tests;

using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Exceptions;
using AvailableCardActions.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;

[TestClass]
public sealed class ActionAuthorizatorTests
{
    private readonly ActionAuthorizator _authorizator;

    public ActionAuthorizatorTests()
    {
        var logger = new Mock<ILogger<ActionAuthorizator>>();
        _authorizator = new ActionAuthorizator(logger.Object);
    }

    [TestMethod]
    public void GetAuthorizedActions_ReturnsIntersectionOfTypeAndStatusRules()
    {
        var cardDetails = new CardDetails("Card1", CardType.Prepaid, CardStatus.Active, true);

        var actions = _authorizator.GetAuthorizedActions(cardDetails).ToList();

        var expectedActions = new[]
        {
            SystemAction.Action1,
            SystemAction.Action3,
            SystemAction.Action4,
            SystemAction.Action6,
            SystemAction.Action8,
            SystemAction.Action9,
            SystemAction.Action10,
            SystemAction.Action11,
            SystemAction.Action12,
            SystemAction.Action13,
        };

        CollectionAssert.AreEquivalent(expectedActions.ToList(), actions);
    }

    [TestMethod]
    public void GetAuthorizedActions_RespectsConditionalRules()
    {
        var cardDetails = new CardDetails("Card1", CardType.Credit, CardStatus.Ordered, false);

        var actions = _authorizator.GetAuthorizedActions(cardDetails).ToList();

        Assert.IsTrue(actions.Contains(SystemAction.Action7), "Action7 should be allowed when PIN is not set.");
        Assert.IsFalse(actions.Contains(SystemAction.Action6), "Action6 should not be allowed when PIN is not set.");
    }

    [TestMethod]
    public void GetAuthorizedActions_ReturnsEmpty_WhenCardTypeRulesMissing()
    {
        var cardDetails = new CardDetails("Card1", (CardType)999, CardStatus.Active, true);

        Assert.ThrowsException<RulesNotSetException>(() => _authorizator.GetAuthorizedActions(cardDetails));
    }
}
