# CPSysUDB
[NuGet Packages](https://www.nuget.org/packages/CPSysUDB/)
.NET Framework 4.7.2

## pt-BR
### Descrição
A biblioteca foi criada com a finalidade de tornar o desenvolvimento de uma aplicação mais ágil, facilitando a criação e atualização do banco de dados, comunicação com SQL Server, MySQL, Firebird e SQLite, contolar as rotinas de banco de dados e integração universal entre todas as plataformas compatíveis com C#.

### Motivação
- Criar um sistema em um banco de dados específico mas em algum cliente era necessário outro banco de dados
- Criação e atualização do banco de dados do cliente pela própria aplicação
- Não usar querys de banco de dados

### CPSysSQLFramework2
Esta versão é a idéia final do projeto com diversas melhorias como o uso de entidades criadas pelo desenvolvedor.

#### Recursos
- Uso de entidades (classes), cada entidade é uma tabela no banco de dados
- Rotina para sempre recriar o banco de dados (muito útil para desenvolvimento) com opção para desabilitar
- Controle total das rotinas de criação e atualização do banco de dados
- Comunicação híbrida com SQL Server, MySQL, Firebird e SQLite sem necessidade de criar querys separadas apenas trocando os dados de conexão
- Auto atualização do banco de dados, tabelas e campos
- Criação de PK com auto incremento e FK
- Respeitar regras do banco de dados
- Cria o banco de dados automaticamente
- Create DataBase, Create Table, Insert, Update, Delete, Select
- Where, Order By, Group By, Union, Limit e Joins
- Suporte para funções do banco de dados como GETDATE e DATEADD
- Suporte para usar um campo de uma tabela no where.
- Suporte para comando distinct.
- Criação de funções, eventos e gatilhos dentro da biblioteca.
- Uso das funções min, max, avg, sum, count, upper, lower, trim para os campos do select e where.

#### Compatibilidades
- Suporte para int, string, DateTime, double e enum
- Compatível com WEB e Desktop

#### Restições
- Respeitar as regras do banco de dados
- Criar as tabelas em ordem de dependëncia

#### Como Usar
- Importe a biblioteca
```cs
using CPSysUDB;
```
- Inicie uma conexão
```cs
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLSRV(@"SERVER\SQLEXPRESS", true, "db_teste2", "sa", "*****"), true, 
true);// indica que a conexão será persistente, sendo nexessário usar a função CloseConnection()
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionMYSQL(@"localhost", true, "db_teste2", "root", ""), true, true);
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionFIREBIRD(@"localhost", true, "db_teste2.fdb", "SYSDBA", "masterkey"), true, true);
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLITE(@"db_teste2.sqlite", "Version=3;"), true, true);
```
- Excluír todas as tabelas e criar novamente, é útil para o desenvolvimento mas para produção é necessário remover o comando
```cs
cPSysSQLFramework2.DropAllTables();// reinicie o banco de dados do zero
```
- Crie a sua entidade
```cs
public enum Grade
{
	A, B, C, D, F
}

[CPSysUDB.Attribute.ClassAttribute("e")]// declare a entidade como objeto do banco de dados dando a ela um apelido
public class Enrollment
{
	[CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.PRIMARY_KEY_IDENTITY)]// declare um campo como PK ou PK auto incremento
    public int EnrollmentID { get; set; }
    [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.FOREIGN_KEY, typeof(Course), "CourseID")]// declare um campo como FK
    public int CourseID { get; set; }
    [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.FOREIGN_KEY, typeof(Student), "ID")]
    public int StudentID { get; set; }
    [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL)]// declare um campo normal, é necessário declarar todos os camps que será da tabela
    public Grade Grade { get; set; }
}
```
- Crie as tabelas
```cs
cPSysSQLFramework2.CreateOrAlterTable<Enrollment>();
```
- Insira dados por valor 
```cs
List<CPSysUDB.DAL.Values> valores1 = new List<CPSysUDB.DAL.Values>();
valores1.Add(new CPSysUDB.DAL.Values("teste"));
valores1.Add(new CPSysUDB.DAL.Values(1));
valores1.Add(new CPSysUDB.DAL.Values(20.8));
cPSysSQLFramework2.InsertInto<Course>(valores1);
```
- Insira dados por objeto 
```cs
Enrollment enrollment = new Enrollment();
enrollment.CourseID = 1;
enrollment.StudentID = 1;
enrollment.Grade = Grade.A;
cPSysSQLFramework2.InsertInto<Enrollment>(enrollment);
```
- Atualize dados
```cs
List<CPSysSQLFramework2.Where> wheres1 = new List<CPSysSQLFramework2.Where>();
wheres1.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));
List<string> campos1 = new List<string>();
campos1.Add("Title");// campos a ser atualizado
List<CPSysUDB.DAL.Values> values1 = new List<CPSysUDB.DAL.Values>();
values1.Add(new CPSysUDB.DAL.Values("testes1234"));// valores a ser atualizado respectivamente
cPSysSQLFramework2.Update<Course>(campos1, values1, wheres1);
```
- Exclua dados
```cs
List<CPSysSQLFramework2.Where> wheres3 = new List<CPSysSQLFramework2.Where>();
wheres3.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Enrollment>("EnrollmentID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));// where do delete
cPSysSQLFramework2.Delete<Enrollment>(wheres3);
```
- Consulte dados
```cs
List<CPSysSQLFramework2.Select> selects1 = new List<CPSysSQLFramework2.Select>();
List<CPSysSQLFramework2.Join> Join1 = new List<CPSysSQLFramework2.Join>();
List<CPSysSQLFramework2.Field> campos2 = new List<CPSysSQLFramework2.Field>();
List<CPSysSQLFramework2.Where> wheres4 = new List<CPSysSQLFramework2.Where>();
List<CPSysSQLFramework2.OrderBy> orders = new List<CPSysSQLFramework2.OrderBy>();
List<CPSysSQLFramework2.GroupBy> groups = new List<CPSysSQLFramework2.GroupBy>();
campos2.Add(new CPSysSQLFramework2.Field().NewField<Course>("*"));
campos2.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>("*"));
campos2.Add(new CPSysSQLFramework2.Field().NewField<Student>("*"));
Join1.Add(new CPSysSQLFramework2.Join(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), new CPSysSQLFramework2.Field().NewField<Enrollment>("CourseID")));
Join1.Add(new CPSysSQLFramework2.Join(new CPSysSQLFramework2.Field().NewField<Enrollment>("StudentID"), new CPSysSQLFramework2.Field().NewField<Student>("ID")));
//wheres4.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(new CPSysSQLFramework2.Field().NewField<Student>("ID"))));// use um campo de uma tabela no where
wheres4.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));
orders.Add(new CPSysSQLFramework2.OrderBy().NewOrderBy(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Order.DESC));
groups.Add(new CPSysSQLFramework2.GroupBy().NewGroupBy(new CPSysSQLFramework2.Field().NewField<Student>("ID")));
selects1.Add(new CPSysSQLFramework2.Select().NewSelect<Course>(campos2, Join1, wheres4, orders, groups, 5));
DataSet ds1 = cPSysSQLFramework2.SelectValue(selects1);
if (ds1 != null)
{
	Console.WriteLine("    COUNT: " + ds1.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds1.Tables[0].Columns)
    {
		column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine("    " + column);
    foreach (DataRow dataRow in ds1.Tables[0].Rows)
    {
		rows = "";
        foreach (var item in dataRow.ItemArray)
        {
			rows = rows + " # " + item;
        }
        Console.WriteLine("    " + rows);
    }
}
```
- Criando funções
```cs
cPSysSQLFramework2.DeclareFunction("function_teste", (sender, e) => {
    Console.WriteLine("InsertInto Course Function: " + cPSysSQLFramework2.InsertInto<Course>((List<CPSysUDB.DAL.Values>)sender));
    if (cPSysSQLFramework2.ErrorMsg != "")
    {
        Console.WriteLine("    ERRO: " + cPSysSQLFramework2.ErrorMsg);
    }
});
```
- Executando Funções
```cs
List<CPSysUDB.DAL.Values> valores3 = new List<CPSysUDB.DAL.Values>();
valores3.Add(new CPSysUDB.DAL.Values("ExecuteFunction"));
valores3.Add(new CPSysUDB.DAL.Values(1));
valores3.Add(new CPSysUDB.DAL.Values(20.8));
cPSysSQLFramework2.ExecuteFunction("function_teste", valores3);
```
- Criando gatilhos
```cs
cPSysSQLFramework2.DeclareTrigger<Course>(
new CPSysUDB.Enums.TriggerType[] { CPSysUDB.Enums.TriggerType.CREATE, CPSysUDB.Enums.TriggerType.ALTER, CPSysUDB.Enums.TriggerType.INSERT }, (sender, e) =>
{
    TriggerArgs trg = (TriggerArgs)sender;
    Console.WriteLine("Msg: " + trg.Msg);
    Console.WriteLine("Response: " + trg.Response);
    Console.WriteLine("TriggerType: " + trg.TriggerType);
    Console.WriteLine(trg.Query);
});
```
- Criando eventos
```cs
cPSysSQLFramework2.DeclareEvent(1, CPSysUDB.Enums.TypeEvent.SECONDS, (sender, e) => {
    Console.WriteLine("EVENT: 1 SECONDS " + DateTime.Now);
});
```
- Criando apelidos
```cs
List<CPSysSQLFramework2.Select> selects4 = new List<CPSysSQLFramework2.Select>();
List<CPSysSQLFramework2.Field> campos4 = new List<CPSysSQLFramework2.Field>();
campos4.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>("EnrollmentID", "id"));
campos4.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>("Grade", "grade"));
selects4.Add(new CPSysSQLFramework2.Select().NewSelect<Enrollment>(campos4));
DataSet ds4 = cPSysSQLFramework2.SelectValue(selects4);
if (ds4 != null)
{
    Console.WriteLine("    COUNT: " + ds4.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds4.Tables[0].Columns)
    {
        column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine("    " + column);
    foreach (DataRow dataRow in ds4.Tables[0].Rows)
    {
        rows = "";
        foreach (var item in dataRow.ItemArray)
        {
            rows = rows + " # " + item;
        }
        Console.WriteLine("    " + rows);
    }
}
```
- Criando funções do select
```cs
List<CPSysSQLFramework2.Select> selects5 = new List<CPSysSQLFramework2.Select>();
List<CPSysSQLFramework2.Field> campos5 = new List<CPSysSQLFramework2.Field>();
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.COUNT, "EnrollmentID", "conta"));
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.MAX, "EnrollmentID", "max"));
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.MIN, "EnrollmentID", "min"));
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.SUM, "EnrollmentID", "sum"));
selects5.Add(new CPSysSQLFramework2.Select().NewSelect<Enrollment>(campos5));
DataSet ds5 = cPSysSQLFramework2.SelectValue(selects5);
if (ds5 != null)
{
    Console.WriteLine("    COUNT: " + ds5.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds5.Tables[0].Columns)
    {
        column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine("    " + column);
    foreach (DataRow dataRow in ds5.Tables[0].Rows)
    {
        rows = "";
        foreach (var item in dataRow.ItemArray)
        {
            rows = rows + " # " + item;
        }
        Console.WriteLine("    " + rows);
    }
}
```
- Fechando a conexão
```cs
cPSysSQLFramework2.CloseConnection();
```

## en-US
### Description
The library was created with a process of making the development of an application more agile, facilitating the creation and updating of the database, communication with SQL Server, MySQL, Firebird and SQLite, controlling as database routines and universal integration between all compatible platforms. Ç #.

### Motivation
- Create a system in a specific database but in some client another database was needed
- Creation and update of the customer database by the application itself
- Do not use database queries

### CPSysSQLFramework2
This version is the final idea of ​​the project with several improvements such as the use of entities created by the developer.

#### Resources
- Use of entities (classes), each entity is a table in the database
- Routine to always recreate the database (very useful for development) with option to disable
- Full control of database creation and update routines
- Hybrid communication with SQL Server, MySQL, Firebird and SQLite without the need to create separate queries just by exchanging connection data
- Auto update of database, tables and fields
- PK creation with auto increment and FK
- Respect database rules
- Creates the database automatically
- Create DataBase, Create Table, Insert, Update, Delete, Select
- Where, Order By, Group By, Union, Limit and Joins
- Support for database functions like GETDATE and DATEDD
- Support for using a field from a table in where.
- Support for distinct command.
- Creation of functions, events and triggers within the library.
- Use of min, max, avg, sum, count, upper, lower, trim functions for select and where fields.

#### Compatibilities
- Support for int, string, DateTime, double and enum
- Compatible with WEB and Desktop

#### Refunds
- Respect database rules
- Create the tables in order of dependence

#### How to use
- import a library
```cs
using CPSysUDB;
```
- Start a connection
```cs
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLSRV(@"SERVER\SQLEXPRESS", true, "db_teste2", "sa", "*****"), true, 
true);// indicates that the connection will be persistent, making it necessary to use the CloseConnection() function
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionMYSQL(@"localhost", true, "db_teste2", "root", ""), true, true);
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionFIREBIRD(@"localhost", true, "db_teste2.fdb", "SYSDBA", "masterkey"), true, true);
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLITE(@"db_teste2.sqlite", "Version=3;"), true, true);
```
- Delete all tables and create again, it is useful for development but for production it is necessary to remove the command
```cs
cPSysSQLFramework2.DropAllTables(); // restart the database from scratch
```
- Create your entity
```cs
public enum Grade
{
	A B C D F
}

[CPSysUDB.Attribute.ClassAttribute("e")]// declare the entity as a database object giving it a nickname
public class Enrollment
{
	[CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.PRIMARY_KEY_IDENTITY)] // declare a field as PK or PK auto increment
    public int EnrollmentID { get; set; }
    [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.FOREIGN_KEY, typeof(Course), "CourseID")]// declare a field as FK
    public int CourseID { get; set; }
    [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.FOREIGN_KEY, typeof(Student), "ID")]
    public int StudentID { get; set; }
    [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL)] // declare a normal field, it is necessary to declare all the fields that will be of the table
    public Grade Grade { get; set; }
}
```
- Create the tables
```cs
cPSysSQLFramework2.CreateOrAlterTable<Enrollment>();
```
- Enter data by value
```cs
List<CPSysUDB.DAL.Values> values1 = new List<CPSysUDB.DAL.Values>();
values1.Add(new CPSysUDB.DAL.Values("test"));
values1.Add(new CPSysUDB.DAL.Values(1));
values1.Add(new CPSysUDB.DAL.Values(20.8));
cPSysSQLFramework2.InsertInto<Course>(values1);
```
- Enter data by object
```cs
Enrollment enrollment = new Enrollment();
enrollment.CourseID = 1;
enrollment.StudentID = 1;
enrollment.Grade = Grade.A;
cPSysSQLFramework2.InsertInto<Enrollment>(enrollment);
```
- Update data
```cs
List<CPSysSQLFramework2.Where> wheres1 = new List<CPSysSQLFramework2.Where>();
wheres1.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));
List<string> fields1 = new List<string>();
fields1.Add("Title"); // fields to update
List<CPSysUDB.DAL.Values> values1 = new List<CPSysUDB.DAL.Values>();
values1.Add(new CPSysUDB.DAL.Values("tests1234")); // values ​​to be updated respectively
cPSysSQLFramework2.Update<Course>(fields1, values1, wheres1);
```
- Delete data
```cs
List<CPSysSQLFramework2.Where> wheres3 = new List<CPSysSQLFramework2.Where>();
wheres3.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Enrollment>("EnrollmentID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));// where do delete
cPSysSQLFramework2.Delete<Enrollment>(wheres3);
```
- See data
```cs
List<CPSysSQLFramework2.Select> selects1 = new List<CPSysSQLFramework2.Select>();
List<CPSysSQLFramework2.Join> Join1 = new List<CPSysSQLFramework2.Join>();
List<CPSysSQLFramework2.Field> fields2 = new List<CPSysSQLFramework2.Field>();
List<CPSysSQLFramework2.Where> wheres4 = new List<CPSysSQLFramework2.Where>();
List<CPSysSQLFramework2.OrderBy> orders = new List<CPSysSQLFramework2.OrderBy>();
List<CPSysSQLFramework2.GroupBy> groups = new List<CPSysSQLFramework2.GroupBy>();
fields2.Add(new CPSysSQLFramework2.Field().NewField<Course>("*"));
fields2.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>("*"));
fields2.Add(new CPSysSQLFramework2.Field().NewField<Student>("*"));
Join1.Add(new CPSysSQLFramework2.Join(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), new CPSysSQLFramework2.Field().NewField<Enrollment>("CourseID")));
Join1.Add(new CPSysSQLFramework2.Join(new CPSysSQLFramework2.Field().NewField<Enrollment>("StudentID"), new CPSysSQLFramework2.Field().NewField<Student>("ID")));
//wheres4.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(new CPSysSQLFieldFrame(). NewField<Student>("ID")))); // use a field from a table in where
wheres4.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));
orders.Add(new CPSysSQLFramework2.OrderBy().NewOrderBy(new CPSysSQLFramework2.Field().NewField<Course>("CourseID"), CPSysUDB.Enums.Order.DESC));
groups.Add(new CPSysSQLFramework2.GroupBy().NewGroupBy(new CPSysSQLFramework2.Field().NewField<Student>("ID")));
selects1.Add(new CPSysSQLFramework2.Select().NewSelect<Course>(fields2, Join1, wheres4, orders, groups, 5, CPSysUDB.Enums.Union.NONE, true));
DataSet ds1 = cPSysSQLFramework2.SelectValue(selects1);
if (ds1 != null)
{
	Console.WriteLine(" COUNT: " + ds1.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds1.Tables[0].Columns)
    {
		column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine(" " + column);
    foreach (DataRow dataRow in ds1.Tables[0].Rows)
    {
		rows = "";
        foreach (var item in dataRow.ItemArray)
        {
			rows = rows + " # " + item;
        }
        Console.WriteLine(" " + rows);
    }
}
```
- Creating functions
```cs
cPSysSQLFramework2.DeclareFunction("function_teste", (sender, e) => {
    Console.WriteLine("InsertInto Course Function: " + cPSysSQLFramework2.InsertInto<Course>((List<CPSysUDB.DAL.Values>)sender));
    if (cPSysSQLFramework2.ErrorMsg != "")
    {
        Console.WriteLine("    ERRO: " + cPSysSQLFramework2.ErrorMsg);
    }
});
```
- Executing Functions
```cs
List<CPSysUDB.DAL.Values> valores3 = new List<CPSysUDB.DAL.Values>();
valores3.Add(new CPSysUDB.DAL.Values("ExecuteFunction"));
valores3.Add(new CPSysUDB.DAL.Values(1));
valores3.Add(new CPSysUDB.DAL.Values(20.8));
cPSysSQLFramework2.ExecuteFunction("function_teste", valores3);
```
- Creating triggers
```cs
cPSysSQLFramework2.DeclareTrigger<Course>(
new CPSysUDB.Enums.TriggerType[] { CPSysUDB.Enums.TriggerType.CREATE, CPSysUDB.Enums.TriggerType.ALTER, CPSysUDB.Enums.TriggerType.INSERT }, (sender, e) =>
{
    TriggerArgs trg = (TriggerArgs)sender;
    Console.WriteLine("Msg: " + trg.Msg);
    Console.WriteLine("Response: " + trg.Response);
    Console.WriteLine("TriggerType: " + trg.TriggerType);
    Console.WriteLine(trg.Query);
});
```
- Creating events
```cs
cPSysSQLFramework2.DeclareEvent(1, CPSysUDB.Enums.TypeEvent.SECONDS, (sender, e) => {
    Console.WriteLine("EVENT: 1 SECONDS " + DateTime.Now);
});
```
- Creating nicknames
```cs
List<CPSysSQLFramework2.Select> selects4 = new List<CPSysSQLFramework2.Select>();
List<CPSysSQLFramework2.Field> campos4 = new List<CPSysSQLFramework2.Field>();
campos4.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>("EnrollmentID", "id"));
campos4.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>("Grade", "grade"));
selects4.Add(new CPSysSQLFramework2.Select().NewSelect<Enrollment>(campos4));
DataSet ds4 = cPSysSQLFramework2.SelectValue(selects4);
if (ds4 != null)
{
    Console.WriteLine("    COUNT: " + ds4.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds4.Tables[0].Columns)
    {
        column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine("    " + column);
    foreach (DataRow dataRow in ds4.Tables[0].Rows)
    {
        rows = "";
        foreach (var item in dataRow.ItemArray)
        {
            rows = rows + " # " + item;
        }
        Console.WriteLine("    " + rows);
    }
}
```
- Creating select functions
```cs
List<CPSysSQLFramework2.Select> selects5 = new List<CPSysSQLFramework2.Select>();
List<CPSysSQLFramework2.Field> campos5 = new List<CPSysSQLFramework2.Field>();
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.COUNT, "EnrollmentID", "conta"));
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.MAX, "EnrollmentID", "max"));
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.MIN, "EnrollmentID", "min"));
campos5.Add(new CPSysSQLFramework2.Field().NewField<Enrollment>(CPSysUDB.Enums.FieldFunction.SUM, "EnrollmentID", "sum"));
selects5.Add(new CPSysSQLFramework2.Select().NewSelect<Enrollment>(campos5));
DataSet ds5 = cPSysSQLFramework2.SelectValue(selects5);
if (ds5 != null)
{
    Console.WriteLine("    COUNT: " + ds5.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds5.Tables[0].Columns)
    {
        column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine("    " + column);
    foreach (DataRow dataRow in ds5.Tables[0].Rows)
    {
        rows = "";
        foreach (var item in dataRow.ItemArray)
        {
            rows = rows + " # " + item;
        }
        Console.WriteLine("    " + rows);
    }
}
```
- Closing the connection
```cs
cPSysSQLFramework2.CloseConnection();
```

# CREATOR
[**Follow me**](https://github.com/pinalrafael?tab=followers) for my next creations