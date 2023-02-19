# Identity Postgres

## Getting Started

- Get the contents of `db.sql` and run this against a Postgres database
- Update the "Database" connection string in `IdentityPostgres/appsettings.json`

## Development

The development of this application takes a database first approach, we use a scaffolding tool to generate the code required to access the database

If you are making any changes to the database structure be sure to amend the `db.sql` script to reflect this and run the scaffolding command below

### Scaffolding

In Visual Studio press `Ctrl+'` to open the package manager console, here we can run the command below to update the code for any database changes

`Scaffold-DbContext Name=ConnectionStrings:Database Npgsql.EntityFrameworkCore.PostgreSQL -NoOnConfiguring -NoPluralize -DataAnnotations -Context IdentityContext -Schema identity -ContextDir Data -OutputDir Data/Tables -Force`