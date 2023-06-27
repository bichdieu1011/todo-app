resource "random_uuid" "demo_application_scope_id" {}

resource "azuread_application" "demo_application_registry" {
  display_name     = "ar-interview-us-demoapp-1"
  sign_in_audience = "AzureADMyOrg"

  identifier_uris = ["api://ar-interview-us-demoapp-1"]

  required_resource_access {
    resource_app_id = "00000003-0000-0000-c000-000000000000" # Microsoft Graph

    resource_access {
      id   = "e1fe6dd8-ba31-4d61-89e7-88639da4683d" #User.Read
      type = "Scope"
    }
  }

  api {
    requested_access_token_version = 1

    mapped_claims_enabled = true

    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to access example on behalf of the signed-in user."
      admin_consent_display_name = "Access API"
      user_consent_description   = "Allow the application to access example on behalf of the signed-in user."
      user_consent_display_name  = "Access API"
      enabled                    = true
      type                       = "User"
      value                      = "api.scope"
      id                         = random_uuid.demo_application_scope_id.result
    }
  }

}


resource "azuread_service_principal" "demo_app_service_principal" {
  application_id = azuread_application.demo_application_registry.application_id
}

resource "azuread_application_password" "demo_app_secret" {
  application_object_id = azuread_application.demo_application_registry.object_id

}
