-- ================================================
-- STAGING: Claim Header
-- ================================================
CREATE TABLE IF NOT EXISTS stg_claim_header (
    stg_claim_header_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    source_file TEXT NOT NULL,
    row_no INT NOT NULL,
    claim_number TEXT NOT NULL,
    jkn_number TEXT,
    provider_code TEXT,
    encounter_date DATE,
    encounter_type TEXT,
    total_charge NUMERIC,
    total_approved NUMERIC,
    submitted_at TIMESTAMP,
    raw_json JSONB,
    loaded_at TIMESTAMP DEFAULT NOW(),
    CONSTRAINT uq_stg_claim_header_claim_number UNIQUE (claim_number)
);

-- ================================================
-- STAGING: Claim Line
-- ================================================
CREATE TABLE IF NOT EXISTS stg_claim_line (
    stg_claim_line_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    source_file TEXT NOT NULL,
    row_no INT NOT NULL,
    claim_number TEXT NOT NULL REFERENCES stg_claim_header(claim_number) ON DELETE CASCADE,
    line_no INT,
    code_type TEXT,
    code TEXT,
    qty NUMERIC,
    unit_price NUMERIC,
    extra_json JSONB,
    loaded_at TIMESTAMP DEFAULT NOW()
);
