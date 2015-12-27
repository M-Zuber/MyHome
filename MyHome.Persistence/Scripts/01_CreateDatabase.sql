USE master;
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = '')
DROP DATABASE MyHome;

CREATE DATABASE MyHome;