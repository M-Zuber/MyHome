USE MyHome;
GO

IF OBJECT_ID('Accounting.PaymentMethod', 'U') IS NOT NULL
DROP TABLE Accounting.PaymentMethod; 

CREATE TABLE Accounting.PaymentMethod
(
	[Id] INTEGER NOT NULL IDENTITY,
	[Name] NVARCHAR(45) NOT NULL,

	CONSTRAINT PK_PaymentMethod PRIMARY KEY (Id)
);

IF OBJECT_ID('Accounting.ExpenseCategory', 'U') IS NOT NULL
DROP TABLE Accounting.ExpenseCategory; 

CREATE TABLE Accounting.ExpenseCategory 
(
	[Id] INTEGER NOT NULL IDENTITY,
	[Name] NVARCHAR(45) NOT NULL,

	CONSTRAINT PK_ExpenseCategory PRIMARY KEY (Id)
);


IF OBJECT_ID('Accounting.Expense', 'U') IS NOT NULL
DROP TABLE Accounting.Expense; 

CREATE TABLE Accounting.Expense (
	[Id] INTEGER NOT NULL IDENTITY,
	[Amount] DECIMAL(19,4) NOT NULL,
	[Date] DATE NULL,
	[CategoryId] INTEGER NOT NULL,
	[PaymentMethodId] INTEGER NOT NULL,
	[Comments] NVARCHAR(200) NULL DEFAULT(''),

	CONSTRAINT PK_Expense PRIMARY KEY (Id),
	CONSTRAINT FK_Expense_Category FOREIGN KEY (CategoryId)
		REFERENCES Accounting.ExpenseCategory(Id),
	CONSTRAINT FK_Expense_PaymentMethod FOREIGN KEY (PaymentMethodId)
		REFERENCES Accounting.PaymentMethod(Id)
);

IF OBJECT_ID('Accounting.IncomeCategory', 'U') IS NOT NULL
DROP TABLE Accounting.IncomeCategory; 

CREATE TABLE Accounting.IncomeCategory 
(
	[Id] INTEGER NOT NULL IDENTITY,
	[Name] NVARCHAR(45) NOT NULL,

	CONSTRAINT PK_IncomeCategory PRIMARY KEY (Id)
);

IF OBJECT_ID('Accounting.Income', 'U') IS NOT NULL
DROP TABLE Accounting.Income; 

CREATE TABLE Accounting.Income (
	[Id] INTEGER NOT NULL IDENTITY,
	[Amount] DECIMAL(19,4) NOT NULL,
	[Date] DATE NULL,
	[CategoryId] INTEGER NOT NULL,
	[PaymentMethodId] INTEGER NOT NULL,
	[Comments] NVARCHAR(200) NULL DEFAULT(''),

	CONSTRAINT PK_Income PRIMARY KEY (Id),
	CONSTRAINT FK_Income_Category FOREIGN KEY (CategoryId)
		REFERENCES Accounting.IncomeCategory(Id),
	CONSTRAINT FK_Income_PaymentMethod FOREIGN KEY (PaymentMethodId)
		REFERENCES Accounting.PaymentMethod(Id)
);

