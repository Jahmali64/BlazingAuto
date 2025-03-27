namespace BlazingAuto.Domain.Entities;

public partial class VideoGame
{
    public int VideoGameId { get; set; }

    public string? Name { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public bool Visible { get; set; }

    public bool Trash { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
