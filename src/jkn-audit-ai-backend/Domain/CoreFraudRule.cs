namespace jkn_audit_ai_backend.Domain
{
    public class CoreFraudRule
    {
        public Guid RuleId { get; set; } = Guid.NewGuid();
        public string RuleCode { get; set; } = null!; // UNIQUE, ex: OVER_TARIFF
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Severity { get; set; } = 1; // 1..5
        public string RuleSql { get; set; } = ""; // template SQL
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<CoreFraudFinding> Findings { get; set; } = new List<CoreFraudFinding>();
    }
}
