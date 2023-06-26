variable "resource_group" {
  description = "Resource group for environment"
  type        = string
  default     = ""
}


variable "deploy_location" {
  description = "Location for deploying"
  type        = string
  default     = ""
}

variable "environment" {
  description = "Name of the environemnt"
  type        = string
  default     = ""
}

variable "department" {
  description = "department for deploying"
  type        = string
  default     = ""
}

variable "app_source" {
  description = "source that deploy this resource"
  type        = string
  default     = ""
}

variable "client_id" {
  description = "client id of application register"
  type        = string
  default     = ""
}


variable "client_secret" {
  description = "value of application secret"
  type = string
  default = ""
}

variable "tenant_id" {
  description = "value of directory"
  type = string
  default = ""
}

variable "keyvault_name" {
  description = "name of key-vault service"
  type = string
  default = ""
}

variable "azure_devops_project_id" {
  description = "id of azure devops project"
  type = string
  default = ""
}