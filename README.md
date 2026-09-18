# CPSysUDBCore

Database abstraction and SQL query builder library for .NET.

**CPSysUDBCore** provides a unified API for applications that need to work with different database engines, reducing provider-specific code and simplifying database operations, SQL query construction and database management.

The current version is a modern rewrite of the original CPSysUDB library, targeting **.NET 10**.

## Features

- .NET 10
- Database abstraction
- SQL Server
- MySQL
- Firebird
- SQLite
- CRUD operations
- Entity-based database modeling
- Automatic table creation and updates
- Database creation and management
- SQL query builder
- WHERE conditions
- BETWEEN conditions
- ORDER BY
- GROUP BY
- HAVING
- DISTINCT
- JOINs
- Subqueries
- UNION
- Pagination
- SQL functions
- Database functions such as GETDATE and DATEADD
- Field aliases
- Aggregate functions
- Transactions
- Database events
- Database functions
- Database triggers
- Encrypted database configuration

---

# Requirements

- .NET 10 SDK

CPSysUDBCore currently targets:

```text
net10.0
```

---

# Installation

Install the package using the .NET CLI:

```bash
dotnet add package CPSysUDBCore
```

Or using Visual Studio's NuGet Package Manager:

```powershell
Install-Package CPSysUDBCore
```

The package includes support for:

- SQL Server
- MySQL
- Firebird
- SQLite

---

# Quick Start

The main entry point of the library is `ICPSysSQLFrameworkCore`.

A connection can be created using `ConnectionData` and then passed to the framework.

For example, using SQLite:

```csharp
using CPSysUDBCore;
using CPSysUDBCore.DataBase;
using CPSysUDBCore.Enums;

ConnectionData connectionData =
    ConnectionData.CreateConnectionSQLITE(
        "db_test_core.sqlite",
        string.Empty,
        true,
        true,
        true);

ICPSysSQLFrameworkCore framework =
    ICPSysSQLFrameworkCore.Instance(connectionData);
```

The same abstraction can be used with the supported database providers.

---

# Supported Databases

CPSysUDBCore provides a common database abstraction for:

```text
SQL Server
MySQL
Firebird
SQLite
```

The application can select the database provider through `ConnectionData`.

This allows the database layer to be changed without requiring a completely different implementation for each database engine.

Conceptually:

```text
Application
     |
     v
CPSysUDBCore
     |
     +-- SQL Server
     +-- MySQL
     +-- Firebird
     +-- SQLite
```

---

# ConnectionData

`ConnectionData` contains the information required to establish a database connection.

Connection configurations can be created using the provider-specific factory methods:

```text
CreateConnectionSQLSRV
CreateConnectionMYSQL
CreateConnectionFIREBIRD
CreateConnectionSQLITE
```

Example:

```csharp
ConnectionData connectionData =
    ConnectionData.CreateConnectionSQLITE(
        "db_test_core.sqlite",
        string.Empty,
        true,
        true,
        true);
```

After creating the connection configuration:

```csharp
ICPSysSQLFrameworkCore framework =
    ICPSysSQLFrameworkCore.Instance(connectionData);
```

---

# Configuration Files

CPSysUDBCore also provides support for storing database configuration in an encrypted configuration file.

A configuration can be created using:

```csharp
CPSysSQLFrameworkCore.CreateConfigFile(
    "databaseconfig.cfsq",
    connectionData,
    "87654321",
    "123456",
    "12345678");
```

The configuration can later be read:

```csharp
ConnectionData connectionDataRead =
    CPSysSQLFrameworkCore.ReadConfigFile(
        "databaseconfig.cfsq",
        "87654321",
        "12345678");
```

The resulting configuration can then be used by the framework:

```csharp
ICPSysSQLFrameworkCore framework =
    ICPSysSQLFrameworkCore.Instance(connectionDataRead);
```

This approach allows applications to avoid keeping database connection information directly in the source code.

---

# Entity-Based Database Modeling

One of the main features of CPSysUDBCore is the ability to represent database tables through C# classes.

For example:

```csharp
using CPSysUDBCore.Models;

public class Person : Table
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime InsertDate { get; set; }
}
```

Another entity can represent a related table:

```csharp
public class Address : Table
{
    public int Id { get; set; }

    public string Publicplace { get; set; }

    public string TypeAddress { get; set; }

    public int PersonID { get; set; }
}
```

The entities can then be used to create or update their corresponding database tables.

---

# Creating Tables

Tables can be created or updated from the entity definition:

```csharp
framework.CreateOrAlterTable<Person>();
framework.CreateOrAlterTable<Address>();
```

This allows the application to manage database structures from its own model definitions.

It can be particularly useful during application development and deployment.

---

# Database Creation

CPSysUDBCore also provides database management functionality.

The framework can be used to create and manage the database before creating its tables.

```csharp
framework.CreateDataBase();
```

The exact behavior depends on the selected database provider and its capabilities.

---

# CRUD Operations

CPSysUDBCore supports the main database operations:

```text
INSERT
UPDATE
DELETE
SELECT
```

Operations can be represented using the entity/query builder and then executed through the framework.

For example, an entity can be sent to:

```csharp
framework.Execute(person);
```

Queries can be executed using:

```csharp
DataSet? dataSet = framework.Query(person);
```

The query result is returned as a `DataSet`.

---

# SELECT

Entities can be used to construct SELECT statements.

Example:

```csharp
Person person = new Person();

person.Select<Person>();

DataSet? dataSet = framework.Query(person);
```

The result can then be processed using the standard ADO.NET types:

```csharp
if (dataSet != null)
{
    foreach (DataRow row in dataSet.Tables[0].Rows)
    {
        Console.WriteLine(row["Name"]);
    }
}
```

---

# WHERE

Filtering can be added using `Where<T>()`.

Example:

```csharp
Person person = new Person();

person.Select<Person>();

person.Where<Person>(
    "Id",
    Command.EQUALS,
    1);

DataSet? dataSet = framework.Query(person);
```

The condition is represented by the query builder instead of manually concatenating the SQL statement.

---

# BETWEEN

Range conditions can be created using `Between<T>()`.

Conceptually:

```csharp
person.Between<Person>(
    "Id",
    1,
    100);
```

This can be used when a query needs to filter values within a range.

---

# ORDER BY

Results can be ordered using `OrderBy<T>()`.

Example:

```csharp
person.OrderBy<Person>(
    "Name",
    Order.ASC);
```

Descending order can be specified using the corresponding `Order` value.

---

# GROUP BY

Grouping is supported through `GroupBy<T>()`.

Example:

```csharp
person.GroupBy<Person>("Name");
```

---

# HAVING

Grouped queries can also use `HAVING`.

Example:

```csharp
person.Having<Person>(
    "Id",
    Command.BIGGEREQUALS,
    1);
```

`GROUP BY` and `HAVING` can be combined when building aggregate queries.

---

# DISTINCT

Queries can use `DISTINCT` to remove duplicate results.

Example:

```csharp
person.Distinct();
```

---

# JOIN

CPSysUDBCore supports joins between entities.

The query builder provides `Join<T>()` and `Join(...)` functionality for constructing queries involving multiple tables.

Typical SQL relationships can be represented through the entity model and query builder.

Supported join scenarios include:

```text
INNER JOIN
LEFT JOIN
RIGHT JOIN
```

The generated SQL depends on the database provider.

---

# Field Aliases

Fields can be given aliases when constructing a SELECT query.

Example:

```csharp
person.Select<Person>("Id", "id");
person.Select<Person>("Name", "name");
```

This can be useful when the application needs predictable column names in the resulting `DataSet`.

---

# SQL Functions

CPSysUDBCore supports database functions in SELECT expressions.

For example, aggregate functions can be used through `ColumnFunction`.

```csharp
person.Select<Person>(
    "Id",
    ColumnFunction.SUM,
    "Sum");
```

Supported functions include operations such as:

```text
LOWER
UPPER
SUM
COUNT
AVG
MIN
MAX
```

The available functions depend on the database provider.

---

# Database Functions

The query builder can also represent database-specific functions.

For example:

```csharp
DataValueReserved(
    FunctionsType.GETDATE,
    ...);
```

This allows database functions such as `GETDATE` and `DATEADD` to be represented without hard-coding the complete SQL statement in the application.

---

# Subqueries

Subqueries are supported by the query builder.

Example:

```csharp
Person person2 = new Person();

person2.Select<Person>("Id");
person2.Limit(1);

Person person = new Person();

person.Select<Person>();

person.Where<Person>(
    "Id",
    Command.EQUALS,
    person2);
```

This allows a query to use another query as part of its condition.

---

# UNION

CPSysUDBCore supports combining queries using `UNION`.

Additional operations are available for UNION queries, including:

```text
WhereUnion
GroupByUnion
HavingUnion
OrderByUnion
DistinctUnion
LimitUnion
OffsetUnion
```

This makes it possible to build more complex queries while keeping the query construction inside the library.

---

# Pagination

Queries can be limited and offset using:

```csharp
person.Limit(100);
person.Offset(1, 100);
```

This is useful for APIs, lists and applications that need to process large datasets in smaller portions.

---

# INSERT

Entities can also be used to represent data that should be inserted into the database.

Example:

```csharp
Person person = new Person
{
    Name = "Rafael",
    InsertDate = DateTime.Now
};

person.Insert<Person>();

framework.Execute(person);
```

---

# UPDATE

An entity can be configured for an update operation.

Example:

```csharp
Person person = new Person();

person.Update(
    "Name",
    "Rafael");

person.Where<Person>(
    "Id",
    Command.EQUALS,
    1);

framework.Execute(person);
```

---

# DELETE

Delete operations can be combined with conditions.

Example:

```csharp
Person person = new Person();

person.Delete();

person.Where<Person>(
    "Id",
    Command.EQUALS,
    1);

framework.Execute(person);
```

Using a `WHERE` condition is recommended when deleting specific records.

---

# Raw SQL

The framework also supports direct SQL execution through the database abstraction.

For operations that require SQL that is not represented by the query builder, the database layer can execute SQL directly.

The core database abstraction exposes operations such as:

```csharp
database.Execute(sql);
```

and:

```csharp
DataSet? dataSet = database.Query(sql);
```

This provides an escape hatch for database-specific operations while still keeping the database communication inside the CPSysUDBCore layer.

---

# Transactions

CPSysUDBCore provides transaction support for operations that must be treated as a single unit.

Typical transaction logic follows:

```text
Begin
  |
  +-- Operation 1
  |
  +-- Operation 2
  |
  +-- Operation 3
  |
  +-- Success -> Commit
  |
  +-- Error   -> Rollback
```

Transactions are particularly useful when multiple INSERT, UPDATE or DELETE operations must either all succeed or all be reverted.

---

# Database Functions, Events and Triggers

The library also provides functionality for declaring and managing database-related routines.

These include:

- Functions
- Events
- Triggers

These features allow database-specific routines to be managed together with the application's database structure.

The exact implementation and supported functionality depend on the selected database engine.

---

# Resetting the Database

During development, it can be useful to recreate the database tables from the entity definitions.

CPSysUDBCore provides:

```csharp
framework.DropAllTables();
```

This operation is intended primarily for development and testing.

**Do not use `DropAllTables()` in production unless intentionally resetting the database.**

---

# Complete Basic Example

A simple SQLite application can be structured as follows:

```csharp
using CPSysUDBCore;
using CPSysUDBCore.DataBase;
using CPSysUDBCore.Enums;
using CPSysUDBCore.Models;

ConnectionData connectionData =
    ConnectionData.CreateConnectionSQLITE(
        "db_test_core.sqlite",
        string.Empty,
        true,
        true,
        true);

ICPSysSQLFrameworkCore framework =
    ICPSysSQLFrameworkCore.Instance(connectionData);

framework.CreateDataBase();

framework.CreateOrAlterTable<Person>();
framework.CreateOrAlterTable<Address>();

Person person = new Person
{
    Name = "Rafael",
    InsertDate = DateTime.Now
};

person.Insert<Person>();

framework.Execute(person);

person.Select<Person>();

DataSet? dataSet = framework.Query(person);

if (dataSet != null)
{
    foreach (DataRow row in dataSet.Tables[0].Rows)
    {
        Console.WriteLine(row["Name"]);
    }
}
```

---

# Example Project

The repository contains an example project demonstrating the use of the library.

The example focuses on the current `CPSysUDBCore` implementation and can be used as a reference when integrating the package into another .NET application.

The example project demonstrates concepts such as:

```text
ConnectionData
ICPSysSQLFrameworkCore
Person
Address
CreateOrAlterTable
INSERT
SELECT
WHERE
ORDER BY
GROUP BY
HAVING
JOIN
Subquery
UNION
Pagination
```

---

# Architecture

The main abstraction can be represented as:

```text
Application
    |
    v
ICPSysSQLFrameworkCore
    |
    v
Database Abstraction
    |
    +-- SQL Server
    +-- MySQL
    +-- Firebird
    +-- SQLite
```

The query builder works on top of this abstraction:

```text
Entity / Table
      |
      v
Query Builder
      |
      +-- SELECT
      +-- WHERE
      +-- BETWEEN
      +-- GROUP BY
      +-- HAVING
      +-- ORDER BY
      +-- JOIN
      +-- SUBQUERY
      +-- UNION
      +-- PAGINATION
      |
      v
Database Abstraction
      |
      v
Database Provider
```

---

# Why CPSysUDBCore?

The library was originally created to solve a practical problem: applications sometimes need to support different database engines without maintaining completely separate database implementations.

Examples include:

- A project developed using SQL Server that needs to run with SQLite.
- A customer that requires MySQL instead of SQL Server.
- An application that needs to create or update its own database structure.
- Projects that need reusable database and SQL-building components.
- Applications that should keep database-specific code isolated from business logic.

CPSysUDBCore keeps this concept while modernizing the implementation for the current .NET ecosystem.

---

# CPSysUDB Legacy

Before CPSysUDBCore, the project was developed for **.NET Framework 4.7.2** under the name `CPSysUDB`.

The original library included many of the concepts that are now being continued in CPSysUDBCore:

- SQL Server
- MySQL
- Firebird
- SQLite
- Entity-based tables
- CRUD
- WHERE
- ORDER BY
- GROUP BY
- UNION
- JOIN
- Pagination
- Database creation
- Automatic table creation and updates
- Functions
- Events
- Triggers

The legacy implementation remains available for applications that still depend on .NET Framework.

For new .NET applications, **CPSysUDBCore is the recommended project line**, targeting .NET 10.

---

# Migration from CPSysUDB

The new library is a rewrite rather than simply a framework-target change.

The main objective is to preserve the original project's purpose while providing a modern implementation for .NET.

Legacy applications may use:

```text
CPSysUDB
CPSysSQLFramework3
.NET Framework 4.7.2
```

New applications should use:

```text
CPSysUDBCore
ICPSysSQLFrameworkCore
.NET 10
```

The API was reorganized during the migration, so applications migrating from the legacy version should adapt their initialization and database access code to the new API rather than treating the new package as a binary-compatible replacement.

---

# Compatibility

## CPSysUDBCore

```text
.NET 10
```

Supported database engines:

```text
SQL Server
MySQL
Firebird
SQLite
```

## CPSysUDB

Legacy implementation:

```text
.NET Framework 4.7.2
```

---

# Dependencies

CPSysUDBCore uses the following database providers:

```text
FirebirdSql.Data.FirebirdClient
Microsoft.Data.SqlClient
Microsoft.Data.Sqlite.Core
MySql.Data
SQLitePCLRaw.bundle_e_sqlite3
```

These dependencies are installed automatically when CPSysUDBCore is installed through NuGet.

---

# Important Notes

### Database-specific behavior

Although CPSysUDBCore provides a common abstraction, database engines have different SQL features and rules.

Applications should still consider the capabilities and restrictions of the selected database.

### Production databases

Operations that modify database structure or remove tables should be used carefully in production.

In particular:

```csharp
framework.DropAllTables();
```

is intended primarily for development and testing.

### SQL portability

The query builder is designed to reduce provider-specific SQL, but database-specific functions and behavior may still vary between SQL Server, MySQL, Firebird and SQLite.

---

# License

CPSysUDBCore is released under the MIT License.

See the `LICENSE.md` file included with the project for the complete license text.

---

# Author

**Rafael Pinal**

CPSysUDBCore is part of the CPSysUDB project family.

The project is being developed as a reusable database abstraction and SQL query builder for modern .NET applications.