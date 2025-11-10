namespace jkn_audit_ai_backend.Domain
{
    public class CoreFraudFinding
    {
        public Guid FindingId { get; set; } = Guid.NewGuid();

        public Guid ClaimId { get; set; }
        public CoreClaim Claim { get; set; } = null!;

        public Guid? RuleId { get; set; }
        public CoreFraudRule? Rule { get; set; }

        public decimal Score { get; set; } = 0; // 0..1
        public string? DetailsJson { get; set; } // jsonb
        public string Status { get; set; } = "OPEN"; // OPEN/UNDER_REVIEW/CLEARED/CONFIRMED

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
