namespace jkn_audit_ai_backend.Domain
{
    public class CoreClaimLine
    {
        public Guid ClaimLineId { get; set; } = Guid.NewGuid();
        public Guid ClaimId { get; set; }
        public CoreClaim Claim { get; set; } = null!;
        public int LineNo { get; set; }
        public string CodeType { get; set; } = null!; // PROC/DRUG/TARIF
        public string Code { get; set; } = null!;
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }

        // computed column di DB (Qty * UnitPrice)
        public decimal LineCharge { get; set; }
        public decimal ApprovedAmount { get; set; } = 0;

        // jsonb → map sebagai string (EF akan set columnType di DbContext)
        public string? ExtraJson { get; set; }
    }
}
