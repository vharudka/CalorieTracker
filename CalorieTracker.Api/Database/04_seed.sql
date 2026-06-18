DECLARE @UserId UNIQUEIDENTIFIER = '195EE264-892C-4CD6-B97B-82E994565ED4';
DECLARE @Date DATE = '2026-06-01';

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