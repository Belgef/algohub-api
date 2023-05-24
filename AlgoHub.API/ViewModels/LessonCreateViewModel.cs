using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.ViewModels;

public class LessonCreateViewModel
{
    [RegularExpression(@"[\S ]{5,200}")]
    public string Title { get; set; } = null!;
    public string LessonContent { get; set; } = null!;
    public IFormFile? Image { get; set; }
}
