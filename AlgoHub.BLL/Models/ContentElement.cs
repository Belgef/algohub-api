using Microsoft.AspNetCore.Http;

namespace AlgoHub.API.Models;

public class ContentElement
{
    public ContentType ContentType { get; set; }
    public string? Value { get; set; }
    public IFormFile? Image { get; set; }
    public string? Code { get; set; }

}
