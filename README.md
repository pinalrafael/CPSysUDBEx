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

### CPSysSQLFramework3
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
- Where, Order By, Group By, Union, Limit, offset e Joins
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
// cria conexão de forma normal
CPSysSQLFramework3 cPSysSQLFramework3 = new CPSysSQLFramework3(CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLSRV(@"RAFAEL-PC\SQLEXPRESS", true, "db_test3", "sa", "*****"), true,
true);// indica que a conexão será persistente, sendo nexessário usar a função CloseConnection()
CPSysSQLFramework3 cPSysSQLFramework3 = new CPSysSQLFramework3(CPSysUDB.Configuration.ConnectionData.CreateConnectionMYSQL(@"localhost", true, "db_test3", "root", ""), true, true);
CPSysSQLFramework3 cPSysSQLFramework3 = new CPSysSQLFramework3(CPSysUDB.Configuration.ConnectionData.CreateConnectionFIREBIRD(@"localhost", true, "db_test3.fdb", "SYSDBA", "masterkey"), true, true);
CPSysSQLFramework3 cPSysSQLFramework3 = new CPSysSQLFramework3(CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLITE(@"db_test3.sqlite", "Version=3;"), true, true);

// cria conexão encriptada
CPSysSQLFramework3.CreateConfigFile("darabaseconfig.cfsq", CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLSRV(@"RAFAEL-PC\SQLEXPRESS", true, "db_test3", "sa", "****"), "87654321", "123456", "12345678");
CPSysSQLFramework3.CreateConfigFile("darabaseconfig.cfsq", CPSysUDB.Configuration.ConnectionData.CreateConnectionMYSQL(@"localhost", true, "db_test3", "root", ""), "87654321", "123456", "12345678");
CPSysSQLFramework3.CreateConfigFile("darabaseconfig.cfsq", CPSysUDB.Configuration.ConnectionData.CreateConnectionFIREBIRD(@"localhost", true, "db_test3.fdb", "SYSDBA", "masterkey"), "87654321", "123456", "12345678");
CPSysSQLFramework3.CreateConfigFile("darabaseconfig.cfsq", CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLITE(@"db_test3.sqlite", "Version=3;"), "87654321", "123456", "12345678");

CPSysSQLFramework3 cPSysSQLFramework3 = new CPSysSQLFramework3("darabaseconfig.cfsq", "87654321", "12345678", "123456", true, true);
```
- Excluír todas as tabelas e criar novamente, é útil para o desenvolvimento mas para produção é necessário remover o comando
```cs
CPSysSQLFramework3.DropAllTables();// reinicie o banco de dados do zero
```
- Crie a sua entidade
```cs
public enum Grade
{
	A, B, C, D, F
}

[CPSysUDB.Attribute.ClassAttribute("e",
"prefixo")]// declare a entidade como objeto do banco de dados dando a ela um apelido e prefixo
public class Enrollment : Table
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
CPSysSQLFramework3.CreateOrAlterTable<Enrollment>();
```
- Insira dados por objeto 
```cs
Enrollment enrollment = new Enrollment();
enrollment.CourseID = 1;
enrollment.StudentID = 1;
enrollment.Grade = Grade.A;
enrollment.Insert<Enrollment>();
cPSysSQLFramework2.Execute(enrollment);
```
- Atualize dados
```cs
Enrollment enrollment = new Enrollment();
enrollment.Update("Grade", Grade.A);
enrollment.Where<Enrollment>("Id", Command.EQUALS, 1);

cPSysSQLFramework2.Execute(enrollment);
```
- Exclua dados
```cs
Enrollment enrollment = new Enrollment();
enrollment.Delete();
enrollment.Where<Enrollment>("Id", Command.EQUALS, 1);

cPSysSQLFramework2.Execute(enrollment);
```
- Consulte dados
```cs
Enrollment enrollment = new Enrollment();
enrollment.Select<Enrollment>();
enrollment.Where<Enrollment>("id", CPSysUDB.Enums.Command.BIGGEREQUALS, 1);
enrollment.OrderBy<Enrollment>("id", CPSysUDB.Enums.Order.ASC);
enrollment.GroupBy<Enrollment>("id");
enrollment.Distinct();
enrollment.Limit(1000);
enrollment.Offset(1, 1000);

DataSet ds1 = cPSysSQLFramework3.Query(acc);// SELECUTA O SELECT
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
cPSysSQLFramework3.DeclareFunction("function_teste", (sender, e) => {
    Enrollment enrollment = (Enrollment)evento;
    cPSysSQLFramework3.Execute(enrollment);
    if (cPSysSQLFramework3.ErrorMsg != "")
    {
        Console.WriteLine("    ERRO: " + cPSysSQLFramework3.ErrorMsg);
    }
});
```
- Executando Funções
```cs
Enrollment enrollment = new Enrollment();
enrollment.CourseID = 1;
enrollment.StudentID = 1;
enrollment.Grade = Grade.A;
enrollment.Insert<Enrollment>();

cPSysSQLFramework3.ExecuteFunction("function_teste", enrollment);
```
- Criando gatilhos
```cs
cPSysSQLFramework3.DeclareTrigger<Course>(
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
cPSysSQLFramework3.DeclareEvent(1, CPSysUDB.Enums.TypeEvent.SECONDS, (sender, e) => {
    Console.WriteLine("EVENT: 1 SECONDS " + DateTime.Now);
});
```
- Criando apelidos
```cs
Enrollment enrollment = new Enrollment();
enrollment.Select<Enrollment>("EnrollmentID", "id");
enrollment.Select<Enrollment>("Grade", "grade");

DataSet ds4 = cPSysSQLFramework3.Query(acc);// SELECUTA O SELECT
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
enrollment = new Enrollment();
enrollment.Select<Enrollment>("Id", FieldFunction.SUM, "Sum");
DataSet ds5 = cPSysSQLFramework3.Query(enrollment);
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
cPSysSQLFramework3.CloseConnection();
```

# CREATOR
[**Follow me**](https://github.com/pinalrafael?tab=followers) for my next creations