using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using UnitTest_FTP.Services;

namespace UnitTest_FTP
{
    [TestClass]
    public class FtpWebRequestTests
    {
        private string FTP_Path
        {
            get
            {
                return ConfigurationManager.AppSettings["FTP_Path"] as string;
            }
        }
        private string FTP_UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["FTP_UserName"] as string;
            }
        }
        private string FTP_Password
        {
            get
            {
                return ConfigurationManager.AppSettings["FTP_Password"] as string;
            }
        }

        [TestMethod]
        public void create_directory()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();
            var result = service.CreateDirectory(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void create_directory_updatefile_create_again()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();
            var result = service.CreateDirectory(value);
            Assert.IsTrue(result);

            var filePath = System.IO.Path.GetTempFileName();
            File.WriteAllText(filePath, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

            var fileName = System.IO.Path.Combine(value, filePath.Split('\\').Last());
            result = service.Upload(fileName, File.ReadAllBytes(filePath));
            Assert.IsTrue(result);

            result = service.CreateDirectory(value);
            Assert.IsTrue(result);

            File.WriteAllText(filePath, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
            result = service.Upload(fileName + ".new", File.ReadAllBytes(filePath));
            Assert.IsTrue(result);

            result = service.CreateDirectory(value);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async void uploadtaskasync()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();
            var result = service.CreateDirectory(value);
            Assert.IsTrue(result);

            var filePath = System.IO.Path.GetTempFileName();
            File.WriteAllText(filePath, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

            var fileName = System.IO.Path.Combine(value, filePath.Split('\\').Last());
            result = await service.UploadTaskAsync(fileName, File.ReadAllBytes(filePath));
            Assert.IsTrue(result);
         }

        [TestMethod]
        public void directory_exists_fail()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();
            var result = service.Exists(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void directory_exists()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();

            var result = service.CreateDirectory(value);
            result = service.Exists(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void directory_exists_which_already_exists()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();

            var result = service.CreateDirectory(value);

            Assert.IsTrue(result);

            result = service.CreateDirectory(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void file_exists()
        {
            var service = new FTPService(this.FTP_Path, this.FTP_UserName, this.FTP_Password);
            var value = Guid.NewGuid().ToString();
            var result = service.CreateDirectory(value);
            Assert.IsTrue(result);

            var filePath = System.IO.Path.GetTempFileName();
            File.WriteAllText(filePath, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

            var fileNameOnline = System.IO.Path.Combine(value, filePath.Split('\\').Last());

            result = service.Exists(fileNameOnline);
            Assert.IsFalse(result);

            result = service.Upload(fileNameOnline, File.ReadAllBytes(filePath));
            Assert.IsTrue(result);

            result = service.Exists(fileNameOnline);
            Assert.IsTrue(result);
        }
    }
}
