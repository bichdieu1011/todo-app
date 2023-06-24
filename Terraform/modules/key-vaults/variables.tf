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


variable "application_object_id" {
  type = string
  default = ""
}