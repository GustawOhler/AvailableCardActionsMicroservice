namespace AvailableCardActions.Api.Tests;

using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Interfaces;
using AvailableCardActions.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;

[TestClass]
public sealed class CardServiceTests
{
    private readonly Mock<IActionAuthorizator> _authorizator;
    private readonly Mock<ILogger<CardService>> _logger;
    private readonly CardService _service;

    public CardServiceTests()
    {
        _authorizator = new Mock<IActionAuthorizator>();
        _logger = new Mock<ILogger<CardService>>();
        _service = new CardService(_authorizator.Object, _logger.Object);
    }

    [TestMethod]
    public async Task GetCardDetails_ReturnsDetails_WhenUserAndCardExist()
    {
        var result = await _service.GetCardDetails("User1", "Card11");

        Assert.IsNotNull(result);
        Assert.AreEqual("Card11", result.CardNumber);
    }

    [TestMethod]
    public async Task GetCardDetails_ReturnsNull_WhenUserOrCardDoesNotExist()
    {
        var result = await _service.GetCardDetails("UnknownUser", "Card11");

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetAuthorizedActions_ReturnsAuthorizatorResult_WhenCardExists()
    {
        var expectedActions = new[]
        {
            SystemAction.Action1,
            SystemAction.Action3
        };

        _authorizator.Setup(x => x.GetAuthorizedActions(It.Is<CardDetails>(d => d.CardNumber == "Card11")))
            .Returns(expectedActions);

        var result = await _service.GetAuthorizedActions("User1", "Card11");

        CollectionAssert.AreEquivalent(expectedActions.ToList(), result.ToList());
        _authorizator.Verify(x => x.GetAuthorizedActions(It.Is<CardDetails>(d => d.CardNumber == "Card11")), Times.Once);
    }

    [TestMethod]
    public async Task GetAuthorizedActions_ReturnsEmpty_WhenCardDoesNotExist()
    {
        var result = await _service.GetAuthorizedActions("UnknownUser", "NonExistingCard");

        Assert.IsFalse(result.Any());
        _authorizator.Verify(x => x.GetAuthorizedActions(It.IsAny<CardDetails>()), Times.Never);
    }
}
