namespace jkn_audit_ai_backend.Domain
{
    public class CoreEncounterDx
    {
        public Guid EncounterDxId { get; set; } = Guid.NewGuid();
        public Guid EncounterId { get; set; }
        public CoreEncounter Encounter { get; set; } = null!;
        public string DxCode { get; set; } = null!;
        public bool IsPrimary { get; set; } = false;
    }
}
