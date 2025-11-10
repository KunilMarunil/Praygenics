using jkn_audit_ai_backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace jkn_audit_ai_backend.Infra
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> opt) : base(opt) { }

        public DbSet<CoreMember> Members => Set<CoreMember>();
        public DbSet<CoreProvider> Providers => Set<CoreProvider>();
        public DbSet<CoreEncounter> Encounters => Set<CoreEncounter>();
        public DbSet<CoreEncounterDx> EncounterDx => Set<CoreEncounterDx>();
        public DbSet<CoreClaim> Claims => Set<CoreClaim>();
        public DbSet<CoreClaimLine> ClaimLines => Set<CoreClaimLine>();
        public DbSet<CoreFraudRule> FraudRules => Set<CoreFraudRule>();
        public DbSet<CoreFraudFinding> FraudFindings => Set<CoreFraudFinding>();
        public DbSet<CoreModelScore> ModelScores => Set<CoreModelScore>();
        public DbSet<CorePayment> Payments => Set<CorePayment>();
        public DbSet<CoreClaimAuditTrail> ClaimAudit => Set<CoreClaimAuditTrail>();

        public DbSet<StgClaimHeader> StgHeaders => Set<StgClaimHeader>();
        public DbSet<StgClaimLine> StgLines => Set<StgClaimLine>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // extension untuk gen_random_uuid()
            b.HasPostgresExtension("pgcrypto");

            // ===== Unique indexes
            b.Entity<CoreMember>().HasIndex(x => x.JknNumber).IsUnique();
            b.Entity<CoreProvider>().HasIndex(x => x.ProviderCode).IsUnique();
            b.Entity<CoreClaim>().HasIndex(x => x.ClaimNumber).IsUnique();

            // ===== Relasi dasar
            b.Entity<CoreEncounter>()
              .HasOne(e => e.Member).WithMany(m => m.Encounters).HasForeignKey(e => e.MemberId);

            b.Entity<CoreEncounter>()
              .HasOne(e => e.Provider).WithMany(p => p.Encounters).HasForeignKey(e => e.ProviderId);

            b.Entity<CoreClaim>()
              .HasOne(c => c.Encounter).WithMany(e => e.Claims).HasForeignKey(c => c.EncounterId);

            b.Entity<CoreClaimLine>()
              .HasOne(l => l.Claim).WithMany(c => c.Lines).HasForeignKey(l => l.ClaimId);

            b.Entity<CoreFraudFinding>()
              .HasOne(f => f.Claim).WithMany(c => c.Findings).HasForeignKey(f => f.ClaimId);

            b.Entity<CoreFraudFinding>()
              .HasOne(f => f.Rule).WithMany(r => r.Findings).HasForeignKey(f => f.RuleId);

            b.Entity<CorePayment>()
              .HasOne(p => p.Claim).WithMany(c => c.Payments).HasForeignKey(p => p.ClaimId);

            b.Entity<CoreModelScore>()
              .HasOne(ms => ms.Claim).WithMany(c => c.ModelScores).HasForeignKey(ms => ms.ClaimId);

            // ===== Index performa
            b.Entity<CoreEncounter>().HasIndex(x => new { x.MemberId, x.EncounterDate });
            b.Entity<CoreEncounter>().HasIndex(x => new { x.ProviderId, x.EncounterDate });
            b.Entity<CoreClaim>().HasIndex(x => x.EncounterId);
            b.Entity<CoreClaim>().HasIndex(x => x.ClaimStatus);
            b.Entity<CoreClaimLine>().HasIndex(x => new { x.CodeType, x.Code });
            b.Entity<CoreFraudFinding>().HasIndex(x => x.ClaimId);
            b.Entity<CoreFraudFinding>().HasIndex(x => x.Status);
            b.Entity<CoreModelScore>().HasIndex(x => new { x.ModelName, x.Version });
            b.Entity<CoreModelScore>().HasIndex(x => x.ClaimId);

            // ===== Kolom computed & jsonb
            b.Entity<CoreClaimLine>()
                .Property(x => x.LineCharge)
                .HasComputedColumnSql("\"Qty\" * \"UnitPrice\"", stored: true);

            b.Entity<CoreClaimLine>().Property(x => x.ExtraJson).HasColumnType("jsonb");
            b.Entity<CoreFraudFinding>().Property(x => x.DetailsJson).HasColumnType("jsonb");
            b.Entity<CoreModelScore>().Property(x => x.FeaturesJson).HasColumnType("jsonb");
            b.Entity<StgClaimHeader>().Property(x => x.RawJson).HasColumnType("jsonb");
            b.Entity<StgClaimLine>().Property(x => x.ExtraJson).HasColumnType("jsonb");

            // ===== (Opsional) Natural key Encounter: kombinasi unik
            b.Entity<CoreEncounter>()
                .HasIndex(x => new { x.MemberId, x.ProviderId, x.EncounterDate, x.EncounterType })
                .IsUnique();

            // ===== Validasi bisnis ringan (server-side via trigger/constraint bisa ditambah di migrasi SQL)
            // ex: totalApproved <= totalCharge → nanti kita enforce via trigger atau di service layer.
        }
    }
}
