namespace jkn_audit_ai_backend.DTOs
{
    public record WorklistItemDto(
        Guid ClaimId,
        string ClaimNumber,
        string ProviderName,
        DateOnly EncounterDate,
        decimal TotalCharge,
        decimal PriorityScore,   // max(model_prob, severity/5)
        string TopReason,        // rule paling “nendang” (ex: OVER_TARIFF)
        string FindingStatus     // OPEN/UNDER_REVIEW/...
    );
}
