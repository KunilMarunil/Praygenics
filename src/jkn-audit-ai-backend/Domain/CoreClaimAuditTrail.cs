namespace jkn_audit_ai_backend.Domain
{
    public class CoreClaimAuditTrail
    {
        public Guid AuditId { get; set; } = Guid.NewGuid();
        public Guid? ClaimId { get; set; } // boleh null utk aksi global
        public string Action { get; set; } = null!; // ex: CLAIM_STATUS_APPROVED
        public string? Actor { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
