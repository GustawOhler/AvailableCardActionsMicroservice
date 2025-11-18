using System.ComponentModel.DataAnnotations;
using AvailableCardActions.Api.Domain;
using AvailableCardActions.Api.Exceptions;
using AvailableCardActions.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvailableCardActions.Api.Controllers;

[ApiController]
public class CardsController : ControllerBase
{
    private readonly ILogger<CardsController> _logger;
    private readonly ICardService _cardService;

    public CardsController(ILogger<CardsController> logger, ICardService cardService)
    {
        _logger = logger;
        _cardService = cardService;
    }

    [HttpGet("users/{userId}/cards/{cardNumber}/available-actions")]
    public async Task<ActionResult<IEnumerable<SystemAction>>> GetAvailableCardActions(
        [FromRoute, Required] string cardNumber,
        [FromRoute, Required] string userId)
    {
        try
        {
            var actions = await _cardService.GetAuthorizedActions(userId, cardNumber);

            return Ok(actions);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Error occurred while getting available card actions for user {UserId} and card {CardNumber}", userId, cardNumber);
            return NotFound("Card details not found.");
        }
    }
}
