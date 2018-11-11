1) Prerequisites

install-package Microsoft.AspNetCore.Identity
install-package IdentityModel
install-package IdentityServer4
install-package MediatR
install-package FluentValidation

1.2) References

CM.Services.Identity.Contract

2) Recreating microservice

2.1) Run `dotnet new classlib  -o CM.Services.Identity.Concrete -f netcoreapp2.1`
2.2) Move: 
	- directories:
		- *
	- files:
		- readme.recreating.txt
