1.1) Prerequisites

install-package IdentityServer4

1.2) References

CM.Services.Identity.Concrete
CM.Services.Identity.Infrastructure
CM.Shared.Web

2) Recreating microservice

2.1) Run `dotnet new mvc -o CM.Services.Identity.API`
2.4) Move: 
	- directories:
		- *
	- files:
		- readme.recreating.txt
2.5) Modify:
	- Startup.cs
	- Program.cs
	- appSettings.json 
		- add Private section
2.6) Add docker support (linux)
2.7) Alter:
	- docker-compose.yml
		- add dependencies for other container (sql)
		- add CM.Services.Identity.API to CM.Proxy container into dependencies and link list
	- docker-compose.override.yml
		- add network section to container configuration
		- add shared-variables.env file to container configuration	
		- add connection string to environments variables