using CPSysUDB;
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
    public partial class Form1 : Form
    {
        /*
         * CRIE UM OBJETO PRINCIPAL, VOCÊ USARÁ SEMPRE EM SEU PROJETO
         * CASO TENHA ROTINAS PARA USO DO BANCO DE DADOS CONSTANTES CRIE UM SEGUNDO OBJETO SOMENTE PARA AS ROTINAS 
         * SERÁ NECESSÁRIO INICIALIZAR E CRIAR TODOS OS OBJETOS MAS SOMENTE UM CRIARÁ O BANCO DE DADOS
         */
        CPSysUDB.Conexao db;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
             * VOCÊ PODE INFORMAR MYSQL OU SQLSRV PARA SE CONECTAR NO BANCO DE DAODS SEGUINDO COM AS INFORMAÇÕES DE ACESSO
             */
            this.db = Inicializar.InicializarDB(CPSysUDB.Conexao.ConexaoData.Banco.SQLSRV, // QUAL BANCO DE DADOS USARÁ
                @"RAFAEL-PC\SQLEXPRESS", // SERVIDOR DO BANCO DE DADOS
                true,// USO DE SENHA
                "db_teste",// NOME DO BANCO DE DADOS
                "sa", // USUÁRIO DE LOGIN
                "***",// SENHA DE LOGIN
                true);// Persist Security Info

            //this.db.NewDataBase("novodb");// CRIA UM DATABASE E TROCA O DB 

            /*
             * CASO VOCÊ NÃO QUERIA EXECUTAR ATUALIZAR TODA VEZ DO BANCO DE DADOS, INFORME FALSE PARA ELE CRIAR APENAS AS TABELAS EM MEMÓRIA
             */
            this.db = Inicializar.Create(this.db, // INFORME O OBJETO INICIADO PARA A CRIAÇÃO DO BANCO DE DADOS
                true); // CASO INFORMADO COMO TRUE ELE EXECUTA A CRIAÇÃO DAS TABELAS ALTERANDO OS CAMPOS E CRIANDO OS NOVOS CAMPOS

            this.db.TestConexao();// FAZ UM TESTE DE CONEXÃO

            this.db.getTableByName("acessos"); // RETORNA UM OBJETO TABELA PELO NOME

            this.db.getCampoByName(db.getTableByName("acessos"), "id"); // RETORNA UM CAMPO PELO NOME DE UMA TABELA

            List<Conexao.Table> table = this.db.Tables;// LE TODAS AS TABELAS ATUAIS

            bool erro = this.db.Error; // VERIFICA SE HOUVE ERRO

            string erromsg = this.db.ErrorMsg;// PEGA A MENSAGEM DE ERRO

            bool errocx = this.db.ErrorConexao; // VERIFICA SE HOUVE ERRO DE CONEXAO

            string errocxmsg = this.db.ErrorMsgConexao;// PEGA A MENSAGEM DE ERRO DE CONEXAO

            this.db.UseLogQuery = true; // PADRÃO FALSE, QUANDO ATIVO ADICIONA TODAS AS QUERYS EM UM LOG (USE EM MODO DEBUG PARA VERIFICAR SE AS QUERYS ESTÃO MONTANDO CERTA)

            List<string> querys = this.db.LogQuery; // ACESSE TODO O LOG DE QUERYS QUANDO A OPÇÃO A CIMA ESTIVER ATIVA

            /*
             * ADICIONAR UMA FK NA MÃO, NÃO É NECESSÁRIO EXECUTAR QUANDO FOR ADICIONAR FK, POIS O PRÓPRIO NewTable CRIA CASO SEJA UMA FK
             * 
            this.db.AddForeignKey(db.getTableByName("acessos"),// TABELA
                "FK_TESTE", NOME DA FK
                this.db.getCampoByName(db.getTableByName("acessos"), "id"),// ID DA TABELA
                db.getTableByName("TABELA2"),// TABELA REFERENCES
                this.db.getCampoByName(db.getTableByName("TABELA2"), "idfK"));// ID DA TABELA REFERENCES
            *
            * 
            * EXECUTA COMANDOS NA MÃO
            * this.db.Execute("INSERT ...");
            * 
            * EXECUTA QUERYS NA MÃO
            * this.db.Query("SELECT ...");
            */

            this.db.VerifyTable("NOME TABELA"); // VERIFICA SE UMA TABELA EXISTE

            this.db.VerifyColumn("NOME TABELA", "COME CAMPO"); // VERIFICA SE UM CAMPO DE UMA TABELA EXISTE

            this.Atualizar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // LISTA DOS CAMPOS PARA INSERT, DEVE CONTER A MESMA QUANTIDADE DE CAMPOS DA TABELA
            List<object> tela = new List<object>();
            tela.Add(txtAddNome.Text);

            this.db.Insert(this.db.getTableByName("acessos"), // TABELA DO INSERT
                tela);// VALORES

            this.Atualizar();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            List<CPSysUDB.Conexao.Campos> campos = new List<CPSysUDB.Conexao.Campos>();// INICIA OS CAMPOS DO UPDATE

            campos.Add(this.db.getCampoByName(this.db.getTableByName("acessos"), "nome"));

            // VALORES DO UPDATE
            List<object> tela = new List<object>();
            tela.Add(txtUpdateName.Text);

            List<CPSysUDB.Conexao.Where> where = new List<CPSysUDB.Conexao.Where>();// INICIAR AS CONDIÇÕES PARA UPDATE

            where.Add(new CPSysUDB.Conexao.Where(db.getCampoByName(this.db.getTableByName("acessos"), "id"),// CAMPO DO WHERE 
                CPSysUDB.Conexao.Where.Command.EQUALS, // CONDIÇÃO DO WHERE
                lblId.Text));// VALOR DO WHERE

            this.db.Update(this.db.getTableByName("acessos"), // TABELA DO UPDATE
                this.db.getTableByName("acessos").Campos, // CASO OS CAMPOS NÃO SEJA ESPECIFICADO DEVE INFORMAR TODOS OS VALORES EXCETO AUTO INCREMENT
                tela, // VALORES DO UPDATE
                where);// CONDIÇÃO PARA UPDATE

            this.Atualizar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            List<CPSysUDB.Conexao.Where> where = new List<CPSysUDB.Conexao.Where>();// INICIAR AS CONDIÇÕES PARA DELETE

            where.Add(new CPSysUDB.Conexao.Where(db.getCampoByName(this.db.getTableByName("acessos"), "id"),// CAMPO DO WHERE 
                CPSysUDB.Conexao.Where.Command.EQUALS, // CONDIÇÃO DO WHERE
                lblId.Text));// VALOR DO WHERE

            this.db.Delete(this.db.getTableByName("acessos"), // TABELA DO DELETE
                where);// CONDIÇÃO PARA DELETE

            this.Atualizar();
        }

        private void gvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                lblId.Text = gvLista.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtUpdateName.Text = gvLista.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void Atualizar()
        {
            List<CPSysUDB.Conexao.Select> select = new List<CPSysUDB.Conexao.Select>();// INICIANDO UM OU MAIS SELECTS
            List<CPSysUDB.Conexao.Campos> campos = new List<CPSysUDB.Conexao.Campos>();// ADICIONANDO CAMPOS DO SELECT
            List<CPSysUDB.Conexao.Join> join = new List<CPSysUDB.Conexao.Join>(); // ADICONANDO JOIN AO SELECT
            List<CPSysUDB.Conexao.Where> where = new List<CPSysUDB.Conexao.Where>(); // ADICIONANDO CONDIÇÕES WHERE PARA O SELECT
            List<CPSysUDB.Conexao.OrderBy> orderby = new List<CPSysUDB.Conexao.OrderBy>();// ADICIONANDO ORDER BY AO SELECT
            List<CPSysUDB.Conexao.GroupBy> groupby = new List<CPSysUDB.Conexao.GroupBy>();// ADICIONANDO GROUP BY AO SELECT

            campos.Add(this.db.getCampoByName(this.db.getTableByName("acessos"), "id"));
            campos.Add(this.db.getCampoByName(this.db.getTableByName("acessos"), "nome"));

            join.Add(new Conexao.Join(this.db.getTableByName("telasacessos"), // TABELA DO JOIN
                this.db.getCampoByName(this.db.getTableByName("telasacessos"), "idAcessos"), // CAMPO DA TABELA DO JOIN
                this.db.getCampoByName(this.db.getTableByName("acessos"), "id"), // CAMPO DA TABELA PAI
                Conexao.Join.Joins.INNER));// TIPO DO JOIN INNER, LEFT, RIGHT

            where.Add(new CPSysUDB.Conexao.Where(db.getCampoByName(this.db.getTableByName("acessos"), "id"),// CAMPO DO WHERE 
                CPSysUDB.Conexao.Where.Command.BIGGEREQUALS, // CONDIÇÃO DO WHERE
                1, // VALOR DO WHERE (UM CAMPO TAMBEM PODE SER INFORMADO)
                null, // VALOR 2 CASO DE BETWEEN
                Conexao.Where.Union.NONE, // AND OU OR CASO DE MAIS WHERE
                false, // ABRE UM PARENTESES
                false, // FECHA PARENTESES
                null, // SELECTS PARA SUBQUERY NO WHERE
                null));// NOMES DAS SUBQUERY

            orderby.Add(new Conexao.OrderBy(this.db.getCampoByName(this.db.getTableByName("acessos"), "id"), // CAMPO DO ORDERBY
                Conexao.OrderBy.Ordem.ASC));// ORDENAÇÃO ASC OU DESC

            groupby.Add(new Conexao.GroupBy(this.db.getCampoByName(this.db.getTableByName("acessos"), "id"))); // CAMPO DO GROUPBY

            select.Add(new CPSysUDB.Conexao.Select(this.db.getTableByName("acessos"), // TABELA DO SELECT
                campos, // CAMPOS DO SELECT
                false, // USAR DISTINCT
                null, // JOIN
                where, // WHERE
                orderby, // ORDER BY
                null, // GROUP BY
                Conexao.Select.Union.NONE, // UNION CASO DE MAIS SELECTS NA LISTA
                null, // SUBQUERY
                null, // NOMES DAS SUBQUERYS
                -1, // LIMITE DE CAMPOS NO SELECT -1 SEM LIMITE
                ""));// STRINGS ADICIONAIS

            DataSet ds = this.db.SelectValue(select);// INFORMAR O SELECT, SEMPRE RETORNANDO UM DATATABLE 

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
