
output "client_id" {
  description = "ID of the application registration"
  value       = azuread_application.demo_application_registry.application_id
}

output "client_secret" {
  value = azuread_application_password.demo_app_secret.value
}

output "application_object_id" {
  value = azuread_application.demo_application_registry.object_id
}

output "application_principle_id" {
  value = azuread_service_principal.demo_app_service_principal.object_id
}