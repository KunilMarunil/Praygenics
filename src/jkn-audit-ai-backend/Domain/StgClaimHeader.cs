namespace jkn_audit_ai_backend.Domain
{
    public class StgClaimHeader
    {
        public long Id { get; set; }
        public string? SourceFile { get; set; }
        public int RowNo { get; set; }
        public string ClaimNumber { get; set; } = null!;
        public string JknNumber { get; set; } = null!;
        public string ProviderCode { get; set; } = null!;
        public DateOnly EncounterDate { get; set; }
        public string EncounterType { get; set; } = null!;
        public decimal TotalCharge { get; set; }
        public decimal? TotalApproved { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string? RawJson { get; set; } // jsonb
        public DateTime LoadedAt { get; set; } = DateTime.UtcNow;
    }
}
