dotnet ef migrations add initialmigration --project ../TodoApp.DatabaseTodoApp.Database.csproj --startup-project ../TodoApp.DatabaseTodoApp.Database.csproj
dotnet ef database update --verbose --project ToDoApp.DataBase   --startup-project ToDoApp.WebApp