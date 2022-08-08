using CPSysUDB;
using CPSysUDB.Events;
using CPSysUDBEx.ClassesCPSysSQLFramework2;
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
    public partial class ExemploCPSysSQLFramework2 : Form
    {
        /*
         * CRIE UM OBJETO PRINCIPAL, VOCÊ USARÁ SEMPRE EM SEU PROJETO
         * CASO TENHA ROTINAS PARA USO DO BANCO DE DADOS CONSTANTES CRIE UM SEGUNDO OBJETO SOMENTE PARA AS ROTINAS 
         * SERÁ NECESSÁRIO INICIALIZAR E CRIAR TODOS OS OBJETOS MAS SOMENTE UM CRIARÁ O BANCO DE DADOS
         */
        CPSysUDB.CPSysSQLFramework2 db;

        public ExemploCPSysSQLFramework2()
        {
            InitializeComponent();
        }

        private void CPSysSQLFramework2_Load(object sender, EventArgs e)
        {
            /*
                         * VOCÊ PODE INFORMAR MYSQL OU SQLSRV PARA SE CONECTAR NO BANCO DE DAODS SEGUINDO COM AS INFORMAÇÕES DE ACESSO
                         */
            this.db = InicializarCPSysSQLFramework2.InicializarDB(CPSysUDB.Enums.DataBases.MYSQL, // QUAL BANCO DE DADOS USARÁ
                @"localhost", // SERVIDOR DO BANCO DE DADOS
                true,// USO DE SENHA
                "db_testev2",// NOME DO BANCO DE DADOS
                "root", // USUÁRIO DE LOGIN
                "",// SENHA DE LOGIN
                true);// Persist Security Info

            // CRIANDO TRIGGER
            this.db.DeclareTrigger<acessos>(
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
            this.db.DeclareEvent(1, // INTERVALO
                CPSysUDB.Enums.TypeEvent.SECONDS, // PERÍODO DE INTERVALO
                (evento, ev) => {// FUNÇÃO QUE SERÁ EXECUTADA QUANDO OCORRER
                    this.Invoke((Action)delegate {
                        lblData.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    });
                });

            // CRIANDO FUNÇÕES
            this.db.DeclareFunction("insert_acessos", // NOME PARA A FUNÇÃO
                (evento, ev) => {// FUNÇÃO QUE SERÁ EXECUTADA QUANDO OCORRER
                    this.db.InsertInto<acessos>((List<CPSysUDB.DAL.Values>)evento);
                });

            /*
             * CASO VOCÊ NÃO QUERIA EXECUTAR ATUALIZAR TODA VEZ DO BANCO DE DADOS, INFORME FALSE PARA ELE CRIAR APENAS AS TABELAS EM MEMÓRIA
             */
            this.db = InicializarCPSysSQLFramework2.Create(this.db, // INFORME O OBJETO INICIADO PARA A CRIAÇÃO DO BANCO DE DADOS
                true); // CASO INFORMADO COMO TRUE ELE EXECUTA A CRIAÇÃO DAS TABELAS ALTERANDO OS CAMPOS E CRIANDO OS NOVOS CAMPOS

            this.db.TestConnection();// FAZ UM TESTE DE CONEXÃO

            string erromsg = this.db.ErrorMsg;// PEGA A MENSAGEM DE ERRO

            this.db.UseLogQuery = true; // PADRÃO FALSE, QUANDO ATIVO ADICIONA TODAS AS QUERYS EM UM LOG (USE EM MODO DEBUG PARA VERIFICAR SE AS QUERYS ESTÃO MONTANDO CERTA)

            List<string> querys = this.db.LogQuery; // ACESSE TODO O LOG DE QUERYS QUANDO A OPÇÃO A CIMA ESTIVER ATIVA

            /*
            * EXECUTA COMANDOS NA MÃO
            * this.db.Execute("INSERT ...");
            * 
            * EXECUTA QUERYS NA MÃO
            * this.db.Query("SELECT ...");
            */

            this.Atualizar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // LISTA DOS CAMPOS PARA INSERT, DEVE CONTER A MESMA QUANTIDADE DE CAMPOS DA TABELA
            List<CPSysUDB.DAL.Values> valores2 = new List<CPSysUDB.DAL.Values>();// INICIA OS VALORES
            valores2.Add(new CPSysUDB.DAL.Values(txtAddNome.Text));// ADICIONE OS VALORES NA ORDEM RESPECTIVA DA CLASSE
            valores2.Add(new CPSysUDB.DAL.Values(1));
            valores2.Add(new CPSysUDB.DAL.Values(2.5));
            valores2.Add(new CPSysUDB.DAL.Values(CPSysUDB.DAL.Values.Functions.GETDATE));//USE FUNÇÕES DO BANCO DE DADOS
            valores2.Add(new CPSysUDB.DAL.Values(Grade.A));
            if (!chbUseFun.Checked)
            {
                this.db.InsertInto<acessos>(valores2);// EXECUTA O INSERT
            }
            else
            {
                this.db.ExecuteFunction("insert_acessos", valores2);// EXECUTA A FUNÇÃO
            }

            this.Atualizar();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            List<string> campos1 = new List<string>();// INICIA OS CAMPOS
            List<CPSysUDB.DAL.Values> values1 = new List<CPSysUDB.DAL.Values>();// INICIA OS VALORES RESPECTIVOS
            List<CPSysUDB.CPSysSQLFramework2.Where> wheres1 = new List<CPSysUDB.CPSysSQLFramework2.Where>();// INICIA O WHERE
            campos1.Add("nome");// ADICIONE OS CAMPOS PARA ATUALIZAÇÃO
            // campos1.Add("CAMPO2");
            values1.Add(new CPSysUDB.DAL.Values(txtUpdateName.Text));//ADICIONE OS VALORES RESPECTIVOS DOS CAMPOS
            //values1.Add(new CPSysUDB.DAL.Values("VALOR 2"));
            wheres1.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<acessos>("id"), // INFOME A TABELA E O CAMPO DO WHERE
                CPSysUDB.Enums.Command.EQUALS, // INFOEME O COMANDO
                new CPSysUDB.DAL.Values(lblId.Text)));// INFORME O VALOR DO WHERE
            this.db.Update<acessos>(campos1, values1, wheres1);// EXECUTA UPDATE

            this.Atualizar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            List<CPSysSQLFramework2.Where> wheres3 = new List<CPSysSQLFramework2.Where>();// CRIA O WHERE
            wheres3.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<acessos>("id"),
                CPSysUDB.Enums.Command.EQUALS,
                new CPSysUDB.DAL.Values(lblId.Text)));
            this.db.Delete<acessos>(wheres3);// EXECUTA O DELETE

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

        private void Atualizar()
        {
            List<CPSysSQLFramework2.Select> selects1 = new List<CPSysSQLFramework2.Select>();// CRIA SELECT
            //List<CPSysSQLFramework2.Join> Join1 = new List<CPSysSQLFramework2.Join>();// CRIA JOIN
            List<CPSysSQLFramework2.Field> campos2 = new List<CPSysSQLFramework2.Field>();// CRIA CAMPOS
            List<CPSysSQLFramework2.Where> wheres4 = new List<CPSysSQLFramework2.Where>();// CRIA WHERE
            List<CPSysSQLFramework2.OrderBy> orders = new List<CPSysSQLFramework2.OrderBy>();// CRIA ORDER BY
            //List<CPSysSQLFramework2.GroupBy> groups = new List<CPSysSQLFramework2.GroupBy>();// CRIA GRUP BY
            campos2.Add(new CPSysSQLFramework2.Field().NewField<acessos>("*"));// INFORME TODOS OS CAMPOS DO SELECT OU * PARA TODOS
            //Join1.Add(new CPSysSQLFramework2.Join(new CPSysSQLFramework2.Field().NewField<acessos>("id"), // TABELA E CAMPO DE SAÍDA DO JOIN
            //    new CPSysSQLFramework2.Field().NewField<telasacessos>("idAcessos")));// TABELA E CAMPO FINAL DO JOIN
            wheres4.Add(new CPSysSQLFramework2.Where(new CPSysSQLFramework2.Field().NewField<acessos>("id"), // TABELA E CAMPO DO WHERE
                CPSysUDB.Enums.Command.BIGGEREQUALS, // COMANDO DO WHERE
                new CPSysUDB.DAL.Values(1)));// VALOR DO WHERE
            orders.Add(new CPSysSQLFramework2.OrderBy().NewOrderBy(new CPSysSQLFramework2.Field().NewField<acessos>("id"), // TABELA E CAMPO DO ORDER BY
                CPSysUDB.Enums.Order.ASC));// ORDENAÇÃO
            //groups.Add(new CPSysSQLFramework2.GroupBy().NewGroupBy(new CPSysSQLFramework2.Field().NewField<Student>("ID")));// TABELA E CAMPO DO GROUP BY
            selects1.Add(new CPSysSQLFramework2.Select().NewSelect<acessos>(campos2, null, wheres4, orders, null, -1, CPSysUDB.Enums.Union.NONE, true));
            db.UseLogQuery = true;
            DataSet ds = this.db.SelectValue(selects1);// SELECUTA O SELECT

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
    }
}