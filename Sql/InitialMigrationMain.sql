CREATE DATABASE monezy;

\c monezy;

CREATE TABLE "Users" (
	"IdUser" INT NOT NULL,
	"NameUser" VARCHAR(30) NOT NULL,
	"SurnameUser" VARCHAR(50),
	CONSTRAINT "PK_User" PRIMARY KEY ("IdUser")
);

CREATE TABLE "Expenses" (
	"IdExpense" INT NOT NULL,
	"NameExpense" VARCHAR(20) NOT NULL,
	"DescriptionExpense" VARCHAR(50),
	"ValueExpense" FLOAT NOT NULL,
	"TypeExpense" INT NOT NULL,
	"CreatedExpense" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	"DateExpense" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	"UserId" INT NOT NULL,
	CONSTRAINT "PK_Expenses" PRIMARY KEY ("IdExpense"),
	CONSTRAINT "FK_Expenses" FOREIGN KEY ("UserId") REFERENCES "Users" ("IdUser")
);

