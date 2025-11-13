-- ============================================
-- CORE 1️⃣ PROVIDER
-- ============================================
INSERT INTO core_provider (provider_id, provider_code, provider_name, provider_type, region_code)
VALUES
('1', 'RS001', 'RS Cendana Medika', 'Hospital', 'JKT'),
('2', 'RS002', 'Klinik Sehati', 'Clinic', 'BDG'),
('3', 'RS003', 'RS Kasih Ibu', 'Hospital', 'SBY'),
('4', 'RS004', 'Klinik Sakura', 'Clinic', 'DPS'),
('5', 'RS005', 'RS Harapan Bunda', 'Hospital', 'MDN');

-- ============================================
-- CORE 2️⃣ MEMBER
-- ============================================
INSERT INTO core_member (member_id, national_id, jkn_number, full_name, birth_date, gender, address)
VALUES
(gen_random_uuid(), '3201011234560001', 'JKN001', 'Budi Santoso', '1990-01-15', 'M', 'Jakarta'),
(gen_random_uuid(), '3201011234560002', 'JKN002', 'Siti Rahmawati', '1988-03-20', 'F', 'Bandung'),
(gen_random_uuid(), '3201011234560003', 'JKN003', 'Andi Pratama', '1995-07-12', 'M', 'Surabaya'),
(gen_random_uuid(), '3201011234560004', 'JKN004', 'Dewi Lestari', '1992-11-05', 'F', 'Denpasar'),
(gen_random_uuid(), '3201011234560005', 'JKN005', 'Rizky Kurniawan', '1987-09-22', 'M', 'Medan');

-- ============================================
-- CORE 3️⃣ ENCOUNTER (manual contoh)
-- ============================================
-- Ambil 1 provider & 1 member dulu
INSERT INTO core_encounter (encounter_id, member_id, provider_id, encounter_date, encounter_type)
VALUES
(gen_random_uuid(), (SELECT member_id FROM core_member LIMIT 1), (SELECT provider_id FROM core_provider LIMIT 1), '2025-10-01', 'INPATIENT'),
(gen_random_uuid(), (SELECT member_id FROM core_member OFFSET 1 LIMIT 1), (SELECT provider_id FROM core_provider OFFSET 1 LIMIT 1), '2025-10-02', 'OUTPATIENT');

-- ============================================
-- CORE 4️⃣ ENCOUNTER DX
-- ============================================
INSERT INTO core_encounter_dx (encounter_dx_id, encounter_id, dx_code, is_primary)
VALUES
(gen_random_uuid(), (SELECT encounter_id FROM core_encounter LIMIT 1), 'DX001', TRUE),
(gen_random_uuid(), (SELECT encounter_id FROM core_encounter OFFSET 1 LIMIT 1), 'DX002', FALSE);

-- ============================================
-- CORE 5️⃣ CLAIM
-- ============================================
INSERT INTO core_claim (claim_id, encounter_id, claim_number, total_charge, total_approved, claim_status)
VALUES
(gen_random_uuid(), (SELECT encounter_id FROM core_encounter LIMIT 1), 'CLM0001', 1000000, 900000, 'APPROVED'),
(gen_random_uuid(), (SELECT encounter_id FROM core_encounter OFFSET 1 LIMIT 1), 'CLM0002', 1200000, 950000, 'SUBMITTED');

-- ============================================
-- CORE 6️⃣ CLAIM LINE
-- ============================================
INSERT INTO core_claim_line (claim_line_id, claim_id, line_no, code_type, code, qty, unit_price, approved_amount)
VALUES
(gen_random_uuid(), (SELECT claim_id FROM core_claim LIMIT 1), 1, 'PROC', 'PROC01', 2, 250000, 240000),
(gen_random_uuid(), (SELECT claim_id FROM core_claim OFFSET 1 LIMIT 1), 1, 'DRUG', 'OBAT02', 1, 50000, 45000);

-- ============================================
-- CORE 7️⃣ PAYMENT
-- ============================================
INSERT INTO core_payment (payment_id, claim_id, payment_amount, payment_date, method, notes)
VALUES
(gen_random_uuid(), (SELECT claim_id FROM core_claim LIMIT 1), 800000, CURRENT_DATE, 'TRANSFER', 'Batch 1'),
(gen_random_uuid(), (SELECT claim_id FROM core_claim OFFSET 1 LIMIT 1), 850000, CURRENT_DATE - INTERVAL '1 day', 'TRANSFER', 'Batch 2');

-- ============================================
-- CORE 8️⃣ FRAUD RULE
-- ============================================
INSERT INTO core_fraud_rule (rule_id, rule_code, name, description, severity, rule_sql)
VALUES
(gen_random_uuid(), 'FR001', 'Duplicate Claim', 'Duplicate claim number within short period', 4, 'SELECT * FROM core_claim'),
(gen_random_uuid(), 'FR002', 'Overcharge', 'Charge exceeds threshold', 3, 'SELECT * FROM core_claim WHERE total_charge > 10000000');

-- ============================================
-- CORE 9️⃣ FRAUD FINDING
-- ============================================
INSERT INTO core_fraud_finding (finding_id, claim_id, rule_id, score, details_json)
VALUES
(gen_random_uuid(), (SELECT claim_id FROM core_claim LIMIT 1), (SELECT rule_id FROM core_fraud_rule LIMIT 1), 0.87, '{"note":"Auto generated"}'::jsonb);

-- ============================================
-- CORE 🔟 CLAIM AUDIT TRAIL
-- ============================================
INSERT INTO core_claim_audit_trail (audit_id, claim_id, action, actor, reason)
VALUES
(gen_random_uuid(), (SELECT claim_id FROM core_claim LIMIT 1), 'CREATE', 'system', 'Initial import');

-- ============================================
-- CORE 1️⃣1️⃣ MODEL SCORE
-- ============================================
INSERT INTO core_model_score (model_score_id, claim_id, model_name, version, probability_fraud, features_json)
VALUES
(gen_random_uuid(), (SELECT claim_id FROM core_claim LIMIT 1), 'fraud_model_v1', '1.0', 0.23, '{"feature":"example"}'::jsonb);

-- =====================================================
-- 1️⃣ STAGING HEADER
-- =====================================================
INSERT INTO stg_claim_header (
    stg_claim_header_id, source_file, row_no, claim_number,
    jkn_number, provider_code, encounter_date, encounter_type,
    total_charge, total_approved, submitted_at, raw_json
)
VALUES
(gen_random_uuid(), 'import_october_2025.csv', 1, 'CLM0001', 'JKN001', 'RS001', '2025-10-01', 'INPATIENT', 1000000, 900000, NOW(), '{"source":"RS system"}'::jsonb);

-- =====================================================
-- 2️⃣ STAGING LINE
-- =====================================================
INSERT INTO stg_claim_line (
    stg_claim_line_id, source_file, row_no, claim_number, line_no,
    code_type, code, qty, unit_price, extra_json
)
VALUES
(gen_random_uuid(), 'import_october_2025.csv', 1, 'CLM0001', 1, 'PROC', 'PROC01', 2, 250000, '{"note":"test line"}'::jsonb);

-- =====================================================
-- 1️⃣ DATA MART: DIM DATE
-- =====================================================
INSERT INTO dm_dim_date (d, y, m, d_in_month, dow, is_weekend)
VALUES
('2025-10-01', 2025, 10, 1, 3, FALSE),
('2025-10-02', 2025, 10, 2, 4, FALSE),
('2025-10-03', 2025, 10, 3, 5, FALSE);

-- =====================================================
-- 2️⃣ DATA MART: FACT CLAIM
-- =====================================================
INSERT INTO dm_fact_claim (
    fact_claim_id, claim_id, d_encounter, provider_id, member_id,
    claim_status, total_charge, total_approved, findings_count, model_prob_max
)
VALUES
(gen_random_uuid(), (SELECT claim_id FROM core_claim LIMIT 1), '2025-10-01',
 (SELECT provider_id FROM core_provider LIMIT 1), (SELECT member_id FROM core_member LIMIT 1),
 'APPROVED', 1000000, 900000, 1, 0.85);
