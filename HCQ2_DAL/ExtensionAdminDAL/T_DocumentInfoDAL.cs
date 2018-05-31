using HCQ2_Common.SQL;
using HCQ2_Model;
using HCQ2_Model.DocModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Common;

namespace HCQ2_DAL_MSSQL
{
    public partial class T_DocumentInfoDAL:HCQ2_IDAL.IT_DocumentInfoDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        /// <summary>
        ///  sql语句
        /// </summary>
        private StringBuilder sb = new StringBuilder();
        private List<int> listFolder = new List<int>();
        #region 0. 我的文档
        /// <summary>
        /// 0. 我的文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTableByOwnDoc(DocTableParamModel model,int user_id)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP  {0} info.file_id,info.file_name,info.create_id,info.alias_name,info.file_type,info.file_size,info.issue_start,info.issue_end,info.create_time,info.create_name,info.file_note,info.url,doc_type=(CASE WHEN ISNULL(code.doc_type,'')='' THEN info.doc_type ELSE code.doc_type END),doc_number,info.file_money,info.down_number,codeType.file_classify,info.file_status FROM
            (SELECT file_id FROM T_DocumentFolderRelation WHERE folder_id=@folder_id AND create_id=@create_id) ration INNER JOIN
            (SELECT file_id,file_name,alias_name,file_type,file_size=(CASE WHEN file_size>999 THEN CAST(CAST(file_size/1024 AS DECIMAL(10,2)) AS NVARCHAR(20))+'M' ELSE CAST(file_size AS NVARCHAR(20))+'KB' END),
            CONVERT(varchar(100),issue_start,23) AS issue_start,CONVERT(varchar(100),issue_end,23) AS issue_end,CONVERT(varchar(100),create_time,23) AS create_time,
            issue_name,create_name,file_note,attach_url AS url,ROW_NUMBER() OVER(order by file_id asc) as DispOrder,create_id,doc_type,doc_number,file_money,down_number,file_classify,file_status FROM dbo.T_DocumentInfo WHERE if_remove=0", model.rows));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            //文档状态
            if (!string.IsNullOrEmpty(model.file_status) && model.file_status!="-1")
                sb.Append(string.Format(" AND file_status={0} ", model.file_status));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start,model.issue_end));
            else if(!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ",model.issue_start));
            else if(string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(string.Format(@") info ON ration.file_id=info.file_id LEFT JOIN
            (SELECT item2.doc_type,item2.issue_value FROM
	            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='docType') item1 INNER JOIN
	            (SELECT code_name AS doc_type,code_value AS issue_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) code ON info.doc_type=code.issue_value LEFT JOIN (SELECT item2.file_classify,item2.file_classify_value FROM
            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='FileClassify') item1 INNER JOIN
            (SELECT code_name AS file_classify,code_value AS file_classify_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) codeType ON
             info.file_classify=codeType.file_classify_value where info.DispOrder>{0};", (model.page - 1) * model.rows));
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            _param.Add("@create_id", user_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<DocTreeResultModel>(dt);
        }
        /// <summary>
        ///  0. 我的文档 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        public int GetTableByOwnDocCount(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(@"SELECT COUNT(*) FROM
            (SELECT file_id FROM T_DocumentFolderRelation WHERE folder_id=@folder_id AND create_id=@create_id) ration INNER JOIN
            (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=0 ");
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            //文档状态
            if (!string.IsNullOrEmpty(model.file_status) && model.file_status != "-1")
                sb.Append(string.Format(" AND file_status={0} ", model.file_status));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(@") info ON ration.file_id=info.file_id;");
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            _param.Add("@create_id", user_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        #endregion

        #region 1. 我的分享
        /// <summary>
        /// 1. 我的分享
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTableByOwnShareDoc(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} info.file_id,info.file_name,info.create_id,info.alias_name,info.file_type,info.file_size,info.issue_start,info.issue_end,info.create_time,info.create_name,info.file_note,info.url,doc_type=(CASE WHEN ISNULL(code.doc_type,'')='' THEN info.doc_type ELSE code.doc_type END),doc_number,info.file_money,info.down_number,codeType.file_classify,info.file_status FROM
            (SELECT file_id FROM T_DocumentSetType WHERE share_id=@user_id AND folder_id=@folder_id) share INNER JOIN
            (SELECT file_id,file_name,alias_name,file_type,file_size=(CASE WHEN file_size>999 THEN CAST(CAST(file_size/1024 AS DECIMAL(10,2)) AS NVARCHAR(20))+'M' ELSE CAST(file_size AS NVARCHAR(20))+'KB' END),
            CONVERT(varchar(100),issue_start,23) AS issue_start,CONVERT(varchar(100),issue_end,23) AS issue_end,CONVERT(varchar(100),create_time,23) AS create_time,
            issue_name,create_name,file_note,attach_url AS url,ROW_NUMBER() OVER(order by file_id asc) as DispOrder,create_id,doc_type,doc_number,file_money,down_number,file_classify,file_status FROM dbo.T_DocumentInfo WHERE if_remove=0 ", model.rows));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(string.Format(@")info ON share.file_id=info.file_id LEFT JOIN
            (SELECT item2.doc_type,item2.issue_value FROM
	            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='docType') item1 INNER JOIN
	            (SELECT code_name AS doc_type,code_value AS issue_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) code ON info.doc_type=code.issue_value LEFT JOIN (SELECT item2.file_classify,item2.file_classify_value FROM
            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='FileClassify') item1 INNER JOIN
            (SELECT code_name AS file_classify,code_value AS file_classify_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) codeType ON
             info.file_classify=codeType.file_classify_value WHERE info.DispOrder>{0};", (model.page - 1) * model.rows));
            _param?.Clear();
            _param.Add("@user_id", user_id);
            _param.Add("@folder_id",model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<DocTreeResultModel>(dt);
        }
        /// <summary>
        /// 1. 我的分享 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetTableByOwnShareDocCount(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(@"SELECT COUNT(*) FROM
            (SELECT file_id FROM T_DocumentSetType WHERE share_id=@user_id AND folder_id=@folder_id) share INNER JOIN
            (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=0 ");
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(@")info ON share.file_id=info.file_id;");
            _param?.Clear();
            _param.Add("@user_id", user_id);
            _param.Add("@folder_id", model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        #endregion

        #region 2. 分享文档
        /// <summary>
        /// 2. 分享文档
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTableShareByOwnDoc(DocTableParamModel model, int user_id, List<int> roles)
        {
            string _roles = (roles.Count > 0) ? string.Join(",", roles) : "0";
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} info.file_id,info.file_name,info.alias_name,info.file_type,info.file_size,info.issue_start,info.issue_end,info.create_time,info.create_name,info.file_note,info.url,doc_type=(CASE WHEN ISNULL(code.doc_type,'')='' THEN info.doc_type ELSE code.doc_type END),doc_number,info.file_money,info.down_number,codeType.file_classify,info.file_status FROM
                (SELECT * FROM 
                  (SELECT file_id FROM T_DocumentSetType WHERE folder_id=@folder_id AND role_id IN({1})
	                UNION
	                SELECT file_id FROM T_DocumentSetType WHERE folder_id=@folder_id AND user_id=@user_id
                   )a
                 ) share INNER JOIN
                 (SELECT file_id,file_name,alias_name,file_type,file_size=(CASE WHEN file_size>999 THEN CAST(CAST(file_size/1024 AS DECIMAL(10,2)) AS NVARCHAR(20))+'M' ELSE CAST(file_size AS NVARCHAR(20))+'KB' END),
                CONVERT(varchar(100),issue_start,23) AS issue_start,CONVERT(varchar(100),issue_end,23) AS issue_end,CONVERT(varchar(100),create_time,23) AS create_time,
                issue_name,create_name,file_note,attach_url AS url,ROW_NUMBER() OVER(order by file_id asc) as DispOrder,create_id,doc_type,doc_number,file_money,down_number,file_classify,file_status FROM dbo.T_DocumentInfo WHERE if_remove=0", model.rows, _roles));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(string.Format(@") info
                  ON info.file_id=share.file_id LEFT JOIN
            (SELECT item2.doc_type,item2.issue_value FROM
	            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='docType') item1 INNER JOIN
	            (SELECT code_name AS doc_type,code_value AS issue_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) code 
	            ON info.doc_type=code.issue_value LEFT JOIN (SELECT item2.file_classify,item2.file_classify_value FROM
            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='FileClassify') item1 INNER JOIN
            (SELECT code_name AS file_classify,code_value AS file_classify_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) codeType ON
             info.file_classify=codeType.file_classify_value WHERE info.DispOrder>{0};", (model.page - 1) * model.rows));
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            _param.Add("@user_id", user_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<DocTreeResultModel>(dt);
        }
        /// <summary>
        /// 2. 分享文档 数量统计
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetTableShareByOwnDocCount(DocTableParamModel model, int user_id, List<int> roles)
        {
            string _roles = (roles.Count > 0) ? string.Join(",", roles) : "0";
            sb?.Clear();
            sb.Append(string.Format(@"SELECT COUNT(*) FROM
                (SELECT * FROM 
                  (SELECT file_id FROM T_DocumentSetType WHERE folder_id=@folder_id AND role_id IN({0})
	                UNION
	                SELECT file_id FROM T_DocumentSetType WHERE folder_id=@folder_id AND user_id=@user_id
                   )a
                 ) share INNER JOIN
                 (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=0 ", _roles));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(@") info
                  ON info.file_id=share.file_id;");
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            _param.Add("@user_id", user_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        #endregion

        #region 3. 公用文档
        /// <summary>
        /// 3. 公用文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTablePublicDoc(DocTableParamModel model, int user_id)
        {
            List<int> fList = GetChildFileID(model.folder_id,user_id);
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} info.file_id,info.file_name,info.create_id,info.alias_name,info.file_type,info.file_size,info.issue_start,info.issue_end,info.create_time,info.create_name,info.file_note,info.url,doc_type=(CASE WHEN ISNULL(code.doc_type,'')='' THEN info.doc_type ELSE code.doc_type END),doc_number,info.file_money,info.down_number,codeType.file_classify,info.file_status FROM 
            (SELECT file_id FROM dbo.T_DocumentFolderRelation WHERE folder_id in({1})) raction INNER JOIN
            (SELECT file_id,file_name,alias_name,file_type,file_size=(CASE WHEN file_size>999 THEN CAST(CAST(file_size/1024 AS DECIMAL(10,2)) AS NVARCHAR(20))+'M' ELSE CAST(file_size AS NVARCHAR(20))+'KB' END),
            CONVERT(varchar(100),issue_start,23) AS issue_start,CONVERT(varchar(100),issue_end,23) AS issue_end,CONVERT(varchar(100),create_time,23) AS create_time,
            issue_name,create_name,file_note,attach_url AS url,ROW_NUMBER() OVER(order by file_id asc) as DispOrder,create_id,doc_type,doc_number,file_money,down_number,file_classify,file_status FROM dbo.T_DocumentInfo WHERE if_remove=0 AND file_status=5 ", model.rows, string.Join(",", fList)));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(string.Format(@") info ON raction.file_id = info.file_id LEFT JOIN
            (SELECT item2.doc_type,item2.issue_value FROM
	            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='docType') item1 INNER JOIN
	            (SELECT code_name AS doc_type,code_value AS issue_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) code 
	            ON info.doc_type=code.issue_value LEFT JOIN (SELECT item2.file_classify,item2.file_classify_value FROM
            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='FileClassify') item1 INNER JOIN
            (SELECT code_name AS file_classify,code_value AS file_classify_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) codeType ON
             info.file_classify=codeType.file_classify_value WHERE info.DispOrder>{0};", (model.page - 1) * model.rows));
            _param.Clear();
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(),CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<DocTreeResultModel>(dt);
        }
        /// <summary>
        /// 3. 公用文档 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetTablePublicDocCount(DocTableParamModel model, int user_id)
        {
            List<int> fList = GetChildFileID(model.folder_id,user_id);
            sb?.Clear();
            sb.Append(string.Format(@"SELECT COUNT(*) FROM 
            (SELECT file_id FROM dbo.T_DocumentFolderRelation WHERE folder_id in({0})) raction INNER JOIN
            (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=0 AND file_status=5 ", string.Join(",", fList)));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(@") info ON raction.file_id = info.file_id;");
            _param.Clear();
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        /// <summary>
        ///  3.公用文档 统计自己目录下文档数量
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetTablePublicDocCountByOwn(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT COUNT(*) FROM 
            (SELECT file_id FROM dbo.T_DocumentFolderRelation WHERE folder_id=@folder_id) raction INNER JOIN
            (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=0 AND file_status=5 "));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(@") info ON raction.file_id = info.file_id;");
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        /// <summary>
        ///  根据folder_id获取子节点fodler_id集合
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        private List<int> GetChildFileID(int folder_id,int user_id)
        {
            listFolder?.Clear();
            listFolder.Add(folder_id);
            List<T_DocumentFolder> docFolder = new T_DocumentFolderDAL().Select(s => !string.IsNullOrEmpty(s.folder_name)).ToList();
            var tempList = docFolder.Where(s => s.folder_pid == folder_id);
            if (null == tempList || tempList.Count() <= 0)
                return listFolder;
            foreach (T_DocumentFolder item in tempList)
            {
                listFolder.Add(item.folder_id);
                var strList = docFolder.Where(s => s.folder_pid == item.folder_id).ToList();
                if (null != strList || strList.Count() > 0)
                    GetList(docFolder, strList,user_id);
            }
            return listFolder;
        }
        private void GetList(List<T_DocumentFolder> docFolder, List<T_DocumentFolder> strList,int user_id)
        {
            foreach (T_DocumentFolder item in strList)
            {
                listFolder.Add(item.folder_id);
                var tempList = docFolder.Where(s => s.folder_pid == item.folder_id).ToList();
                if (null != strList || strList.Count() > 0)
                    GetList(docFolder, tempList,user_id);
            }
        }
        #endregion

        #region 4. 回收站
        /// <summary>
        /// 4. 回收站
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTableRemoveDoc(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} info.file_id,info.file_name,info.create_id,info.alias_name,info.file_type,info.file_size,info.issue_start,info.issue_end,info.create_time,info.create_name,info.file_note,info.url,doc_type=(CASE WHEN ISNULL(code.doc_type,'')='' THEN info.doc_type ELSE code.doc_type END),doc_number,info.file_money,info.down_number,codeType.file_classify,info.file_status FROM 
            (SELECT file_id FROM T_DocumentFolderRelation WHERE folder_id=@folder_id) del INNER JOIN
            (SELECT file_id,file_name,alias_name,file_type,file_size=(CASE WHEN file_size>999 THEN CAST(CAST(file_size/1024 AS DECIMAL(10,2)) AS NVARCHAR(20))+'M' ELSE CAST(file_size AS NVARCHAR(20))+'KB' END),
            CONVERT(varchar(100),issue_start,23) AS issue_start,CONVERT(varchar(100),issue_end,23) AS issue_end,CONVERT(varchar(100),create_time,23) AS create_time,
            issue_name,create_name,file_note,attach_url AS url,ROW_NUMBER() OVER(order by file_id asc) as DispOrder,create_id,doc_type,doc_number,file_money,down_number,file_classify,file_status FROM dbo.T_DocumentInfo WHERE  if_remove=1 ", model.rows));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(string.Format(@")info ON del.file_id = info.file_id LEFT JOIN
            (SELECT item2.doc_type,item2.issue_value FROM
	            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='docType') item1 INNER JOIN
	            (SELECT code_name AS doc_type,code_value AS issue_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) code 
	            ON info.doc_type=code.issue_value LEFT JOIN (SELECT item2.file_classify,item2.file_classify_value FROM
            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='FileClassify') item1 INNER JOIN
            (SELECT code_name AS file_classify,code_value AS file_classify_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) codeType ON
             info.file_classify=codeType.file_classify_value WHERE info.DispOrder>{0};", (model.page - 1) * model.rows));
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<DocTreeResultModel>(dt);
        }
        /// <summary>
        /// 4. 回收站 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetTableRemoveDocCount(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(@"SELECT COUNT(*) FROM (SELECT file_id FROM T_DocumentFolderRelation WHERE folder_id=@folder_id) del INNER JOIN (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=1 ");
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(") info ON del.file_id = info.file_id");
            _param?.Clear();
            _param.Add("@folder_id", model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        #endregion

        #region 5. 待审核资源
        /// <summary>
        ///  5：待审核资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTableApproveDoc(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP  {0} info.file_id,info.file_name,info.create_id,info.alias_name,info.file_type,info.file_size,info.issue_start,info.issue_end,info.create_time,info.create_name,info.file_note,info.url,doc_type=(CASE WHEN ISNULL(code.doc_type,'')='' THEN info.doc_type ELSE code.doc_type END),doc_number,info.file_money,info.down_number,codeType.file_classify,info.file_status FROM
            (SELECT file_id FROM T_DocumentFolderRelation WHERE folder_id=@folder_id) ration INNER JOIN
            (SELECT file_id,file_name,alias_name,file_type,file_size=(CASE WHEN file_size>999 THEN CAST(CAST(file_size/1024 AS DECIMAL(10,2)) AS NVARCHAR(20))+'M' ELSE CAST(file_size AS NVARCHAR(20))+'KB' END),
            CONVERT(varchar(100),issue_start,23) AS issue_start,CONVERT(varchar(100),issue_end,23) AS issue_end,CONVERT(varchar(100),create_time,23) AS create_time,
            issue_name,create_name,file_note,attach_url AS url,ROW_NUMBER() OVER(order by file_id asc) as DispOrder,create_id,doc_type,doc_number,file_money,down_number,file_classify,file_status FROM dbo.T_DocumentInfo WHERE if_remove=0 AND file_status=1 ", model.rows));
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(string.Format(@") info ON ration.file_id=info.file_id LEFT JOIN
            (SELECT item2.doc_type,item2.issue_value FROM
	            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='docType') item1 INNER JOIN
	            (SELECT code_name AS doc_type,code_value AS issue_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) code ON info.doc_type=code.issue_value LEFT JOIN (SELECT item2.file_classify,item2.file_classify_value FROM
            (SELECT item_id,item_code FROM dbo.T_ItemCode WHERE item_code='FileClassify') item1 INNER JOIN
            (SELECT code_name AS file_classify,code_value AS file_classify_value,item_id FROM dbo.T_ItemCodeMenum) item2 ON item1.item_id = item2.item_id) codeType ON
             info.file_classify=codeType.file_classify_value where info.DispOrder>{0};", (model.page - 1) * model.rows));
            _param.Clear();
            _param.Add("@folder_id", model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            DataTable dt = SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param));
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<DocTreeResultModel>(dt);
        }
        /// <summary>
        ///  5：待审核资源 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        public int GetTableApproveDocCount(DocTableParamModel model, int user_id)
        {
            sb?.Clear();
            sb.Append(@"SELECT COUNT(*) FROM
            (SELECT file_id FROM T_DocumentFolderRelation WHERE folder_id=@folder_id) ration INNER JOIN
            (SELECT file_id FROM dbo.T_DocumentInfo WHERE if_remove=0 AND file_status=1 ");
            //文档名称
            if (!string.IsNullOrEmpty(model.keyword))
                sb.Append(string.Format(" AND (file_name like '%@keyword%' OR create_name like '%@keyword%') "));
            if (!string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE)  AND issue_start<=CAST('{1}' AS DATE) ", model.issue_start, model.issue_end));
            else if (!string.IsNullOrEmpty(model.issue_start) && string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_start>=CAST('{0}' AS DATE) ", model.issue_start));
            else if (string.IsNullOrEmpty(model.issue_start) && !string.IsNullOrEmpty(model.issue_end))
                sb.Append(string.Format(" AND issue_end<=CAST('{0}' AS DATE) ", model.issue_end));
            sb.Append(@") info ON ration.file_id=info.file_id;");
            _param.Clear();
            _param.Add("@folder_id", model.folder_id);
            if (!string.IsNullOrEmpty(model.keyword))
                _param.Add("@keyword", model.keyword);
            return Helper.ToInt(SqlHelper.ExecuteScalar(sb.ToString(), CommandType.Text, SqlHelper.GetParameters(_param)));
        }
        #endregion
    }
}
