using System;
using System.Net;
using System.Threading.Tasks;

namespace UnitTest_FTP.Services
{
    public class FTPService
    {
        readonly string Path;
        readonly string UserName;
        readonly string Password;
        const string RESULT_FILEEXISTS = "File exists";
        const string RESULT_FILEORDIRECTORY_NOT_EXISTS = "No such file or directory";

        private ICredentials Credentials
        {
            get
            {
                return new NetworkCredential(this.UserName, this.Password);
            }
        }

        public FTPService(string path, string userName, string password)
        {
            this.Path = path;
            this.UserName = userName;
            this.Password = password;
        }

        public bool Upload(string dataFolderAndFileName, byte[] data)
        {
            using (var client = new WebClient())
            {
                client.Credentials = this.Credentials;

                try
                {
                    var result = client.UploadData(System.IO.Path.Combine(this.Path, dataFolderAndFileName), "STOR", data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> UploadTaskAsync(string dataFolderAndFileName, byte[] data)
        {
            using (var client = new WebClient())
            {
                client.Credentials = this.Credentials;

                try
                {
                    var address = new Uri(System.IO.Path.Combine(this.Path, dataFolderAndFileName));
                    var result = await client.UploadDataTaskAsync(address, "STOR", data);
                    return result.Length > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool CreateDirectory(string name)
        {
            var filename = System.IO.Path.Combine(this.Path, name);
            var ftpReq = (FtpWebRequest)WebRequest.Create(filename);
            ftpReq.Method = WebRequestMethods.Ftp.MakeDirectory;
            ftpReq.Credentials = this.Credentials;

            try
            {
                var result = (FtpWebResponse)ftpReq.GetResponse();
                return result.StatusCode.Equals(FtpStatusCode.PathnameCreated);
            }
            catch (WebException ex)
            {
                using (var response = (FtpWebResponse)ex.Response)
                    return response.StatusDescription.Contains(RESULT_FILEEXISTS);
            }
        }

        public bool Exists(string name)
        {
            var filename = System.IO.Path.Combine(this.Path, name);
            var ftpReq = (FtpWebRequest)WebRequest.Create(filename);
            ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpReq.Credentials = this.Credentials;

            try
            {
                ftpReq.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
