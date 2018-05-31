using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HPSF;

namespace HCQ2_Common
{
    /// <summary>
    ///  说明：NPOI操作Execl帮助类
    ///  创建人：陈敏
    ///  创建时间：2014-12-29
    /// </summary>
    public static class NpoiHelper
    {
        #region 获取字体样式
        /// <summary>
        ///  获取字体样式
        /// </summary>
        /// <param name="hssfworkbook">workbook</param>
        /// <param name="fontfamily">字体</param>
        /// <param name="fontcolor">颜色</param>
        /// <param name="fontsize">大小</param>
        /// <param name="isWeight">是否加粗，默认不需要</param>
        /// <returns></returns>
        public static IFont GetFontStyle(IWorkbook hssfworkbook, string fontfamily, HSSFColor fontcolor, int fontsize, bool isWeight = false)
        {
            IFont font1 = hssfworkbook.CreateFont();
            if (string.IsNullOrEmpty(fontfamily))
            {
                font1.FontName = fontfamily;
            }
            if (fontcolor != null)
            {
                font1.Color = fontcolor.GetIndex();
            }
            if (isWeight)
            {//是否加粗
                font1.Boldweight = short.MaxValue;
            }
            font1.IsItalic = true;
            font1.FontHeightInPoints = (short)fontsize;
            return font1;
        }
        #endregion

        #region 合并单元格
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            var cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        #endregion

        #region 创建单元格的边框，背景颜色，以及对齐方式
        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="hssfworkbook">workbook对象</param>
        /// <param name="font">单元格字体</param>
        /// <param name="fillBackgroundColor">单元格背景</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">水平对齐方式</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle(IWorkbook hssfworkbook, IFont font, HSSFColor fillBackgroundColor, HorizontalAlignment ha, VerticalAlignment va)
        {
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;
            if (fillBackgroundColor != null)
            {
                cellstyle.FillBackgroundColor = fillBackgroundColor.GetIndex();
            }
            if (font != null)
            {
                cellstyle.SetFont(font);
            }
            //有边框
            cellstyle.BorderBottom = BorderStyle.THIN;
            cellstyle.BorderLeft = BorderStyle.THIN;
            cellstyle.BorderRight = BorderStyle.THIN;
            cellstyle.BorderTop = BorderStyle.THIN;
            return cellstyle;
        }
        #endregion

        #region NOPI操作DataTable转换为Execl
        /// <summary>
        ///  通过NPOI将DataTable转换为Execl
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="fileName">指定Execl文件名称</param>
        /// <param name="sheetName">指定Execl工作表名称</param>
        public static void DataTableToExeclForNpoi(DataTable sourceTable,string fileName,string sheetName)
        {
            var ms = ExportDataTableToExcel(sourceTable, sheetName) as MemoryStream;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("你", Encoding.UTF8) + ".xls");
            if (ms != null) HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
        }
        public static void DataTableToExeclForNpoi(DataTable myTitData, DataTable myData, string fileName, string sheetName)
        {
            var ms = ExportDataTableToExcel(myTitData, myData, sheetName) as MemoryStream;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + ".xls");
            if (ms != null) HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        ///  DataTable转Excel
        /// </summary>
        /// <param name="myTitData">数据源</param>
        /// <param name="tName">表头集合</param>
        /// <param name="fileName">xls文件名</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="keyName">用于标识唯一行的 记录标识，字段名称不区分大小写</param>
        /// <param name="isMerge">是否需要合并相同项目</param>
        /// <param name="notMerge">不需要合并的项目 如eg：,id,name,</param>
        public static void DataTableToExeclForNpoi(DataTable myTitData, Dictionary<string,string> tName, string fileName,
            string sheetName,string keyName,bool isMerge=false, string notMerge=null)
        {
            var ms = ExportDataTableToExcel(myTitData, tName, sheetName,keyName, isMerge,notMerge) as MemoryStream;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
            if (ms != null) HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        ///  由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="sheetName">Excel工作表</param>
        /// <returns></returns>
        private static Stream ExportDataTableToExcel(DataTable sourceTable, string sheetName)
        {
            var workbook = new HSSFWorkbook();
            var ms = new MemoryStream();
            var sheet = (HSSFSheet)workbook.CreateSheet(sheetName);
            var headerRow = (HSSFRow)sheet.CreateRow(0);
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            int rowIndex = 1;
            foreach (DataRow row in sourceTable.Rows)
            {
                var dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }
        /// <summary>
        /// 用DataTable导出Excel
        /// 参数:字典名数据源、字段内容数据源
        /// 说明：订制
        /// </summary>
        /// <param name="myData"></param>
        /// <param name="myTitData"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static Stream ExportDataTableToExcel(DataTable myTitData, DataTable myData, string sheetName)
        {
            var workBook = new HSSFWorkbook();
            var stream = new MemoryStream();
            var sheet = (HSSFSheet)workBook.CreateSheet(sheetName);
            int mymax = myTitData.Rows.Count, rowNum = 0;
            for (int i = 0; i < mymax; i++)
            {
                var row = (HSSFRow)sheet.CreateRow(rowNum);
                row.HeightInPoints = 20;
                SetMergeCell(sheet, rowNum, rowNum, 0, 1);
                row.CreateCell(0).CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
                row.CreateCell(0).CellStyle.Alignment = HorizontalAlignment.CENTER;
                IFont font = workBook.CreateFont();
                font.FontHeightInPoints = 10;
                font.FontName = "微软雅黑";
                row.CreateCell(0).CellStyle.SetFont(font);
                row.CreateCell(0).SetCellValue("<" + myTitData.Rows[i]["data_name"] + ">");
                DataRow[] rowArr = myData.Select("data_name='" + myTitData.Rows[i]["data_name"] + "'");
                int myRowMax = rowArr.Length;
                rowNum++;
                for (int j = 0; j < myRowMax; j++)
                {
                    if (j == 0)
                    {
                        var rowData = (HSSFRow)sheet.CreateRow(rowNum);
                        rowData.CreateCell(0).SetCellValue("数据库字段名");
                        rowData.CreateCell(1).SetCellValue("对应中文名称");
                        rowNum++;
                        var rowDatas = (HSSFRow)sheet.CreateRow(rowNum);
                        rowDatas.CreateCell(0).SetCellValue(rowArr[j]["field_name"].ToString());
                        rowDatas.CreateCell(1).SetCellValue(rowArr[j]["field_cname"].ToString());
                    }
                    else
                    {
                        var rowDatas = (HSSFRow)sheet.CreateRow(rowNum);
                        rowDatas.CreateCell(0).SetCellValue(rowArr[j]["field_name"].ToString());
                        rowDatas.CreateCell(1).SetCellValue(rowArr[j]["field_cname"].ToString());
                    }
                    rowNum++;
                }
                rowNum++;
            }
            //文字置中
            //cs.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
            //cs.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            sheet.SetColumnWidth(0, 25 * 256);
            sheet.SetColumnWidth(1, 25 * 256);
            workBook.Write(stream);
            stream.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Stream ExportDataTableToExcel(DataTable myTitData, Dictionary<string, string> tName, string sheetName,string keyName, bool isMerge = false, string notMerge=null)
        {
            var stream = new MemoryStream();
            using (myTitData)
            {
                //1.0 创建excel
                var workBook = new HSSFWorkbook();
                //1.1 创建工作表
                var sheet = (HSSFSheet) workBook.CreateSheet(sheetName);
                #region 右击文件 属性信息  
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "HCQ2";
                    workBook.DocumentSummaryInformation = dsi;
                    //文档信息
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "Joychen"; //填加xls文件作者信息  
                    si.ApplicationName = "HCQ2"; //填加xls文件创建程序信息  
                    si.LastAuthor = "Joychen"; //填加xls文件最后保存者信息  
                    si.Comments = "作者信息"; //填加xls文件作者信息  
                    si.Title = "工资发放模板"; //填加xls文件标题信息  
                    si.Subject = "工资发放模板";//填加文件主题信息  
                    
                    si.CreateDateTime = System.DateTime.Now;
                    workBook.SummaryInformation = si;
                }
                #endregion
                //1.2 绘制表头 遍历表头集合
                List<string> listKey = new List<string>(tName.Keys);
                IRow headerRow = sheet.CreateRow(0);
                IFont font = workBook.CreateFont();
                headerRow.HeightInPoints = 40;
                //font.FontHeightInPoints = 10;
                font.Boldweight = (short)FontBoldWeight.BOLD;
                ICell icell;
                for (int i = 0; i < listKey.Count; i++)
                {
                    icell = headerRow.CreateCell(i);
                    icell.CellStyle = GetCellStyle(workBook, font,  new HSSFColor.BLUE(),  HorizontalAlignment.CENTER, VerticalAlignment.CENTER);
                    icell.SetCellValue(tName[listKey[i]]);
                }

                #region 1.3 绘制数据行
                int contextIndex = 1; //内容行索引
                foreach (DataRow row in myTitData.Rows)
                {
                    IRow cRow = sheet.CreateRow(contextIndex);
                    cRow.HeightInPoints = 25;
                    //1.3.1 循环表头集合添加行数据
                    ICell cell;
                    for (int i = 0; i < listKey.Count; i++) {
                        cell = cRow.CreateCell(i);
                        cell.CellStyle = GetCellStyle(workBook, null, null, HorizontalAlignment.CENTER,
                            VerticalAlignment.CENTER);
                        if (row[listKey[i]].GetType().Name.ToLower().Equals("datetime") && !string.IsNullOrEmpty(Helper.ToString(row[listKey[i]])))
                        {
                            System.DateTime dtStr;
                            System.DateTime.TryParse(Helper.ToString(row[listKey[i]]), out dtStr);
                            cell.SetCellValue(dtStr.ToString("D"));
                        }
                        else
                            cell.SetCellValue(Helper.ToString(row[listKey[i]]));
                    }      
                    contextIndex++;
                }
                #endregion
                //1.4 列、宽自适应，只对英文和数字有效
                for (int j = 0; j < myTitData.Rows.Count; j++)
                    sheet.AutoSizeColumn(j);
                #region 1.5 获取当前列的宽度，然后对比本列的长度，取最大值

                for (int columnNum = 0; columnNum <= myTitData.Rows.Count; columnNum++)
                {
                    #region  获取当前列的宽度

                    int columnWidth = sheet.GetColumnWidth(columnNum)/256;
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                    {
                        IRow currentRow;
                        //1.6 当前行未被使用过
                        if (sheet.GetRow(rowNum) == null)
                            currentRow = sheet.CreateRow(rowNum);
                        else
                            currentRow = sheet.GetRow(rowNum);
                        if (currentRow.GetCell(columnNum) != null)
                        {
                            ICell currentCell = currentRow.GetCell(columnNum);
                            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                            if (columnWidth < length)
                                columnWidth = length;
                        }
                    }
                    sheet.SetColumnWidth(columnNum, columnWidth * 256+150);

                    #endregion
                }

                #endregion

                #region 1.6 合并单元格
                /****************************
                    * CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
                    * 按列合并
                    ****************************/
                if (isMerge && myTitData.Columns.Contains(keyName))
                {
                    //包含主键信息
                    int lineStart,//开始行标记
                        lineEnd,//结束行标记
                        colMark;//开始、结束列标记
                    object obj;//记录当前行遍历的制定值
                    object keyValue;//记录每行用于标识主键的值
                    for (int i = 0; i < listKey.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(notMerge) && notMerge.Contains("," + listKey[i] + ","))
                            //当前项目不需要合并
                            continue;
                        lineStart = 1;
                        lineEnd = 1;
                        colMark = i;
                        obj = myTitData.Rows[0][listKey[i]];//每次将第一行的列数据作为初始值
                        keyValue = myTitData.Rows[0][keyName];
                        for (int j = 1; j < myTitData.Rows.Count; j++)
                        {
                            if (obj.Equals(myTitData.Rows[j][listKey[i]]) && keyValue.Equals(myTitData.Rows[j][keyName]))
                                lineEnd = j+1;
                            else
                            {
                                if (lineEnd - lineStart > 0)
                                    //合并单元格
                                    sheet.AddMergedRegion(new CellRangeAddress(lineStart, lineEnd, colMark, colMark));
                                lineStart = j+1;
                                lineEnd = j+1;
                                obj = myTitData.Rows[j][listKey[i]];
                                keyValue = myTitData.Rows[j][keyName];
                            }
                        }
                    }
                }
                #endregion

                //1.7 将表内容写入流 
                workBook.Write(stream);
                stream.Flush();
                stream.Position = 0;
                return stream;
            }
        }
        /// <summary>
        /// 合并单元格
        /// 参数：Sheet,开始行、结束行、开始列、结束列
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="topRowIndex"></param>
        /// <param name="bottomRowIndex"></param>
        /// <param name="topCellIndex"></param>
        /// <param name="bottomCellIndex"></param>
        private static void SetMergeCell(HSSFSheet sheet, int topRowIndex, int bottomRowIndex, int topCellIndex, int bottomCellIndex)
        {
            var range = new CellRangeAddress(topRowIndex, bottomRowIndex, topCellIndex, bottomCellIndex);
            sheet.AddMergedRegion(range);
        }
        #endregion

        public static void DataToExeclForNpoi(DataTable sourceTable, string fileName, string sheetName)
        {
            //System.IO.MemoryStream ms = ExportDataTableToExcel(sourceTable, sheetName) as MemoryStream;
            //ms.Seek(0, System.IO.SeekOrigin.Begin);
            //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "****总表" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            //Response.BinaryWrite(ms.ToArray());
            //ms.Close();
            //ms.Dispose();
        }

        #region Excel读取到DataTable
        /// <summary>
        /// 获取excel内容
        /// </summary>
        /// <param name="filePath">excel文件路径</param>
        /// <returns></returns>
        public static DataTable ImportExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            using (FileStream fsRead = System.IO.File.OpenRead(filePath))
            {
                IWorkbook wk = null;
                //获取后缀名
                string extension = filePath.Substring(filePath.LastIndexOf(".")).ToString().ToLower();
                //判断是否是excel文件
                if (extension == ".xls")
                {
                    wk = new HSSFWorkbook(fsRead);
                    //获取第一个sheet
                    ISheet sheet = wk.GetSheetAt(0);
                    //获取第一行
                    IRow headrow = sheet.GetRow(0);
                    //创建列
                    for (int i = headrow.FirstCellNum; i < headrow.Cells.Count; i++)
                    {
                        //  DataColumn datacolum = new DataColumn(headrow.GetCell(i).StringCellValue);
                        DataColumn datacolum = new DataColumn("F" + (i + 1));
                        dt.Columns.Add(datacolum);
                    }
                    //读取每行,从第二行起
                    for (int r = 1; r <= sheet.LastRowNum; r++)
                    {
                        bool result = false;
                        DataRow dr = dt.NewRow();
                        //获取当前行
                        IRow row = sheet.GetRow(r);
                        //读取每列
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            ICell cell = row.GetCell(j); //一个单元格
                            dr[j] = GetCellValue(cell); //获取单元格的值
                            //全为空则不取
                            if (dr[j].ToString() != "")
                                result = true;
                        }
                        if (result == true)
                            dt.Rows.Add(dr); //把每行追加到DataTable
                    }
                }
            }
            return dt;
        }

        //对单元格进行判断取值
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.BLANK: //空数据类型 这里类型注意一下，不同版本NPOI大小写可能不一样,有的版本是Blank（首字母大写)
                    return string.Empty;
                case CellType.BOOLEAN: //bool类型
                    return cell.BooleanCellValue.ToString();
                case CellType.ERROR:
                    return cell.ErrorCellValue.ToString();
                case CellType.NUMERIC: //数字类型
                    if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //其它数字
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown: //无法识别类型
                default: //默认类型
                    return cell.ToString();//
                case CellType.STRING: //string 类型
                    return cell.StringCellValue;
                case CellType.FORMULA: //带公式类型
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
        #endregion
    }
}
