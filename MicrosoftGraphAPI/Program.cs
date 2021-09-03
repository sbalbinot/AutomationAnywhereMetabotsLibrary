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
            //MicrosoftGraphWrapper wrapper = new MicrosoftGraphWrapper();
            //string s = null;
            //string team_id = "abfb1309-2fb2-428d-bf5c-74f492277027";
            //string channel_id = "19:cb155a2670d64a0d828515b624107c90@thread.tacv2";
            //string channel_name = "DEV Reports";
            //string config_file = @"C:\Users\steph\OneDrive\Documentos\Config.xml";
            //string upload_file = @"C:\Users\steph\OneDrive\Documentos\teste - Copy.csv";
            //// string message = "<p>O processo <b>Processo Teste</b> foi finalizado com <b>sucesso</b>.</p><p>Por favor, verifique o relatório de execução em anexo.</p>";
            //string message =
            //"<p>Segue relatório de execuções RPA.</p><br/>" +
            //"<table style=\"background-color: white; border: solid 1px #DDEEEE; border-collapse: collapse; border-spacing: 0; font: normal 13px Arial, sans-serif;\">" +
            //"<thead style=\"background-color: #DDEFEF; border: solid 1px #DDEEEE; color: #336B6B; padding: 10px; text-align: left; text-shadow: 1px 1px 1px #fff;\">" +
            //    "<tr>" +
            //        "<th style=\"background-color: #DDEFEF; border: solid 1px #DDEEEE; color: #336B6B; padding: 10px; text-align: left; text-shadow: 1px 1px 1px #fff;\">Name</th>" +
            //        "<th style=\"background-color: #DDEFEF; border: solid 1px #DDEEEE; color: #336B6B; padding: 10px; text-align: left; text-shadow: 1px 1px 1px #fff;\">Position</th>" +
            //        "<th style=\"background-color: #DDEFEF; border: solid 1px #DDEEEE; color: #336B6B; padding: 10px; text-align: left; text-shadow: 1px 1px 1px #fff;\">Height</th>" +
            //        "<th style=\"background-color: #DDEFEF; border: solid 1px #DDEEEE; color: #336B6B; padding: 10px; text-align: left; text-shadow: 1px 1px 1px #fff;\">Born</th>" +
            //        "<th style=\"background-color: #DDEFEF; border: solid 1px #DDEEEE; color: #336B6B; padding: 10px; text-align: left; text-shadow: 1px 1px 1px #fff;\">Salary</th>" +
            //    "</tr>" +
            //"</thead>" +
            //"<tbody style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">" +
            //    "<tr>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">DeMarcus Cousins</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">C</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">6'11\"</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">08-13-1990</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">$4,917,000</td>" +
            //    "</tr>" +
            //    "<tr>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">Isaiah Thomas</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">PG</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">5'9\"</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">02-07-1989</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">$473,604</td>" +
            //    "</tr>" +
            //    "<tr>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">Ben McLemore</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">SG</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">6'5\"</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">02-11-1993</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">$2,895,960</td>" +
            //    "</tr>" +
            //    "<tr>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">Marcus Thornton</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">SG</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">6'4\"</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">05-05-1987</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">$7,000,000</td>" +
            //    "</tr>" +
            //    "<tr>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">Jason Thompson</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">PF</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">6'11\"</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">06-21-1986</td>" +
            //        "<td style=\"border: solid 1px #DDEEEE; color: #333; padding: 10px; text-shadow: 1px 1px 1px #fff;\">$3,001,000</td>" +
            //    "</tr>" +
            //"</tbody>" +
            //"</table>";
            //try
            //{
            //    //string filesFolder = wrapper.teamsGetChannelFilesFolder(
            //    //    config_file,
            //    //    team_id,
            //    //    channel_id
            //    //    );

            //    //Console.WriteLine(filesFolder);
            //    //string driveId = filesFolder.Split('|')[0];
            //    //string folderId = filesFolder.Split('|')[1];


            //    //string fileData = wrapper.driveUploadFile(
            //    //    config_file,
            //    //    driveId,
            //    //    folderId,
            //    //    upload_file
            //    //    );

            //    //Console.WriteLine(fileData);
            //    //string fileId = fileData.Split('|')[0];
            //    //string fileName = fileData.Split('|')[1];
            //    //string fileUrl = fileData.Split('|')[2];

            //    //s = wrapper.teamsSendChannelMessageHtmlWithAttachments(
            //    //    config_file,
            //    //    team_id,
            //    //    channel_id,
            //    //    message,
            //    //    fileId,
            //    //    fileName,
            //    //    fileUrl
            //    //    );

            //    s = wrapper.teamsSendChannelMessageHtmlWithMentions(
            //        config_file,
            //        team_id,
            //        channel_id,
            //        message,
            //        channel_name,
            //        channel_name
            //    );

            //    //s = wrapper.teamsSendChannelMessageHtmlWithMentionsAndAttachments(
            //    //    config_file,
            //    //    team_id,
            //    //    channel_id,
            //    //    message,
            //    //    channel_name,
            //    //    channel_name,
            //    //    fileId,
            //    //    fileName,
            //    //    fileUrl
            //    //    );

            //}
            //catch (Exception ex)
            //{
            //    s = ex.Message;
            //}
            //Console.WriteLine(s);
            //Console.Read();
        }
    }
}
