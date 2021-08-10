using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSysUDBEx
{
    public class Inicializar
    {
        public static string Erros { get; set; }

        public static CPSysUDB.Conexao InicializarDB(CPSysUDB.Conexao.ConexaoData.Banco tipo, string servidor, bool usarSenha, string database, string usuario, string senha, bool persistSecurityInfo)
        {
            return new CPSysUDB.Conexao(new CPSysUDB.Conexao.ConexaoData(servidor, tipo, usarSenha, database, usuario, senha, persistSecurityInfo));
            /*
             * OBS: SE NÃO INFORMAR O BANCO DE DADOS ELE CONECTA NO BANCO PADRÃO DO SQLSRV OU MYSQL
             */
        }

        public static CPSysUDB.Conexao Create(CPSysUDB.Conexao db, bool create)
        {
            Erros = "";

            /*
             * ADICIONE QUANTOS CAMPOS FOREM NECESSÁRIO E SE NECESSÁRIO ADCIONAR CAMPOS EM PRODUÇÃO APENAS ADICIONE A LISTA E QUANDO EXECUTAR NOVAMENTE PARA CRIAR AS TABELAS OS CAMPOS SERÃO ADICIONADOS AUTOMATICAMENTE
             */
            List<CPSysUDB.Conexao.Campos> campos = new List<CPSysUDB.Conexao.Campos>();// CRIE UMA LIST CAMPOS PARA OS CAMPOS DE CADA TABELA
            campos.Add(new CPSysUDB.Conexao.Campos("id", // NOME DO CAMPO
                "acessosCodigo", // APELIDO (USADO EM SELECT EX: campo'apelido')
                "ac", // APELIDO DA TABELA (USADO NO SELECT EX: c.campo)
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), // TIPO DO CAMPO SÓ É NECESSÁRIO INFORMAR O VALOR CASO SEJA VARCHAR(100), DECIMAL(10,2)
                true, // PRIMARY KEY
                true)); // AUTO INCREMENTO
            campos.Add(new CPSysUDB.Conexao.Campos("nome", "acessosNome", "ac",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            campos.Add(new CPSysUDB.Conexao.Campos("dataehora", "acessosData", "ac",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME))); // DEFINE UM VALOR PADRÃO PARA REGISTROS ANTIGOS DA TABELA EM CASO DE NOVO CAMPO
            CPSysUDB.Conexao.Table acessos = new CPSysUDB.Conexao.Table("acessos", // INFORMAR O NOME DA TABELA
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

            List<CPSysUDB.Conexao.Campos> campos1 = new List<CPSysUDB.Conexao.Campos>();
            campos1.Add(new CPSysUDB.Conexao.Campos("id", "telasAcessosCodigo", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            campos1.Add(new CPSysUDB.Conexao.Campos("valor", "telasAcessosValor", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            campos1.Add(new CPSysUDB.Conexao.Campos("dataehora", "telasAcessosData", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME), false, false, true, false, null, null, 
                CPSysUDB.Conexao.Campos.Function.COUNT, // EXECUTA FUNÇÕES NO CAMPO NO CASO DE UM SELECT
                DateTime.Now)); // DEFINE UM VALOR PADRÃO PARA REGISTROS ANTIGOS DA TABELA EM CASO DE NOVO CAMPO
            campos1.Add(new CPSysUDB.Conexao.Campos("idAcessos", "telasAcessosCodigoAcessos", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, // ACEITA NULLO
                true, // FPREIGN KEY
                db.getTableByName("acessos"), // TABELA DO FOREIGN KEY
                db.getCampoByName(db.getTableByName("acessos"), "id")));// CAMPO DA REFERENCES DO FOREIGN KEY
            CPSysUDB.Conexao.Table telasacessos = new CPSysUDB.Conexao.Table("telasacessos", "tea", campos1);
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

        private static void Insert(CPSysUDB.Conexao db)
        {
            // telas
            if (ValidaExistente(db, "acessos", "nome", "AUTO ADD") == 0)// VERIFICA SE O VALOR JÁ EXISTE
            {
                List<CPSysUDB.Conexao.Values> tela = new List<CPSysUDB.Conexao.Values>();
                tela.Add(new CPSysUDB.Conexao.Values("AUTO ADD"));
                tela.Add(new CPSysUDB.Conexao.Values(CPSysUDB.Conexao.Values.Functions.GETDATE));// USA A FUNÇÃO GETDATE NO SQL
                db.Insert(db.getTableByName("acessos"), tela);
            }
        }

        private static int ValidaExistente(CPSysUDB.Conexao db, string table, string campo, string valor)
        {
            try
            {
                List<CPSysUDB.Conexao.Select> select = new List<CPSysUDB.Conexao.Select>();
                List<CPSysUDB.Conexao.Campos> campos = new List<CPSysUDB.Conexao.Campos>();
                List<CPSysUDB.Conexao.Where> where = new List<CPSysUDB.Conexao.Where>();
                campos.Add(db.getCampoByName(db.getTableByName(table), campo));
                where.Add(new CPSysUDB.Conexao.Where(db.getCampoByName(db.getTableByName(table), campo),
                    CPSysUDB.Conexao.Where.Command.EQUALS, new CPSysUDB.Conexao.Values(valor)));
                select.Add(new CPSysUDB.Conexao.Select(db.getTableByName(table), campos, false, null, where));
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
