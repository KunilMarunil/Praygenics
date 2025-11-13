-- ================================================
-- 1. Provider
-- ================================================
CREATE TABLE IF NOT EXISTS core_provider (
    provider_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    provider_code VARCHAR(50) UNIQUE NOT NULL,
    provider_name VARCHAR(255) NOT NULL,
    provider_type VARCHAR(50),
    region_code VARCHAR(50),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- ================================================
-- 2. Member
-- ================================================
CREATE TABLE IF NOT EXISTS core_member (
    member_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    national_id VARCHAR(50),
    jkn_number VARCHAR(50) UNIQUE NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    birth_date DATE,
    gender VARCHAR(10),
    address TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- ================================================
-- 3. Encounter
-- ================================================
CREATE TABLE IF NOT EXISTS core_encounter (
    encounter_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    member_id UUID REFERENCES core_member(member_id),
    provider_id UUID REFERENCES core_provider(provider_id),
    encounter_date DATE NOT NULL,
    encounter_type VARCHAR(50),
    discharge_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- ================================================
-- 4. Encounter Diagnosis
-- ================================================
CREATE TABLE IF NOT EXISTS core_encounter_dx (
    encounter_dx_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    encounter_id UUID REFERENCES core_encounter(encounter_id) ON DELETE CASCADE,
    dx_code VARCHAR(50) NOT NULL,
    is_primary BOOLEAN DEFAULT FALSE
);

-- ================================================
-- 5. Claim
-- ================================================
CREATE TABLE IF NOT EXISTS core_claim (
    claim_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    encounter_id UUID REFERENCES core_encounter(encounter_id) ON DELETE SET NULL,
    claim_number VARCHAR(100) UNIQUE NOT NULL,
    claim_status VARCHAR(50) DEFAULT 'SUBMITTED',
    total_charge NUMERIC(15,2),
    total_approved NUMERIC(15,2),
    submitted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    decided_at TIMESTAMP,
    payer_notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- ================================================
-- 6. Claim Line
-- ================================================
CREATE TABLE IF NOT EXISTS core_claim_line (
    claim_line_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    claim_id UUID REFERENCES core_claim(claim_id) ON DELETE CASCADE,
    line_no INT NOT NULL,
    code_type VARCHAR(50),
    code VARCHAR(100),
    qty NUMERIC(10,2) DEFAULT 1,
    unit_price NUMERIC(15,2),
    line_charge NUMERIC(15,2) GENERATED ALWAYS AS (qty * unit_price) STORED,
    approved_amount NUMERIC(15,2),
    extra_json JSONB,
    CONSTRAINT uq_claim_line UNIQUE (claim_id, line_no)
);

-- ================================================
-- 7. Payment
-- ================================================
CREATE TABLE IF NOT EXISTS core_payment (
    payment_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    claim_id UUID REFERENCES core_claim(claim_id) ON DELETE CASCADE,
    payment_amount NUMERIC(15,2) NOT NULL,
    payment_date DATE NOT NULL,
    method VARCHAR(50),
    notes TEXT
);

-- ================================================
-- 8. Fraud Rule
-- ================================================
CREATE TABLE IF NOT EXISTS core_fraud_rule (
    rule_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    rule_code VARCHAR(100) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    severity INT CHECK (severity BETWEEN 1 AND 5),
    rule_sql TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- ================================================
-- 9. Fraud Finding
-- ================================================
CREATE TABLE IF NOT EXISTS core_fraud_finding (
    finding_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    claim_id UUID REFERENCES core_claim(claim_id) ON DELETE CASCADE,
    rule_id UUID REFERENCES core_fraud_rule(rule_id),
    score NUMERIC(5,2) CHECK (score BETWEEN 0 AND 1),
    details_json JSONB,
    status VARCHAR(50) DEFAULT 'OPEN',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- ================================================
-- 10. Claim Audit Trail
-- ================================================
CREATE TABLE IF NOT EXISTS core_claim_audit_trail (
    audit_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    claim_id UUID REFERENCES core_claim(claim_id) ON DELETE CASCADE,
    action VARCHAR(50) CHECK (action IN ('APPROVE','DENY','REVIEW','UPDATE','CREATE')),
    actor VARCHAR(100),
    reason TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ================================================
-- 11. Model Score
-- ================================================
CREATE TABLE IF NOT EXISTS core_model_score (
    model_score_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    claim_id UUID REFERENCES core_claim(claim_id) ON DELETE CASCADE,
    model_name VARCHAR(100) NOT NULL,
    version VARCHAR(50),
    probability_fraud NUMERIC(5,4) CHECK (probability_fraud BETWEEN 0 AND 1),
    features_json JSONB,
    scored_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
