-- Create user
CREATE USER calorie_tracker_user WITH PASSWORD 'your password';

-- Allow connecting to the database
GRANT CONNECT ON DATABASE calorie_tracker TO calorie_tracker_user;

-- Allow using the schema (required to see functions)
GRANT USAGE ON SCHEMA public TO calorie_tracker_user;

-- Allow executing ALL existing functions
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO calorie_tracker_user;

-- Prevent direct table access
REVOKE ALL PRIVILEGES ON ALL TABLES IN SCHEMA public FROM calorie_tracker_user;

-- Prevent direct sequence access (important for SERIAL / IDENTITY)
REVOKE ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public FROM calorie_tracker_user;

-- Prevent future tables from granting privileges accidentally
ALTER DEFAULT PRIVILEGES IN SCHEMA public
REVOKE ALL ON TABLES FROM calorie_tracker_user;

-- Prevent future sequences from granting privileges accidentally
ALTER DEFAULT PRIVILEGES IN SCHEMA public
REVOKE ALL ON SEQUENCES FROM calorie_tracker_user;

-- Prevent future functions from granting EXECUTE accidentally
ALTER DEFAULT PRIVILEGES IN SCHEMA public
REVOKE ALL ON FUNCTIONS FROM calorie_tracker_user;