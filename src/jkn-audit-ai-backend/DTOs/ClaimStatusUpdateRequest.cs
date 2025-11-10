namespace jkn_audit_ai_backend.DTOs
{
    public class ClaimStatusUpdateRequest
    {
        public string Status { get; set; } = null!;  // APPROVED/DENIED/ADJUSTED/REVIEW
        public string? PayerNotes { get; set; }
    }
}
