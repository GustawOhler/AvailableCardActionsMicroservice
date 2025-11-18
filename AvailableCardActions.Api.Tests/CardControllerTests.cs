namespace AvailableCardActions.Api.Tests;

using AvailableCardActions.Api.Controllers;
using AvailableCardActions.Api.Interfaces;
using AvailableCardActions.Api.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;

[TestClass]
public sealed class CardControllerTests
{
    Mock<ILogger<CardController>> logger;
    Mock<ICardService> cardService;
    public CardControllerTests()
    {
        logger = new Mock<ILogger<CardController>>();
        cardService = new Mock<ICardService>();
    }

    [TestMethod]
    public async Task GetAvailableCardActions_ValidInput_ValidOutput()
    {
        var userId = "User1";
        var cardNumber = "Card11";
        var expectedActions = new[]
        {
            SystemAction.Action1,
            SystemAction.Action2
        };

        cardService.Setup(x => x.GetAuthorizedActions(userId, cardNumber))
            .ReturnsAsync(expectedActions);

        var controller = new CardController(logger.Object, cardService.Object);

        var result = await controller.GetAvailableCardActions(cardNumber, userId);

        CollectionAssert.AreEquivalent(expectedActions.ToList(), result.ToList());
        cardService.Verify(x => x.GetAuthorizedActions(userId, cardNumber), Times.Once);
    }
}
