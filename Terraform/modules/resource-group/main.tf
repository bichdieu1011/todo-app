
data "azurerm_client_config" "current" {
}

data "azurerm_builtin_role_definition" "Role" {
  name = "Contributor"
}


resource "azurerm_resource_group" "demoapp_resource_group" {
  name     = var.resource_group
  location = var.deploy_location
   tags = {
    environment = var.environment
    department = var.department
    source = var.app_source
  }
}
