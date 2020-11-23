using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Integration
{
    //CSV
    public class Workbook
    {
        //Remove os valores duplicados de um csv.
        //Delimitador do CSV: delimitado por ";" - Caso necessite outra quebra, só adicionar isso como parâmetro...
        //
        //@params
        //
        //@duplicatedColumnName - Nome da coluna que repete o valor
        //@referenceColumnname - Nome da coluna que tem o valor que difere as duas linhas repetidas (que possua valor diferente de @correctReferenteValue)
        //@correctReferenceValue - valor incorreto que a coluna @referenceColumnName do registro repetido pode possuir.
        //@ignoredDuplicatedColumnValue - Valor que não precisa ser verificado na coluna @duplicatedColumnName
        public void RemoveDuplicatedRowsByColumnsValue(string workbookName, string duplicatedColumnName, string referenceColumnName, string correctReferenceValue, string ignoredDuplicatedColumnValue)
        {
            int indexPrimeiraColuna = 999;
            int indexSegundaColuna = 999;
            List<string> primeirasColunasCorretasIdentificadas = new List<string>();
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(workbookName))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var cols = line.Split(';');
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (indexPrimeiraColuna == 999 && cols[i].Equals(duplicatedColumnName))
                            indexPrimeiraColuna = i;
                        if (indexSegundaColuna == 999 && cols[i].Equals(referenceColumnName))
                            indexSegundaColuna = i;

                        //Se já tiver encontrado o index das duas colunas, verifica se o valor da segunda coluna é igual ao valor correto recebido como parâmetro e se a primeira coluna não contém o valor que deve ser ignorado
                        if ((i == indexSegundaColuna && cols[i].Equals(correctReferenceValue)) && !cols[indexPrimeiraColuna].Equals(ignoredDuplicatedColumnValue))
                        {
                            primeirasColunasCorretasIdentificadas.Add(cols[indexPrimeiraColuna]);
                        }
                    }
                    line = reader.ReadLine();
                }
            }
            
            using (StreamReader reader = new StreamReader(workbookName))
            {
                var line = reader.ReadLine();
                List<string> values = new List<string>();
                while (line != null)
                {
                    values.Clear();
                    var cols = line.Split(';');
                    bool ignorar = false;
                    for (int i = 0; i < cols.Length; i++)
                    {
                        bool primeiraIteracao = (i == 0);

                        //Aqui já estão definidos os index das colunas.

                        //Não deve ignorar o cabeçalho.
                        //Se o valor da primeira coluna estiver na lista de primeirasColunasCorretasIdentificadas e o valor da segunda coluna for diferente do valor correto (??????e o valor não for um dos que foi ignorado la em cima?????), ignora ela.
                        if (primeiraIteracao && !cols[indexSegundaColuna].Equals(referenceColumnName) && (primeirasColunasCorretasIdentificadas.IndexOf(cols[indexPrimeiraColuna]) != -1 && !cols[indexSegundaColuna].Equals(correctReferenceValue)))
                            ignorar = true;

                        values.Add(cols[i]);
                    }
                    if (!ignorar)
                    {
                        var newLine = string.Join(";", values);
                        lines.Add(newLine);
                    }
                    line = reader.ReadLine();
                }
            }

            //Reescreve o CSV adicionando todas as linhas, exceto as duplicadas.
            using (StreamWriter writer = new StreamWriter(workbookName, false, Encoding.UTF8))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        public string IsRowExists(string workbookName, string row)
        {
            bool rowExists = false;

            using (StreamReader reader = new StreamReader(workbookName))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Equals(row))
                    {
                        rowExists = true;
                        break;
                    }
                    line = reader.ReadLine();
                }
            }

            return rowExists.ToString();
        }

    }
}
