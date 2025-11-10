namespace jkn_audit_ai_backend.Domain
{
    public class CorePayment
    {
        public Guid PaymentId { get; set; } = Guid.NewGuid();
        public Guid ClaimId { get; set; }
        public CoreClaim Claim { get; set; } = null!;
        public decimal PaymentAmount { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string? Method { get; set; }
        public string? Notes { get; set; }
    }
}
