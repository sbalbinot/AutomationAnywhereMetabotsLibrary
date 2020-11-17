using System;
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
            string retorno = x.fileFindAllXPathValues(xmlFile, "/ConsultaGuia/guias/guia/movimentos/movimento/cdMovimento");

            Console.WriteLine(retorno);

            //Busca o Parent Node que contiver o código do movimento passado como parâmetro (busca o node do movimento inteiro)
            retorno = x.fileFindParentNodeByChildValueIncludes(xmlFile, "cdMovimento", "40202s550");

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
