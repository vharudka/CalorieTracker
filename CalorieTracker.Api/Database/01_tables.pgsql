-- USERS
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    password_salt TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

-- FOOD ENTRIES
CREATE TABLE food_entries (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,

    food_name TEXT NOT NULL,
    barcode TEXT, -- optional

    calories INTEGER NOT NULL,
    protein NUMERIC(10,2),
    fat NUMERIC(10,2),
    carbohydrates NUMERIC(10,2),

    sugar NUMERIC(10,2),
    fiber NUMERIC(10,2),
    saturated_fat NUMERIC(10,2),
    sodium NUMERIC(10,2),
    cholesterol NUMERIC(10,2),
    potassium NUMERIC(10,2),
    calcium NUMERIC(10,2),
    iron NUMERIC(10,2),

    eaten_at TIMESTAMP NOT NULL
);

-- FOOD CACHE
CREATE TABLE food_cache (
    food_name TEXT NOT NULL,
    barcode TEXT, -- optional

    -- Core nutrients
    calories INTEGER NOT NULL,
    protein NUMERIC(10,2),
    fat NUMERIC(10,2),
    carbohydrates NUMERIC(10,2),

    -- Detailed nutrients
    sugar NUMERIC(10,2),
    fiber NUMERIC(10,2),
    saturated_fat NUMERIC(10,2),
    sodium NUMERIC(10,2),
    cholesterol NUMERIC(10,2),
    potassium NUMERIC(10,2),
    calcium NUMERIC(10,2),
    iron NUMERIC(10,2),

    updated_at TIMESTAMP NOT NULL DEFAULT NOW(),

    PRIMARY KEY (food_name, barcode)
);