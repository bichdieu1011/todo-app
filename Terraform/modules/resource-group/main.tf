
data "azurerm_client_config" "current" {
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

resource "azurerm_role_assignment" "demoapp_api_rg_role_assign_to_azure_devops" {
  role_definition_name = "Contributor"
  scope = azurerm_resource_group.demoapp_resource_group.id
  principal_id = data.azurerm_client_config.current.object_id
}