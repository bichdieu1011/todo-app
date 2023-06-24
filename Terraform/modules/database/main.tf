
resource "azurerm_mssql_server" "demoapp_sql_server" {
  name                         = "sql-us-demoapp-sqlserver"
  resource_group_name          = var.resource_group
  location                     = var.deploy_location
  version                      = "12.0"
  administrator_login          = var.admin_username
  administrator_login_password = var.admin_password

  tags = {
    environment = var.environment
    department  = var.department
    source      = var.app_source
  }
}

resource "azurerm_mssql_database" "demoapp_sql_database" {
  name      = "sql-us-demoapp-sqldb"
  server_id = azurerm_mssql_server.demoapp_sql_server.id
  collation = "SQL_Latin1_General_CP1_CI_AS"

  max_size_gb    = 5
  sku_name       = "Basic"
  zone_redundant = false

  tags = {
    environment = var.environment
    department  = var.department
    source      = var.app_source
  }
}
