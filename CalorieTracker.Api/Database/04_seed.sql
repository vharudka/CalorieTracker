DECLARE @PasswordHash NVARCHAR(255) = 'tp6U7uQ08wMJWZCMHwsc/9lhHpE5wzKO56EMO3rxFMk=';
DECLARE @Salt NVARCHAR(255) = 'm82yxN5xZ/DqQcr9Zy3NvA==';

INSERT INTO Users (Username, PasswordHash, PasswordSalt)
VALUES ('user', @PasswordHash, @Salt);

DECLARE @UserId UNIQUEIDENTIFIER;
SELECT @UserId = Id FROM Users WHERE Username = 'user';

DECLARE @Date DATE = '2026-06-01';

INSERT INTO dbo.UserGoals (UserId, DailyCalorieLimit)
VALUES (@UserId, 1550)

INSERT INTO dbo.FoodCache (Barcode, Name, Calories, Protein, Fat, Carbohydrates, UpdatedAt)
VALUES 
('3017624010701', 'Nutella', 539.00, 6.30, 30.90, 57.50, '2026-06-04 15:31:31.660'),
('3270160717323', 'Pizza N°6 - Jambon, Speck, Roquette, Mozzarella', 212.00, 12.00, 7.20, 24.00, '2026-06-18 07:16:22.786'),
('5900512984513', 'Ser Rycerski', 352.33, 26.00, 27.00, 1.10, '2026-06-18 07:01:07.206'),
('5902180580103', 'Kasza owsiana prażona pęczak', 379.00, 12.90, 7.60, 60.30, '2026-06-18 07:47:44.673'),
('5902481019197', 'Kasza bulgur', 348.00, 11.00, 1.50, 70.00, '2026-06-18 07:47:23.523'),
('5902481019951', 'kasza kuskus durum', 355.00, 14.00, 2.00, 68.00, '2026-06-18 07:47:30.826'),
('5906207370074', 'Camembert orzech', 357.00, 17.00, 32.00, 0.50, '2026-06-18 07:46:42.623'),
('5906750296111', 'Jaja ekologiczne', 138.00, 13.00, 9.50, 0.50, '2026-06-18 07:19:13.253'),
('5906827000122', 'Roasted Buckwheat Kasza Gryczana Prażona', 367.00, 13.00, 3.00, 69.00, '2026-06-18 07:47:38.006'),
('5907809284172', 'Mleko UHT 3.2%', 60.00, 3.00, 3.20, 4.70, '2026-06-18 07:05:38.420');

WHILE @Date <= '2026-06-30'
BEGIN
    DECLARE @CalorieFactor FLOAT = 0.8 + RAND() * 0.6;

    INSERT INTO FoodEntries (Id, UserId, Name, Barcode, Grams, Calories, Protein, Fat, Carbohydrates, EatenAt)
    VALUES (NEWID(), @UserId, 'Jaja ekologiczne', '5906750296111',
            150 * @CalorieFactor, 138 * @CalorieFactor, 13 * @CalorieFactor, 9.5 * @CalorieFactor, 0.5 * @CalorieFactor,
            DATEADD(HOUR, 8, CAST(@Date AS DATETIME2)));

    INSERT INTO FoodEntries (Id, UserId, Name, Barcode, Grams, Calories, Protein, Fat, Carbohydrates, EatenAt)
    VALUES (NEWID(), @UserId, 'Kasza bulgur', '5902481019197',
            180 * @CalorieFactor, 348 * @CalorieFactor, 11 * @CalorieFactor, 1.5 * @CalorieFactor, 70 * @CalorieFactor,
            DATEADD(HOUR, 12, CAST(@Date AS DATETIME2)));

    INSERT INTO FoodEntries (Id, UserId, Name, Barcode, Grams, Calories, Protein, Fat, Carbohydrates, EatenAt)
    VALUES (NEWID(), @UserId, 'Ser Rycerski', '5900512984513',
            50 * @CalorieFactor, 352.33 * @CalorieFactor, 26 * @CalorieFactor, 27 * @CalorieFactor, 1.1 * @CalorieFactor,
            DATEADD(HOUR, 12, CAST(@Date AS DATETIME2)));

    INSERT INTO FoodEntries (Id, UserId, Name, Barcode, Grams, Calories, Protein, Fat, Carbohydrates, EatenAt)
    VALUES (NEWID(), @UserId, 'Pizza N°6 - Jambon, Speck, Roquette, Mozzarella', '3270160717323',
            200 * @CalorieFactor, 212 * @CalorieFactor, 12 * @CalorieFactor, 7.2 * @CalorieFactor, 24 * @CalorieFactor,
            DATEADD(HOUR, 18, CAST(@Date AS DATETIME2)));

    IF RAND() > 0.3
        INSERT INTO FoodEntries (Id, UserId, Name, Barcode, Grams, Calories, Protein, Fat, Carbohydrates, EatenAt)
        VALUES (NEWID(), @UserId, 'Nutella', '3017624010701',
                30 * @CalorieFactor, 539 * @CalorieFactor, 6.3 * @CalorieFactor, 30.9 * @CalorieFactor, 57.5 * @CalorieFactor,
                DATEADD(HOUR, 21, CAST(@Date AS DATETIME2)));

    SET @Date = DATEADD(DAY, 1, @Date);
END;