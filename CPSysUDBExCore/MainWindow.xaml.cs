using CPSysUDBCore;
using CPSysUDBCore.DataBase;
using CPSysUDBCore.Enums;
using CPSysUDBCore.Events;
using CPSysUDBCore.Models;
using CPSysUDBCoreEx.Classes;
using System;
using System.Data;
using System.Windows;

namespace CPSysUDBCoreEx
{
    public partial class MainWindow : Window
    {
        /*
         * OBJETO PRINCIPAL DA BIBLIOTECA.
         *
         * Normalmente você terá um objeto principal responsável
         * pelas operações com o banco de dados.
         */
        private ICPSysSQLFrameworkCore cPSysSQLFrameworkCore;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
             * CRIA O ARQUIVO DE CONFIGURAÇÃO.
             *
             * Neste exemplo estamos utilizando SQLite.
             *
             * Caso queira testar outro banco, substitua a
             * ConnectionData pela conexão correspondente.
             */
            ConnectionData connectionData =
            ConnectionData.CreateConnectionSQLITE(
                "db_test_core.sqlite", string.Empty, true, true, true);

            CPSysSQLFrameworkCore.CreateConfigFile("databaseconfig.cfsq", connectionData, "87654321", "12345678");
            ConnectionData connectionDataRead = CPSysSQLFrameworkCore.ReadConfigFile("databaseconfig.cfsq", "87654321", "12345678");

            /*
             * INICIALIZA A BIBLIOTECA.
             */
            cPSysSQLFrameworkCore = ICPSysSQLFrameworkCore.Instance(connectionDataRead);

            /*
             * ATIVA O LOG DAS QUERIES.
             *
             * Útil durante o desenvolvimento para verificar
             * as queries geradas pela biblioteca.
             */
            cPSysSQLFrameworkCore.UseLogQuery = true;

            /*
             * PARA DESENVOLVIMENTO:
             *
             * Remove todas as tabelas e permite que elas sejam
             * recriadas novamente.
             *
             * NÃO UTILIZE ISSO EM PRODUÇÃO.
             */
            cPSysSQLFrameworkCore.DropAllTables();

            /*
             * AS TABELAS DEVEM SER CRIADAS NA ORDEM DE DEPENDÊNCIA.
             */
            if (cPSysSQLFrameworkCore.CreateOrAlterTable<acessos>() <= 0)
            {
                lblMsg.Text =
                    "[acessos] ErrorMsg=" +
                    cPSysSQLFrameworkCore.ErrorMsg;
            }

            /*
             * INSERE UM REGISTRO PADRÃO CASO NÃO EXISTA.
             */
            if (ValidaExistente("id", 1) == 0)
            {
                acessos ac = new acessos();

                ac.nome = "AUTO ADD";
                ac.statu = 1;
                ac.valor = 2.5;
                ac.dataehora = DateTime.Now;
                ac.grade = Grade.A;

                /*
                 * Insere o registro utilizando o próprio objeto.
                 */
                ac.Insert<acessos>();

                /*
                 * Executa a operação no banco.
                 */
                lblMsg.Text =
                    "[Execute] " +
                    cPSysSQLFrameworkCore.Execute(ac);
            }

            /*
             * TESTA A CONEXÃO COM O BANCO.
             */
            cPSysSQLFrameworkCore.TestConnection();

            string erromsg =
                cPSysSQLFrameworkCore.ErrorMsg;

            if (!string.IsNullOrEmpty(erromsg))
            {
                lblMsg.Text = erromsg;
            }

            /*
             * O LOG PODE SER CONSULTADO ATRAVÉS DA PROPRIEDADE
             * LogQuery.
             */
            var querys =
                cPSysSQLFrameworkCore.LogQuery;

            /*
             * EXECUTA UMA CONSULTA PARA PREENCHER A GRID.
             */
            Atualizar();

            /*
             * Exemplo de evento periódico.
             *
             * Se a API de eventos da versão Core permanecer
             * compatível com a versão anterior:
             *
             * cPSysSQLFrameworkCore.DeclareEvent(
             *     1,
             *     TypeEvent.SECONDS,
             *     (evento, ev) =>
             *     {
             *         Dispatcher.Invoke(() =>
             *         {
             *             lblData.Text =
             *                 DateTime.Now.ToString(
             *                     "dd/MM/yyyy HH:mm:ss");
             *         });
             *     });
             */
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            /*
             * CRIA O OBJETO PARA O INSERT.
             */
            acessos ac = new acessos();

            ac.nome = txtAddNome.Text;
            ac.statu = 1;

            /*
             * Valor informado pelo usuário.
             */
            if (double.TryParse(
                    txtAddValor.Text,
                    out double valor))
            {
                ac.valor = valor;
            }
            else
            {
                ac.valor = 0;
            }

            ac.dataehora = DateTime.Now;
            ac.grade = Grade.A;

            /*
             * Permite utilizar uma função previamente declarada
             * na biblioteca.
             */
            if (!chbUseFun.IsChecked.GetValueOrDefault())
            {
                /*
                 * Executa o INSERT.
                 */
                ac.Insert<acessos>();

                lblMsg.Text =
                    "[Execute] " +
                    cPSysSQLFrameworkCore.Execute(ac);
            }
            else
            {
                /*
                 * Caso exista a função insert_acessos.
                 */
                cPSysSQLFrameworkCore.ExecuteFunction(
                    "insert_acessos",
                    ac
                );
            }

            Atualizar();
        }

        private void btnAtualizar_Click(
            object sender,
            RoutedEventArgs e)
        {
            /*
             * Cria o objeto para UPDATE.
             */
            acessos ac = new acessos();

            /*
             * Define o campo que será alterado.
             */
            ac.Update(
                "nome",
                txtUpdateName.Text
            );

            /*
             * Define o WHERE.
             */
            ac.Where<acessos>(
                "id",
                Command.EQUALS,
                lblId.Text
            );

            /*
             * Executa o UPDATE.
             */
            lblMsg.Text =
                "[Execute] " +
                cPSysSQLFrameworkCore.Execute(ac);

            Atualizar();
        }

        private void btnDeletar_Click(
            object sender,
            RoutedEventArgs e)
        {
            /*
             * Cria o objeto para DELETE.
             */
            acessos ac = new acessos();

            /*
             * Define a operação.
             */
            ac.Delete();

            /*
             * Define o registro que será excluído.
             */
            ac.Where<acessos>(
                "id",
                Command.EQUALS,
                lblId.Text
            );

            /*
             * Executa o DELETE.
             */
            lblMsg.Text =
                "[Execute] " +
                cPSysSQLFrameworkCore.Execute(ac);

            Atualizar();
        }

        private void gvLista_MouseLeftButtonUp(
            object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            if (gvLista.SelectedItem == null)
                return;

            /*
             * Obtém o registro selecionado.
             *
             * Como a DataGrid está utilizando DataTable como
             * fonte de dados, o item selecionado normalmente
             * será um DataRowView.
             */
            if (gvLista.SelectedItem is DataRowView row)
            {
                if (row.Row.Table.Columns.Contains("id"))
                {
                    lblId.Text =
                        row["id"]?.ToString() ?? "";
                }

                if (row.Row.Table.Columns.Contains("nome"))
                {
                    txtUpdateName.Text =
                        row["nome"]?.ToString() ?? "";
                }
            }
        }

        private void Window_Closing(
            object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            /*
             * Fecha a conexão antes de encerrar a aplicação.
             */
            cPSysSQLFrameworkCore?.CloseConnection();
        }

        private void Atualizar()
        {
            /*
             * Cria o objeto utilizado para montar o SELECT.
             */
            acessos acc = new acessos();

            /*
             * SELECT * FROM acessos
             */
            acc.Select<acessos>();

            /*
             * Outros exemplos:
             *
             * acc.Where<acessos>(
             *     "id",
             *     Command.BIGGEREQUALS,
             *     1);
             *
             * acc.OrderBy<acessos>(
             *     "id",
             *     Order.ASC);
             *
             * acc.GroupBy<acessos>("id");
             *
             * acc.Distinct();
             *
             * acc.Limit(1000);
             *
             * acc.Offset(1, 1000);
             */

            /*
             * Executa o SELECT.
             */
            DataSet ds =
                cPSysSQLFrameworkCore.Query(acc);

            /*
             * Exibe o resultado na DataGrid.
             */
            if (ds != null &&
                ds.Tables.Count > 0)
            {
                gvLista.ItemsSource =
                    ds.Tables[0].DefaultView;
            }
            else
            {
                gvLista.ItemsSource = null;
            }

            /*
             * Limpa a seleção e os campos.
             */
            gvLista.UnselectAll();

            lblId.Text = "";
            txtUpdateName.Text = "";
            txtAddNome.Text = "";
        }

        private int ValidaExistente(
            string campo,
            object valor)
        {
            try
            {
                /*
                 * Cria o objeto para consulta.
                 */
                acessos acc = new acessos();

                /*
                 * SELECT.
                 */
                acc.Select<acessos>();

                /*
                 * WHERE.
                 */
                acc.Where<acessos>(
                    campo,
                    Command.EQUALS,
                    valor
                );

                /*
                 * Executa a consulta.
                 */
                DataSet ds =
                    cPSysSQLFrameworkCore.Query(acc);

                if (ds == null ||
                    ds.Tables.Count == 0)
                {
                    return 0;
                }

                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                return -1;
            }
        }
    }
}