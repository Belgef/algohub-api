namespace AlgoHub.DAL.Entities;

public class Language
{
    public int? LanguageId { get; set; }
    public string? LanguageName { get; set; }
    public string? LanguageInternalName { get; set; }
    public string? LanguageIconName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}