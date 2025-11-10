namespace jkn_audit_ai_backend.DTOs
{
    public class PagingDto
    {
        public int Page { get; set; }           // halaman saat ini (1-based)
        public int PageSize { get; set; }       // ukuran halaman
        public int TotalItems { get; set; }     // total baris
        public int TotalPages { get; set; }     // total halaman
        public List<T> Items { get; set; } = new();
    }
}
