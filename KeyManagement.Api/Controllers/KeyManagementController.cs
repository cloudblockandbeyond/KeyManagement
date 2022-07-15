using KeyManagement.Api.Services;
using KeyManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyManagementController : ControllerBase
{
    private readonly ILogger<KeyManagementController> _logger;
    private readonly IKeyManagementService _keyManagementService;

    public KeyManagementController(ILogger<KeyManagementController> logger
        , IKeyManagementService keyManagementService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _keyManagementService = keyManagementService ?? throw new ArgumentNullException(nameof(keyManagementService));
    }

    [HttpGet("{kid}")]
    public async Task<IActionResult> Get(string kid)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(kid))
                return BadRequest();

            var key = await _keyManagementService.GetKeyAsync(kid);

            if (key == null)
                return NotFound();

            return Ok(key);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error occurred while getting the key");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            var key = await _keyManagementService.CreateKeyAsync();
            return CreatedAtAction(nameof(Get), new { key.PublicKey.kid }, key);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error occurred while creating the key");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
