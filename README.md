# Evolve

This repository is configured and used for Asp.net Core Angular development. 
## System Requirements
* SQL
* IIS

## Purpose of the document

These instructions serves as a guide to getting the Evolve Assesment solution up and running on your development machine.

## Getting Started

### Get Source Code
Pull latest source code from the repository
  > https://github.com/cebolamadlaba/Evolve.git
  
#Packages

`"npm install"`

>Evolve\Assessment.Web\ClientApp


# Database Migration Commands

`Add-Migration "<comments>"`
> Scaffolds a migration script for any pending model changes

`Update-Database`
> Applies any pending migrations to the database

`Update-Database -Migration Added_Vendor_Table`
> Applies any pending migrations to the database based on a specific migration
> If you already applied a migration (or several migrations) to the database but need to revert it, you can use the same command to apply migrations, but specify the name of the migration you want to roll back to

`Remove-Migration`
> Sometimes you add a migration and realize you need to make additional changes to your EF Core model before applying it. To remove the last migration, use this command

