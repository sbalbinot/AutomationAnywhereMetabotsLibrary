using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftGraphAPI
{
    class Program
    {
        public static void Main()
        {
            //    MicrosoftGraphWrapper wrapper = new MicrosoftGraphWrapper();
            //    string s = null;
            //    string team_id = "abfb1309-2fb2-428d-bf5c-74f492277027";
            //    string channel_id = "19:86ed558c776f4c5eb387e725b3c86643@thread.tacv2";
            //    string config_file = @"C:\Users\steph\OneDrive\Documentos\Config.xml";
            //    string upload_file = @"C:\Users\steph\OneDrive\Documentos\teste - Copy.csv";
            //    string message = "<p>teste</p>";
            //    try
            //    {
            //        string filesFolder = wrapper.teamsGetChannelFilesFolder(
            //            config_file,
            //            team_id,
            //            channel_id
            //            );

            //        Console.WriteLine(filesFolder);
            //        string driveId = filesFolder.Split('|')[0];
            //        string folderId = filesFolder.Split('|')[1];


            //        string fileData = wrapper.driveUploadFile(
            //            config_file,
            //            driveId,
            //            folderId,
            //            upload_file
            //            );

            //        Console.WriteLine(fileData);
            //        string fileId = fileData.Split('|')[0];
            //        string fileName = fileData.Split('|')[1];
            //        string fileUrl = fileData.Split('|')[2];

            //        s = wrapper.teamsSendChannelMessageHtmlWithAttachments(
            //            config_file,
            //            team_id,
            //            channel_id,
            //            message,
            //            fileId,
            //            fileName,
            //            fileUrl
            //            );

            //    }
            //    catch (Exception ex)
            //    {
            //        s = ex.Message;
            //    }
            //    Console.WriteLine(s);
            //    Console.Read();
            //}
        }
    }
}
