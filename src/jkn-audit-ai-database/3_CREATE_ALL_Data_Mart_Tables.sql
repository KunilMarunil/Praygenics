-- ================================================
-- DATAMART: Date Dimension
-- ================================================
CREATE TABLE IF NOT EXISTS dm_dim_date (
    d DATE PRIMARY KEY,
    y INT NOT NULL,
    m INT NOT NULL,
    d_in_month INT NOT NULL,
    dow INT NOT NULL,
    is_weekend BOOLEAN NOT NULL
);

-- ================================================
-- DATAMART: Fact Claim
-- ================================================
CREATE TABLE IF NOT EXISTS dm_fact_claim (
    fact_claim_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    claim_id UUID REFERENCES core_claim(claim_id) ON DELETE CASCADE,
    d_encounter DATE REFERENCES dm_dim_date(d),
    provider_id UUID REFERENCES core_provider(provider_id),
    member_id UUID REFERENCES core_member(member_id),
    claim_status VARCHAR,
    total_charge NUMERIC,
    total_approved NUMERIC,
    findings_count INT,
    model_prob_max NUMERIC
);
