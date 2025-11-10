namespace jkn_audit_ai_backend.DTOs
{
    public class ProviderDto
    {
        public string ProviderCode { get; set; } = null!;
        public string ProviderName { get; set; } = null!;
        public string? ProviderType { get; set; }
        public string? RegionCode { get; set; }
    }
}
