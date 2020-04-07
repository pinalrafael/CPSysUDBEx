using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSysUDBEx
{
    public class Inicializar
    {
        public static string Erros { get; set; }

        /*public static CPSysUDB.Conexao InicializarDB()
        {
            if (System.IO.File.Exists(Form1.SysDir + "//Conexao.txt"))
            {
                string[] con = System.IO.File.ReadAllLines(Form1.SysDir + "//Conexao.txt");

                CPSysUDB.Conexao.ConexaoData.Banco b = CPSysUDB.Conexao.ConexaoData.Banco.SQLSRV;

                if (Chef.Classes.Validacoes.Decript(con[1]) == "MYSQL")
                {
                    b = CPSysUDB.Conexao.ConexaoData.Banco.MYSQL;
                }

                return new CPSysUDB.Conexao(new CPSysUDB.Conexao.ConexaoData(Chef.Classes.Validacoes.Decript(con[0]),
                    b,
                    Convert.ToBoolean(Chef.Classes.Validacoes.Decript(con[2])),
                    Chef.Classes.Validacoes.Decript(con[3]),
                    Chef.Classes.Validacoes.Decript(con[4]),
                    Chef.Classes.Validacoes.Decript(con[5]),
                    Convert.ToBoolean(Chef.Classes.Validacoes.Decript(con[6]))));
            }
            return null;
        }

        public static CPSysUDB.Conexao Create(CPSysUDB.Conexao db, bool create)
        {
            Erros = "";

            // tabela acessos
            List<CPSysUDB.Conexao.Campos> acessosCampos = new List<CPSysUDB.Conexao.Campos>();
            acessosCampos.Add(new CPSysUDB.Conexao.Campos("id", "acessosCodigo", "ac",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            acessosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "acessosNome", "ac",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            acessosCampos.Add(new CPSysUDB.Conexao.Campos("superus", "acessosSuper", "ac",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 super usuário, ignora as permissões padrão, 0 usuario normal
            acessosCampos.Add(new CPSysUDB.Conexao.Campos("statu", "acessosStatus", "ac",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            CPSysUDB.Conexao.Table acessos = new CPSysUDB.Conexao.Table("acessos", "ac", acessosCampos);
            db.NewTable(acessos, create, true);
            if (db.Error)
            {
                Erros += " [acessos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [acessos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // tabela telas
            List<CPSysUDB.Conexao.Campos> telasCampos = new List<CPSysUDB.Conexao.Campos>();
            telasCampos.Add(new CPSysUDB.Conexao.Campos("id", "telasCodigo", "te",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            telasCampos.Add(new CPSysUDB.Conexao.Campos("nome", "telasNome", "te",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            telasCampos.Add(new CPSysUDB.Conexao.Campos("pai", "telasPai", "te",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            telasCampos.Add(new CPSysUDB.Conexao.Campos("descricao", "telasDescricao", "te",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            CPSysUDB.Conexao.Table telas = new CPSysUDB.Conexao.Table("telas", "te", telasCampos);
            db.NewTable(telas, create, true);
            if (db.Error)
            {
                Erros += " [telas] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [telas] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // telas que o usuario acessa
            List<CPSysUDB.Conexao.Campos> telasAcessosCampos = new List<CPSysUDB.Conexao.Campos>();
            telasAcessosCampos.Add(new CPSysUDB.Conexao.Campos("id", "telasAcessosCodigo", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            telasAcessosCampos.Add(new CPSysUDB.Conexao.Campos("idTelas", "telasAcessosCodigoTelas", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("telas"), db.getCampoByName(db.getTableByName("telas"), "id")));
            telasAcessosCampos.Add(new CPSysUDB.Conexao.Campos("idAcessos", "telasAcessosCodigoAcessos", "tea",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("acessos"), db.getCampoByName(db.getTableByName("acessos"), "id")));
            CPSysUDB.Conexao.Table telasacessos = new CPSysUDB.Conexao.Table("telasacessos", "tea", telasAcessosCampos);
            db.NewTable(telasacessos, create, true);
            if (db.Error)
            {
                Erros += " [telasacessos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [telasacessos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // lojas
            List<CPSysUDB.Conexao.Campos> lojasCampos = new List<CPSysUDB.Conexao.Campos>();
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("id", "lojasCodigo", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("nome", "lojasNome", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("cnpj", "lojasCnpj", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("endereco", "lojasEndereco", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("telefones", "lojasTelefones", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("api", "lojasApi", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("statu", "lojasStatus", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("sepCategoria", "lojasSepCategoria", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 separa por categoria a impressao 0, nao separado
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("sepProduto", "lojasSepProduto", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 separa por produto a impressao 0, nao separado
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("precoEntrega", "lojasPrecoEntrega", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("valorGarcom", "lojasValorGarcom", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("usoFirebase", "lojasusoFirebase", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 para uso do firebase 0 pra uso do banco de dados
            lojasCampos.Add(new CPSysUDB.Conexao.Campos("estoque", "lojasusoEstoque", "lo",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 valida o estoque 0, n valida
            CPSysUDB.Conexao.Table lojas = new CPSysUDB.Conexao.Table("lojas", "lo", lojasCampos);
            db.NewTable(lojas, create, true);
            if (db.Error)
            {
                Erros += " [lojas] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [lojas] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // usuarios
            List<CPSysUDB.Conexao.Campos> usuariosCampos = new List<CPSysUDB.Conexao.Campos>();
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("id", "usuariosCodigo", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("idAcessos", "acessosCodigo", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("acessos"), db.getCampoByName(db.getTableByName("acessos"), "id")));
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "lojasCodigo", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("statu", "usuariosStatus", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "usuariosCadastro", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "usuariosNome", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("usuario", "usuariosUsuario", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            usuariosCampos.Add(new CPSysUDB.Conexao.Campos("senha", "usuariosSenha", "us",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            CPSysUDB.Conexao.Table usuarios = new CPSysUDB.Conexao.Table("usuarios", "us", usuariosCampos);
            db.NewTable(usuarios, create, true);
            if (db.Error)
            {
                Erros += " [usuarios] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [usuarios] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // comandas
            List<CPSysUDB.Conexao.Campos> comandasCampos = new List<CPSysUDB.Conexao.Campos>();
            comandasCampos.Add(new CPSysUDB.Conexao.Campos("id", "comandasCodigo", "co",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            comandasCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "comandasLojasCodigo", "co",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            comandasCampos.Add(new CPSysUDB.Conexao.Campos("nome", "comandasNome", "co",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            comandasCampos.Add(new CPSysUDB.Conexao.Campos("statu", "comandasStatus", "co",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            CPSysUDB.Conexao.Table comandas = new CPSysUDB.Conexao.Table("comandas", "co", comandasCampos);
            db.NewTable(comandas, create, true);
            if (db.Error)
            {
                Erros += " [comandas] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [comandas] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // categorias
            List<CPSysUDB.Conexao.Campos> categoriasCampos = new List<CPSysUDB.Conexao.Campos>();
            categoriasCampos.Add(new CPSysUDB.Conexao.Campos("id", "categoriasCodigo", "ca",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            categoriasCampos.Add(new CPSysUDB.Conexao.Campos("nome", "categoriasNome", "ca",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            CPSysUDB.Conexao.Table categorias = new CPSysUDB.Conexao.Table("categorias", "ca", categoriasCampos);
            db.NewTable(categorias, create, true);
            if (db.Error)
            {
                Erros += " [categorias] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [categorias] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // subcategorias
            List<CPSysUDB.Conexao.Campos> subcategoriasCampos = new List<CPSysUDB.Conexao.Campos>();
            subcategoriasCampos.Add(new CPSysUDB.Conexao.Campos("id", "subcategoriasCodigo", "sca",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            subcategoriasCampos.Add(new CPSysUDB.Conexao.Campos("idCategorias", "subcategoriasCategoriaCodigo", "sca",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("categorias"), db.getCampoByName(db.getTableByName("categorias"), "id")));
            subcategoriasCampos.Add(new CPSysUDB.Conexao.Campos("nome", "subcategoriasNome", "sca",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            subcategoriasCampos.Add(new CPSysUDB.Conexao.Campos("statu", "subcategoriasStatus", "sca",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            CPSysUDB.Conexao.Table subcategorias = new CPSysUDB.Conexao.Table("subcategorias", "sca", subcategoriasCampos);
            db.NewTable(subcategorias, create, true);
            if (db.Error)
            {
                Erros += " [subcategorias] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [subcategorias] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // categorias itens
            List<CPSysUDB.Conexao.Campos> categoriasitensCampos = new List<CPSysUDB.Conexao.Campos>();
            categoriasitensCampos.Add(new CPSysUDB.Conexao.Campos("id", "categoriasitensCodigo", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            categoriasitensCampos.Add(new CPSysUDB.Conexao.Campos("nome", "categoriasitensNome", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            CPSysUDB.Conexao.Table categoriasitens = new CPSysUDB.Conexao.Table("categoriasitens", "cai", categoriasitensCampos);
            db.NewTable(categoriasitens, create, true);
            if (db.Error)
            {
                Erros += " [categoriasitens] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [categoriasitens] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // itens
            List<CPSysUDB.Conexao.Campos> itensCampos = new List<CPSysUDB.Conexao.Campos>();
            itensCampos.Add(new CPSysUDB.Conexao.Campos("id", "itensCodigo", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("idCategoriasItens", "itensCategoriasItensCodigo", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("categoriasitens"), db.getCampoByName(db.getTableByName("categoriasitens"), "id")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "itensLojasCodigo", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "itensCadastro", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("statu", "itensStatus", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            itensCampos.Add(new CPSysUDB.Conexao.Campos("nome", "itensNome", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("preco", "itensPreco", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("estoque", "itensEstoque", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("estoqueMin", "itensEstoqueMin", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("tipo", "itensTipo", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("precoadicional", "itensPrecoAcicional", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            itensCampos.Add(new CPSysUDB.Conexao.Campos("qtdeadicional", "itensQuantidadeAdicional", "it",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            CPSysUDB.Conexao.Table itens = new CPSysUDB.Conexao.Table("itens", "it", itensCampos);
            db.NewTable(itens, create, true);
            if (db.Error)
            {
                Erros += " [itens] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [itens] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // tributacao https://ajuda.programanex.com.br/tutorial/3-completando-o-produto-para-emitir-sat-cf-e
            List<CPSysUDB.Conexao.Campos> tributacaoCampos = new List<CPSysUDB.Conexao.Campos>();
            tributacaoCampos.Add(new CPSysUDB.Conexao.Campos("id", "tributacaoCodigo", "trib",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            tributacaoCampos.Add(new CPSysUDB.Conexao.Campos("nome", "tributacaoNome", "trib",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            tributacaoCampos.Add(new CPSysUDB.Conexao.Campos("origem", "tributacaoOrigem", "trib",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            tributacaoCampos.Add(new CPSysUDB.Conexao.Campos("csosn", "tributacaoCsosn", "trib",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            tributacaoCampos.Add(new CPSysUDB.Conexao.Campos("cfop", "tributacaoCfop", "trib",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            tributacaoCampos.Add(new CPSysUDB.Conexao.Campos("icms", "tributacaoIcms", "trib",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            CPSysUDB.Conexao.Table tributacao = new CPSysUDB.Conexao.Table("tributacao", "trib", tributacaoCampos);
            db.NewTable(tributacao, create, true);
            if (db.Error)
            {
                Erros += " [tributacao] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [tributacao] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // produtos
            List<CPSysUDB.Conexao.Campos> produtosCampos = new List<CPSysUDB.Conexao.Campos>();
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("id", "produtosCodigo", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("idSubcategorias", "produtosSubcategoriasCodigo", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("subcategorias"), db.getCampoByName(db.getTableByName("subcategorias"), "id")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "produtosLojasCodigo", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("idTributacao", "produtosTributacaoCodigo", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("tributacao"), db.getCampoByName(db.getTableByName("tributacao"), "id")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "produtosCadastro", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("statu", "produtosStatus", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "produtosNome", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("descricao", "produtosDescricao", "pro",
               new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("codigobarra", "produtosCodigobarra", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("preco", "produtosPreco", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("tipo", "produtosTipo", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("promocao", "produtosPromocao", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("desconto", "produtosDesconto", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("permiteAdicional", "produtosPermiteAdicional", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            produtosCampos.Add(new CPSysUDB.Conexao.Campos("ncm", "produtosNcm", "pro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));// usar ﻿Tabela do IBPT
            CPSysUDB.Conexao.Table produtos = new CPSysUDB.Conexao.Table("produtos", "pro", produtosCampos);
            db.NewTable(produtos, create, true);
            if (db.Error)
            {
                Erros += " [produtos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [produtos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // itens produtos
            List<CPSysUDB.Conexao.Campos> itensProdutosCampos = new List<CPSysUDB.Conexao.Campos>();
            itensProdutosCampos.Add(new CPSysUDB.Conexao.Campos("id", "itensProdutosCodigo", "ippro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            itensProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idItens", "itensProdutosItemCodigo", "ippro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("itens"), db.getCampoByName(db.getTableByName("itens"), "id")));
            itensProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idProdutos", "itensProdutosProdutoCodigo", "ippro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("produtos"), db.getCampoByName(db.getTableByName("produtos"), "id")));
            itensProdutosCampos.Add(new CPSysUDB.Conexao.Campos("quantidade", "itensProdutosProdutoQuantidade", "ippro",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            CPSysUDB.Conexao.Table itensprodutos = new CPSysUDB.Conexao.Table("itensprodutos", "ippro", itensProdutosCampos);
            db.NewTable(itensprodutos, create, true);
            if (db.Error)
            {
                Erros += " [itensprodutos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [itensprodutos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // combos
            List<CPSysUDB.Conexao.Campos> combosCampos = new List<CPSysUDB.Conexao.Campos>();
            combosCampos.Add(new CPSysUDB.Conexao.Campos("id", "combosCodigo", "com",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            combosCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "combosCadastro", "com",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            combosCampos.Add(new CPSysUDB.Conexao.Campos("statu", "combosStatus", "com",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            combosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "combosNome", "com",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            combosCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "combosLojasCodigo", "com",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            CPSysUDB.Conexao.Table combos = new CPSysUDB.Conexao.Table("combos", "com", combosCampos);
            db.NewTable(combos, create, true);
            if (db.Error)
            {
                Erros += " [combos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [combos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // obçoes combos
            List<CPSysUDB.Conexao.Campos> opcoesCombosCampos = new List<CPSysUDB.Conexao.Campos>();
            opcoesCombosCampos.Add(new CPSysUDB.Conexao.Campos("id", "opcoesCombosCodigo", "opcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            opcoesCombosCampos.Add(new CPSysUDB.Conexao.Campos("idCombos", "opcoesCombosCombosCodigo", "opcom",
               new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
               true, true, db.getTableByName("combos"), db.getCampoByName(db.getTableByName("combos"), "id")));
            opcoesCombosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "opcoesCombosNome", "opcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            opcoesCombosCampos.Add(new CPSysUDB.Conexao.Campos("statu", "opcoesCombosStatus", "opcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 obrigatorio 0, opcional
            CPSysUDB.Conexao.Table opcoescombos = new CPSysUDB.Conexao.Table("opcoescombos", "opcom", opcoesCombosCampos);
            db.NewTable(opcoescombos, create, true);
            if (db.Error)
            {
                Erros += " [opcoescombos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [opcoescombos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // opcçoes combos produtos
            List<CPSysUDB.Conexao.Campos> opcoesCombosProdutosCampos = new List<CPSysUDB.Conexao.Campos>();
            opcoesCombosProdutosCampos.Add(new CPSysUDB.Conexao.Campos("id", "opcoesCombosProdutosCodigo", "opcompr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            opcoesCombosProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idOpcoesCombos", "opcoesCombosProdutosOpcoesComboCodigo", "opcompr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("opcoescombos"), db.getCampoByName(db.getTableByName("opcoescombos"), "id")));
            opcoesCombosProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idProdutos", "opcoesCombosProdutosProdutoCodigo", "opcompr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("produtos"), db.getCampoByName(db.getTableByName("produtos"), "id")));
            opcoesCombosProdutosCampos.Add(new CPSysUDB.Conexao.Campos("quantidade", "opcoesCombosProdutosQuantidade", "opcompr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            opcoesCombosProdutosCampos.Add(new CPSysUDB.Conexao.Campos("preco", "opcoesCombosProdutosPreco", "opcompr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table opcoescombosprodutos = new CPSysUDB.Conexao.Table("opcoescombosprodutos", "opcompr", opcoesCombosProdutosCampos);
            db.NewTable(opcoescombosprodutos, create, true);
            if (db.Error)
            {
                Erros += " [opcoescombosprodutos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [opcoescombosprodutos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // caixas
            List<CPSysUDB.Conexao.Campos> caixasCampos = new List<CPSysUDB.Conexao.Campos>();
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("id", "caixasCodigo", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "caixasLojasCodigo", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("statu", "caixasStatus", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "caixasCadastro", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("nome", "caixasNome", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("statuAbertura", "caixasStatusAbertura", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 aberto 0, fechado
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("aberto", "caixasAberto", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("fechado", "caixasFechado", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("valor", "caixasValor", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            caixasCampos.Add(new CPSysUDB.Conexao.Campos("ultimaAbertura", "caixasUltimaAbertura", "cai",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "100")));
            CPSysUDB.Conexao.Table caixas = new CPSysUDB.Conexao.Table("caixas", "cai", caixasCampos);
            db.NewTable(caixas, create, true);
            if (db.Error)
            {
                Erros += " [caixas] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [caixas] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // formas pgto
            List<CPSysUDB.Conexao.Campos> formasPgtoCampos = new List<CPSysUDB.Conexao.Campos>();
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("id", "formasPgtoCodigo", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "formasPgtoLojasCodigo", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("statu", "formasPgtoStatus", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 atuvo 0, inativo
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "formasPgtoCadastro", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("nome", "formasPgtoNome", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("tipo", "formasPgtoTipo", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("juros", "formasPgtoJuros", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            formasPgtoCampos.Add(new CPSysUDB.Conexao.Campos("creden", "formasCreden", "frpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            CPSysUDB.Conexao.Table formaspgto = new CPSysUDB.Conexao.Table("formaspgto", "frpg", formasPgtoCampos);
            db.NewTable(formaspgto, create, true);
            if (db.Error)
            {
                Erros += " [formaspgto] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [formaspgto] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // sangria
            List<CPSysUDB.Conexao.Campos> sangriaCampos = new List<CPSysUDB.Conexao.Campos>();
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("id", "sangriaCodigo", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("idCaixa", "sangriaCaixaCodigo", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("caixas"), db.getCampoByName(db.getTableByName("caixas"), "id")));
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "sangriaCadastro", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("valor", "sangriaValor", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("caixa", "sangriaCaixa", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("motivo", "sangriaMotivo", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            sangriaCampos.Add(new CPSysUDB.Conexao.Campos("descricao", "caixasNome", "san",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            CPSysUDB.Conexao.Table sangria = new CPSysUDB.Conexao.Table("sangria", "san", sangriaCampos);
            db.NewTable(sangria, create, true);
            if (db.Error)
            {
                Erros += " [sangria] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [sangria] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // contas pagar
            List<CPSysUDB.Conexao.Campos> contasPagarCampos = new List<CPSysUDB.Conexao.Campos>();
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("id", "contasPagarCodigo", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "contasPagarLojasCodigo", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("idForma", "contasPagarFormaCodigo", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("formaspgto"), db.getCampoByName(db.getTableByName("formaspgto"), "id")));
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("statu", "contasPagarStatus", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 ativo 0, inativo
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "contasPagarCadastro", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("valor", "contasPagarValor", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("vencimento", "contasPagarVencimento", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            contasPagarCampos.Add(new CPSysUDB.Conexao.Campos("descricao", "contasPagarNome", "conpg",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            CPSysUDB.Conexao.Table contaspagar = new CPSysUDB.Conexao.Table("contaspagar", "conpg", contasPagarCampos);
            db.NewTable(contaspagar, create, true);
            if (db.Error)
            {
                Erros += " [contaspagar] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [contaspagar] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // balcao
            List<CPSysUDB.Conexao.Campos> balcaoCampos = new List<CPSysUDB.Conexao.Campos>();
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("id", "balcaoCodigo", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "balcaoLojasCodigo", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("statu", "balcaoStatus", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 aberto 0, fechado
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "balcaoCadastro", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("adicional", "balcaoAdicional", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("desconto", "balcaoDesconto", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("tipo", "balcaoTipo", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 balcao 2, mesa 3, delivery
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("nome", "balcaoNome", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("comanda", "balcaoComanda", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("mesa", "balcaoMesa", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("forma", "balcaoForma", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("troco", "balcaoTroco", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("telefone", "balcaoTelefone", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("endereco", "balcaoEndereco", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("obs", "balcaoObs", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("descricao", "balcaoDescricao", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoCampos.Add(new CPSysUDB.Conexao.Campos("total", "balcaoTotal", "bal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table balcao = new CPSysUDB.Conexao.Table("balcao", "bal", balcaoCampos);
            db.NewTable(balcao, create, true);
            if (db.Error)
            {
                Erros += " [balcao] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [balcao] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // produtos balcao
            List<CPSysUDB.Conexao.Campos> balcaoProdutosCampos = new List<CPSysUDB.Conexao.Campos>();
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("id", "balcaoProdutosCodigo", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idBalcao", "balcaoProdutosBalcaoCodigo", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("balcao"), db.getCampoByName(db.getTableByName("balcao"), "id")));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idProdutos", "balcaoProdutosProdutoCodigo", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("produtos"), db.getCampoByName(db.getTableByName("produtos"), "id")));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("statu", "balcaoProdutosStatus", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 aberto 0, produzido -1, adicionado
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("quantidade", "balcaoProdutosQuantidade", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("preco", "balcaoProdutosPreco", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "balcaoProdutosNome", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idCaixas", "balcaoProdutosCaixasCodigo", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("caixas"), db.getCampoByName(db.getTableByName("caixas"), "id")));
            balcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("precoaux", "balcaoProdutosPrecoaux", "balpr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table balcaoprodutos = new CPSysUDB.Conexao.Table("balcaoprodutos", "balpr", balcaoProdutosCampos);
            db.NewTable(balcaoprodutos, create, true);
            if (db.Error)
            {
                Erros += " [balcaoprodutos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [balcaoprodutos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // itens produtos balcao
            List<CPSysUDB.Conexao.Campos> itensBalcaoProdutosCampos = new List<CPSysUDB.Conexao.Campos>();
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("id", "itensBalcaoProdutosCodigo", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idItens", "itensBalcaoProdutosItemCodigo", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("itens"), db.getCampoByName(db.getTableByName("itens"), "id")));
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idBalcaoProdutos", "balcaoProdutosCodigo", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("balcaoprodutos"), db.getCampoByName(db.getTableByName("balcaoprodutos"), "id")));
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("quantidade", "itensBalcaoProdutosQuantidade", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("nome", "itensBalcaoProdutosNome", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idBalcao", "itensBalcaoProdutosBalcaoCodigo", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("balcao"), db.getCampoByName(db.getTableByName("balcao"), "id")));
            itensBalcaoProdutosCampos.Add(new CPSysUDB.Conexao.Campos("valor", "itensBalcaoProdutosValor", "balprit",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table itensbalcaoprodutos = new CPSysUDB.Conexao.Table("itensbalcaoprodutos", "balprit", itensBalcaoProdutosCampos);
            db.NewTable(itensbalcaoprodutos, create, true);
            if (db.Error)
            {
                Erros += " [itensbalcaoprodutos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [itensbalcaoprodutos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // pagamentos balcao
            List<CPSysUDB.Conexao.Campos> pagamentosBalcaoCampos = new List<CPSysUDB.Conexao.Campos>();
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("id", "pagamentosBalcaoCodigo", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("idBalcao", "pagamentosBalcaoBalcaoCodigo", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("balcao"), db.getCampoByName(db.getTableByName("balcao"), "id")));
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("idCaixas", "pagamentosBalcaoCaixasCodigo", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("caixas"), db.getCampoByName(db.getTableByName("caixas"), "id")));
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("idForma", "pagamentosBalcaoFormapgtoCodigo", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("formaspgto"), db.getCampoByName(db.getTableByName("formaspgto"), "id")));
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "pagamentosBalcaoCadastro", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("cpf", "pagamentosBalcaoCpf", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.VARCHAR, "100")));
            pagamentosBalcaoCampos.Add(new CPSysUDB.Conexao.Campos("valor", "pagamentosBalcaoValor", "pgtbal",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table pagamentosbalcao = new CPSysUDB.Conexao.Table("pagamentosbalcao", "pgtbal", pagamentosBalcaoCampos);
            db.NewTable(pagamentosbalcao, create, true);
            if (db.Error)
            {
                Erros += " [pagamentosbalcao] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [pagamentosbalcao] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // compra
            List<CPSysUDB.Conexao.Campos> compraCampos = new List<CPSysUDB.Conexao.Campos>();
            compraCampos.Add(new CPSysUDB.Conexao.Campos("id", "compraCodigo", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "compraLojasCodigo", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("statu", "compraStatus", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 aberto 0, fechado
            compraCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "balcaoCadastro", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("adicional", "compraAdicional", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("desconto", "compraDesconto", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("tipo", "compraTipo", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));// 1 balcao 2, mesa 3, delivery
            compraCampos.Add(new CPSysUDB.Conexao.Campos("nome", "compraNome", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("cnpj", "compraCnpj", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("descricao", "compraDescricao", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "1000")));
            compraCampos.Add(new CPSysUDB.Conexao.Campos("total", "compraTotal", "comp",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table compra = new CPSysUDB.Conexao.Table("compra", "comp", compraCampos);
            db.NewTable(compra, create, true);
            if (db.Error)
            {
                Erros += " [compra] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [compra] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // produtos compra
            List<CPSysUDB.Conexao.Campos> compraProdutosCampos = new List<CPSysUDB.Conexao.Campos>();
            compraProdutosCampos.Add(new CPSysUDB.Conexao.Campos("id", "compraProdutosCodigo", "comppr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            compraProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idCompra", "compraProdutosCompraCodigo", "comppr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("compra"), db.getCampoByName(db.getTableByName("compra"), "id")));
            compraProdutosCampos.Add(new CPSysUDB.Conexao.Campos("idItens", "compraProdutosItensCodigo", "comppr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("itens"), db.getCampoByName(db.getTableByName("itens"), "id")));
            compraProdutosCampos.Add(new CPSysUDB.Conexao.Campos("quantidade", "compraProdutosQuantidade", "comppr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,5")));
            compraProdutosCampos.Add(new CPSysUDB.Conexao.Campos("preco", "compraProdutosPreco", "comppr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table compraprodutos = new CPSysUDB.Conexao.Table("compraprodutos", "comppr", compraProdutosCampos);
            db.NewTable(compraprodutos, create, true);
            if (db.Error)
            {
                Erros += " [compraprodutos] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [compraprodutos] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // compra pagamentos
            List<CPSysUDB.Conexao.Campos> pagamentosCompraCampos = new List<CPSysUDB.Conexao.Campos>();
            pagamentosCompraCampos.Add(new CPSysUDB.Conexao.Campos("id", "pagamentosCompraCodigo", "pgtcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            pagamentosCompraCampos.Add(new CPSysUDB.Conexao.Campos("idCompra", "pagamentosCompraCompraCodigo", "pgtcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("compra"), db.getCampoByName(db.getTableByName("compra"), "id")));
            pagamentosCompraCampos.Add(new CPSysUDB.Conexao.Campos("idCaixas", "pagamentosCompraCaixasCodigo", "pgtcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("caixas"), db.getCampoByName(db.getTableByName("caixas"), "id")));
            pagamentosCompraCampos.Add(new CPSysUDB.Conexao.Campos("idForma", "pagamentosCompraFormapgtoCodigo", "pgtcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("formaspgto"), db.getCampoByName(db.getTableByName("formaspgto"), "id")));
            pagamentosCompraCampos.Add(new CPSysUDB.Conexao.Campos("cadastro", "pagamentosCompraCadastro", "pgtcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DATETIME)));
            pagamentosCompraCampos.Add(new CPSysUDB.Conexao.Campos("valor", "pagamentosCompraValor", "pgtcom",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.DOUBLE, "10,2")));
            CPSysUDB.Conexao.Table pagamentoscompra = new CPSysUDB.Conexao.Table("pagamentoscompra", "pgtcom", pagamentosCompraCampos);
            db.NewTable(pagamentoscompra, create, true);
            if (db.Error)
            {
                Erros += " [pagamentoscompra] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [pagamentoscompra] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // impressoes
            List<CPSysUDB.Conexao.Campos> impressaoCampos = new List<CPSysUDB.Conexao.Campos>();
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("id", "impressaoCodigo", "impr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), true, true));
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("idLojas", "impressaoLojasCodigo", "impr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT), false, false,
                true, true, db.getTableByName("lojas"), db.getCampoByName(db.getTableByName("lojas"), "id")));
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("venda", "impressaoVenda", "impr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("cate", "impressaoCate", "impr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("indx", "impressaoIndx", "impr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("tipo", "impressaoTipo", "impr",// 1 alimento 1, bebida 0, padrao
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.INT)));
            impressaoCampos.Add(new CPSysUDB.Conexao.Campos("texto", "impressaoTexto", "impr",
                new CPSysUDB.Conexao.TypeCampos(CPSysUDB.Conexao.Campos.Types.NVARCHAR, "2000")));
            CPSysUDB.Conexao.Table impressao = new CPSysUDB.Conexao.Table("impressao", "impr", impressaoCampos);
            db.NewTable(impressao, create, true);
            if (db.Error)
            {
                Erros += " [impressao] ErrorMsg=" + db.ErrorMsg + "\n";
            }
            if (db.ErrorConexao)
            {
                Erros += " [impressao] ErrorMsgConexao=" + db.ErrorMsgConexao + "\n";
            }

            // inserindo padroes
            if (create)
            {
                Insert(db);
            }
            return db;
        }

        private static void Insert(CPSysUDB.Conexao db)
        {
            // telas
            if (ValidaExistente(db, "telas", "nome", "PDV") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("PDV");
                tela.Add("PDV");
                tela.Add("Pedido de Venda");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmRapida") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmRapida");
                tela.Add("PDV");
                tela.Add("Venda Rápida");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmComanda") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmComanda");
                tela.Add("PDV");
                tela.Add("Comandas");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmDelivery") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmDelivery");
                tela.Add("PDV");
                tela.Add("Delivery");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmCompra") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmCompra");
                tela.Add("PDV");
                tela.Add("Compra de Item");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmSangria") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmSangria");
                tela.Add("PDV");
                tela.Add("Sangria de Caixa");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmCozinha") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmCozinha");
                tela.Add("PDV");
                tela.Add("Pedidos da Cozinha");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "Estoque") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("Estoque");
                tela.Add("Estoque");
                tela.Add("Controle de Estoque");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmEstoque") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmEstoque");
                tela.Add("Estoque");
                tela.Add("Controle do Estoque");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "Financeiro") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("Financeiro");
                tela.Add("Financeiro");
                tela.Add("Financeiro");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmTributa") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmTributa");
                tela.Add("Financeiro");
                tela.Add("Tirbutação dos Produtos");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmFormas") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmFormas");
                tela.Add("Financeiro");
                tela.Add("Formas de Pagamento");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmLanca") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmLanca");
                tela.Add("Financeiro");
                tela.Add("Lançamentos");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmPagar") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmPagar");
                tela.Add("Financeiro");
                tela.Add("Contas a Pagar");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmReceber") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmReceber");
                tela.Add("Financeiro");
                tela.Add("Contas a Receber");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "Cadastros") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("Cadastros");
                tela.Add("Cadastros");
                tela.Add("Cadastros");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmAcessos") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmAcessos");
                tela.Add("Cadastros");
                tela.Add("Acessos");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmLojas") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmLojas");
                tela.Add("Cadastros");
                tela.Add("Lojas");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmUsers") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmUsers");
                tela.Add("Cadastros");
                tela.Add("Usuários");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmComandas") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmComandas");
                tela.Add("Cadastros");
                tela.Add("Comandas");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmSubCat") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmSubCat");
                tela.Add("Cadastros");
                tela.Add("Subcategorias");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmItens") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmItens");
                tela.Add("Cadastros");
                tela.Add("Itens");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmProd") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmProd");
                tela.Add("Cadastros");
                tela.Add("Produtos");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmCombo") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmCombo");
                tela.Add("Cadastros");
                tela.Add("Combos");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmCaixa") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmCaixa");
                tela.Add("Cadastros");
                tela.Add("Caixas");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "Configuracoes") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("Configuracoes");
                tela.Add("Configurações");
                tela.Add("Configurações");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmLocal") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmLocal");
                tela.Add("Configurações");
                tela.Add("Máquina Local");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "Relatorios") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("Relatorios");
                tela.Add("Relatórios");
                tela.Add("Relatórios");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmLucro") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmLucro");
                tela.Add("Relatórios");
                tela.Add("Lucro Mensal");
                db.Insert(db.getTableByName("telas"), tela);
            }

            if (ValidaExistente(db, "telas", "nome", "frmQrcode") == 0)
            {
                List<object> tela = new List<object>();
                tela.Add("frmQrcode");
                tela.Add("Configurações");
                tela.Add("QrCode das Comandas");
                db.Insert(db.getTableByName("telas"), tela);
            }

            // insert acessos ADM
            if (ValidaExistente(db, "acessos", "nome", "Administrador") == 0)
            {
                List<object> acessosADM = new List<object>();
                acessosADM.Add("Administrador");
                acessosADM.Add(1);
                acessosADM.Add(1);
                db.Insert(db.getTableByName("acessos"), acessosADM);
                // telas de acessos
                List<object> PDV = new List<object>();
                PDV.Add(1); // tela
                PDV.Add(1); // acesso
                db.Insert(db.getTableByName("telasacessos"), PDV);

                List<object> frmRapida = new List<object>();
                frmRapida.Add(2);
                frmRapida.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmRapida);

                List<object> frmComanda = new List<object>();
                frmComanda.Add(3);
                frmComanda.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmComanda);

                List<object> frmDelivery = new List<object>();
                frmDelivery.Add(4);
                frmDelivery.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmDelivery);

                List<object> frmCompra = new List<object>();
                frmCompra.Add(5);
                frmCompra.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmCompra);

                List<object> frmSangria = new List<object>();
                frmSangria.Add(6);
                frmSangria.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmSangria);

                List<object> frmCozinha = new List<object>();
                frmCozinha.Add(7);
                frmCozinha.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmCozinha);

                List<object> Estoque = new List<object>();
                Estoque.Add(8);
                Estoque.Add(1);
                db.Insert(db.getTableByName("telasacessos"), Estoque);

                List<object> frmEstoque = new List<object>();
                frmEstoque.Add(9);
                frmEstoque.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmEstoque);

                List<object> Financeiro = new List<object>();
                Financeiro.Add(10);
                Financeiro.Add(1);
                db.Insert(db.getTableByName("telasacessos"), Financeiro);

                List<object> frmTributa = new List<object>();
                frmTributa.Add(11);
                frmTributa.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmTributa);

                List<object> frmFormas = new List<object>();
                frmFormas.Add(12);
                frmFormas.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmFormas);

                List<object> frmLanca = new List<object>();
                frmLanca.Add(13);
                frmLanca.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmLanca);

                List<object> frmPagar = new List<object>();
                frmPagar.Add(14);
                frmPagar.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmPagar);

                List<object> frmReceber = new List<object>();
                frmReceber.Add(15);
                frmReceber.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmReceber);

                List<object> Cadastros = new List<object>();
                Cadastros.Add(16);
                Cadastros.Add(1);
                db.Insert(db.getTableByName("telasacessos"), Cadastros);

                List<object> frmAcessos = new List<object>();
                frmAcessos.Add(17);
                frmAcessos.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmAcessos);

                List<object> frmLojas = new List<object>();
                frmLojas.Add(18);
                frmLojas.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmLojas);

                List<object> frmUsers = new List<object>();
                frmUsers.Add(19);
                frmUsers.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmUsers);

                List<object> frmComandas = new List<object>();
                frmComandas.Add(20);
                frmComandas.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmComandas);

                List<object> frmSubCat = new List<object>();
                frmSubCat.Add(21);
                frmSubCat.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmSubCat);

                List<object> frmItens = new List<object>();
                frmItens.Add(22);
                frmItens.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmItens);

                List<object> frmProd = new List<object>();
                frmProd.Add(23);
                frmProd.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmProd);

                List<object> frmCombo = new List<object>();
                frmCombo.Add(24);
                frmCombo.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmCombo);

                List<object> frmCaixa = new List<object>();
                frmCaixa.Add(25);
                frmCaixa.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmCaixa);

                List<object> Configuracoes = new List<object>();
                Configuracoes.Add(26);
                Configuracoes.Add(1);
                db.Insert(db.getTableByName("telasacessos"), Configuracoes);

                List<object> frmLocal = new List<object>();
                frmLocal.Add(27);
                frmLocal.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmLocal);

                List<object> Relatorios = new List<object>();
                Relatorios.Add(28);
                Relatorios.Add(1);
                db.Insert(db.getTableByName("telasacessos"), Relatorios);

                List<object> frmLucro = new List<object>();
                frmLucro.Add(29);
                frmLucro.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmLucro);

                List<object> frmQrcode = new List<object>();
                frmQrcode.Add(30);
                frmQrcode.Add(1);
                db.Insert(db.getTableByName("telasacessos"), frmQrcode);
            }

            // insert acessos Caixa
            if (ValidaExistente(db, "acessos", "nome", "Caixa") == 0)
            {
                List<object> acessosCaixa = new List<object>();
                acessosCaixa.Add("Caixa");
                acessosCaixa.Add(0);
                acessosCaixa.Add(1);
                db.Insert(db.getTableByName("acessos"), acessosCaixa);
                // telas de acessos
                List<object> PDV = new List<object>();
                PDV.Add(1);
                PDV.Add(2);
                db.Insert(db.getTableByName("telasacessos"), PDV);

                List<object> frmRapida = new List<object>();
                frmRapida.Add(2);
                frmRapida.Add(2);
                db.Insert(db.getTableByName("telasacessos"), frmRapida);

                List<object> frmComanda = new List<object>();
                frmComanda.Add(3);
                frmComanda.Add(2);
                db.Insert(db.getTableByName("telasacessos"), frmComanda);

                List<object> frmDelivery = new List<object>();
                frmDelivery.Add(4);
                frmDelivery.Add(2);
                db.Insert(db.getTableByName("telasacessos"), frmDelivery);
            }

            // insert acessos Estoque
            if (ValidaExistente(db, "acessos", "nome", "Estoque") == 0)
            {
                List<object> estoqueCaixa = new List<object>();
                estoqueCaixa.Add("Estoque");
                estoqueCaixa.Add(0);
                estoqueCaixa.Add(1);
                db.Insert(db.getTableByName("acessos"), estoqueCaixa);
                // telas de acessos
                List<object> Estoque = new List<object>();
                Estoque.Add(8);
                Estoque.Add(3);
                db.Insert(db.getTableByName("telasacessos"), Estoque);

                List<object> frmEstoque = new List<object>();
                frmEstoque.Add(9);
                frmEstoque.Add(3);
                db.Insert(db.getTableByName("telasacessos"), frmEstoque);
            }

            // insert acessos Cozinha
            if (ValidaExistente(db, "acessos", "nome", "Cozinha") == 0)
            {
                List<object> cozinhaCaixa = new List<object>();
                cozinhaCaixa.Add("Cozinha");
                cozinhaCaixa.Add(0);
                cozinhaCaixa.Add(1);
                db.Insert(db.getTableByName("acessos"), cozinhaCaixa);
                // telas de acessos
                List<object> PDV = new List<object>();
                PDV.Add(1);
                PDV.Add(4);
                db.Insert(db.getTableByName("telasacessos"), PDV);

                List<object> frmCozinha = new List<object>();
                frmCozinha.Add(7);
                frmCozinha.Add(4);
                db.Insert(db.getTableByName("telasacessos"), frmCozinha);
            }

            // loja padrao
            if (ValidaExistente(db, "lojas", "nome", "Matriz") == 0)
            {
                List<object> loja = new List<object>();
                loja.Add("Matriz");
                loja.Add("");
                loja.Add("");
                loja.Add("");
                loja.Add("");
                loja.Add(1);
                loja.Add(0);
                loja.Add(0);
                loja.Add(0);
                loja.Add(0);
                loja.Add(0);
                loja.Add(0);
                db.Insert(db.getTableByName("lojas"), loja);
            }

            // usuario padrao
            if (ValidaExistente(db, "usuarios", "usuario", "manager") == 0)
            {
                List<object> usuario = new List<object>();
                usuario.Add(1);
                usuario.Add(1);
                usuario.Add(1);
                usuario.Add(DateTime.Now);
                usuario.Add("Manager");
                usuario.Add("manager");
                usuario.Add("81dc9bdb52d04dc20036dbd8313ed055");// 1234
                db.Insert(db.getTableByName("usuarios"), usuario);
            }

            // comandas padrao
            for (int x = 1; x <= 100; x++)
            {
                if (ValidaExistente(db, "comandas", "nome", x.ToString()) == 0)
                {
                    List<object> comandas = new List<object>();
                    comandas.Add(1);
                    comandas.Add(x.ToString());
                    comandas.Add(1);
                    db.Insert(db.getTableByName("comandas"), comandas);
                }
            }

            // categorias padrao
            if (ValidaExistente(db, "categorias", "nome", "Alimento") == 0)
            {
                List<object> categorias = new List<object>();
                categorias.Add("Alimento");
                db.Insert(db.getTableByName("categorias"), categorias);
            }

            if (ValidaExistente(db, "categorias", "nome", "Bebida") == 0)
            {
                List<object> categorias1 = new List<object>();
                categorias1.Add("Bebida");
                db.Insert(db.getTableByName("categorias"), categorias1);
            }

            // categorias itens padrao
            if (ValidaExistente(db, "categoriasitens", "nome", "Adicional") == 0)
            {
                List<object> categorias = new List<object>();
                categorias.Add("Adicional - Normal");
                db.Insert(db.getTableByName("categoriasitens"), categorias); // pode ser adicionado no produto e tambem usado como adicional e pode ser removido
            }

            if (ValidaExistente(db, "categoriasitens", "nome", "Insumo") == 0)
            {
                List<object> categorias1 = new List<object>();
                categorias1.Add("Insumo");
                db.Insert(db.getTableByName("categoriasitens"), categorias1); // nao pode ser visto para alterar os itens
            }

            if (ValidaExistente(db, "categoriasitens", "nome", "Normal") == 0)
            {
                List<object> categorias2 = new List<object>();
                categorias2.Add("Normal");
                db.Insert(db.getTableByName("categoriasitens"), categorias2); // o item pode ser removido
            }

            if (ValidaExistente(db, "categoriasitens", "nome", "Não Removível") == 0)
            {
                List<object> categorias3 = new List<object>();
                categorias3.Add("Não Removível");
                db.Insert(db.getTableByName("categoriasitens"), categorias3); // nao pode ser removido
            }

            // caixa padrao
            if (ValidaExistente(db, "caixas", "nome", "Caixa 1") == 0)
            {
                List<object> caixa = new List<object>();
                caixa.Add(1);
                caixa.Add(1);
                caixa.Add(DateTime.Now);
                caixa.Add("Caixa 1");
                caixa.Add(0);
                caixa.Add(DateTime.Now);
                caixa.Add(DateTime.Now);
                caixa.Add(0);
                caixa.Add("");
                db.Insert(db.getTableByName("caixas"), caixa);
            }

            // formas pgto padrao
            if (ValidaExistente(db, "formaspgto", "nome", "Dinheiro") == 0)
            {
                List<object> formas = new List<object>();
                formas.Add(1);
                formas.Add(1);
                formas.Add(DateTime.Now);
                formas.Add("Dinheiro");
                formas.Add("01");
                formas.Add(0);
                formas.Add(0);
                db.Insert(db.getTableByName("formaspgto"), formas);
            }

            // tributacao padrao
            if (ValidaExistente(db, "tributacao", "nome", "Nenhuma") == 0)
            {
                List<object> tributacao = new List<object>();
                tributacao.Add("Nenhuma");
                tributacao.Add("0");
                tributacao.Add("900");
                tributacao.Add("5101");
                tributacao.Add("0");
                db.Insert(db.getTableByName("tributacao"), tributacao);
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
                    CPSysUDB.Conexao.Where.Command.EQUALS, valor));
                select.Add(new CPSysUDB.Conexao.Select(db.getTableByName(table), campos, false, null, where));
                DataSet ds = db.SelectValue(select);

                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                return -1;
            }
        }*/
    }
}
