namespace jkn_audit_ai_backend.DTOs
{
    public class MemberDto
    {
        public string JknNumber { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}
