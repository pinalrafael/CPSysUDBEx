using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSysUDBEx.ClassesCPSysSQLFramework2
{
    [CPSysUDB.Attribute.ClassAttribute("t")]// DEFINIR COMO TABELA DE BANCO DE DADOS E INFORMAR UM APELIDO ÚNICO PARA ELA
    public class telasacessos
    {
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.PRIMARY_KEY_IDENTITY)]// DEFINIR COMO CAMPO DA TAELA TIPO DE CAMPO PK AUTO INCREMENTO
        public int id { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL, "10,2")]// CAMPOS DOUBLE É DECIMAL(10,2) NO BANCO DE DADOS
        public double valor { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.NORMAL)]
        public DateTime dataehora { get; set; }
        [CPSysUDB.Attribute.SQLAttribute(CPSysUDB.Enums.SQLTypes.FOREIGN_KEY, typeof(acessos), "id")]// PARA CRIAR UMA FK É NECESSÁRIO INFORMAR A CLASSE PAI E O CAMPO REFERENTE
        public int idAcessos { get; set; }
    }
}
