namespace jkn_audit_ai_backend.Domain
{
    public class CoreMember
    {
        public Guid MemberId { get; set; } = Guid.NewGuid();
        public string JknNumber { get; set; } = null!;
        public string? FullName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public ICollection<CoreEncounter> Encounters { get; set; } = new List<CoreEncounter>();
    }
}
