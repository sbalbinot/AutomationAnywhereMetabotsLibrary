using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class File
    {
        public string GetFileExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            return Path.GetExtension(fileName);
        }

        public string GetFileBetweenDates(string directory, string start, string end, string formato)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(directory);

            DateTime dateStart = DateTime.ParseExact(start, formato, Globalization.CultureInfo.GetCultureInfo("pt-BR"));
            DateTime dateEnd = DateTime.ParseExact(end, formato, Globalization.CultureInfo.GetCultureInfo("pt-BR"));

            FileInfo[] files = DirInfo.GetFiles().Where(f => f.LastWriteTime <= dateEnd && f.LastWriteTime >= dateStart).ToArray();

            if (files.Length == 0)
            {
                throw new Exception("Não foram encontrados arquivos entre as datas fornecidas.");
            } else if (files.Length > 1)
            {
                throw new Exception("Mais um de arquivo foi encontrado com base nas datas fornecidas.");
            }

            return files[0].FullName;
        }

        //Retorna a lista com nome completo dos arquivos separada por | (pipe)
        public string GetFilesBetweenDates(string directory, string start, string end, string formato, string fullName)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(directory);

            DateTime dateStart = DateTime.ParseExact(start, formato, Globalization.CultureInfo.GetCultureInfo("pt-BR"));
            DateTime dateEnd = DateTime.ParseExact(end, formato, Globalization.CultureInfo.GetCultureInfo("pt-BR"));

            Console.WriteLine(dateStart.ToString());
            Console.WriteLine(dateEnd.ToString());

            FileInfo[] files = DirInfo.GetFiles().Where(f => f.LastWriteTime <= dateEnd && f.LastWriteTime >= dateStart).ToArray();

            string returnFiles = "";

            if (files.Length == 0)
            {
                throw new Exception("Não foram encontrados arquivos entre as datas fornecidas.");
            }

            foreach (var file in files)
            {
                returnFiles = returnFiles + ";" + (fullName.Equals("true") ? file.FullName : file.Name);
            }

            return returnFiles.Remove(0, 1);
        }

        //Retorna a lista com nome completo dos arquivos separada por | (pipe)
        public string GetFilesAfterDate(string directory, string date, string formato, string fullName)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(directory);

            DateTime dateTime = DateTime.ParseExact(date, formato, Globalization.CultureInfo.GetCultureInfo("pt-BR"));

            FileInfo[] files = DirInfo.GetFiles().Where(f => f.LastWriteTime >= dateTime).ToArray();

            string returnFiles = "";

            if (files.Length == 0)
            {
                throw new Exception("Não foram encontrados arquivos entre as datas fornecidas.");
            }

            foreach (var file in files)
            {
                returnFiles = returnFiles + ";" + (fullName.Equals("true") ? file.FullName : file.Name);
            }

            return returnFiles.Remove(0, 1);
        }

        //Retorna a lista com nome completo dos arquivos separada por | (pipe)
        public string GetFilesBeforeDate(string directory, string date, string formato, string fullName)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(directory);

            DateTime dateTime = DateTime.ParseExact(date, formato, Globalization.CultureInfo.GetCultureInfo("pt-BR"));

            FileInfo[] files = DirInfo.GetFiles().Where(f => f.LastWriteTime <= dateTime).ToArray();

            string returnFiles = "";

            if (files.Length == 0)
            {
                throw new Exception("Não foram encontrados arquivos entre as datas fornecidas.");
            }

            foreach (var file in files)
            {
                returnFiles = returnFiles + ";" + (fullName.Equals("true") ? file.FullName : file.Name);
            }

            return returnFiles.Remove(0, 1);
        }

        public string ConvertTxtToPdf(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            string fileNamePdf = fileName.Replace(".txt", ".pdf");


            //Read the Data from Input File

            StreamReader rdr = new StreamReader(fileName);

            //Create a New instance on Document Class

            Document doc = new Document();

            //Create a New instance of PDFWriter Class for Output File

            PdfWriter.GetInstance(doc, new FileStream(fileNamePdf, FileMode.Create, FileAccess.Write, FileShare.None));

            System.Threading.Thread.Sleep(5000);

            //Open the Document

            doc.Open();

            //Add the content of Text File to PDF File

            string ln;
            string lastLn = null;

            while ((ln = rdr.ReadLine()) != null)
            {
                Paragraph paragraph = new Paragraph();

                if (lastLn == null || ln.Contains("=") || (!lastLn.All(char.IsWhiteSpace) && !ln.Contains(":")))
                    paragraph.Alignment = 1;
                else
                    paragraph.Alignment = 0;

                paragraph.Add(ln);

                doc.Add(paragraph);

                lastLn = ln;
            } 

            //Close the Document

            doc.Close();

            return fileNamePdf;
        }
    }
}
