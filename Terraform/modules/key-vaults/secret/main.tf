
variable "key_vault_id" {
  type = string
  default = ""
}


variable "secret" {
    type = object({
      connectionString = string
    })
    default = {connectionString=""}
  
}

resource "azurerm_key_vault_secret" "demo_app_key_vault_secret_1" {
  name         = "ConnectionString--SQL"
  value        = var.secret.connectionString
  key_vault_id = var.key_vault_id
}