dotnet ef migrations add adduserIdentifierId --project TodoApp.Database/TodoApp.Database.csproj --startup-project TodoApp.Tools/TodoApp.Tools.csproj

dotnet ef migrations remove  --project TodoApp.Database/TodoApp.Database.csproj --startup-project TodoApp.Tools/TodoApp.Tools.csproj


dotnet ef database update --verbose --project TodoApp.Database/TodoApp.Database.csproj   --startup-project TodoApp.Tools/TodoApp.Tools.csproj --migrateDatabase