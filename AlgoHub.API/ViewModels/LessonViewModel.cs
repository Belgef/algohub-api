namespace AlgoHub.API.ViewModels;

public class LessonViewModel
{
    public int LessonId { get; set; }
    public string Title { get; set; } = null!;
    public string LessonContent { get; set; } = null!;
    public UserViewModel? Author { get; set; }
    public string? ImageName { get; set; }
    public int Views { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime CreateDate { get; set; }
}