namespace jkn_audit_ai_backend.DTOs
{
    public class FindingUpdateRequest
    {
        public string Status { get; set; } = null!; // UNDER_REVIEW/CLEARED/CONFIRMED
        public string? Note { get; set; }
    }
}
