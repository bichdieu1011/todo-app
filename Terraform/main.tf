# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.48.0"
    }
  }

  required_version = ">= 1.1.0"

  backend "azurerm" {
    # resource_group_name = "rg-us-demo-apps"
    # storage_account_name = "terraformstate"
    # container_name = "tfcontainer"
    # key = "terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

module "resource-group" {
  source          = "./modules/resource-group"
  resource_group  = var.resource_group
  deploy_location = var.deploy_location
}


module "app_db"{
  source = "./modules/database"  
  depends_on      = [module.resource-group]
  admin_username = var.sql_server_admin
  admin_password = var.sql_server_admin_password
  resource_group  = var.resource_group
  deploy_location = var.deploy_location
  environment     = var.environment
  department      = var.department
  app_source      = var.app_source

}

module "app_register"{
  source = "./modules/application-register"  
  depends_on      = [module.resource-group]
}

module "app_key_vault"{
  source = "./modules/key-vaults"
  resource_group  = var.resource_group
  deploy_location = var.deploy_location
  environment     = var.environment
  department      = var.department
  app_source      = var.app_source
  application_object_id = module.app_register.application_object_id
  depends_on      = [module.resource-group, module.app_register]
}

module "app_key_vault_secret"{
  source = "./modules/key-vaults/secret"  
  depends_on      = [module.app_db, module.app_key_vault]
  key_vault_id = module.app_key_vault.key_vault_id
  secret = {
    connectionString = "Server=tcp:${module.app_db.sql_server_name}.database.windows.net,1433;Initial Catalog=${module.app_db.sql_database_name};Persist Security Info=False;User ID=${var.sql_server_admin};Password=${var.sql_server_admin_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}

module "webapplication" {
  source          = "./modules/web-application"
  resource_group  = var.resource_group
  deploy_location = var.deploy_location
  environment     = var.environment
  department      = var.department
  app_source      = var.app_source
  client_id       = module.app_register.client_id
  tenant_id       = var.tenant_id
  client_secret   = module.app_register.client_secret
  depends_on      = [module.resource-group,  module.app_register, module.app_db, module.app_key_vault]
}
