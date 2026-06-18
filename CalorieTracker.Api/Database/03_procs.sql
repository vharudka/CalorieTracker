CREATE PROCEDURE spCreateUser
    @Id UNIQUEIDENTIFIER,
    @Username NVARCHAR(255),
    @PasswordHash NVARCHAR(255),
    @PasswordSalt NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (Id, Username, PasswordHash, PasswordSalt)
    VALUES (@Id, @Username, @PasswordHash, @PasswordSalt);

    SELECT Id, Username, PasswordHash, PasswordSalt
    FROM Users
    WHERE Id = @Id;
END

GO

CREATE PROCEDURE spGetUserByUsername
    @Username NVARCHAR(255)
AS
BEGIN
    SELECT TOP 1 Id, Username, PasswordHash, PasswordSalt
    FROM Users
    WHERE Username = @Username;
END

GO

CREATE PROCEDURE spCreateFoodEntry
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @Barcode NVARCHAR(20),
    @Grams DECIMAL(10,2),
    @Calories DECIMAL(10,2),
    @Protein DECIMAL(10,2),
    @Fat DECIMAL(10,2),
    @Carbohydrates DECIMAL(10,2),
    @EatenAt DATETIME2
AS
BEGIN
    INSERT INTO FoodEntries (
        Id, UserId, Name, Barcode, Grams, Calories, Protein, Fat, Carbohydrates, EatenAt
    )
    VALUES (
        @Id, @UserId, @Name, @Barcode, @Grams, @Calories, @Protein, @Fat, @Carbohydrates, @EatenAt
    );

    SELECT *
    FROM FoodEntries
    WHERE Id = @Id;
END

GO

CREATE PROCEDURE spUpdateFoodEntry
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @Name NVARCHAR(255),
    @Barcode NVARCHAR(20),
    @Grams DECIMAL(10,2),
    @Calories DECIMAL(10,2),
    @Protein DECIMAL(10,2),
    @Fat DECIMAL(10,2),
    @Carbohydrates DECIMAL(10,2),
    @EatenAt DATETIME2
AS
BEGIN
    UPDATE FoodEntries
    SET
        Name = @Name,
        Barcode = @Barcode,
        Grams = @Grams,
        Calories = @Calories,
        Protein = @Protein,
        Fat = @Fat,
        Carbohydrates = @Carbohydrates,
        EatenAt = @EatenAt
    WHERE Id = @Id AND UserId = @UserId;

    SELECT *
    FROM FoodEntries
    WHERE Id = @Id AND UserId = @UserId;
END

GO

CREATE PROCEDURE spGetFoodEntry
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1 *
    FROM FoodEntries
    WHERE Id = @Id AND UserId = @UserId;
END

GO

CREATE PROCEDURE spGetFoodEntriesByUser
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM FoodEntries
    WHERE UserId = @UserId
    ORDER BY EatenAt DESC;
END

GO

CREATE PROCEDURE spDeleteFoodEntry
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM FoodEntries
    WHERE Id = @Id AND UserId = @UserId;
END

GO

CREATE PROCEDURE spUpsertUserGoals
    @UserId UNIQUEIDENTIFIER,
    @DailyCalorieLimit INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM UserGoals WHERE UserId = @UserId)
    BEGIN
        UPDATE UserGoals
        SET DailyCalorieLimit = @DailyCalorieLimit
        WHERE UserId = @UserId;
    END
    ELSE
    BEGIN
        INSERT INTO UserGoals (UserId, DailyCalorieLimit)
        VALUES (@UserId, @DailyCalorieLimit);
    END

    SELECT *
    FROM UserGoals
    WHERE UserId = @UserId;
END

GO

CREATE PROCEDURE spGetUserGoals
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1 *
    FROM UserGoals
    WHERE UserId = @UserId;
END

GO

CREATE PROCEDURE spGetDailyStats
    @UserId UNIQUEIDENTIFIER,
    @Date DATE
AS
BEGIN
    SELECT 
        ISNULL(SUM(Calories), 0) AS TotalCalories
    FROM FoodEntries
    WHERE UserId = @UserId
      AND CAST(EatenAt AS DATE) = @Date;

    SELECT DailyCalorieLimit
    FROM UserGoals
    WHERE UserId = @UserId;

    SELECT *
    FROM FoodEntries
    WHERE UserId = @UserId
      AND CAST(EatenAt AS DATE) = @Date
    ORDER BY EatenAt DESC;
END

GO

CREATE PROCEDURE spGetWeeklyStats
    @UserId UNIQUEIDENTIFIER,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT ISNULL(SUM(Calories), 0) AS TotalCalories
    FROM FoodEntries
    WHERE UserId = @UserId
      AND CAST(EatenAt AS DATE) BETWEEN @StartDate AND @EndDate;

    SELECT 
		DAY(EatenAt) AS Day,
		ISNULL(SUM(Calories), 0) AS Calories
	FROM FoodEntries
	WHERE UserId = @UserId
	  AND CAST(EatenAt AS DATE) BETWEEN @StartDate AND @EndDate
	GROUP BY DAY(EatenAt)
	ORDER BY Day;

    SELECT 
        ISNULL(SUM(Protein), 0) AS TotalProtein,
        ISNULL(SUM(Fat), 0) AS TotalFat,
        ISNULL(SUM(Carbohydrates), 0) AS TotalCarbohydrates
    FROM FoodEntries
    WHERE UserId = @UserId
      AND CAST(EatenAt AS DATE) BETWEEN @StartDate AND @EndDate;

    SELECT DailyCalorieLimit
    FROM UserGoals
    WHERE UserId = @UserId;
END

GO

CREATE PROCEDURE spGetMonthlyStats
    @UserId UNIQUEIDENTIFIER,
    @Year INT,
    @Month INT
AS
BEGIN
    SELECT ISNULL(SUM(Calories), 0) AS TotalCalories
    FROM FoodEntries
    WHERE UserId = @UserId
      AND YEAR(EatenAt) = @Year
      AND MONTH(EatenAt) = @Month;

    SELECT 
		DAY(EatenAt) AS Day,
		ISNULL(SUM(Calories), 0) AS Calories
	FROM FoodEntries
	WHERE UserId = @UserId
	  AND YEAR(EatenAt) = @Year
	  AND MONTH(EatenAt) = @Month
	GROUP BY DAY(EatenAt)
	ORDER BY Day;

    SELECT 
        ISNULL(SUM(Protein), 0) AS TotalProtein,
        ISNULL(SUM(Fat), 0) AS TotalFat,
        ISNULL(SUM(Carbohydrates), 0) AS TotalCarbohydrates
    FROM FoodEntries
    WHERE UserId = @UserId
      AND YEAR(EatenAt) = @Year
      AND MONTH(EatenAt) = @Month;

    SELECT DailyCalorieLimit
    FROM UserGoals
    WHERE UserId = @UserId;
END

GO

CREATE PROCEDURE spGetFoodCacheByBarcode
    @Barcode NVARCHAR(20)
AS
BEGIN
    SELECT Name,
           Barcode,
           Calories,
           Protein,
           Fat,
           Carbohydrates,
           UpdatedAt
    FROM FoodCache
    WHERE Barcode = @Barcode;
END

GO

CREATE PROCEDURE spInsertFoodCache
    @Name NVARCHAR(255),
    @Barcode NVARCHAR(20),
    @Calories DECIMAL(10,2),
    @Protein DECIMAL(10,2),
    @Fat DECIMAL(10,2),
    @Carbohydrates DECIMAL(10,2),
    @UpdatedAt DATETIME2
AS
BEGIN
    INSERT INTO FoodCache (Name, Barcode, Calories, Protein, Fat, Carbohydrates, UpdatedAt)
    VALUES (@Name, @Barcode, @Calories, @Protein, @Fat, @Carbohydrates, @UpdatedAt);

    SELECT Name,
           Barcode,
           Calories,
           Protein,
           Fat,
           Carbohydrates,
           UpdatedAt
    FROM FoodCache
    WHERE Barcode = @Barcode;
END

GRANT EXECUTE ON OBJECT::dbo.spCreateUser TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetUserByEmail TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spCreateFoodEntry TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spUpdateFoodEntry TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetFoodEntry TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetFoodEntriesByUser TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spDeleteFoodEntry TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spUpsertUserGoals TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetUserGoals TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetDailyStats TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetWeeklyStats TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetMonthlyStats TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spGetFoodCacheByBarcode TO CalorieTrackerRole;
GRANT EXECUTE ON OBJECT::dbo.spInsertFoodCache TO CalorieTrackerRole;