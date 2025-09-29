namespace Exizent.CaseManagement.Client.Models
{
    public class NoteResourceRepresentation
    {
        public Guid Id { get; init; }
        public int ActorId { get; init; }
        public string Message { get; init; } = "";
        public DateTime CreatedAt { get; init; }
        public bool IsDeleted { get; init; }
    }
}
