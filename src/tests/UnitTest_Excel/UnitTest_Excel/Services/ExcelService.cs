using OfficeOpenXml;
using System;
using System.Data;
using System.IO;

namespace UnitTest_Excel.Services
{
    public class ExcelService
    {
        const string extension = ".xlsx";

        public byte[] Get(DataSet dts)
        {
            var fileName = string.Concat(Path.GetTempPath(), Guid.NewGuid().ToString(), extension);

            var file = new FileInfo(fileName);

            using (var package = new ExcelPackage(file))
            {
                for (int i = 0; i < dts.Tables.Count; i++)
                    this.DoTable(dts.Tables[i], package);

                package.Save();

                var fileBytes = File.ReadAllBytes(fileName);
                File.Delete(file.FullName);
                return fileBytes;//TODO - remove temp file
            }
        }

        private void DoTable(DataTable dtb, ExcelPackage package)
        {
            using (var dtbTmp = new DataTable(dtb.TableName))
            {
                foreach (var column in dtb.Columns)
                    dtbTmp.Columns.Add(column.ToString(), typeof(string));

                foreach (DataRow row in dtb.Rows)
                    dtbTmp.Rows.Add(row.ItemArray);

                var worksheet = package.Workbook.Worksheets.Add(dtb.TableName);
                worksheet.Cells["A1"].LoadFromDataTable(dtbTmp, true);
            }
        }

    }
}

