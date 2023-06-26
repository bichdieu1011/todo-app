dotnet ef migrations add initialmigration --project TodoApp.DatabaseTodo/App.Database.csproj --startup-project TodoApp.Tools/TodoApp.Tools.csproj

dotnet ef database update --verbose --project TodoApp.Database/TodoApp.Database.csproj   --startup-project TodoApp.Tools/TodoApp.Tools.csproj --migrateDatabase