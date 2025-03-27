using BlazingAuto.Application.Services.VideoGames.Dtos;
using BlazingAuto.Domain.Entities;
using BlazingAuto.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BlazingAuto.Application.Services.VideoGames;

public interface IVideoGameService {
    Task<List<VideoGameDto>> GetAllAsync();
    Task<VideoGameDto?> GetByVideoGameIdAsync(int videoGameId);
    Task<VideoGameDto> AddAsync(CreateVideoGameDto dto);
    Task<int> UpdateAsync(int videoGameId, UpdateVideoGameDto dto);
    Task<int> DeleteAsync(int videoGameId);
}

public sealed class VideoGameService : IVideoGameService {
    private readonly IDbContextFactory<BlazingAutoDbContext> _blazingAutoDbContextFactory;
    private readonly CancellationToken _cancellationToken;
    
    public VideoGameService(IDbContextFactory<BlazingAutoDbContext> blazingAutoDbContextFactory, CancellationToken cancellationToken) {
        _blazingAutoDbContextFactory = blazingAutoDbContextFactory;
        _cancellationToken = cancellationToken;
    }

    public async Task<List<VideoGameDto>> GetAllAsync() {
        await using BlazingAutoDbContext blazingAutoDbContext = await _blazingAutoDbContextFactory.CreateDbContextAsync(_cancellationToken);
        
        return await blazingAutoDbContext.VideoGames.Where(vg => vg.Visible && !vg.Trash).Select(vg => new VideoGameDto {
            VideoGameId = vg.VideoGameId,
            Name = vg.Name ?? string.Empty,
            ReleaseDate = vg.ReleaseDate,
        }).ToListAsync(_cancellationToken);
    }
    
    public async Task<VideoGameDto?> GetByVideoGameIdAsync(int videoGameId) {
        await using BlazingAutoDbContext blazingAutoDbContext = await _blazingAutoDbContextFactory.CreateDbContextAsync(_cancellationToken);
        
        return await blazingAutoDbContext.VideoGames.Where(vg => vg.VideoGameId == videoGameId && vg.Visible && !vg.Trash).Select(vg => new VideoGameDto {
            VideoGameId = vg.VideoGameId,
            Name = vg.Name ?? string.Empty,
            ReleaseDate = vg.ReleaseDate,
        }).FirstOrDefaultAsync(_cancellationToken);
    }
    
    public async Task<VideoGameDto> AddAsync(CreateVideoGameDto dto) {
        await using BlazingAutoDbContext blazingAutoDbContext = await _blazingAutoDbContextFactory.CreateDbContextAsync(_cancellationToken);

        VideoGame videoGame = new() {
            Name = dto.Name,
            ReleaseDate = dto.ReleaseDate,
            Visible = true,
            Trash = false,
            CreatedAt = DateTime.Now
        };
        await blazingAutoDbContext.AddAsync(videoGame, _cancellationToken);
        await blazingAutoDbContext.SaveChangesAsync(_cancellationToken);

        return new VideoGameDto {
            VideoGameId = videoGame.VideoGameId,
            Name = videoGame.Name,
            ReleaseDate = videoGame.ReleaseDate,
        };
    }
    
    public async Task<int> UpdateAsync(int videoGameId, UpdateVideoGameDto dto) {
        await using BlazingAutoDbContext blazingAutoDbContext = await _blazingAutoDbContextFactory.CreateDbContextAsync(_cancellationToken);

        return await blazingAutoDbContext.VideoGames.Where(vg => vg.VideoGameId == videoGameId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(vg => vg.Name, dto.Name)
                .SetProperty(vg => vg.ReleaseDate, dto.ReleaseDate), _cancellationToken);
    }
    
    public async Task<int> DeleteAsync(int videoGameId) {
        await using BlazingAutoDbContext blazingAutoDbContext = await _blazingAutoDbContextFactory.CreateDbContextAsync(_cancellationToken);

        return await blazingAutoDbContext.VideoGames.Where(vg => vg.VideoGameId == videoGameId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(vg => vg.Trash, true)
                .SetProperty(vg => vg.DeletedAt, DateTime.Now), _cancellationToken);
    }
}
