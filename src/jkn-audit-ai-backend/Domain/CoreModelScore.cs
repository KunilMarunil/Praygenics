namespace jkn_audit_ai_backend.Domain
{
    public class CoreModelScore
    {
        public Guid ModelScoreId { get; set; } = Guid.NewGuid();
        public Guid ClaimId { get; set; }
        public CoreClaim Claim { get; set; } = null!;
        public string ModelName { get; set; } = "fraud_lr";
        public string? Version { get; set; } = "v1";
        public decimal ProbabilityFraud { get; set; } // 0..1
        public string? FeaturesJson { get; set; } // jsonb
        public DateTime ScoredAt { get; set; } = DateTime.UtcNow;
    }
}
