using CPSysUDBEx.ClassesCPSysSQLFramework2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSysUDBEx
{
    public class InicializarCPSysSQLFramework2
    {
        public static string Erros { get; set; }

        public static CPSysUDB.CPSysSQLFramework2 InicializarDB(CPSysUDB.Enums.DataBases tipo, string servidor, bool usarSenha, string database, string usuario, string senha, bool persistSecurityInfo)
        {
            CPSysUDB.Configuration.ConnectionData connectionData = null;
            switch (tipo)
            {
                case CPSysUDB.Enums.DataBases.SQLSRV:
                    connectionData = CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLSRV(servidor, usarSenha, database, usuario, senha, persistSecurityInfo);
                    break;
                case CPSysUDB.Enums.DataBases.MYSQL:
                    connectionData = CPSysUDB.Configuration.ConnectionData.CreateConnectionMYSQL(servidor, usarSenha, database, usuario, senha, persistSecurityInfo);
                    break;
                case CPSysUDB.Enums.DataBases.FIREBIRD:
                    connectionData = CPSysUDB.Configuration.ConnectionData.CreateConnectionFIREBIRD(servidor, usarSenha, database, usuario, senha);
                    break;
                case CPSysUDB.Enums.DataBases.SQLITE:
                    connectionData = CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLITE(database, "Version=3;");
                    break;
            }
            return new CPSysUDB.CPSysSQLFramework2(connectionData, // DADOS DA CONEXÃO
                true);// CRIAR BANCO DE DADOS
            /*
             * OBS: SE NÃO INFORMAR O BANCO DE DADOS ELE CONECTA NO BANCO PADRÃO DO SQLSRV OU MYSQL
             */
        }

        public static CPSysUDB.CPSysSQLFramework2 Create(CPSysUDB.CPSysSQLFramework2 db, bool create)
        {
            Erros = "";

            /*
             * PARA DELETAR TODAS AS TABELAS E RECRIA-LAS NOVAMENTE.
             * É MUITO ÚTIL PARA DESENVOLVIMENTO POS EXCLUI AS TABELAS ATUAIS MAS PARA PRODUÇÃO É NECESSÁRIO REMOVER O CAMPO
             */
            db.DropAllTables();

            /*
             * É NECESSÁRIO CRIAR AS TABELAS POR ORDEM DE DEPENDÊNCIA
             */
            if (db.CreateOrAlterTable<acessos>() <= 0)
            {
                Erros += " [acessos] ErrorMsg=" + db.ErrorMsg + "\n";
            }

            if (db.CreateOrAlterTable<telasacessos>() <= 0)
            {
                Erros += " [telasacessos] ErrorMsg=" + db.ErrorMsg + "\n";
            }

            // inserindo padroes
            if (create)
            {
                Insert(db);// EXECUTA PRIMEIROS INSERTS PADRÃO DO BANCO
            }
            return db;
        }

        private static void Insert(CPSysUDB.CPSysSQLFramework2 db)
        {
            // telas
            if (ValidaExistente(db, "id", 1) == 0)// VERIFICA SE O VALOR JÁ EXISTE
            {
                /*
                 * PARA INSERIR UM VALOR POR OBJETO
                 */
                /*acessos ac = new acessos();
                ac.nome = "AUTO ADD";
                ac.statu = 1;
                ac.valor = 2.5;
                ac.dataehora = DateTime.Now;
                ac.grade = Grade.A;
                db.InsertInto<acessos>(ac);*/

                /*
                 * PARA INSERIR UM VALOR PELA BIBLIOTECA
                 */
                List<CPSysUDB.DAL.Values> valores2 = new List<CPSysUDB.DAL.Values>();
                valores2.Add(new CPSysUDB.DAL.Values("AUTO ADD"));
                valores2.Add(new CPSysUDB.DAL.Values(1));
                valores2.Add(new CPSysUDB.DAL.Values(2.5));
                valores2.Add(new CPSysUDB.DAL.Values(CPSysUDB.DAL.Values.Functions.GETDATE));//USE FUNÇÕES DO BANCO DE DADOS
                valores2.Add(new CPSysUDB.DAL.Values(Grade.A));
                db.InsertInto<acessos>(valores2);
            }
        }

        private static int ValidaExistente(CPSysUDB.CPSysSQLFramework2 db, string campo, object valor)
        {
            try
            {
                List<CPSysUDB.CPSysSQLFramework2.Select> select = new List<CPSysUDB.CPSysSQLFramework2.Select>();
                List<CPSysUDB.CPSysSQLFramework2.Field> campos = new List<CPSysUDB.CPSysSQLFramework2.Field>();
                List<CPSysUDB.CPSysSQLFramework2.Where> where = new List<CPSysUDB.CPSysSQLFramework2.Where>();
                campos.Add(new CPSysUDB.CPSysSQLFramework2.Field().NewField<acessos>("*"));// INFORME TODOS OS CAMPOS DO SELECT OU * PARA TODOS
                where.Add(new CPSysUDB.CPSysSQLFramework2.Where(new CPSysUDB.CPSysSQLFramework2.Field().NewField<acessos>(campo), CPSysUDB.Enums.Command.EQUALS, new CPSysUDB.DAL.Values(valor)));
                select.Add(new CPSysUDB.CPSysSQLFramework2.Select().NewSelect<acessos>(campos, null, where));
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