using BlazingAuto.Application.Services.VideoGames;
using BlazingAuto.Application.Services.VideoGames.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazingAuto.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class VideoGameController : Controller {
    private readonly IVideoGameService _videoGameService;
    private readonly ILogger<VideoGameController> _logger;

    public VideoGameController(IVideoGameService videoGameService, ILogger<VideoGameController> logger) {
        _videoGameService = videoGameService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<VideoGameDto>>> GetAll() {
        _logger.LogInformation("Attempting to get all video games");

        try {
            List<VideoGameDto> videoGames = await _videoGameService.GetAllAsync();
            return Ok(videoGames);
        } catch (Exception ex) {
            _logger.LogError(ex, "Failed to get video games");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VideoGameDto>> Get(int id) {
        _logger.LogInformation("Attempting to video game by id: {id}", id);

        if (id < 1) {
            _logger.LogWarning("Video game with id {id} not valid", id);
            return BadRequest("Invalid id");
        }

        try {
            VideoGameDto? videoGame = await _videoGameService.GetByVideoGameIdAsync(id);
            if (videoGame is { VideoGameId: > 0 }) return Ok(videoGame);
            
            _logger.LogWarning("Video game with id {id} not found", id);
            return NotFound($"Video game with id {id} not found");
        } catch (Exception ex) {
            _logger.LogError(ex, "Failed to get video game");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<VideoGameDto>> Create([FromBody] CreateVideoGameDto createVideoGame) {
        _logger.LogInformation("Attempting to create video game");

        try {
            VideoGameDto videoGame = await _videoGameService.AddAsync(createVideoGame);
            return Ok(videoGame);
        } catch (Exception ex) {
            _logger.LogError(ex, "Failed to create video game");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVideoGameDto updateVideoGame) {
        _logger.LogInformation("Attempting to update video game");
        
        if (id < 1) {
            _logger.LogWarning("Video game with id {id} not valid", id);
            return BadRequest("Invalid id");
        }

        try {
            int rowsAffected = await _videoGameService.UpdateAsync(id, updateVideoGame);
            if (rowsAffected > 0) return NoContent();
            
            _logger.LogWarning("Video game with id {id} not found", id);
            return NotFound($"Video game with id {id} not found");
        } catch (Exception ex) {
            _logger.LogError(ex, "Failed to update video game");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        _logger.LogInformation("Attempting to delete video game");
        
        if (id < 1) {
            _logger.LogWarning("Video game with id {id} not valid", id);
            return BadRequest("Invalid id");
        }

        try {
            int rowsAffected = await _videoGameService.DeleteAsync(id);
            if (rowsAffected > 0) return NoContent();
            
            _logger.LogWarning("Video game with id {id} not found", id);
            return NotFound($"Video game with id {id} not found");
        } catch (Exception ex) {
            _logger.LogError(ex, "Failed to delete video game");
            return StatusCode(500, "Internal server error");
        }
    }
}
