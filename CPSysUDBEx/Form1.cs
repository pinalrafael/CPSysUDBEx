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
         * CASO TENHA ROTINAS PARA USO DO BANCO DE DADOS CONSTANTES, CRIE UM SEGUNDO OBJETO SOMENTE PARA AS ROTINAS 
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
                "dbteste",// NOME DO BANCO DE DADOS
                "sa", // USUÁRIO DE LOGIN
                "sql@123",// SENHA DE LOGIN
                true);// Persist Security Info

            this.db.NewDataBase("novodb");// CRIA UM DATABASE

            /*
             * CASO VOCÊ NÃO QUERIA EXECUTAR ATUALIZAR TODA VEZ DO BANCO DE DADOS, INFORME FALSE PARA ELE CRIAR APENAS AS TABELAS EM MEMÓRIA
             */
            this.db = Inicializar.Create(this.db, // INFORME O OBJETO INICIADO PARA A CRIAÇÃO DO BANCO DE DADOS
                true); // CASO INFORMADO COMO TRUE ELE EXECUTA A CRIAÇÃO DO BANCO DE DADOS, ATUALIZANDO AS TABELAS

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


        }
    }
}
