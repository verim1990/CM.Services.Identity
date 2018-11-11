1) Prerequisites
install-package Autofac
install-package IdentityServer4
install-package IdentityServer4.EntityFramework
install-package Microsoft.AspNetCore.Identity.EntityFrameworkCore
install-package Microsoft.EntityFrameworkCore
install-package Microsoft.EntityFrameworkCore.SqlServer
install-package Microsoft.EntityFrameworkCore.Design

1.2) References

CM.Services.Identity.Contract
CM.Shared.Kernel

2) Recreating microservice

2.1) Run `dotnet new classlib  -o CM.Services.Identity.Infrastracture -f netcoreapp2.1`
2.2) Move: 
	- directories:
		- *
	- files:
		- readme.recreating.txt
2.3) Set Certificate/idsrv3test.pfx as Embeded resource with Copy always option

3) Initialization (API as startup and defualt project, connection string in appsettings is needed)

Ensure that connection string is added to appSettings.json of CM.Services.Identity.API project. Also ensure that DbContext is included in Startup.cs.
Set CM.Services.Identity.API as startup project and CM.Services.Identity.Infrastracture as default project and run command

add-migration Initial -c ApplicationContext -p CM.Services.Identity.Infrastracture -o Migrations
add-migration Grants -c PersistedGrantDbContext -p CM.Services.Identity.Infrastracture -o Migrations/IdentityServer/PersistedGrantDb
add-migration Config -c ConfigurationDbContext -p CM.Services.Identity.Infrastracture -o Migrations/IdentityServer/ConfigurationDb
