using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Processos.Financeiro
{
    public class GeracaoNotasFaturasBoletos
    {
        public string ReadInputData(string arquivoExportacaoFaturas, string relatorioContasReceber, string arquivoSaida)
        {
            if (!File.Exists(arquivoExportacaoFaturas))
                throw new Exception("O arquivo \"" + arquivoExportacaoFaturas + "\" não existe.");

            if (!File.Exists(relatorioContasReceber))
                throw new Exception("O arquivo \"" + relatorioContasReceber + "\" não existe.");

            string result = "Titulo Inicial;Titulo Final;Arquivo";
            List<string> lines;           
            List<string> linesAux;
            int counter = 0;
            string tituloInicial;
            string tituloFinal;
            string arquivo = "Export";

            lines = File.ReadAllLines(arquivoExportacaoFaturas)
                .Select(line => line.Trim()).Where(line => Regex.IsMatch(line, "^[0-9]{9,9}"))
                .Select(line => line.Substring(83).Split(' ')[0])
                .ToList();

            if (lines.Count > 0)
            {
                counter = (int)Math.Ceiling(Convert.ToDecimal(lines.Count / 5000)) + 1;

                //Console.WriteLine("Total de linhas " + lines.Count);
                //Console.WriteLine("Contador " + counter);

                for (int i = 0; i < counter; i++)
                {
                    if (lines.Count > 5000)
                    {
                        linesAux = lines.Take(5000).ToList();
                        lines.RemoveRange(0, 5000);
                    }
                    else
                        linesAux = lines.ToList();

                    tituloInicial = linesAux.First();
                    tituloFinal = linesAux.Last();

                    //Console.WriteLine("Titulo Inicial: " + tituloInicial);
                    //Console.WriteLine("Titulo Final: " + tituloFinal);
                    //Console.WriteLine("Arquivo: " + arquivo);

                    result = result + "|" + tituloInicial + ";" + tituloFinal + ";" + arquivo;
                }
            }


            arquivo = "Contas";

            var wb = new XLWorkbook(relatorioContasReceber, XLEventTracking.Disabled).Worksheet(1);

            lines = wb.Rows()
                .Select(row => new { titulo = row.Cell("E").Value.ToString().Trim(), ocorrencia = row.Cell("I").Value.ToString().Trim() })
                .Where(row => Regex.IsMatch(row.titulo, "^[0-9]*") && row.ocorrencia.ToUpper() == "ENTRADA CONFIRMADA")
                .Select(row => row.titulo)
                .ToList();

            if (lines.Count > 0)
            { 
                //File.WriteAllLines(@"C:\Users\steph\Unimedpoa\RPA - Documents\Financeiro\Geração de Notas, Faturas e Boletos\Arquivos Complementares\Geração de Boletos\output-sem-split.csv", lines);

                counter = (int)Math.Ceiling(Convert.ToDecimal(lines.Count / 5000)) + 1;

                //Console.WriteLine("Total de linhas " + lines.Count);
                //Console.WriteLine("Contador " + counter);

                for (int i = 0; i < counter; i++)
                {
                    if (lines.Count > 5000)
                    {
                        linesAux = lines.Take(5000).ToList();
                        lines.RemoveRange(0, 5000);
                    }
                    else
                        linesAux = lines.ToList();

                    tituloInicial = linesAux.First();
                    tituloFinal = linesAux.Last();

                    //Console.WriteLine("Arquivo: " + arquivo);
                    //Console.WriteLine("Titulo Inicial: " + tituloInicial);
                    //Console.WriteLine("Titulo Final: " + tituloFinal);

                    result = result + "|" + tituloInicial + ";" + tituloFinal + ";" + arquivo;
                }
            }


            //Console.WriteLine("Result: " + result);
            File.WriteAllLines(arquivoSaida, result.Split('|'));

            //Console.Read();

            return arquivoSaida;
        }
    }
}
