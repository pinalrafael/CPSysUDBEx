# CPSysUDB
-----

[NuGet Packages](https://www.nuget.org/packages/CPSysUDB/1.0.3.8)
<br/>
.NET Framework 4.7.2

# A DLL
-----

A biblioteca é grátis para uso comercial ou não comercial.
<br/>
Tornar o banco de dados do sistema híbrido facilitando a mudança para MySQL ou SQL Server a qaulquer momento.
<br/>
Facilitar a atualização do banco de dados no cliente não precisando executar scripts para criar novas tabelas e campo.
<br/>
Funciona em projetos desktop e web testados.

# USANDO
-----

1. Instale a biblioteca pelo pacote NuGet Packages;
2. Faça referência a biblioteca em seu código;
```cs
using CPSysUDB;
```
3. Inicie um objeto para cada conexão simultânea, informando os dados de conexão e o banco de dados;
```cs
Conexao c = new Conexao(new Conexao.ConexaoData(@"RAFAEL-PC\SQLEXPRESS", Conexao.ConexaoData.Banco.SQLSRV, true, "DB_CHEF", "sa", "***"));
```
4. O projeto CPSysUDBEx é um exemplo completo de uso da dll;

# SUPORTE
-----

[**Siga-me**](https://github.com/pinalrafael?tab=followers) para minhas próximas criações