dotnet ef migrations add initialmigration --project TodoApp.DatabaseTodoApp.Database.csproj --startup-project TodoApp.DatabaseTodoApp.Database.csproj

dotnet ef database update --verbose --project TodoApp.Database/TodoApp.Database.csproj   --startup-project TodoApp.Tools/TodoApp.Tools.csproj --migrateDatabase