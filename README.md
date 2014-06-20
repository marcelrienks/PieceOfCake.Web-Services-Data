# Scrummage #
NOTE: Still in Development!  
This is my Scrum Management Tools suite.  
This is designed to help manage a development team using the Scrum framework.
Tools include Administring users, projects and sprints, as well as Product Backlog planning, Sprint Backlog planning, Sprint Execution and review meetings.

## Technologies and Patterns used ##
Based on ScrummageV1, this version simply updates to the latest tech and libraries.
This project is a test bed for implementing the Repository and Unit of Work Pattern for an MVC 5 project.
It uses Code First approach through Entity Framework 6.
There are aslo examples of how to use Async Awaits repository and Controller calls (not currently implemented).
Twitter Bootstrap has been used for presentation.

## Development ##
* Create Management of Members
* Create Authentication layer
* Look and Feel
* Manage Projects
* Manage Sprints
* Manage Statuses
* Manage Feature
* Manage Projects
* Manage Backlog Items
* Manage Tasks
* Manage Attachments

### ToDo: ###
* Investigate using Mock DbSet instead of having a fake Repository
* Investigate option of creating view model layer (this will clean up password field on Member for example)
* verify of username, password on create of member without causing a post back
* add functionality to Member edit for Password and Avatar (including validation)
* investigate creating unit tests for the repository, by creating a fake contaxt/dbset class, which can be dependancy injected in?

## Resources ##
### Roles: ###
This allows administrators to have basic CRUDL functionality of user/member Roles

### Members: ###
This allows administrators to have basic CRUDL functionality of Members
Including adding an Avatar to memnbers

## Notes: ##
### Migration ###
The migration commands to run from the 'Package Manager Console'  

**Enable-Migrations**  
This will enable migrations to be used  

**Add-Migration**  
This will scaffold the next migration based on changes you have made to your model.  
Or this will create the initial migration if run for the first time.  

**Update-Database**  
This will apply any pending changes to and or create the database.  

## Errors: ##
### cannot attach the file as database  ###
Ensure the Data Connection is deleted in 'Server Explorer' in VS2012
Ensure Database is deleted in all versions of localdb from 'SQL Server Object Explorer' in VS2012
Ensure the database is not attached in SqlExpress

If this does not solve the issue, open the "Developer Command Propmpt for VisualStudio" under your start/programs menu.
Run the following commands:

sqllocaldb.exe stop v11.0
sqllocaldb.exe delete v11.0
