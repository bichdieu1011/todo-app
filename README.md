# Todo App

This repository is setup to build and deploy in Azure DevOps
To run the project locally, you need to follow these step

1. create Azure resource, using Terraform script in Terraform folder
2. update configuration in project TodoApp.WebApp follows the setting of newly created Azure resources
3. Start running project TodoApp.WebApp
4. update configuration in file env. in folder TodoApp.WebApp/Client follows the setting of newly created Azure resources
5. start the Angular project

# Setup Azure Pipeline
To build the project in Azure using Azure DevOps, a developer could setup the Azure pipelines using these .yml files:
1. azure-pipelines-for-terraform.yml
2. azure-pipelines-buid-deploy-web-api.yml
3. azure-pipelines-build-deploy-web-frontend.yml
>
To deploy the project using Azure DevOps, developers need to setup deploys resources following this order: Terraform -> Web Api -> Web FrontEnd, a sample of a pipeline as below
>Pipeline to release script manage Azure resources using Terraform
>![image](https://github.com/bichdieu1011/todo-app/assets/3540949/4b2e878d-821e-4f54-ac7e-5771e3bd45ed)
>![image](https://github.com/bichdieu1011/todo-app/assets/3540949/064250bf-720d-48ca-82a0-0fb4c847a3e6)


