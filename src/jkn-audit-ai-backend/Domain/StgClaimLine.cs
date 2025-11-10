namespace jkn_audit_ai_backend.Domain
{
    public class StgClaimLine
    {
        public long Id { get; set; }
        public string? SourceFile { get; set; }
        public int RowNo { get; set; }
        public string ClaimNumber { get; set; } = null!;
        public int LineNo { get; set; }
        public string CodeType { get; set; } = null!;
        public string Code { get; set; } = null!;
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ExtraJson { get; set; } // jsonb
        public DateTime LoadedAt { get; set; } = DateTime.UtcNow;
    }
}
