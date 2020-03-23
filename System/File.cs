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

        public string GetFileBetweenDates(string directory, string start, string end)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(directory);

            DateTime dateStart = DateTime.Parse(start);
            DateTime dateEnd = DateTime.Parse(end);

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
    }
}
