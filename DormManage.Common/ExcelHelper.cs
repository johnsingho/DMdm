using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DormManage.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 使用第三方插件NPOI读取Excel内容到DataTable,默认读取Excel文件的第一个Sheet
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public DataTable GetDataFromExcel(string filePath)
        {
            ISheet sheet = null; ;
            if (filePath.ToLower().EndsWith("xlsx"))
            {
                XSSFWorkbook xssFWorkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssFWorkbook = new XSSFWorkbook(file);
                }
                sheet = xssFWorkbook.GetSheetAt(0);
            }
            else if (filePath.ToLower().EndsWith("xls"))
            {
                HSSFWorkbook hssfWorkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfWorkbook = new HSSFWorkbook(file);
                }
                sheet = hssfWorkbook.GetSheetAt(0);
            }
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            int t = 0;
            while (rows.MoveNext())
            {
                IRow row = rows.Current as IRow;
                int cellCount = row.LastCellNum;
                if (cellCount == -1) continue;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (t == 0)
                    {
                        DataColumn column = null;
                        if (cell == null)
                        {
                            column = new DataColumn(string.Empty, typeof(string));
                        }
                        else
                        {
                            column = new DataColumn(cell.ToString().Trim(), typeof(string));
                        }
                        dt.Columns.Add(column);
                    }
                    else
                    {
                        if (i > dt.Columns.Count - 1) break;
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString().Trim();
                        }
                    }
                }                
                if (t > 0)
                {
                    dt.Rows.Add(dr);
                }
                t++;
            }
            return dt;
        }

        //一旦遇到空行，就中止
        public DataTable GetDataFromExcelBreakEmptyLine(string filePath)
        {
            ISheet sheet = null; ;
            if (filePath.ToLower().EndsWith("xlsx"))
            {
                XSSFWorkbook xssFWorkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssFWorkbook = new XSSFWorkbook(file);
                }
                sheet = xssFWorkbook.GetSheetAt(0);
            }
            else if (filePath.ToLower().EndsWith("xls"))
            {
                HSSFWorkbook hssfWorkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfWorkbook = new HSSFWorkbook(file);
                }
                sheet = hssfWorkbook.GetSheetAt(0);
            }
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            int t = 0;
            while (rows.MoveNext())
            {
                IRow row = rows.Current as IRow;
                int cellCount = row.LastCellNum;
                if (cellCount == -1) continue;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (t == 0)
                    {
                        DataColumn column = null;
                        if (cell == null)
                        {
                            column = new DataColumn(string.Empty, typeof(string));
                        }
                        else
                        {
                            column = new DataColumn(cell.ToString().Trim(), typeof(string));
                        }
                        dt.Columns.Add(column);
                    }
                    else
                    {
                        if (i > dt.Columns.Count - 1) break;
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString().Trim();
                        }
                    }
                }

                if (t > 0)
                {
                    var bEmptyLine = true;
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        if (null != dr[i])
                        {
                            var sCell = dr[i].ToString();
                            if (!string.IsNullOrEmpty(sCell))
                            {
                                bEmptyLine = false;
                                break;
                            }
                        }
                    }
                    if (bEmptyLine)
                    {
                        break; //一旦遇到空行，就中止
                    }
                    dt.Rows.Add(dr);
                }
                t++;
            }
            return dt;
        }

        /// <summary>
        /// 使用第三方插件NPOI读取Excel指定Sheet内容到DataTable
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">sheet名</param>
        /// <returns></returns>
        public DataTable GetDataFromExcel(string filePath, string sheetName)
        {
            ISheet sheet = null;
            if (filePath.ToLower().EndsWith("xlsx"))
            {
                XSSFWorkbook xssFWorkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssFWorkbook = new XSSFWorkbook(file);
                }
                sheet = xssFWorkbook.GetSheet(sheetName);
            }
            else if (filePath.ToLower().EndsWith("xls"))
            {
                HSSFWorkbook hssfWorkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfWorkbook = new HSSFWorkbook(file);
                }
                sheet = hssfWorkbook.GetSheet(sheetName);
            }
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            int t = 0;
            while (rows.MoveNext())
            {
                IRow row = rows.Current as IRow;
                int cellCount = row.LastCellNum;
                if (cellCount == -1) continue;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (t == 0)
                    {
                        DataColumn column = null;
                        if (cell == null)
                        {
                            column = new DataColumn(string.Empty, typeof(string));
                        }
                        else
                        {
                            column = new DataColumn(cell.ToString().Trim(), typeof(string));
                        }
                        dt.Columns.Add(column);
                    }
                    else
                    {
                        if (i > dt.Columns.Count - 1) break;
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString().Trim();
                        }
                    }
                }
                if (t > 0)
                {
                    dt.Rows.Add(dr);
                }
                t++;
            }
            return dt;
        }

        /// <summary>
        /// 将datatable导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName"></param>
        public void RenderToExcel(DataTable table, string fileName)
        {
            using (MemoryStream ms = RenderToExcel(table))
            {
                SaveToFile(ms, fileName);
            }
        }

        /// <summary>
        /// 保存Excel文档流到文件
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="fileName">文件名</param>
        private void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
                data = null;
            }
        }

        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private MemoryStream RenderToExcel(DataTable table)
        {
            MemoryStream ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);
                // handling header.
                foreach (DataColumn column in table.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value
                // handling value.
                int rowIndex = 1;
                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    rowIndex++;
                }
                AutoSizeColumns(sheet);

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private void AutoSizeColumns(ISheet sheet)
        {
            if (sheet.PhysicalNumberOfRows > 0)
            {
                IRow headerRow = sheet.GetRow(0);

                for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
        }
    }
}
