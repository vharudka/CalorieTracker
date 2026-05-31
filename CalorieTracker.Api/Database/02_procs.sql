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