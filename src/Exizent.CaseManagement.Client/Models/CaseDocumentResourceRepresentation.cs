namespace Exizent.CaseManagement.Client.Models;

public class CaseDocumentResourceRepresentation
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int FileSize { get; set; }
    public bool IsInfected { get; set; }
}