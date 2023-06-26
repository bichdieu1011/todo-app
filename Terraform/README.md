### Init terraform and provider
Prepare your working directory for other commands
```
terraform init
```

### Validate
Check whether the configuration is valid
```
terraform validate
```

### Plan
Show changes required by the current configuration
```
terraform plan
```

### Apply
Create or update infrastructure
```
terraform apply
```

Add options auto approve

```
terraform apply -auto-approve
```

The state file is stored in Azure blob storage, and we call it as backend state.

For each environemnt, we will have a corresponding config file in folder environment which define values for that evinronment.

To trigger deploy infrastructure by environemnt, run the command below
```
terraform apply -var-file="environment/{environment file name}.tfvars"
```