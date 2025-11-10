namespace jkn_audit_ai_backend.DTOs
{
    public class FindingDto
    {
        public Guid FindingId { get; set; }
        public string RuleCode { get; set; } = "";      // ex: OVER_TARIFF
        public decimal Score { get; set; }              // 0..1
        public string Status { get; set; } = "";        // OPEN/UNDER_REVIEW/CLEARED/CONFIRMED
        public object? Details { get; set; }            // bentuk bebas (map dari jsonb)
        public DateTime CreatedAt { get; set; }
    }
}
