using HCQ2_Model;
using HCQ2_Model.BaneUser;
using HCQ2_Model.BaneUser.APP.Result;
using HCQ2_Model.WebApiModel.ParamModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HCQ2_DAL_MSSQL
{
    public partial class Bane_UserDAL:HCQ2_IDAL.IBane_UserDAL
    {
        /// <summary>
        ///  参数
        /// </summary>
        private Dictionary<string, object> _param = new Dictionary<string, object>();
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        ///  获取戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaneListModel> GetBaneData(BaneListParams param,List<int> perList, bool isManager)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} * FROM  
            (SELECT ROW_NUMBER() OVER(ORDER BY user_id ASC) rowNumber,user_name,user_sex,user_identify,user_type,user_phone,DATEDIFF(yy,CONVERT(varchar,user_birth,101),GETDATE())AS user_age,
            (CASE WHEN LEN(user_identify)=18 THEN LEFT(user_identify,6)+REPLICATE('*',8)+RIGHT(user_identify,4) 
            WHEN LEN(user_identify)=15 THEN LEFT(user_identify,6)+REPLICATE('*',6)+RIGHT(user_identify,3) ELSE REPLICATE('*',LEN(user_identify)) END) AS hiden_identify FROM Bane_User 
            WHERE 1=1 ", param.rows));
            if (!string.IsNullOrEmpty(param.baneName))
                sb.Append(string.Format(" AND  user_name LIKE '%{0}%' ", param.baneName));
            if (!string.IsNullOrEmpty(param.baneType))
                sb.Append(string.Format(" AND  user_type='{0}' ", param.baneType));
            //判断是否为父节点
            if (param.isParent)
                if (isManager)
                    sb.AppendFormat(@" AND org_id IN(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path<>'{0}' AND folder_path LIKE '{0}%') AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={1} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({2}))  ", param.folder_path, param.user_id, string.Join(",", perList));
                else
                    sb.Append(string.Format(" AND org_id IN(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path<>'{0}' AND folder_path LIKE '{0}%') AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id in({2})))", param.folder_path, param.user_id, string.Join(",", perList)));
            else
                if (isManager)
                sb.AppendFormat(@" AND org_id={0} ", param.orgId);
            else
                sb.Append(string.Format(" AND org_id={0} AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2})))", param.orgId, param.user_id, string.Join(",", perList)));
            sb.Append(string.Format(@") users LEFT JOIN
(SELECT ROW_NUMBER() OVER(PARTITION BY user_identify ORDER BY start_date DESC) rank,CONVERT(varchar(100),start_date,23) AS start_date,
CONVERT(varchar(100),end_date,23) AS end_date,end_reason,user_identify AS a0177 FROM Bane_RecoveryInfo
WHERE 1=1 "));
            if (!string.IsNullOrEmpty(param.baneEnd))
                sb.Append(string.Format(" AND  end_reason='{0}' ", param.baneEnd));
            sb.Append(string.Format(@") info ON users.user_identify=info.a0177 WHERE info.rank=1 AND users.rowNumber>{0};", (param.page - 1) * param.rows));
            DataTable dt= HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneListModel>(dt);
        }
        /// <summary>
        ///  统计戒毒人员一栏数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetGetBaneDataCount(BaneListParams param, List<int> perList, bool isManager)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT COUNT(*) FROM  
            (SELECT user_sex,user_identify,user_type,user_phone FROM Bane_User 
            WHERE 1=1  "));
            if (!string.IsNullOrEmpty(param.baneName))
                sb.Append(string.Format(" AND  user_name LIKE '%{0}%' ", param.baneName));
            if (!string.IsNullOrEmpty(param.baneType))
                sb.Append(string.Format(" AND  user_type='{0}' ", param.baneType));
            //判断是否为父节点
            if (param.isParent)
                if (isManager)
                    sb.AppendFormat(@" AND org_id IN(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path<>'{0}' AND folder_path LIKE '{0}%') AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={1} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({2})) ", param.folder_path, param.user_id, string.Join(",", perList));
                else
                    sb.Append(string.Format(" AND org_id IN(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path<>'{0}' AND folder_path LIKE '{0}%')  AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2})))", param.folder_path, param.user_id, string.Join(",", perList)));
            else
                if (isManager)
                    sb.AppendFormat(@" AND org_id={0} ", param.orgId);
                else
                    sb.Append(string.Format(" AND org_id={0} AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2})))", param.orgId, param.user_id, string.Join(",", perList)));
            sb.Append(@") users LEFT JOIN
            (SELECT ROW_NUMBER() OVER(PARTITION BY user_identify ORDER BY start_date DESC) rank,user_identify AS a0177 FROM Bane_RecoveryInfo
            WHERE 1=1 ");
            if (!string.IsNullOrEmpty(param.baneEnd))
                sb.Append(string.Format(" AND  end_reason='{0}' ", param.baneEnd));
            sb.Append(@") info ON users.user_identify=info.a0177 WHERE info.rank=1;");
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  获取定期尿检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BaneProModel> GetBaneProData(BaneListParams param, List<int> perList, bool isManager)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT TOP {0} ur_id,user_name,user_sex,user_age,user_identify,user_type,this_date,next_date,ISNULL(approve_status,2) AS approve_status,hiden_identify FROM 
            (SELECT ROW_NUMBER() OVER(ORDER BY user_id ASC) rowNumber,ur_next_date,user_name,user_sex,DATEDIFF(yy,CONVERT(varchar,user_birth,101),GETDATE())AS user_age,user_identify,user_type,CONVERT(varchar(100),ur_next_date,20) AS next_date,(CASE WHEN LEN(user_identify)=18 THEN LEFT(user_identify,6)+REPLICATE('*',8)+RIGHT(user_identify,4) WHEN LEN(user_identify)=15 THEN LEFT(user_identify,6)+REPLICATE('*',6)+RIGHT(user_identify,3) ELSE REPLICATE('*',LEN(user_identify)) END) AS hiden_identify FROM dbo.Bane_User WHERE 1=1 ", param.rows));
            if (param.isParent)
                if (isManager)
                    sb.AppendFormat(@" AND org_id in(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path <> '{0}' AND folder_path LIKE '{0}%') AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={1} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({2})) ", param.folder_path, param.user_id, string.Join(",", perList));
                else
                    sb.Append(string.Format(@" AND org_id in(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path <> '{0}' AND folder_path LIKE '{0}%')  AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2})))", param.folder_path, param.user_id, string.Join(",", perList)));
            else
                if (isManager)
                sb.AppendFormat(@" AND org_id={0} ", param.orgId);
            else
                sb.Append(string.Format(" AND org_id={0} AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2})))", param.orgId, param.user_id, string.Join(",", perList)));
            if (!string.IsNullOrEmpty(param.baneName))
                sb.Append(string.Format(" AND  user_name LIKE '%{0}%' ", param.baneName));
            if (!string.IsNullOrEmpty(param.baneType))
                sb.Append(string.Format(" AND  user_type='{0}' ", param.baneType));
            //定期尿检页面
            if (param.baneTask == "0")
            {
                sb.Append(@")users LEFT JOIN (SELECT * FROM(
                    SELECT ROW_NUMBER() OVER(PARTITION BY user_identify ORDER BY ur_id DESC) number, ur_id, user_identify AS a0177,approve_status,
                    CONVERT(varchar(100), ur_reality_date, 20) AS this_date,ur_reality_date FROM dbo.Bane_UrinalysisRecord ");
                if (param.banedays=="-30")
                    sb.AppendFormat("WHERE approve_status=1 ");
                sb.AppendFormat(") uRecord WHERE uRecord.number = 1)");
            }
            else
                sb.Append(")users INNER JOIN (SELECT ur_id,user_identify AS a0177,approve_status,CONVERT(varchar(100),ur_reality_date,20) AS this_date,ur_reality_date FROM dbo.Bane_UrinalysisRecord WHERE approve_status=0)");
            sb.Append(string.Format(@" record ON record.a0177 = users.user_identify WHERE users.rowNumber>{0} ", (param.page - 1) * param.rows));
            switch (param.banedays)
            {
                //已过检测
                case "0": sb.AppendFormat(" AND DATEDIFF(dd,CONVERT(varchar,users.ur_next_date,101),GETDATE())>{0}", param.banedays); break;
                //一周内
                case "-7": sb.AppendFormat(" AND DATEDIFF(dd,CONVERT(varchar,users.ur_next_date,101),GETDATE()) BETWEEN -7 AND 0"); break;
                //本月应检
                case "30": sb.AppendFormat(" AND MONTH(ur_next_date)=MONTH(GETDATE()) "); break;
                //本月已检
                case "-30": sb.AppendFormat(" AND LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),record.ur_reality_date,23),7) "); break;
            }
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneProModel>(dt);
        }
        /// <summary>
        ///  统计定期尿检人员数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetBaneProDataCount(BaneListParams param, List<int> perList, bool isManager)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT COUNT(*) FROM 
(SELECT ur_next_date,user_name,user_identify,user_type FROM dbo.Bane_User WHERE 1=1 "));
            if (param.isParent)
                if (isManager)
                    sb.AppendFormat(@" AND org_id in(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path <> '{0}' AND folder_path LIKE '{0}%') AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={1} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({2})) ", param.folder_path, param.user_id, string.Join(",", perList));
                else
                    sb.Append(string.Format(@" AND org_id in(SELECT folder_id FROM dbo.T_OrgFolder WHERE folder_path <> '{0}' AND folder_path LIKE '{0}%') AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2})))", param.folder_path, param.user_id, string.Join(",", perList)));
            else
                if (isManager)
                sb.AppendFormat(@" AND org_id={0} ", param.orgId);
            else
                sb.Append(string.Format(" AND org_id={0} AND (user_id IN(SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={1}) OR user_id IN(SELECT user_id FROM Bane_UserPermissRelation WHERE per_id IN({2}))) ", param.orgId, param.user_id, string.Join(",", perList)));
            if (!string.IsNullOrEmpty(param.baneName))
                sb.Append(string.Format(" AND  user_name LIKE '%{0}%' ", param.baneName));
            if (!string.IsNullOrEmpty(param.baneType))
                sb.Append(string.Format(" AND  user_type='{0}' ", param.baneType));
            if (param.baneTask == "0")
            {
                sb.Append(@")users LEFT JOIN (SELECT * FROM(
                    SELECT ROW_NUMBER() OVER(PARTITION BY user_identify ORDER BY ur_id ASC) number, ur_id, user_identify AS a0177, approve_status,
                    CONVERT(varchar(100), ur_reality_date, 23) AS this_date,ur_reality_date FROM dbo.Bane_UrinalysisRecord ");
                if (param.banedays == "-30")
                    sb.AppendFormat("WHERE approve_status=1 ");
                sb.AppendFormat(") uRecord WHERE uRecord.number = 1) record ON record.a0177 = users.user_identify");
            }
            else
                sb.Append(")users INNER JOIN (SELECT ur_id,user_identify AS a0177,ur_reality_date FROM dbo.Bane_UrinalysisRecord WHERE approve_status=0) record ON record.a0177 = users.user_identify ");
            switch (param.banedays)
            {
                //已过检测
                case "0": sb.AppendFormat(" WHERE DATEDIFF(dd,CONVERT(varchar,users.ur_next_date,101),GETDATE())>{0}", param.banedays); break;
                //一周内
                case "-7": sb.AppendFormat(" WHERE DATEDIFF(dd,CONVERT(varchar,users.ur_next_date,101),GETDATE()) BETWEEN -7 AND 0"); break;
                //本月应检
                case "30": sb.AppendFormat(" WHERE MONTH(ur_next_date)=MONTH(GETDATE()) "); break;
                //本月已检
                case "-30": sb.AppendFormat(" AND LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),record.ur_reality_date,23),7) "); break;
            }
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  根据身份证获取数据
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneAddUser GetBaneUser(string user_identify)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT * FROM 
            (SELECT user_id,user_name,iris_data1,iris_data2,alias_name,user_sex,CONVERT(varchar(100),user_birth,23) AS user_birth,user_height,user_identify,
            user_edu,job_status,bane_type,birth_url,family_phone,live_url,move_phone,attn_name,attn_url,attn_relation,attn_phone,
            marital_status,is_live_parent,user_status,is_pro_train,user_skill,user_type,user_phone,CONVERT(varchar(100),ur_next_date,23) AS ur_next_date,
            user_photo,user_note,org_id,user_resume,CONVERT(varchar(100),control_date,23) AS control_date FROM dbo.Bane_User WHERE user_identify='{0}') users LEFT JOIN
            (SELECT ri_id,user_identify as a0177,exec_area,exec_unit,order_unit,is_aids,isolation_url,CONVERT(varchar(100),isolation_out_date,23) AS isolation_out_date,
            cure_ups,in_recovery,CONVERT(varchar(100),start_date,23) AS start_date,CONVERT(varchar(100),end_date,23) AS end_date,end_reason FROM dbo.Bane_RecoveryInfo WHERE user_identify='{0}') info 
            ON users.user_identify=info.a0177;", user_identify));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneAddUser>(dt).FirstOrDefault();
        }
        /// <summary>
        ///  本月应检人数统计
        /// </summary>
        /// <returns></returns>
        public int GetCountByMonth(int user_id, List<int> perList, bool isManager)
        {
            sb?.Clear();
            if (user_id > 0)
            {
                sb.AppendFormat(@"SELECT COUNT(*) FROM 
                (SELECT user_identify FROM dbo.Bane_User WHERE 
	                LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),ur_next_date,23),7)
	                UNION ALL
	                SELECT user_identify FROM Bane_UrinalysisRecord WHERE LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),ur_should_date,23),7)	
                )bane INNER JOIN
                (SELECT user_id,user_identify FROM dbo.Bane_User ");
                if (isManager)
                    sb.AppendFormat(@" WHERE org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={0} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({1})) ", user_id, string.Join(",", perList));
                sb.AppendFormat(@") users ON users.user_identify = bane.user_identify");
                if (!isManager)
                {
                    sb.AppendFormat(@" INNER JOIN
                    (SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={0} UNION SELECT user_id AS person_id FROM dbo.Bane_UserPermissRelation WHERE per_id IN({1})) relation ON relation.person_id = users.user_id;", user_id, string.Join(",", perList));
                }
            }
            else
                sb.Append(@"SELECT COUNT(*) FROM 
                (SELECT user_identify FROM dbo.Bane_User WHERE 
                LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),ur_next_date,23),7)
                UNION ALL
                SELECT user_identify FROM Bane_UrinalysisRecord WHERE LEFT(CONVERT(varchar(100),GETDATE(),23),7)=LEFT(CONVERT(varchar(100),ur_should_date,23),7)
                ) BaneCount;");
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  过检人数统计，根据权限
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int PassCountPerson(int user_id, List<int> perList, bool isManager)
        {
            sb?.Clear();
            if (user_id > 0)
            {
                sb.AppendFormat(@"SELECT COUNT(*) FROM 
                (SELECT user_id FROM dbo.Bane_User WHERE ur_next_date < GETDATE() ");
                if (isManager)
                    sb.AppendFormat(@" AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={0} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({1})) ", user_id, string.Join(",", perList));
                sb.AppendFormat(@") users ");
                if (!isManager)
                    sb.AppendFormat(@" INNER JOIN
                (SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={0} UNION SELECT user_id AS person_id FROM dbo.Bane_UserPermissRelation WHERE per_id IN({1}))relation ON users.user_id=relation.person_id;", user_id, string.Join(",", perList));
            }
            else
                sb.Append(@"SELECT COUNT(*) FROM dbo.Bane_User WHERE ur_next_date < GETDATE();");
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  统计一周内应到检测人员数量
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetWeekCountPerson(int user_id, List<int> perList, bool isManager)
        {
            sb?.Clear();
            if (user_id > 0)
            {
                sb.AppendFormat(@"SELECT COUNT(*) FROM 
                (SELECT user_id FROM dbo.Bane_User WHERE DATEDIFF(dd, CONVERT(varchar, ur_next_date, 101), GETDATE()) BETWEEN - 7 AND 0  ");
                if(isManager)
                    sb.AppendFormat(@" AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={0} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({1})) ", user_id, string.Join(",", perList));
                sb.AppendFormat(@" ) users ");
                if (!isManager)
                    sb.AppendFormat(@" INNER JOIN
                (SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={0} UNION SELECT user_id AS person_id FROM dbo.Bane_UserPermissRelation WHERE per_id IN({1}))relation ON users.user_id=relation.person_id; ", user_id, string.Join(",", perList));
            } 
            else
                sb.Append(@"SELECT COUNT(*) FROM dbo.Bane_User WHERE DATEDIFF(dd, CONVERT(varchar, ur_next_date, 101), GETDATE()) BETWEEN - 7 AND 0;");
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  统计总人数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int AllPersonCount(int user_id, List<int> perList, bool isManager)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT COUNT(*) FROM (SELECT user_id FROM dbo.Bane_User WHERE 1=1 ");
            if (isManager)
                sb.AppendFormat(@" AND org_id IN(
	                SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={0} 
	                UNION
	                SELECT area_code AS unit_id FROM dbo.T_AreaPermissRelation WHERE per_id IN({1})) ", user_id, string.Join(",", perList));
            sb.AppendFormat(") users");
            if (!isManager)
                sb.AppendFormat(@" INNER JOIN 
                (SELECT person_id FROM dbo.T_UserUnitPersonRelation WHERE user_id={0}
                UNION
                SELECT user_id AS person_id FROM Bane_UserPermissRelation WHERE per_id IN({1})) relation ON users.user_id=relation.person_id;", user_id, string.Join(",", perList));
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        /// <summary>
        ///  统计社区数
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetBBSCount(int user_id, List<int> perList, bool isManager)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT COUNT(*) FROM 
            (SELECT unit_id FROM dbo.T_UserUnitRelation WHERE user_id={0}
            UNION
            SELECT area_code AS unit_id FROM T_AreaPermissRelation WHERE per_id IN({1})) bane;", user_id, string.Join(",", perList));
            return HCQ2_Common.Helper.ToInt(HCQ2_Common.SQL.SqlHelper.ExecuteScalar(sb.ToString()));
        }
        //**************************************接口*******************************************
        /// <summary>
        /// 获取人员同步数据
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="deviceid">设备编码</param>
        /// <returns></returns>
        public List<PersonCL> GetPersonsSynchronousData(int userid, string deviceid)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT user_name AS person_name,user_sex AS person_sex,CONVERT(varchar(100),user_birth,23) AS person_birthday,
            live_url AS person_address,user_identify AS person_cardno,iris_data1 AS iris_data,iris_data2 AS big_iris_data FROM dbo.Bane_User WHERE
            update_date>ISNULL((SELECT TOP 1 sy_date FROM T_Synchronous WHERE sy_unit_id IN(SELECT unit_id FROM T_UserUnitRelation WHERE user_id={0}) 
            AND deviceid='{1}' ORDER BY sy_date DESC),'1990-01-01 01:01:01') AND org_id IN(SELECT unit_id FROM T_UserUnitRelation WHERE user_id={0}); ", userid, deviceid));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<PersonCL>(dt);
        }

        /// <summary>
        ///  获取下发所有数据 根据权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<PersonCL> GetPersonsSentDownData(string userid)
        {
            sb?.Clear();
            sb.Append(string.Format(@"SELECT user_name AS person_name, user_sex AS person_sex, CONVERT(varchar(100), user_birth, 23) AS person_birthday,
            live_url AS person_address, user_identify AS person_cardno, iris_data1 AS iris_data, iris_data2 AS big_iris_data FROM dbo.Bane_User WHERE org_id IN(SELECT unit_id FROM T_UserUnitRelation WHERE user_id = (SELECT user_id FROM dbo.T_User WHERE user_guid = '{0}'));", userid));
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString());
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<PersonCL>(dt);
        }

        //**************************************APP接口*******************************************
        /// <summary>
        ///  注册
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="pwd"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public int RegBaneUser(int user_id, string pwd, string phone)
        {
            sb?.Clear();
            sb.AppendFormat(@"UPDATE dbo.Bane_User SET user_pwd='{0}',user_mobile='{1}' WHERE user_id={2};", pwd, phone, user_id);
            return HCQ2_Common.SQL.SqlHelper.ExecuteNonQuery(sb.ToString());
        }
        /// <summary>
        ///  根据身份证获取首页答题信息
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneHomeAnswerModel GetAnswerDAL(string user_identify)
        {
            sb.Clear();
            sb.AppendFormat(@"SELECT users.the_num AS history_count,users.user_total AS num_count,history.the_id,history.the_score,ISNULL(history.last_id,0) AS last_id,ISNULL(history.last_score,0)AS last_score FROM
            (SELECT the_num,user_total,user_identify FROM dbo.Bane_User WHERE user_identify='{0}')users INNER JOIN
            (SELECT score1.user_identify,score1.hs_id AS the_id,score1.hs_score AS the_score,score2.hs_id AS last_id,score2.hs_score AS last_score  FROM 
            (SELECT TOP 1 hs_id,hs_score,user_identify FROM Bane_HistoryScore WHERE user_identify='{0}' ORDER BY hs_time DESC) score1 LEFT JOIN
            (SELECT * FROM( 
            SELECT ROW_NUMBER() OVER(ORDER BY hs_time DESC) AS rowNum,hs_id,hs_score,user_identify FROM Bane_HistoryScore WHERE user_identify='{0}')score WHERE 
            score.rowNum=2) score2 ON score1.user_identify=score2.user_identify) history ON history.user_identify = users.user_identify;", user_identify);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneHomeAnswerModel>(dt).FirstOrDefault();
        }
        /// <summary>
        ///  获取APP 首页个人状态、本次、下次、管控 (日期)
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneDetectionModel GetBaneDetection(string user_identify)
        {
            sb?.Clear();
            sb.AppendFormat(@"SELECT (CASE WHEN CONVERT(varchar(100),GETDATE(),23)>users.ur_next_date THEN '过检' ELSE '正常' END) AS user_status,users.ur_next_date AS next_date,users.control_date,record.ur_reality_date AS the_date FROM
            (SELECT CONVERT(varchar(100),ur_next_date,23) AS ur_next_date,user_identify,CONVERT(varchar(100),control_date,23) AS control_date FROM dbo.Bane_User WHERE user_identify='{0}') users LEFT JOIN
            (SELECT TOP 1 CONVERT(varchar(100),ur_reality_date,23) AS ur_reality_date,user_identify FROM dbo.Bane_UrinalysisRecord WHERE user_identify='{0}' ORDER BY ur_reality_date DESC) record ON record.user_identify = users.user_identify;", user_identify);
            DataTable dt = HCQ2_Common.SQL.SqlHelper.ExecuteDataTable(sb.ToString(), CommandType.Text);
            return HCQ2_Common.Data.DataTableHelper.DataTableToIList<BaneDetectionModel>(dt).FirstOrDefault();
        }
    }
}
