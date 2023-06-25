data "azurerm_subscription" "primary" {}
data "azurerm_client_config" "current" {
}
resource "azurerm_static_site" "demoapp_client" {
  name                = "ss-us-demo-site"
  resource_group_name = var.resource_group
  location            = "eastus2"
}

resource "azurerm_role_assignment" "demoapp_api_st_role_assign_to_azure_devops" {
  role_definition_name = "Contributor"
  scope = azurerm_static_site.demoapp_client.id
  principal_id = data.azurerm_client_config.current.object_id
}

resource "azurerm_service_plan" "demoapp_api_service_plan" {
  name                = "sp-us-demo-servcice-plan"
  resource_group_name = var.resource_group
  location            = var.deploy_location
  os_type             = "Windows"
  sku_name            = "F1"
}

locals {
  tags = {
    environment = var.environment
    department  = var.department
    source      = var.app_source
  }

  app_settings = {
    "Secret--KeyVaultName" = var.KeyVaultName
    "Secret--TenantId"     = var.tenant_id
    "Secret--ClientId"     = var.client_id
    "Secret--ClientSecret" = var.client_secret
    AllowAngularOrigins = azurerm_static_site.demoapp_client.default_host_name
  }
}

resource "azurerm_windows_web_app" "demoapp_api" {
  name                = "wa-us-demo-web-api"
  location            = var.deploy_location
  resource_group_name = var.resource_group
  service_plan_id     = azurerm_service_plan.demoapp_api_service_plan.id
  tags                = local.tags
  
  https_only          = false
  site_config {
    always_on = false
  }

  app_settings = local.app_settings
}

resource "azurerm_role_assignment" "demoapp_api_api_role_assign_to_azure_devops" {
  role_definition_name = "Contributor" 
  scope = azurerm_windows_web_app.demoapp_api.id
  principal_id = data.azurerm_client_config.current.object_id
}