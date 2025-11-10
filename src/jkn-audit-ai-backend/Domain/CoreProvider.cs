namespace jkn_audit_ai_backend.Domain
{
    public class CoreProvider
    {
        public Guid ProviderId { get; set; } = Guid.NewGuid();
        public string ProviderCode { get; set; } = null!;
        public string ProviderName { get; set; } = null!;
        public string? ProviderType { get; set; }
        public string? RegionCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public ICollection<CoreEncounter> Encounters { get; set; } = new List<CoreEncounter>();
    }
}
