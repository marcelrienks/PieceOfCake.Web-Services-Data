# Scrummage #
**NOTE: Still in Development!**  
This is my Scrum Management Tools suite.  
This is designed to help manage a development team using the Scrum framework.
Tools include Administring users, projects and sprints, as well as Product Backlog planning, Sprint Backlog planning, Sprint Execution and review meetings.

## TECHNOLOGIES AND PATTERNS: ##
Based on ScrummageV1, this version simply updates to the latest tech and libraries.
  
This project is a test bed for implementing the Repository and Unit of Work Pattern for an MVC 5 project.
It uses Code First approach through Entity Framework 6.
  
There are aslo examples of how to use Async Awaits repository and Controller calls (not currently implemented).
Twitter Bootstrap has been used for presentation.

### 3rd Party Libraries ###
* GraphDiff  
Used to update the entire graph tree of a context model, including relations

## DEVELOPMENT: ##
* Create Management of Members
* Create Authentication layer
* Look and Feel
* Manage Projects
* Manage Sprints
* Manage Statuses
* Manage Feature
* Manage Backlog Items
* Manage Tasks
* Manage Attachments

### Todo ###
* Refactor code into seperate projects (Possibly Data, Service and Presentation)
* Add validation to prevent role from being deleted if it's assigned to a member
* Investigate using Mock DbSet instead of having a fake Repository
* verify of username, password on create of member without causing a post back
* add functionality to Member edit for Password and Avatar (including validation)
* investigate creating unit tests for the repository, by creating a fake contaxt/dbset class, which can be dependancy injected in?

## RESOURCES: ##
### Roles ###
This allows administrators to have basic CRUDL functionality of user/member Roles

### Members ###
This allows administrators to have basic CRUDL functionality of Members
Including adding an Avatar to memnbers  

## DOMAIN ##
### Roles ###
Members (n)  
int RoleId (PK, Identity)  
string Title (Required, Max=30)  
string Description (Max=180)  

### Members ###
Roles (n)  
Avatar (1)  
int MemberId (PK, Identity)  
string Name (Required, Max=30)  
string ShortName (Required, Max=3)  
string Username (Required, Max=30, Unique)  
string Password (Required, Max=30)  
string Email (Required)  

### Avatar ###
Member (1)  
int MemberId (PK)  
byte[] Image (Required)  

### Project ###
BacklogItems (n)  
int ProjectId (PK, Identity)  
string Name (Required, Max=30)  
string Description (Max=180)  

### Sprint ###
BacklogItems (n)  
int SprintId (PK, Identity)  
string Name (Required, Max=30)  
DateTime StartDate (Nullable)  
DateTime EndDate (Nullable)  

### Status ###
Features (n)  
BacklogItems (n)  
Tasks (n)  
int StatusId (PK, Identity)  
string Title (Required, Max=30) {To Do, In Progress, Done}  
string Description (Max=180)  

### Feature ###
BacklogItems (n)  
Status (1)  
int FeatureId (PK, Identity)  
string Name (Required, Max=30)  
string Tag (Max=30)  

### BacklogItem ###
Project (1)  
Feature (0.1)  
Sprint (0.1)  
Tasks (n)  
Members (n)  
CreatedByMember (1)  
ChangedByMember (0.1)  
ClosedByMember (1)  
Status (1)  
Attachments (n)  
int BacklogItemId (PK, Identity)  
enum Type (Required) {Feature, Bug, Change} 	//If this does not work, make a linked table  
decimal Priority (Required)	//try using decimal like this <BacklogItemId.Priority> //Or test if EF maintains the sequence of arrays  
DateTime CreatedDate (Required)  
DateTime ChangedDate  
DateTime ClosedDate (Required)  
string Title (Required, Max=30)  
string Description (Max=180)  
enum Complexity (Required) {1pt=1, 2pt=2, 3pt=3, 4pt=4, 5pt=5}	//If this does not work, make a linked table  
string Tag (Max=30)  
int HoursEstimated  
int HoursLogged  

### Task ###
Backlog Item (1)  
Members (n)  
Status (1)  
Attachments (n)  
int TaskId (PK, Identity)  
enum Type (Required) {Code, Review, Investigate} 	//If this does not work, make a linked table  
string Tag (Max=30)  
int HoursEstimated  
int HoursLogged  

### Attachment ###
BacklogItem (1)  
Task (1)  
int AttachmentId (PK, Identity)  
byte[] file (Required)  

## NOTES: ##
### Migration ###
The migration commands to run from the 'Package Manager Console'  

**Enable-Migrations**  
This will enable migrations to be used  

**Add-Migration**  
This will scaffold the next migration based on changes you have made to your model.  
Or this will create the initial migration if run for the first time.  

**Update-Database**  
This will apply any pending changes to and or create the database.  

## ERRORS: ##
### cannot attach the file as database  ###
Ensure the Data Connection is deleted in 'Server Explorer' in VS2012
Ensure Database is deleted in all versions of localdb from 'SQL Server Object Explorer' in VS2012
Ensure the database is not attached in SqlExpress

If this does not solve the issue, open the "Developer Command Propmpt for VisualStudio" under your start/programs menu.
Run the following commands:

sqllocaldb.exe stop v11.0
sqllocaldb.exe delete v11.0
