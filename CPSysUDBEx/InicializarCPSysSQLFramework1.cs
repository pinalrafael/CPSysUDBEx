using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSysUDBEx
{
    public class InicializarCPSysSQLFramework1
    {
        public static string Erros { get; set; }

        public static CPSysUDB.CPSysSQLFramework1 InicializarDB(CPSysUDB.Enums.DataBases tipo, string servidor, bool usarSenha, string database, string usuario, string senha, bool persistSecurityInfo)
        {
            return new CPSysUDB.CPSysSQLFramework1(new CPSysUDB.Configuration.ConnectionData(servidor, tipo, usarSenha, database, usuario, senha, persistSecurityInfo));
            /*
             * OBS: SE NÃO INFORMAR O BANCO DE DADOS ELE CONECTA NO BANCO PADRÃO DO SQLSRV OU MYSQL
             */
        }

        public static CPSysUDB.CPSysSQLFramework1 Create(CPSysUDB.CPSysSQLFramework1 db, bool create)
        {
            Erros = "";

            /*
             * ADICIONE QUANTOS CAMPOS FOREM NECESSÁRIO E SE NECESSÁRIO ADCIONAR CAMPOS EM PRODUÇÃO APENAS ADICIONE A LISTA E QUANDO EXECUTAR NOVAMENTE PARA CRIAR AS TABELAS OS CAMPOS SERÃO ADICIONADOS AUTOMATICAMENTE
             */
            List<CPSysUDB.CPSysSQLFramework1.Campos> campos = new List<CPSysUDB.CPSysSQLFramework1.Campos>();// CRIE UMA LIST CAMPOS PARA OS CAMPOS DE CADA TABELA
            campos.Add(new CPSysUDB.CPSysSQLFramework1.Campos("id", // NOME DO CAMPO
                "acessosCodigo", // APELIDO (USADO EM SELECT EX: campo'apelido')
                "ac", // APELIDO DA TABELA (USADO NO SELECT EX: c.campo)
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.INT), // TIPO DO CAMPO SÓ É NECESSÁRIO INFORMAR O VALOR CASO SEJA VARCHAR(100), DECIMAL(10,2)
                true, // PRIMARY KEY
                true)); // AUTO INCREMENTO
            campos.Add(new CPSysUDB.CPSysSQLFramework1.Campos("nome", "acessosNome", "ac",
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.VARCHAR, "100")));
            campos.Add(new CPSysUDB.CPSysSQLFramework1.Campos("dataehora", "acessosData", "ac",
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.DATETIME))); // DEFINE UM VALOR PADRÃO PARA REGISTROS ANTIGOS DA TABELA EM CASO DE NOVO CAMPO
            CPSysUDB.CPSysSQLFramework1.Table acessos = new CPSysUDB.CPSysSQLFramework1.Table("acessos", // INFORMAR O NOME DA TABELA
                "ac", // INFORMAR O APELIDO DA TABELA
                campos);// LISTA COM OS CAMPOS
            db.NewTable(acessos, // NOME DA TABELA
                create, // CASO TRUE EXECUTA A VERIFICAÇÃO E ATUALIZAÇÃO NO DB, CASO FALSE SOMENTE CRIA EM MEMÓRIA
                true);// FAZ A VERIFICAÇÃO DE TODOS OS CAMPOS CRIANDO OS NOVOS E ALTERANDO OS TIPOS CASO O CREATE ESTEJA EM TRUE
            if (db.Error)
            {
                Erros += " [acessos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [acessos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            List<CPSysUDB.CPSysSQLFramework1.Campos> campos1 = new List<CPSysUDB.CPSysSQLFramework1.Campos>();
            campos1.Add(new CPSysUDB.CPSysSQLFramework1.Campos("id", "telasAcessosCodigo", "tea",
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.INT), true, true));
            campos1.Add(new CPSysUDB.CPSysSQLFramework1.Campos("valor", "telasAcessosValor", "tea",
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.DOUBLE, "10,2")));
            campos1.Add(new CPSysUDB.CPSysSQLFramework1.Campos("dataehora", "telasAcessosData", "tea",
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.DATETIME), false, false, true, false, null, null, 
                CPSysUDB.CPSysSQLFramework1.Campos.Function.COUNT, // EXECUTA FUNÇÕES NO CAMPO NO CASO DE UM SELECT
                DateTime.Now)); // DEFINE UM VALOR PADRÃO PARA REGISTROS ANTIGOS DA TABELA EM CASO DE NOVO CAMPO
            campos1.Add(new CPSysUDB.CPSysSQLFramework1.Campos("idAcessos", "telasAcessosCodigoAcessos", "tea",
                new CPSysUDB.CPSysSQLFramework1.TypeCampos(CPSysUDB.CPSysSQLFramework1.Campos.Types.INT), false, false,
                true, // ACEITA NULLO
                true, // FPREIGN KEY
                db.getTableByName("acessos"), // TABELA DO FOREIGN KEY
                db.getCampoByName(db.getTableByName("acessos"), "id")));// CAMPO DA REFERENCES DO FOREIGN KEY
            CPSysUDB.CPSysSQLFramework1.Table telasacessos = new CPSysUDB.CPSysSQLFramework1.Table("telasacessos", "tea", campos1);
            db.NewTable(telasacessos, create, true);
            if (db.Error)
            {
                Erros += " [telasacessos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [telasacessos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // inserindo padroes
            if (create)
            {
                Insert(db);// EXECUTA PRIMEIROS INSERTS PADRÃO DO BANCO
            }
            return db;
        }

        private static void Insert(CPSysUDB.CPSysSQLFramework1 db)
        {
            // telas
            if (ValidaExistente(db, "acessos", "nome", "AUTO ADD") == 0)// VERIFICA SE O VALOR JÁ EXISTE
            {
                List<CPSysUDB.DAL.Values> tela = new List<CPSysUDB.DAL.Values>();
                tela.Add(new CPSysUDB.DAL.Values("AUTO ADD"));
                tela.Add(new CPSysUDB.DAL.Values(CPSysUDB.DAL.Values.Functions.GETDATE));// USA A FUNÇÃO GETDATE NO SQL
                db.Insert(db.getTableByName("acessos"), tela);
            }
        }

        private static int ValidaExistente(CPSysUDB.CPSysSQLFramework1 db, string table, string campo, string valor)
        {
            try
            {
                List<CPSysUDB.CPSysSQLFramework1.Select> select = new List<CPSysUDB.CPSysSQLFramework1.Select>();
                List<CPSysUDB.CPSysSQLFramework1.Campos> campos = new List<CPSysUDB.CPSysSQLFramework1.Campos>();
                List<CPSysUDB.CPSysSQLFramework1.Where> where = new List<CPSysUDB.CPSysSQLFramework1.Where>();
                campos.Add(db.getCampoByName(db.getTableByName(table), campo));
                where.Add(new CPSysUDB.CPSysSQLFramework1.Where(db.getCampoByName(db.getTableByName(table), campo),
                    CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(valor)));
                select.Add(new CPSysUDB.CPSysSQLFramework1.Select(db.getTableByName(table), campos, false, null, where));
                DataSet ds = db.SelectValue(select);

                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                return -1;
            }
        }
    }
}
