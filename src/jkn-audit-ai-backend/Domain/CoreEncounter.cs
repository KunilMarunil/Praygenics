namespace jkn_audit_ai_backend.Domain
{
    public class CoreEncounter
    {
        public Guid EncounterId { get; set; } = Guid.NewGuid();
        public Guid MemberId { get; set; }
        public CoreMember Member { get; set; } = null!;
        public Guid ProviderId { get; set; }
        public CoreProvider Provider { get; set; } = null!;
        public DateOnly EncounterDate { get; set; }
        public string? EncounterType { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public ICollection<CoreClaim> Claims { get; set; } = new List<CoreClaim>();
        public ICollection<CoreEncounterDx> Diagnoses { get; set; } = new List<CoreEncounterDx>();
    }
}
