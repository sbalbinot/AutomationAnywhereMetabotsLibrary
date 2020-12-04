﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    class Program
    {
        
        public static void Main()
        {
            string xmlFile = "C:\\Users\\Zdan\\Desktop\\xmlTestStephano.xml";
            XML x = new XML();
            //string retorno = x.fileFindAllXPathValues("C:\\Users\\Zdan\\Desktop\\xmlTestStephano.xml", "/ConsultaGuia/guias/guia/movimentos/movimento/cdMovimento");
            //string retorno = x.fileFindAllXPathValues(xmlFile, "/ConsultaGuia/guias/guia/movimentos/movimento/cdMovimento[../tipo/text()='I' and ../insOpme/text()='1' and (substring(text(),'1','2')='60' or substring(text(),'1','2')='92')]");////Row/movimentos/movimento/cdMovimento[(../tipo/text()='I') and (string(../insOpme/text())='' or ../insOpme/text()='0') and (string(../insDiaria/text())='' or ../insDiaria/text()='0') and (../nrPacote/text()='0' or string(../nrPacote/text())='')]
            string retorno = x.fileFindAllXPathValues(xmlFile, "//ConsultaGuia/guias/guia/@ID[text()='202002552375' and ../chaveGuia/text()!='202002552375']");////Row/movimentos/movimento/cdMovimento[(../tipo/text()='I') and (string(../insOpme/text())='' or ../insOpme/text()='0') and (string(../insDiaria/text())='' or ../insDiaria/text()='0') and (../nrPacote/text()='0' or string(../nrPacote/text())='')]
                                                                                                                                                                                                         ////Row/movimentos/movimento/cdMoviment[../tipo/text()='I' and ../insOpme/text()='1']//Row/chaveGuia[text()='$guia_principal_chave$' and (../cdLocalAutorizacao/text()='175' or ../cdLocalAutorizacao/text()='158' or ../cdLocalAutorizacao/text()='56' or ../cdLocalAutorizacao/text()='185' or ../cdLocalAutorizacao/text()='186' or ../cdLocalAutorizacao/text()='184' or ../cdLocalAutorizacao/text()='189')] //175,158,56,185,186,184 ou 189
                                                                                                                                                                                                         //Row/movimentos/movimento/cdMovimento[../tipo/text()='P']

            //Movimentos especialidade ORTO, CARDIO ou ONCO
            //Row/movimentos/movimento/cdMovimento[../tipo/text()='P' and (../cdEspProc/text()='ORTO' or ../cdEspProc/text()='CARDIO' or ../cdEspProc/text()='ONCO')]

            //Buscar guia complementar de uma guia principal
            //ConsultaGuia/guias/guia/principal[text()='202002552375' and ../chaveGuia/text()!='202002552375']

            Console.WriteLine(retorno);
            Console.Read();

            //Busca o Parent Node que contiver o código do movimento passado como parâmetro (busca o node do movimento inteiro)
            retorno = x.fileFindParentNodeByChildValueIncludes(xmlFile, "procBaixoRisco", "1");

            Console.WriteLine(retorno);

            //Verifica se tem pacote (se o nrPacote for zero ou em branco, não tem pacote)
            string nrPacote = x.getValueByNode(retorno, "nrPacote");

            Console.WriteLine(nrPacote);

            //Busca o prestador principal
            string prestadorPrincipal = x.fileGetValueByNode(xmlFile, "chavePrestadorPrincipal");

            Console.WriteLine(prestadorPrincipal);

            Console.Read();

            //Date d = new Date();

            //string date = d.formatDate("2020-11-09 16:06:36", "dd/MM/yyyy HH:mm:ss");
            //Console.WriteLine(date);

            //double hours = d.DiffTotalHours(date, "09/11/2020 16:17:12", "dd/MM/yyyy HH:mm:ss");
            //Console.WriteLine(hours);
            //Console.Read();

            //string s;

            //int i;

            //i = d.GetYear("23/12/2020", "dd/mm/yyyy");

            //Console.WriteLine(i);
            //Console.ReadLine();
            //Number n = new Number();
            //string ret = n.SumDecimal("50,00", "-6.818,15");
            //Console.WriteLine(ret);
            //Console.ReadLine();
        }
    }
}
