# CPSysUDB
[NuGet Packages](https://www.nuget.org/packages/CPSysUDB/)
.NET Framework 4.7.2

[TOCM]

## pt-BR
### Descrição
A biblioteca foi criada com a finalidade de tornar o desenvolvimento de uma aplicação mais ágil, facilitando a criação e atualização do banco de dados, comunicação com SQL Server e MySQL, contolar as rotinas de banco de dados e integração universal entre todas as plataformas compatíveis com C#.

### Motivação
- Criar um sistema em um banco de dados específico mas em algum cliente era necessário outro banco de dados
- Criação e atualização do banco de dados do cliente pela própria aplicação
- Não usar querys de banco de dados

### CPSysSQLFramework1
Esta versão foi a idéia inicial do projeto ainda é necessário criar as tabelas manualmente usando os objetos da classe.

#### Recursos
- Controle total das rotinas de criação e atualização do banco de dados
- Comunicação híbrida com SQL Server e MySQL sem necessidade de criar querys separadas apenas trocando os dados de conexão
- Auto atualização do banco de dados, tabelas e campos
- Criação de PK com auto incremento e FK
- Respeitar regras do banco de dados
- Cria o banco de dados automaticamente
- Create DataBase, Create Table, Insert, Update, Delete, Select
- Where, Order By, Group By, Union, Limit, Joins e Sub Querys
- Suporte para usar um campo de uma tabela no where
- Suporte para funções do banco de dados como GETDATE e DATEADD

#### Compatibilidades
- Suporte para INT, VARCHAR, NVARCHAR, DATETIME, DOUBLE e IMAGE
- Compatível com WEB e Desktop

#### Restições
- Respeitar as regras do banco de dados

#### Como Usar
- Inicie uma conexão
```cs
CPSysSQLFramework1 c = new CPSysSQLFramework1(new CPSysUDB.Configuration.ConnectionData(@"localhost", CPSysUDB.Enums.DataBases.MYSQL, true, "db_teste1", "root", ""));// configure a conexao
```
- Crie o banco de dados
```cs
c.NewDataBase("db_teste");
```
- Crie as tabelas
```cs
List<CPSysSQLFramework1.Campos> campos2 = new List<CPSysSQLFramework1.Campos>();
campos2.Add(new CPSysSQLFramework1.Campos("id", "codigo2", "t2", new CPSysSQLFramework1.TypeCampos(CPSysSQLFramework1.Campos.Types.INT), true, true));// crie uma PK auto incremento
campos2.Add(new CPSysSQLFramework1.Campos("texto", "texto2", "t2", new CPSysSQLFramework1.TypeCampos(CPSysSQLFramework1.Campos.Types.NVARCHAR, "100")));// campo simples com parâmetro
campos2.Add(new CPSysSQLFramework1.Campos("cadastro", "cadastro", "t2", new CPSysSQLFramework1.TypeCampos(CPSysSQLFramework1.Campos.Types.DATETIME)));// campo simples sem parâmetro
campos2.Add(new CPSysSQLFramework1.Campos("idtable", "codigotable", "t2", new CPSysSQLFramework1.TypeCampos(CPSysSQLFramework1.Campos.Types.INT), false, false, false, true, c.getTableByName("tabela"), c.getCampoByName(c.getTableByName("tabela"), "id")));// crie uma FK
CPSysSQLFramework1.Table table2 = new CPSysSQLFramework1.Table("tabela2", "t2", campos2);// declare a tabela
c.NewTable(table2, true, true);
```
- Insira dados
```cs
List<CPSysUDB.DAL.Values> valores3 = new List<CPSysUDB.DAL.Values>();
valores3.Add(new CPSysUDB.DAL.Values("teste"));// valor para campo simples
valores3.Add(new CPSysUDB.DAL.Values(1));
valores3.Add(new CPSysUDB.DAL.Values(CPSysUDB.DAL.Values.Functions.GETDATE));// valor usando funções do banco de dados
c.Insert(c.getTableByName("tabela2"), valores3);
```
- Atualize dados
```cs
List<CPSysUDB.DAL.Values> valores2 = new List<CPSysUDB.DAL.Values>();
valores2.Add(new CPSysUDB.DAL.Values("teste 123"));// valor a ser atualizado
List<CPSysSQLFramework1.Where> where = new List<CPSysSQLFramework1.Where>();
where.Add(new CPSysSQLFramework1.Where(c.getCampoByName(c.getTableByName("tabela"), "id"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(3)));
c.Update(c.getTableByName("tabela"), c.getTableByName("tabela").Campos, valores2, where);
```
- Exclua dados
```cs
List<CPSysSQLFramework1.Where> where2 = new List<CPSysSQLFramework1.Where>();
where2.Add(new CPSysSQLFramework1.Where(c.getCampoByName(c.getTableByName("tabela"), "id"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(4)));// where do delete
c.Delete(c.getTableByName("tabela"), where2);
```
- Consulte dados
```cs
List<CPSysSQLFramework1.Select> select = new List<CPSysSQLFramework1.Select>();
List<CPSysSQLFramework1.Where> where3 = new List<CPSysSQLFramework1.Where>();
List<CPSysSQLFramework1.Join> join = new List<CPSysSQLFramework1.Join>();
List<CPSysSQLFramework1.Campos> campss = new List<CPSysSQLFramework1.Campos>();
foreach (CPSysSQLFramework1.Campos item in c.getTableByName("tabela").Campos)
{
	campss.Add(item);
}
foreach (CPSysSQLFramework1.Campos item in c.getTableByName("tabela2").Campos)
{
	campss.Add(item);
}
join.Add(new CPSysSQLFramework1.Join(c.getTableByName("tabela2"), c.getCampoByName(c.getTableByName("tabela"), "id"), c.getCampoByName(c.getTableByName("tabela2"), "idtable")));
where3.Add(new CPSysSQLFramework1.Where(c.getCampoByName(c.getTableByName("tabela"), "id"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));
CPSysSQLFramework1.Select sel = new CPSysSQLFramework1.Select(c.getTableByName("tabela"), campss, false, join, where3, null, null, CPSysUDB.Enums.Union.NONE, null, null, 1);
select.Add(sel);
DataSet ds = c.SelectValue(select);
if (ds != null)
{
	Console.WriteLine("    COUNT: " + ds.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds.Tables[0].Columns)
    {
		column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine("    " + column);
    foreach (DataRow dataRow in ds.Tables[0].Rows)
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

### CPSysSQLFramework2
Esta versão é a idéia final do projeto com diversas melhorias como o uso de entidades criadas pelo desenvolvedor.

#### Recursos
- Uso de entidades (classes), cada entidade é uma tabela no banco de dados
- Rotina para sempre recriar o banco de dados (muito útil para desenvolvimento) com opção para desabilitar
- Controle total das rotinas de criação e atualização do banco de dados
- Comunicação híbrida com SQL Server e MySQL sem necessidade de criar querys separadas apenas trocando os dados de conexão
- Auto atualização do banco de dados, tabelas e campos
- Criação de PK com auto incremento e FK
- Respeitar regras do banco de dados
- Cria o banco de dados automaticamente
- Create DataBase, Create Table, Insert, Update, Delete, Select
- Where, Order By, Group By, Union, Limit e Joins
- Suporte para funções do banco de dados como GETDATE e DATEADD
- Suporte para usar um campo de uma tabela no where

#### Compatibilidades
- Suporte para int, string, DateTime, double e enum
- Compatível com WEB e Desktop

#### Restições
- Respeitar as regras do banco de dados
- Criar as tabelas em ordem de dependëncia

#### Como Usar
- Inicie uma conexão
```cs
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(new CPSysUDB.Configuration.ConnectionData(@"localhost", CPSysUDB.Enums.DataBases.MYSQL, true, "db_teste2", "root", ""), true);// configure a conexao
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

## en-US
### Description
The library was created with a process of making the development of an application more agile, facilitating the creation and updating of the database, communication with SQL Server and MySQL, controlling as database routines and universal integration between all compatible platforms. Ç #.

### Motivation
- Create a system in a specific database but in some client another database was needed
- Creation and update of the customer database by the application itself
- Do not use database queries

### CPSysSQLFramework1
This version was the initial idea of ​​the project it is still necessary to create tables manually using the objects of the class.

#### Resources
- Full control of database creation and update routines
- Hybrid communication with SQL Server and MySQL without the need to create queries just exchanging connection data
- Auto update of database, tables and fields
- PK creation with auto increment and FK
- Respect database rules
- Creates the database automatically
- Create database, create table, insert, update, delete, select
- Where, Sort by, Group by, Union, Boundary, Joints and Sub queries
- Support for using a field from a table in where
- Support for database functions like GETDATE and DATEDD

#### Compatibilities
- Support for INT, VARCHAR, NVARCHAR, DATETIME, DOUBLE and IMAGE
- Compatible with WEB and Desktop

#### Refunds
- Respect database rules

#### How to use
- Start a connection
```cs
CPSysSQLFramework1 c = new CPSysSQLFramework1(new CPSysUDB.Configuration.ConnectionData(@"localhost", CPSysUDB.Enums.DataBases.MYSQL, true, "db_test1", "root", ""));// configure the connection
```
- Create the database
```cs
c.NewDataBase("db_test");
```
- Create the tables
```cs
List<CPSysSQLFramework1.Fields> fields2 = new List<CPSysSQLFramework1.Fields>();
fields2.Add(new CPSysSQLFramework1.Fields("id", "code2", "t2", new CPSysSQLFramework1.TypeFields(CPSysSQLFramework1.Fields.Types.INT), true, true)); // create an auto-increment PK
fields2.Add(new CPSysSQLFramework1.Fields("text", "text2", "t2", new CPSysSQLFramework1.TypeFields(CPSysSQLFramework1.Fields.Types.NVARCHAR, "100")));// simple field with parameter
fields2.Add(new CPSysSQLFramework1.Fields("registration", "registration", "t2", new CPSysSQLFramework1.TypeFields(CPSysSQLFramework1.Fields.Types.DATETIME)));// simple field without parameter
fields2.Add(new CPSysSQLFramework1.Fields("idtable", "codigotable", "t2", new CPSysSQLFramework1.TypeFields(CPSysSQLFramework1.Fields.Types.INT), false, false, false, true, c.getTableByName("table" ), c.getCampoByName(c.getTableByName("table"), "id"))); // create a FK
CPSysSQLFramework1.Table table2 = new CPSysSQLFramework1.Table("table2", "t2", fields2); // declare table
c.NewTable(table2, true, true);
```
- Enter data
```cs
List<CPSysUDB.DAL.Values> values3 = new List<CPSysUDB.DAL.Values>();
values3.Add(new CPSysUDB.DAL.Values("test")); // value for single field
values3.Add(new CPSysUDB.DAL.Values(1));
values3.Add(new CPSysUDB.DAL.Values(CPSysUDB.DAL.Values.Functions.GETDATE)); // value using database functions
c.Insert(c.getTableByName("table2"), values3);
```
- Update data
```cs
List<CPSysUDB.DAL.Values> values2 = new List<CPSysUDB.DAL.Values>();
values2.Add(new CPSysUDB.DAL.Values("test 123")); // value to be updated
List<CPSysSQLFramework1.Where> where = new List<CPSysSQLFramework1.Where>();
where.Add(new CPSysSQLFramework1.Where(c.getFieldByName(c.getTableByName("table"), "id"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(3)));
c.Update(c.getTableByName("table"), c.getTableByName("table").Fields, values2, where);
```
- Delete data
```cs
List<CPSysSQLFramework1.Where> where2 = new List<CPSysSQLFramework1.Where>();
where2.Add(new CPSysSQLFramework1.Where(c.getFieldByName(c.getTableByName("table"), "id"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(4)));// where delete
c.Delete(c.getTableByName("table"), where2);
```
- See data
```cs
List<CPSysSQLFramework1.Select> select = new List<CPSysSQLFramework1.Select>();
List<CPSysSQLFramework1.Where> where3 = new List<CPSysSQLFramework1.Where>();
List<CPSysSQLFramework1.Join> join = new List<CPSysSQLFramework1.Join>();
List<CPSysSQLFramework1.Fields> campss = new List<CPSysSQLFramework1.Fields>();
foreach (CPSysSQLFramework1.Fields item in c.getTableByName("table").Fields)
{
	campss.Add(item);
}
foreach (CPSysSQLFramework1.Fields item in c.getTableByName("Table2").Fields)
{
	campss.Add(item);
}
join.Add(new CPSysSQLFramework1.Join(c.getTableByName("table2"), c.getFieldByName(c.getTableByName("table"), "id"), c.getFieldByName(c.getTableByName("table2"), " idtable")));
where3.Add(new CPSysSQLFramework1.Where(c.getFieldByName(c.getTableByName("table"), "id"), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(1)));
CPSysSQLFramework1.Select sel = new CPSysSQLFramework1.Select(c.getTableByName("table"), campss, false, join, where3, null, null, CPSysUDB.Enums.Union.NONE, null, null, 1);
select.Add(sel);
DataSet ds = c.SelectValue(select);
if (ds != null)
{
	Console.WriteLine(" COUNT: " + ds.Tables[0].Rows.Count);
    string column = "", rows = "";
    foreach (DataColumn dataColumn in ds.Tables[0].Columns)
    {
		column = column + " # " + dataColumn.Caption;
    }
    Console.WriteLine(" " + column);
    foreach (DataRow dataRow in ds.Tables[0].Rows)
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

### CPSysSQLFramework2
This version is the final idea of ​​the project with several improvements such as the use of entities created by the developer.

#### Resources
- Use of entities (classes), each entity is a table in the database
- Routine to always recreate the database (very useful for development) with option to disable
- Full control of database creation and update routines
- Hybrid communication with SQL Server and MySQL without the need to create separate queries just by exchanging connection data
- Auto update of database, tables and fields
- PK creation with auto increment and FK
- Respect database rules
- Creates the database automatically
- Create DataBase, Create Table, Insert, Update, Delete, Select
- Where, Order By, Group By, Union, Limit and Joins
- Support for database functions like GETDATE and DATEDD
- Support for using a field from a table in where

#### Compatibilities
- Support for int, string, DateTime, double and enum
- Compatible with WEB and Desktop

#### Refunds
- Respect database rules
- Create the tables in order of dependence

#### How to use
- Start a connection
```cs
CPSysSQLFramework2 cPSysSQLFramework2 = new CPSysSQLFramework2(new CPSysUDB.Configuration.ConnectionData(@"localhost", CPSysUDB.Enums.DataBases.MYSQL, true, "db_test2", "root", ""), true);// configure the connection
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
selects1.Add(new CPSysSQLFramework2.Select().NewSelect<Course>(fields2, Join1, wheres4, orders, groups, 5));
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

# CREATOR
[**Follow me**](https://github.com/pinalrafael?tab=followers) for my next creations