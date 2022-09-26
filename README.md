# BagStore

## Project Structure

```
/src
- ApplicationCore
- Infrastructure
- Web

/tests
- UnitTests
```

## Packages
```
/ApplicationCore
Install-Package Ardalis.Specification

/Infrastructure
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
Install-Package Ardalis.Specification.EntityFrameworkCore

/Web
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL

```

## Migrations
```
/Infrastructure
Add-Migration IdentityInitialCreate -Context AppIdentityDbContext -OutputDir "Identity\Migrations"
Update-Database -Context AppIdentityDbContext 

Add-Migration BagStoreInitialCreate -Context BagStoreContext -OutputDir "Data\Migrations"
Update-Database -Context BagStoreContext 
```

## Useful Links

* https://github.com/dotnet-architecture/eShopOnWeb
* https://gist.github.com/kottenator/9d936eb3e4e3c3e02598
* https://gist.github.com/yigith/c6f999788b833dc3d22ac6332a053dd1
