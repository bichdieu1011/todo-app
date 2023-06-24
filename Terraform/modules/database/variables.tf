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


variable "admin_username" {
  description = "configuration to store username of sql admin"
  type = string
  default = ""
}

variable "admin_password" {
  description = "configuration to store password of sql admin"
  type = string
  default = ""
}

variable "application_object_id" {
  type = string
  default = ""
}