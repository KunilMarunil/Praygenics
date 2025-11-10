namespace jkn_audit_ai_backend.DTOs
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string? Method { get; set; }
        public string? Notes { get; set; }
    }
}
