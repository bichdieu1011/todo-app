data "azurerm_subscription" "primary" {}
data "azurerm_client_config" "current" {
}
resource "azurerm_static_site" "demoapp_client" {
  name                = "ss-us-demo-site"
  resource_group_name = var.resource_group
  location            = "eastus2"
}


resource "azuread_application" "demo_application_registry_for_client" {
  display_name     = "ar-interview-us-democlient-2"
  sign_in_audience = "AzureADMyOrg"


  required_resource_access {
    resource_app_id = "00000003-0000-0000-c000-000000000000" # Microsoft Graph

    resource_access {
      id   = "e1fe6dd8-ba31-4d61-89e7-88639da4683d" #User.Read
      type = "Scope"
    }
  }

  api {
    requested_access_token_version = 1
  }

  single_page_application {
    redirect_uris = ["http://locahost:4200/", "https://${azurerm_static_site.demoapp_client.default_host_name}/"]
  }

  web {
    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = false
    }
  }
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
    "AzureAd__TenantId"     = var.tenant_id
    "AzureAd__scopes"     = "api.scope"
    "AzureAd__ClientId" = var.client_id
    "AzureAd__ClientSecret" = var.client_secret
    "AzureAd__KeyVaultName" = var.KeyVaultName

    AllowAngularOrigins = "https://${azurerm_static_site.demoapp_client.default_host_name}"
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
