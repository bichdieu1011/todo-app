data "azurerm_client_config" "current" {
}

resource "azurerm_key_vault" "demoapp_key_vault" {
  name                        = "kv-us-demoappforitvb-sc"
  resource_group_name         = var.resource_group
  location                    = var.deploy_location
  sku_name                    = "standard"
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  enabled_for_disk_encryption = true
  soft_delete_retention_days  = 7
  
  tags = {
    environment = var.environment
    department  = var.department
    source      = var.app_source
  }
}


resource "azurerm_key_vault_access_policy" "demoapp_key_vault_access_policy" {
  # name = "Key Management"
  
  
  key_vault_id = azurerm_key_vault.demoapp_key_vault.id

  tenant_id = data.azurerm_client_config.current.tenant_id

  object_id = var.application_object_id

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Purge"
  ]
}

resource "azurerm_key_vault_access_policy" "demoapp_key_vault_access_policy_for_azure_devops" {
  # name = "Key Management"
  
  key_vault_id = azurerm_key_vault.demoapp_key_vault.id
  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = data.azurerm_client_config.current.object_id

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Purge"
  ]
}
