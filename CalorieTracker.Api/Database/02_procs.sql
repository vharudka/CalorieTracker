CREATE PROCEDURE spCreateUser
    @Id UNIQUEIDENTIFIER,
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(255),
    @PasswordSalt NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (Id, Email, PasswordHash, PasswordSalt)
    VALUES (@Id, @Email, @PasswordHash, @PasswordSalt);

    SELECT Id, Email, PasswordHash, PasswordSalt
    FROM Users
    WHERE Id = @Id;
END

GO

CREATE PROCEDURE spGetUserByEmail
    @Email NVARCHAR(255)
AS
BEGIN
    SELECT TOP 1 Id, Email, PasswordHash, PasswordSalt
    FROM Users
    WHERE Email = @Email;
END

GO

CREATE PROCEDURE spCreateFoodEntry
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @FoodName NVARCHAR(255),
    @Barcode NVARCHAR(50),
    @Calories INT,
    @Protein DECIMAL(10,2),
    @Fat DECIMAL(10,2),
    @Carbohydrates DECIMAL(10,2),
    @EatenAt DATETIME2
AS
BEGIN
    INSERT INTO FoodEntries (
        Id, UserId, FoodName, Barcode, Calories, Protein, Fat, Carbohydrates, EatenAt
    )
    VALUES (
        @Id, @UserId, @FoodName, @Barcode, @Calories, @Protein, @Fat, @Carbohydrates, @EatenAt
    );

    SELECT *
    FROM FoodEntries
    WHERE Id = @Id;
END

GO

CREATE PROCEDURE spUpdateFoodEntry
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @FoodName NVARCHAR(255),
    @Barcode NVARCHAR(50),
    @Calories INT,
    @Protein DECIMAL(10,2),
    @Fat DECIMAL(10,2),
    @Carbohydrates DECIMAL(10,2),
    @EatenAt DATETIME2
AS
BEGIN
    UPDATE FoodEntries
    SET
        FoodName = @FoodName,
        Barcode = @Barcode,
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
    SELECT 
        ISNULL(SUM(Calories), 0) AS TotalCalories
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
    SELECT 
        ISNULL(SUM(Calories), 0) AS TotalCalories
    FROM FoodEntries
    WHERE UserId = @UserId
      AND YEAR(EatenAt) = @Year
      AND MONTH(EatenAt) = @Month;

    SELECT DailyCalorieLimit
    FROM UserGoals
    WHERE UserId = @UserId;
END