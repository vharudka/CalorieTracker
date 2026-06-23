# CalorieTracker
CalorieTracker.Api is the backend for the Calorie Tracker application. It allows users to register and log in, add food entries by barcode, and view their nutrition statistics on a daily, weekly, and monthly basis.

## Requirements
- .NET 10
- SQL Server

## Database setup
The Database folder contains four SQL scripts used to set up the database:
- 01_create_db.sql – creates the database if it does not already exist.
- 02_tables.sql – creates all required tables (users, food entries, and statistics).
- 03_procs.sql – creates stored procedures used by the application.
- 04_seed.sql (optional) – inserts sample data for development and testing. (example user password is 123123)

### Note
If you change the password validation rules in the configuration, you may need to update the 04_seed.sql script so the seeded user password meets the new requirements.

## Configuration
```
"ConnectionStrings": {
  "Default": "REPLACE_WITH_A_CONNECTION_STRING"
},
"Jwt": {
  "Key": "REPLACE_WITH_A_LONG_RANDOM_SECRET_KEY",
  "Issuer": "calorie-tracker",
  "Audience": "calorie-tracker",
  "ExpiresMinutes": 60
},
"CacheSettings": {
  "FoodCacheExpiration": "24:00:00",
  "UserGoalsCacheExpiration": "24:00:00"
},
"PasswordValidation": {
  "RequireUppercase": false,
  "RequireLowercase": false,
  "RequireDigit": false,
  "RequireSpecial": false,
  "MinimumLength": 6
}
```
- The connection string must point to the database created using the SQL scripts.
- The JWT key should be a long, random, secret value.
- CacheSettings control how long food data and user goals stay in memory.
- If you change the PasswordValidation rules and you use the 04_seed.sql script, make sure to update the seeded users so they still satisfy the requirements.

## How to run
After configuring the application and applying the database scripts, you can start the API with:
```
dotnet run --project CalorieTracker.Api
```

## Endpoint documentation
Scalar documentation is available when the API is running at:
```
http://localhost:7027/scalar
```

## UI
The frontend for the Calorie Tracker application is available at:
```
http://localhost:5173/login
```
You can find the UI repository and its README here:
```
https://github.com/vharudka/CalorieTracker.Client
```
