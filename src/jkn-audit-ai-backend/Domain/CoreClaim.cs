namespace jkn_audit_ai_backend.Domain
{
    public class CoreClaim
    {
        public Guid ClaimId { get; set; } = Guid.NewGuid();
        public Guid EncounterId { get; set; }
        public CoreEncounter Encounter { get; set; } = null!;
        public string ClaimNumber { get; set; } = null!;
        public string ClaimStatus { get; set; } = "SUBMITTED"; // SUBMITTED/REVIEW/APPROVED/DENIED/ADJUSTED
        public decimal TotalCharge { get; set; }
        public decimal TotalApproved { get; set; } = 0;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DecidedAt { get; set; }
        public string? PayerNotes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public ICollection<CoreClaimLine> Lines { get; set; } = new List<CoreClaimLine>();
        public ICollection<CoreFraudFinding> Findings { get; set; } = new List<CoreFraudFinding>();
        public ICollection<CorePayment> Payments { get; set; } = new List<CorePayment>();
        public ICollection<CoreModelScore> ModelScores { get; set; } = new List<CoreModelScore>();
    }
}
