using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MicrosoftGraphAPI
{
    public class MicrosoftGraphWrapper
    {
        static string tenantId;

        static string appId;

        static string appSecret;

        static string proxyHost;

        static int proxyPort;

        static NetworkCredential credentials;

        //public string teamsSendStatusExecucao(string configFile, string teamId, string channelId, string filePath, string content)
        //{
        //    string fileName = Path.GetFileName(filePath);

        //    byte[] data = System.IO.File.ReadAllBytes(filePath);

        //    Stream stream = new MemoryStream(data);

        //    string[] scopes = { "Sites.ReadWrite.All", "Files.ReadWrite.All", "ChannelMessage.Send" };

        //    GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

        //    DriveItem channelFolder = client.Teams[teamId].Channels[channelId].FilesFolder.Request().GetAsync().ConfigureAwait(true).GetAwaiter().GetResult();

        //    DriveItem uploadedFile = client.Groups[teamId].Drive.Items[channelFolder.Id].ItemWithPath(fileName).Content.Request().PutAsync<DriveItem>(stream).ConfigureAwait(true).GetAwaiter().GetResult();

        //    string uploadedFileId = uploadedFile.ETag.Replace("\"", String.Empty).Replace("{", String.Empty).Replace("}", String.Empty).Split(',')[0];

        //    string uploadedFileUrl = uploadedFile.WebUrl.Split(new string[] { "&action" }, StringSplitOptions.None)[0];

        //    var chatMessage = new ChatMessage
        //    {
        //        Body = new ItemBody
        //        {
        //            ContentType = BodyType.Html,
        //            Content = content + "<attachment id=\"" + uploadedFileId + "\"></attachment>"
        //        },
        //        Attachments = new List<ChatMessageAttachment>()
        //        {
        //            new ChatMessageAttachment
        //            {
        //                Id = uploadedFileId,
        //                ContentType = "reference",
        //                ContentUrl = uploadedFileUrl,
        //                Name = fileName
        //            }
        //        }
        //    };

        //    chatMessage = client.Teams[teamId].Channels[channelId].Messages.Request().AddAsync(chatMessage).ConfigureAwait(true).GetAwaiter().GetResult();

        //    return chatMessage.Id;
        //}

        public string driveGetClient(string configFile)
        {
            try
            {
                string[] scopes = { "Sites.ReadWrite.All" };

                GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

                Drive drive = client.Me.Drive.Request().GetAsync().ConfigureAwait(true).GetAwaiter().GetResult();

                return drive.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro. " + "Descrição do erro: " + ex.ToString().Replace(Environment.NewLine, " "));
            }
        }

        public string driveUploadFile(string configFile, string driveId, string folderId, string filePath)
        {
            try
            {
                string outcome;

                string fileName = Path.GetFileName(filePath);
            
                byte[] data = System.IO.File.ReadAllBytes(filePath);
            
                Stream stream = new MemoryStream(data);
            
                string[] scopes = { "Sites.ReadWrite.All" };

                GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

                DriveItem driveItem = client.Drives[driveId].Items[folderId].ItemWithPath(fileName).Content.Request().PutAsync<DriveItem>(stream).ConfigureAwait(true).GetAwaiter().GetResult();

                outcome = string.Join("|", driveItem.ETag.Replace("\"", String.Empty).Replace("{", String.Empty).Replace("}", String.Empty).Split(',')[0], driveItem.Name, driveItem.WebUrl.Split(new string[] { "&action" }, StringSplitOptions.None)[0]);

                return outcome;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro. " + "Descrição do erro: " + ex.ToString().Replace(Environment.NewLine, " "));
            }
        }

        public string teamsSendChannelMessage(string configFile, string teamId, string channelId, string content)
        {
            try
            {
                string[] scopes = { "ChannelMessage.Send" };

                GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

                var chatMessage = new ChatMessage
                {
                    Body = new ItemBody
                    {
                        Content = content
                    }
                };

                chatMessage = client.Teams[teamId].Channels[channelId].Messages.Request().AddAsync(chatMessage).ConfigureAwait(true).GetAwaiter().GetResult();

                return chatMessage.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro. " + "Descrição do erro: " + ex.ToString().Replace(Environment.NewLine, " "));
            }
        }

        public string teamsSendChannelMessageHtml(string configFile, string teamId, string channelId, string content)
        {
            try
            {
                string[] scopes = { "ChannelMessage.Send" };

                GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

                var chatMessage = new ChatMessage
                {
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = content
                    }
                };

                chatMessage = client.Teams[teamId].Channels[channelId].Messages.Request().AddAsync(chatMessage).ConfigureAwait(true).GetAwaiter().GetResult();

                return chatMessage.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro. " + "Descrição do erro: " + ex.ToString().Replace(Environment.NewLine, " "));
            }
        }

        public string teamsSendChannelMessageHtmlWithAttachments(string configFile, string teamId, string channelId, string content, string fileId, string fileName, string fileUrl)
        {
            try
            {
                string[] scopes = { "ChannelMessage.Send" };

                GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

                var chatMessage = new ChatMessage
                {
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = content + "<attachment id=\"" + fileId + "\"></attachment>"
                    },
                    Attachments = new List<ChatMessageAttachment>()
                    {
                        new ChatMessageAttachment
                        {
                            Id = fileId,
                            ContentType = "reference",
                            ContentUrl = fileUrl,
                            Name = fileName
                        }
                    }
                };

                chatMessage = client.Teams[teamId].Channels[channelId].Messages.Request().AddAsync(chatMessage).ConfigureAwait(true).GetAwaiter().GetResult();

                return chatMessage.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro. " + "Descrição do erro: " + ex.ToString().Replace(Environment.NewLine, " "));
            }
        }

        public string teamsGetChannelFilesFolder(string configFile, string teamId, string channelId)
        {
            try
            {
                string outcome;

                string[] scopes = { "Files.ReadWrite.All" };

                GraphServiceClient client = GetGraphServiceClient(configFile, scopes);

                DriveItem driveItem = client.Teams[teamId].Channels[channelId].FilesFolder.Request().GetAsync().ConfigureAwait(true).GetAwaiter().GetResult();

                outcome = driveItem.ParentReference.DriveId + "|" + driveItem.Id;

                return outcome;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro. " + "Descrição do erro: " + ex.ToString().Replace(Environment.NewLine, " "));
            }
        }

        private static void initializeConfig(string configFile)
        {
            string configFileExtension = Path.GetExtension(configFile);

            switch (configFileExtension)
            {
                case ".xml":
                    var xml = XDocument.Load(configFile);

                    tenantId = xml.XPathSelectElement("/CONFIG/MS_GRAPH/CONFIG/TENANT_ID").Value;
                    appId = xml.XPathSelectElement("/CONFIG/MS_GRAPH/CONFIG/APP_ID").Value;
                    appSecret = xml.XPathSelectElement("/CONFIG/MS_GRAPH/CONFIG/APP_SECRET").Value;
                    proxyHost = xml.XPathSelectElement("/CONFIG/PROXY/HOST").Value;
                    proxyPort = int.Parse(xml.XPathSelectElement("/CONFIG/PROXY/PORT").Value);

                    string username = xml.XPathSelectElement("/CONFIG/MS_GRAPH/CONFIG/USERNAME").Value;
                    string password = xml.XPathSelectElement("/CONFIG/MS_GRAPH/CONFIG/PASSWORD").Value;

                    credentials = new NetworkCredential(username, password);

                    break;
                default:
                    throw new Exception("Extensão do arquivo de configuração desconhecida.");
            }
        }

        private static GraphServiceClient GetGraphServiceClient(string configFile, string[] scopes)
        {
            initializeConfig(configFile);

            string authority = "https://login.microsoftonline.com/" + tenantId;

            IPublicClientApplication app;
            app = PublicClientApplicationBuilder.Create(appId)
                  .WithAuthority(authority)
                  .Build();

            GraphServiceClient graphServiceClient;

            AuthenticationResult result = null;

            var accounts = app.GetAccountsAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            HttpClient httpClient = GraphClientFactory.Create(new DelegateAuthenticationProvider(
                    (requestMessage) =>
                    {
                        if (accounts.Any())
                        {
                            result = app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync().ConfigureAwait(true).GetAwaiter().GetResult();
                        }
                        else
                        {
                            result = app.AcquireTokenByUsernamePassword(scopes, credentials.UserName, credentials.SecurePassword).ExecuteAsync().ConfigureAwait(true).GetAwaiter().GetResult();
                        }

                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
                        return Task.FromResult(0);
                    }), "v1.0", "Global", new WebProxy(proxyHost, proxyPort));
            
            graphServiceClient = new GraphServiceClient(httpClient);

            return graphServiceClient;
        }
    }
}
