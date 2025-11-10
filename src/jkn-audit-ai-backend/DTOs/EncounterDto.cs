namespace jkn_audit_ai_backend.DTOs
{
    public class EncounterDto
    {
        public DateOnly EncounterDate { get; set; }
        public string? EncounterType { get; set; }      // RAWAT_JALAN/RAWAT_INAP/IGD
        public DateOnly? DischargeDate { get; set; }
    }
}
