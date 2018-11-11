1.1) Prerequisites

install-package IdentityServer4.AspNetIdentity
install-package MediatR
install-package Microsoft.AspNetCore.Identity
install-package Microsoft.AspNetCore.Authorization
install-package Microsoft.AspNetCore.Identity.EntityFrameworkCore

1.2) References

2) Recreating microservice

2.1) Run `dotnet new classlib  -o CM.Services.Identity.Contract -f netcoreapp2.1`
2.2) Move: 
	- directories:
		- *
	- files:
		- readme.recreating.txt