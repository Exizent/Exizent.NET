namespace Exizent.CaseManagement.Client.Models.EstateItems;

/// <summary>
/// A document attached to an estate item, as held in the Cases API's document metadata store.
/// </summary>
/// <remarks>
/// <see cref="FileName"/> is the name the document was uploaded under, not the last segment of its
/// storage key — for documents written by the Cases API those differ, the key ending in the document's
/// id. Anything showing a document to a user wants this.
/// </remarks>
public class EstateItemDocumentResourceRepresentation
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long? SizeBytes { get; set; }
    public EstateItemDocumentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UploadedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Where a document has got to between being asked for and being safe to hand out.
/// </summary>
public enum EstateItemDocumentStatus
{
    /// <summary>Metadata written, the upload itself not yet arrived.</summary>
    Pending = 0,

    /// <summary>In storage, not yet scanned.</summary>
    Uploaded = 1,

    /// <summary>Malware scanning found a threat. Never hand this out.</summary>
    Infected = 2,

    /// <summary>Malware scanning is under way.</summary>
    Scanning = 3,

    /// <summary>Scanned and clean — the only status that can be downloaded.</summary>
    Clean = 4,

    /// <summary>Scanning did not complete, so the document cannot be vouched for.</summary>
    ScanFailed = 5
}

/// <summary>
/// The outcome of asking for a document's download URL. A document that is still being scanned is not an
/// error — it is worth telling the user apart from one that is blocked, so these are separate cases rather
/// than a null URL.
/// </summary>
public enum EstateItemDocumentUrlStatus
{
    /// <summary><see cref="EstateItemDocumentUrlResult.Url"/> is set and can be followed.</summary>
    Available,

    /// <summary>Scanning has not finished. Worth retrying shortly.</summary>
    NotYetAvailable,

    /// <summary>Infected or unscannable. It will not become available.</summary>
    Unavailable,

    /// <summary>No such document on this estate item, or it has been deleted.</summary>
    NotFound
}

/// <param name="Status">Which of the four outcomes this is.</param>
/// <param name="Url">The presigned download URL, set only when <paramref name="Status"/> is Available.</param>
public record EstateItemDocumentUrlResult(EstateItemDocumentUrlStatus Status, string? Url = null)
{
    public static readonly EstateItemDocumentUrlResult NotFound = new(EstateItemDocumentUrlStatus.NotFound);
    public static readonly EstateItemDocumentUrlResult NotYetAvailable = new(EstateItemDocumentUrlStatus.NotYetAvailable);
    public static readonly EstateItemDocumentUrlResult Unavailable = new(EstateItemDocumentUrlStatus.Unavailable);

    public static EstateItemDocumentUrlResult Available(string url) =>
        new(EstateItemDocumentUrlStatus.Available, url);
}
