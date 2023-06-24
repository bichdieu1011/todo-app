resource "azuread_application" "demo_application_registry" {
  display_name = "ar-interview-us-demoapp-1"
  
}

resource "azuread_service_principal" "demo_app_service_principal" {
  application_id = azuread_application.demo_application_registry.application_id
}

resource "azuread_application_password" "demo_app_secret" {
  application_object_id = azuread_application.demo_application_registry.object_id  
  
}
