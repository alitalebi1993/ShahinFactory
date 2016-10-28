using FactoryShahin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace Utility
{
    public static class ClassFTP
    {

        public static bool CheckAccess()
        {
            return false;
            //try
            //{
            //    Ping myPing = new Ping();
            //    string host = FactoryShahin.Properties.Settings.Default.IP;
            //    byte[] buffer = new byte[32];
            //    int timeout = 1000;
            //    PingOptions pingOptions = new PingOptions();
            //    PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            //    if (reply.Status != IPStatus.Success)
            //    {
            //        Thread.Sleep(500);
            //        reply = myPing.Send(host, timeout, buffer, pingOptions);
            //        return reply.Status == IPStatus.Success;
            //    }
            //    return reply.Status == IPStatus.Success;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }
        public static string OpenFile()
        {
            System.Windows.Forms.OpenFileDialog OP = new System.Windows.Forms.OpenFileDialog();
            OP.Filter = "All Files (*.*)|*.*";
            OP.FileName = "";
            if (OP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return OP.FileName;
            }
            return "";
        }
        public static void SaveFile(string LocalPath, string ServerPath, string NewName, bool ISrevision)
        {
            TusharFtp.FtpHelperClass F2 = new TusharFtp.FtpHelperClass();
            //F2.FtpPassword = FactoryShahin.Properties.Settings.Default.FTPPassWord;
            //F2.FtpUserName = FactoryShahin.Properties.Settings.Default.FTP_UserName;
            //F2.FtpServer = FactoryShahin.Properties.Settings.Default.IP;
            F2.FtpMakeDirectory(ServerPath);
            if (LocalPath != "")
            {
                if (!ISrevision)
                {
                    F2.FtpFileUpload(LocalPath, ServerPath + @"\");
                    return;
                }
                int i = LocalPath.LastIndexOf(@"\") + 1;
                string NewLocal = LocalPath.Substring(0, i);
                NewLocal = NewLocal + NewName + LocalPath.Substring(LocalPath.LastIndexOf(@"."));
                File.Copy(LocalPath, NewLocal);
                int Index = LocalPath.LastIndexOf(@"\");
                int Index2 = LocalPath.LastIndexOf(@".");
                string OldName = LocalPath.Substring(Index + 1);
                F2.FtpFileUpload(NewLocal, ServerPath + @"\");
                File.Delete(NewLocal);
            }
        }
        private static FtpWebRequest CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
        {
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + FactoryShahin.Properties.Settings.Default.IP + ftpDirectoryPath));
            //request.Proxy = null;
            //request.UsePassive = true;
            //request.UseBinary = true;
            //request.KeepAlive = keepAlive;
            //request.Credentials = new NetworkCredential(userName, password);
            //return request;
            return null;
        }
        public static void DownloadFile(string ServerPath, string FileName, bool flag)
        {
            ////Thread M = new Thread(() =>
            ////{

            ////    FormLoading F = new FormLoading();
            ////    try
            ////    {
            ////        F.ShowDialog();
            ////    }
            ////    catch (Exception)
            ////    {
            ////    }
            ////    finally
            ////    {
            ////        F.Dispose();
            ////    }
            ////});
            ////M.Start();
            //TusharFtp.FtpHelperClass F2 = new TusharFtp.FtpHelperClass();
            //F2.FtpPassword = FactoryShahin.Properties.Settings.Default.FTPPassWord;
            //F2.FtpUserName = FactoryShahin.Properties.Settings.Default.FTP_UserName;
            //F2.FtpServer = FactoryShahin.Properties.Settings.Default.IP;
            //string LocalPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if (!System.IO.Directory.Exists(LocalPath + @"\FactoryShahin\"))
            //    System.IO.Directory.CreateDirectory(LocalPath + @"\FactoryShahin\");
            //string m = F2.FtpFileDownload(ServerPath, LocalPath+ @"\FactoryShahin\");
            ////M.Abort();
            //if (File.Exists(LocalPath + @"\FactoryShahin\" + FileName))
            //{
            //    System.Diagnostics.Process.Start(LocalPath + @"\FactoryShahin\" + FileName);

            //}
            //else
            //{
            //    if (flag)
            //        DownloadFile(ServerPath, FileName, false);
            //    else
            //        BeheshtMBox.Show("ارتباط با سرور برقرار نمی باشد! مجددا تلاش نمایید ", "پیام", BeheshtMBox.Icon.Info, BeheshtMBox.MessageType.OK);

            //}
        }
        public static void DeleteFile(string ServerPath)
        {
            //TusharFtp.FtpHelperClass F = new TusharFtp.FtpHelperClass();
            //F.FtpPassword = FactoryShahin.Properties.Settings.Default.FTPPassWord;
            //F.FtpUserName = FactoryShahin.Properties.Settings.Default.FTP_UserName;
            //F.FtpServer = FactoryShahin.Properties.Settings.Default.IP;
            //string m = F.FtpDeleteFile(ServerPath);
        }
    }
}