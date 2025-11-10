namespace jkn_audit_ai_backend.DTOs
{
    public class ClaimLineDto
    {
        public int LineNo { get; set; }
        public string CodeType { get; set; } = null!;   // PROC/DRUG/TARIF
        public string Code { get; set; } = null!;
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineCharge { get; set; }         // dihitung di DB (generated)
                                                        // Tambahan ringan dari jsonb kalau mau diangkat ke FE nanti:
        public string? Dose { get; set; }               // ekstraksi dari extra_json (opsional)
        public string? Route { get; set; }              // ekstraksi dari extra_json (opsional)
    }
}
