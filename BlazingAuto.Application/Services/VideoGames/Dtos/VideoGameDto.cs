namespace BlazingAuto.Application.Services.VideoGames.Dtos;

public sealed class VideoGameDto {
    public int VideoGameId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
}
