using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using UnitTest_Excel.Services;

namespace UnitTest_Excel
{
    [TestClass]
    public class EEPlustTests
    {
        const string ExcelExtension = ".xlsx";

        [TestMethod]
        public void GenerateExcel()
        {
            using (var dts = new DataSet())
            using (var dtb = new DataTable())
            {
                dts.Tables.Add(dtb);
                dtb.Columns.Add("mycolumn1", typeof(int));
                dtb.Columns.Add("mycolumn2", typeof(string));

                dtb.Rows.Add(new object[] { 1, "value column 1" });

                var service = new ExcelService();
                var fileBytes = service.Get(dts);

                var fileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ExcelExtension);
              //  File.WriteAllBytes(fileName, fileBytes);

                Console.WriteLine(fileName);
               // Process.Start(fileName);
            }
        }
    }
}
