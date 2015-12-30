# Database Setup
1. Install SQL Server
 - You will need to install [SQL Server](http://downloadsqlserverexpress.com). The best option - but the biggest download file - is the `SQL Server Express with Advanced Services` option. This will give you what is needed in order to run the server and a full suite of managment tools.
2. We are using EF Code first. There is no second step.

##Trouble Shooting
If the program still doesn't work, try the following:
- In SQL Server Configuration Manager, under `SQL Server Network Configuration` choose `Protocols for MSSQLSERVER`
 - Ensure that `Named Pipes` and `TCP/IP` are enabled
 - Restart the SQL Server process from the task manager