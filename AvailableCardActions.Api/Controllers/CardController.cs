using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvailableCardActions.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly ILogger<CardController> _logger;
    private readonly ICardService _cardService;

    public CardController(ILogger<CardController> logger, ICardService cardService)
    {
        _logger = logger;
        _cardService = cardService;
    }

    [HttpGet(Name = "GetAvailableCardActions")]
    public async Task<IEnumerable<SystemAction>> GetAvailableCardActions(string cardNumber,
        string userId)
    {
        var actions = await _cardService.GetAuthorizedActions(userId, cardNumber);

        return actions;
    }
}
