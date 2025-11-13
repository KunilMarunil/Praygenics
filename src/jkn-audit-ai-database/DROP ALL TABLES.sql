-- ============================================================
-- DROP ALL TABLES IN CORRECT DEPENDENCY ORDER
-- ============================================================

DROP TABLE IF EXISTS core_claim_audit_trail CASCADE;
DROP TABLE IF EXISTS core_model_score CASCADE;
DROP TABLE IF EXISTS core_fraud_finding CASCADE;
DROP TABLE IF EXISTS core_fraud_rule CASCADE;
DROP TABLE IF EXISTS core_payment CASCADE;
DROP TABLE IF EXISTS core_claim_line CASCADE;
DROP TABLE IF EXISTS core_claim CASCADE;
DROP TABLE IF EXISTS core_encounter_dx CASCADE;
DROP TABLE IF EXISTS core_encounter CASCADE;
DROP TABLE IF EXISTS core_member CASCADE;
DROP TABLE IF EXISTS core_provider CASCADE;

DROP TABLE IF EXISTS
    core_model_score,
    core_claim_audit_trail,
    core_fraud_finding,
    core_fraud_rule,
    core_payment,
    core_claim_line,
    core_claim,
    core_encounter_dx,
    core_encounter,
    core_provider,
    core_member
CASCADE;

-- ============================================================
-- NOTE:
-- CASCADE digunakan biar foreign key & dependency otomatis dihapus.
-- ============================================================
DO $$
BEGIN
    EXECUTE (
        SELECT string_agg('DROP TABLE IF EXISTS ' || quote_ident(schemaname) || '.' || quote_ident(tablename) || ' CASCADE;', E'\n')
        FROM pg_tables
        WHERE schemaname = 'public'
          AND tablename ~ '^(core_|stg_|dm_)'
    );
END$$;
