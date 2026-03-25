-- USERS
CREATE OR REPLACE FUNCTION sp_user_create(
    p_email TEXT,
    p_password_hash TEXT,
    p_password_salt TEXT
)
RETURNS UUID
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
DECLARE 
    new_id UUID;
BEGIN
    SET search_path = public;

    INSERT INTO users (email, password_hash, password_salt)
    VALUES (p_email, p_password_hash, p_password_salt)
    RETURNING id INTO new_id;

    RETURN new_id;
END;
$$;

CREATE OR REPLACE FUNCTION sp_user_get_by_email(
    p_email TEXT
)
RETURNS TABLE (
    Id UUID,
    Email TEXT,
    PasswordHash TEXT,
    PasswordSalt TEXT
)
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
    SET search_path = public;

    RETURN QUERY
    SELECT 
        u.id AS "Id",
        u.email AS "Email",
        u.password_hash AS "PasswordHash",
        u.password_salt AS "PasswordSalt"
    FROM users u
    WHERE u.email = p_email;
END;
$$;

-- FOOD ENTRIES
CREATE OR REPLACE FUNCTION sp_food_entry_create(
    p_user_id UUID,
    p_food_name TEXT,
    p_barcode TEXT,
    p_calories INTEGER,
    p_protein NUMERIC,
    p_fat NUMERIC,
    p_carbohydrates NUMERIC,
    p_sugar NUMERIC,
    p_fiber NUMERIC,
    p_saturated_fat NUMERIC,
    p_sodium NUMERIC,
    p_cholesterol NUMERIC,
    p_potassium NUMERIC,
    p_calcium NUMERIC,
    p_iron NUMERIC,
    p_eaten_at TIMESTAMP
)
RETURNS UUID
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
DECLARE 
    new_id UUID;
BEGIN
    SET search_path = public;

    INSERT INTO food_entries (
        user_id, food_name, barcode,
        calories, protein, fat, carbohydrates,
        sugar, fiber, saturated_fat, sodium, cholesterol,
        potassium, calcium, iron,
        eaten_at
    )
    VALUES (
        p_user_id, p_food_name, p_barcode,
        p_calories, p_protein, p_fat, p_carbohydrates,
        p_sugar, p_fiber, p_saturated_fat, p_sodium, p_cholesterol,
        p_potassium, p_calcium, p_iron,
        p_eaten_at
    )
    RETURNING id INTO new_id;

    RETURN new_id;
END;
$$;

CREATE OR REPLACE FUNCTION sp_food_entry_get_by_user(
    p_user_id UUID
)
RETURNS TABLE (
    Id UUID,
    FoodName TEXT,
    Barcode TEXT,
    Calories INTEGER,
    Protein NUMERIC,
    Fat NUMERIC,
    Carbohydrates NUMERIC,
    Sugar NUMERIC,
    Fiber NUMERIC,
    SaturatedFat NUMERIC,
    Sodium NUMERIC,
    Cholesterol NUMERIC,
    Potassium NUMERIC,
    Calcium NUMERIC,
    Iron NUMERIC,
    EatenAt TIMESTAMP
)
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
    SET search_path = public;

    RETURN QUERY
    SELECT
        id AS "Id",
        food_name AS "FoodName",
        barcode AS "Barcode",
        calories AS "Calories",
        protein AS "Protein",
        fat AS "Fat",
        carbohydrates AS "Carbohydrates",
        sugar AS "Sugar",
        fiber AS "Fiber",
        saturated_fat AS "SaturatedFat",
        sodium AS "Sodium",
        cholesterol AS "Cholesterol",
        potassium AS "Potassium",
        calcium AS "Calcium",
        iron AS "Iron",
        eaten_at AS "EatenAt"
    FROM food_entries
    WHERE user_id = p_user_id
    ORDER BY eaten_at DESC;
END;
$$;

-- FOOD CACHE
CREATE OR REPLACE FUNCTION sp_food_cache_get(
    p_food_name TEXT,
    p_barcode TEXT
)
RETURNS TABLE (
    FoodName TEXT,
    Barcode TEXT,
    Calories INTEGER,
    Protein NUMERIC,
    Fat NUMERIC,
    Carbohydrates NUMERIC,
    Sugar NUMERIC,
    Fiber NUMERIC,
    SaturatedFat NUMERIC,
    Sodium NUMERIC,
    Cholesterol NUMERIC,
    Potassium NUMERIC,
    Calcium NUMERIC,
    Iron NUMERIC,
    UpdatedAt TIMESTAMP
)
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
    SET search_path = public;

    RETURN QUERY
    SELECT
        food_name AS "FoodName",
        barcode AS "Barcode",
        calories AS "Calories",
        protein AS "Protein",
        fat AS "Fat",
        carbohydrates AS "Carbohydrates",
        sugar AS "Sugar",
        fiber AS "Fiber",
        saturated_fat AS "SaturatedFat",
        sodium AS "Sodium",
        cholesterol AS "Cholesterol",
        potassium AS "Potassium",
        calcium AS "Calcium",
        iron AS "Iron",
        updated_at AS "UpdatedAt"
    FROM food_cache
    WHERE (food_name = p_food_name OR p_food_name IS NULL)
      AND (barcode = p_barcode OR p_barcode IS NULL)
    ORDER BY updated_at DESC
    LIMIT 1;
END;
$$;

CREATE OR REPLACE FUNCTION sp_food_cache_upsert(
    p_food_name TEXT,
    p_barcode TEXT,
    p_calories INTEGER,
    p_protein NUMERIC,
    p_fat NUMERIC,
    p_carbohydrates NUMERIC,
    p_sugar NUMERIC,
    p_fiber NUMERIC,
    p_saturated_fat NUMERIC,
    p_sodium NUMERIC,
    p_cholesterol NUMERIC,
    p_potassium NUMERIC,
    p_calcium NUMERIC,
    p_iron NUMERIC
)
RETURNS VOID
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
    SET search_path = public;

    INSERT INTO food_cache (
        food_name, barcode,
        calories, protein, fat, carbohydrates,
        sugar, fiber, saturated_fat, sodium, cholesterol,
        potassium, calcium, iron,
        updated_at
    )
    VALUES (
        p_food_name, p_barcode,
        p_calories, p_protein, p_fat, p_carbohydrates,
        p_sugar, p_fiber, p_saturated_fat, p_sodium, p_cholesterol,
        p_potassium, p_calcium, p_iron,
        NOW()
    )
    ON CONFLICT (food_name, barcode)
    DO UPDATE SET
        calories = EXCLUDED.calories,
        protein = EXCLUDED.protein,
        fat = EXCLUDED.fat,
        carbohydrates = EXCLUDED.carbohydrates,
        sugar = EXCLUDED.sugar,
        fiber = EXCLUDED.fiber,
        saturated_fat = EXCLUDED.saturated_fat,
        sodium = EXCLUDED.sodium,
        cholesterol = EXCLUDED.cholesterol,
        potassium = EXCLUDED.potassium,
        calcium = EXCLUDED.calcium,
        iron = EXCLUDED.iron,
        updated_at = NOW();
END;
$$;