CREATE DATABASE CalorieTracker;
GO

USE CalorieTracker;
GO

CREATE LOGIN CalorieTrackerLogin
WITH PASSWORD = '<REPLACE_WITH_A_PASSWORD>',
     CHECK_POLICY = ON,
     CHECK_EXPIRATION = ON;
GO

CREATE USER CalorieTrackerUser
FOR LOGIN CalorieTrackerLogin;
GO

CREATE ROLE CalorieTrackerRole;
GO

ALTER ROLE CalorieTrackerRole
ADD MEMBER CalorieTrackerUser;
GO

GRANT EXECUTE ON SCHEMA::dbo TO CalorieTrackerRole;
GO

DENY SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO CalorieTrackerUser;
GO