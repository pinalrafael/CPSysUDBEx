using CPSysUDB;
using CPSysUDB.Enums;
using CPSysUDB.Events;
using CPSysUDBEx.ClassesCPSysSQLFramework3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPSysUDBEx
{
    public partial class ExemploCPSysSQLFramework3 : Form
    {
        /*
         * CRIE UM OBJETO PRINCIPAL, VOCÊ USARÁ SEMPRE EM SEU PROJETO
         * CASO TENHA ROTINAS PARA USO DO BANCO DE DADOS CONSTANTES CRIE UM SEGUNDO OBJETO SOMENTE PARA AS ROTINAS 
         * SERÁ NECESSÁRIO INICIALIZAR E CRIAR TODOS OS OBJETOS MAS SOMENTE UM CRIARÁ O BANCO DE DADOS
         */
        CPSysUDB.CPSysSQLFramework3 cPSysSQLFramework3;

        public ExemploCPSysSQLFramework3()
        {
            InitializeComponent();
        }

        private void ExemploCPSysSQLFramework3_Load(object sender, EventArgs e)
        {
            //CPSysSQLFramework3.CreateConfigFile("darabaseconfig.cfsq", CPSysUDB.Configuration.ConnectionData.CreateConnectionMYSQL(@"localhost", true, "db_test3", "root", ""), "87654321", "123456", "12345678");
            CPSysSQLFramework3.CreateConfigFile("darabaseconfig.cfsq", CPSysUDB.Configuration.ConnectionData.CreateConnectionSQLITE(@"db_test3.sqlite", "Version=3;"), "87654321", "123456", "12345678");

            cPSysSQLFramework3 = new CPSysSQLFramework3("darabaseconfig.cfsq", "87654321", "12345678", "123456", true, true);
            cPSysSQLFramework3.UseLogQuery = true;

            /*
             * PARA DELETAR TODAS AS TABELAS E RECRIA-LAS NOVAMENTE.
             * É MUITO ÚTIL PARA DESENVOLVIMENTO POIS EXCLUI AS TABELAS ATUAIS MAS PARA PRODUÇÃO É NECESSÁRIO REMOVER O COMANDO
             */
            cPSysSQLFramework3.DropAllTables();

            /*
             * É NECESSÁRIO CRIAR AS TABELAS POR ORDEM DE DEPENDÊNCIA
             */
            if (cPSysSQLFramework3.CreateOrAlterTable<acessos>() <= 0)
            {
                lblMsg.Text = " [acessos] ErrorMsg=" + cPSysSQLFramework3.ErrorMsg + "\n";
            }

            if (cPSysSQLFramework3.CreateOrAlterTable<telasacessos>() <= 0)
            {
                lblMsg.Text = " [telasacessos] ErrorMsg=" + cPSysSQLFramework3.ErrorMsg + "\n";
            }

            // inserindo padroes
            if (ValidaExistente("id", 1) == 0)// VERIFICA SE O VALOR JÁ EXISTE
            {
                /*
                 * PARA INSERIR UM VALOR POR OBJETO
                 */
                acessos ac = new acessos();
                ac.nome = "AUTO ADD";
                ac.statu = 1;
                ac.valor = 2.5;
                ac.dataehora = DateTime.Now;
                ac.grade = Grade.A;
                ac.Insert<acessos>();
                lblMsg.Text = "[Execute] " + cPSysSQLFramework3.Execute(ac);
            }


            // CRIANDO TRIGGER
            this.cPSysSQLFramework3.DeclareTrigger<acessos>(
            new CPSysUDB.Enums.TriggerType[] {
                CPSysUDB.Enums.TriggerType.CREATE,
                CPSysUDB.Enums.TriggerType.ALTER,
                CPSysUDB.Enums.TriggerType.INSERT,
                CPSysUDB.Enums.TriggerType.UPDATE,
                CPSysUDB.Enums.TriggerType.DELETE// LISTA DAS OCASIÕES DA TRIGGER
            }, (evento, ev) => {// FUNÇÃO QUE SERÁ EXECUTADA QUANDO OCORRER
                TriggerArgs trg = (TriggerArgs)evento;
                lblMsg.Text = "Msg: " + trg.Msg + " Response: " + trg.Response + " TriggerType: " + trg.TriggerType;
            });

            // CRIANDO EVENTOS
            this.cPSysSQLFramework3.DeclareEvent(1, // INTERVALO
                CPSysUDB.Enums.TypeEvent.SECONDS, // PERÍODO DE INTERVALO
                (evento, ev) => {// FUNÇÃO QUE SERÁ EXECUTADA QUANDO OCORRER
                    this.Invoke((Action)delegate {
                        lblData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    });
                });

            // CRIANDO FUNÇÕES
            this.cPSysSQLFramework3.DeclareFunction("insert_acessos", // NOME PARA A FUNÇÃO
                (evento, ev) => {// FUNÇÃO QUE SERÁ EXECUTADA QUANDO OCORRER
                    acessos acc = (acessos)evento;
                    this.cPSysSQLFramework3.Execute(acc);
                });


            this.cPSysSQLFramework3.TestConnection();// FAZ UM TESTE DE CONEXÃO

            string erromsg = this.cPSysSQLFramework3.ErrorMsg;// PEGA A MENSAGEM DE ERRO

            this.cPSysSQLFramework3.UseLogQuery = true; // PADRÃO FALSE, QUANDO ATIVO ADICIONA TODAS AS QUERYS EM UM LOG (USE EM MODO DEBUG PARA VERIFICAR SE AS QUERYS ESTÃO MONTANDO CERTA)

            List<string> querys = this.cPSysSQLFramework3.LogQuery; // ACESSE TODO O LOG DE QUERYS QUANDO A OPÇÃO A CIMA ESTIVER ATIVA

            /*
            * EXECUTA COMANDOS NA MÃO
            * this.cPSysSQLFramework3.Execute("INSERT ...");
            * 
            * EXECUTA QUERYS NA MÃO
            * this.cPSysSQLFramework3.Query("SELECT ...");
            */

            this.Atualizar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // LISTA DOS CAMPOS PARA INSERT, DEVE CONTER A MESMA QUANTIDADE DE CAMPOS DA TABELA
            acessos ac = new acessos();
            ac.nome = txtAddNome.Text;
            ac.statu = 1;
            ac.valor = 2.5;
            ac.dataehora = DateTime.Now;
            ac.grade = Grade.A;
            ac.Insert("dataehora", new DataValueReserved(Functions.GETDATE));

            if (!chbUseFun.Checked)
            {
                lblMsg.Text = "[Execute] " + this.cPSysSQLFramework3.Execute(ac);// EXECUTA O INSERT
            }
            else
            {
                this.cPSysSQLFramework3.ExecuteFunction("insert_acessos", ac);// EXECUTA A FUNÇÃO
            }

            this.Atualizar();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            acessos ac = new acessos();
            ac.Update("nome", txtUpdateName.Text);
            ac.Where<acessos>("id", Command.EQUALS, lblId.Text);
            lblMsg.Text = "[Execute] " + this.cPSysSQLFramework3.Execute(ac);// EXECUTA UPDATE

            this.Atualizar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            acessos ac = new acessos();
            ac.Delete();
            ac.Where<acessos>("id", Command.EQUALS, lblId.Text);
            lblMsg.Text = "[Execute] " + this.cPSysSQLFramework3.Execute(ac);// EXECUTA O DELETE

            this.Atualizar();
        }

        private void gvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lblId.Text = gvLista.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtUpdateName.Text = gvLista.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void ExemploCPSysSQLFramework3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.cPSysSQLFramework3.CloseConnection();
        }

        private void Atualizar()
        {
            acessos acc = new acessos();
            acc.Select<acessos>();
            /*acc.Where<acessos>("id", CPSysUDB.Enums.Command.BIGGEREQUALS, 1);
            acc.OrderBy<acessos>("id", CPSysUDB.Enums.Order.ASC);
            acc.GroupBy<acessos>("id");
            acc.Distinct();
            acc.Limit(1000);
            acc.Offset(1, 1000);*/

            DataSet ds = this.cPSysSQLFramework3.Query(acc);// SELECUTA O SELECT

            for (int i = 0; i < gvLista.RowCount; i++)
            {
                gvLista.Rows[i].DataGridView.Columns.Clear();
            }

            gvLista.DataSource = ds.Tables[0];

            gvLista.ClearSelection();

            lblId.Text = "";
            txtUpdateName.Text = "";
            txtAddNome.Text = "";
        }

        private int ValidaExistente(string campo, object valor)
        {
            try
            {
                acessos acc = new acessos();
                acc.Select<acessos>();
                acc.Where<acessos>(campo, CPSysUDB.Enums.Command.EQUALS, valor);

                DataSet ds = cPSysSQLFramework3.Query(acc);

                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                return -1;
            }
        }
    }
}
