using CPSysUDB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSysUDBEx.ClassesCPSysSQLFramework3
{
    public enum Grade
    {
        A, B, C, D, F
    }

    [CPSysUDB.Attribute.ClassAttribute("a", // DEFINIR COMO TABELA DE BANCO DE DADOS E INFORMAR UM APELIDO ÚNICO PARA ELA
        "permissoes")] // INFORMA O PREFIXO DA TABELA
    public class acessos : Table
    {
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.PRIMARY_KEY_IDENTITY)]// DEFINIR COMO CAMPO DA TAELA TIPO DE CAMPO PK AUTO INCREMENTO
        public int id { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL, "100")]// DEFINIR COMO CAMPO DA TAELA TIPO NOMAL E INFORMAR O TAMANHO DO CAMPO EX: NVARCHAR(100)
        public string nome { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL)]// PARA CAMPOS QUE NÃO TEM TAMANHO NO BANCO DE DADOS NÃO PRECISA INFORMAR
        public int statu { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL, "10,2")]// CAMPOS DOUBLE É DECIMAL(10,2) NO BANCO DE DADOS
        public double valor { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL)]
        public DateTime dataehora { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL)]// PARA CAMPOS ENUM
        public Grade grade { get; set; }
    }
}
