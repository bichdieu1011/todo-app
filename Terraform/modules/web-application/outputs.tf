output "service_plan_id" {
  description = "ID of the VPC"
  value       = azurerm_service_plan.demoapp_api_service_plan
}

output "app_service_id" {
  description = "ID of the VPC"
  value       = azurerm_windows_web_app.demoapp_api
}

output "statis_app_url" {
  value = format("https://%s", azurerm_static_site.demoapp_client.default_host_name)
}

output "api_url" {
  value = format("http://%sazurewebsites.net/", azurerm_windows_web_app.demoapp_api.name)
}

