namespace jkn_audit_ai_backend.DTOs
{
    public class ClaimDetailDto
    {
        // Header klaim
        public Guid ClaimId { get; set; }
        public string ClaimNumber { get; set; } = null!;
        public string Status { get; set; } = "";        // map dari ClaimStatus
        public decimal TotalCharge { get; set; }
        public decimal TotalApproved { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? DecidedAt { get; set; }
        public string? PayerNotes { get; set; }

        // Entitas terkait (diringkas)
        public ProviderDto Provider { get; set; } = new();
        public MemberDto Member { get; set; } = new();
        public EncounterDto Encounter { get; set; } = new();

        // Rincian & temuan
        public List<ClaimLineDto> Lines { get; set; } = new();
        public List<FindingDto> Findings { get; set; } = new();
        public List<PaymentDto> Payments { get; set; } = new();

        // Skor AI (opsional)
        public decimal? ModelProbabilityMax { get; set; }  // maksimum prob dari core_model_score

    }
}
