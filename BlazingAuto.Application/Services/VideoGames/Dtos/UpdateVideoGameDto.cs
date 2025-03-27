namespace BlazingAuto.Application.Services.VideoGames.Dtos;

public sealed class UpdateVideoGameDto {
    public string Name { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
}
