
# output "connectionString" {
#   description = "ID of the application registration"
#   value       = "Server=tcp:${azurerm_mssql_database.demoapp_sql_server.name}.database.windows.net,1433;Initial Catalog=${azurerm_mssql_database.demoapp_sql_database.name};Persist Security Info=False;User ID=${azurerm_mssql_database.demoapp_sql_server.administrator_login};Password=${azurerm_mssql_database.demoapp_sql_server.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
# }



output "key_vault_id" {
  value = azurerm_key_vault.demoapp_key_vault.id
}


output "key_vault_name" {
  value = azurerm_key_vault.demoapp_key_vault.name
}

