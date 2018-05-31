
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCQ2_IBLL;
namespace HCQ2_BLL
{
	public partial class Bane_CriminalRecordBLL : BaseBLL<HCQ2_Model.Bane_CriminalRecord>,IBane_CriminalRecordBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_CriminalRecordDAL;
		}
    }
	public partial class Bane_FamilyRecordBLL : BaseBLL<HCQ2_Model.Bane_FamilyRecord>,IBane_FamilyRecordBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_FamilyRecordDAL;
		}
    }
	public partial class Bane_HistoryScoreBLL : BaseBLL<HCQ2_Model.Bane_HistoryScore>,IBane_HistoryScoreBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_HistoryScoreDAL;
		}
    }
	public partial class Bane_HistoryScoreDetialBLL : BaseBLL<HCQ2_Model.Bane_HistoryScoreDetial>,IBane_HistoryScoreDetialBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_HistoryScoreDetialDAL;
		}
    }
	public partial class Bane_IntegralScoreDetialBLL : BaseBLL<HCQ2_Model.Bane_IntegralScoreDetial>,IBane_IntegralScoreDetialBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_IntegralScoreDetialDAL;
		}
    }
	public partial class Bane_LogDetailBLL : BaseBLL<HCQ2_Model.Bane_LogDetail>,IBane_LogDetailBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_LogDetailDAL;
		}
    }
	public partial class Bane_QuestionInfoBLL : BaseBLL<HCQ2_Model.Bane_QuestionInfo>,IBane_QuestionInfoBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_QuestionInfoDAL;
		}
    }
	public partial class Bane_QuestionValueBLL : BaseBLL<HCQ2_Model.Bane_QuestionValue>,IBane_QuestionValueBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_QuestionValueDAL;
		}
    }
	public partial class Bane_RecoveryInfoBLL : BaseBLL<HCQ2_Model.Bane_RecoveryInfo>,IBane_RecoveryInfoBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_RecoveryInfoDAL;
		}
    }
	public partial class Bane_UrinalysisRecordBLL : BaseBLL<HCQ2_Model.Bane_UrinalysisRecord>,IBane_UrinalysisRecordBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_UrinalysisRecordDAL;
		}
    }
	public partial class Bane_UrinalysisTimeSetBLL : BaseBLL<HCQ2_Model.Bane_UrinalysisTimeSet>,IBane_UrinalysisTimeSetBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_UrinalysisTimeSetDAL;
		}
    }
	public partial class Bane_UserBLL : BaseBLL<HCQ2_Model.Bane_User>,IBane_UserBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_UserDAL;
		}
    }
	public partial class Bane_UserPermissRelationBLL : BaseBLL<HCQ2_Model.Bane_UserPermissRelation>,IBane_UserPermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBane_UserPermissRelationDAL;
		}
    }
	public partial class BMQ_DocumentBLL : BaseBLL<HCQ2_Model.BMQ_Document>,IBMQ_DocumentBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IBMQ_DocumentDAL;
		}
    }
	public partial class T_AreaInfoBLL : BaseBLL<HCQ2_Model.T_AreaInfo>,IT_AreaInfoBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_AreaInfoDAL;
		}
    }
	public partial class T_AreaPermissRelationBLL : BaseBLL<HCQ2_Model.T_AreaPermissRelation>,IT_AreaPermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_AreaPermissRelationDAL;
		}
    }
	public partial class T_AskManagerBLL : BaseBLL<HCQ2_Model.T_AskManager>,IT_AskManagerBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_AskManagerDAL;
		}
    }
	public partial class T_CheckGroupBLL : BaseBLL<HCQ2_Model.T_CheckGroup>,IT_CheckGroupBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_CheckGroupDAL;
		}
    }
	public partial class T_ComplaintsBLL : BaseBLL<HCQ2_Model.T_Complaints>,IT_ComplaintsBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ComplaintsDAL;
		}
    }
	public partial class T_CompProInfoBLL : BaseBLL<HCQ2_Model.T_CompProInfo>,IT_CompProInfoBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_CompProInfoDAL;
		}
    }
	public partial class T_ContractBLL : BaseBLL<HCQ2_Model.T_Contract>,IT_ContractBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ContractDAL;
		}
    }
	public partial class T_DocFolderPermissRelationBLL : BaseBLL<HCQ2_Model.T_DocFolderPermissRelation>,IT_DocFolderPermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_DocFolderPermissRelationDAL;
		}
    }
	public partial class T_DocumentFolderBLL : BaseBLL<HCQ2_Model.T_DocumentFolder>,IT_DocumentFolderBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_DocumentFolderDAL;
		}
    }
	public partial class T_DocumentFolderRelationBLL : BaseBLL<HCQ2_Model.T_DocumentFolderRelation>,IT_DocumentFolderRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_DocumentFolderRelationDAL;
		}
    }
	public partial class T_DocumentInfoBLL : BaseBLL<HCQ2_Model.T_DocumentInfo>,IT_DocumentInfoBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_DocumentInfoDAL;
		}
    }
	public partial class T_DocumentSetTypeBLL : BaseBLL<HCQ2_Model.T_DocumentSetType>,IT_DocumentSetTypeBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_DocumentSetTypeDAL;
		}
    }
	public partial class T_ElementPermissRelationBLL : BaseBLL<HCQ2_Model.T_ElementPermissRelation>,IT_ElementPermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ElementPermissRelationDAL;
		}
    }
	public partial class T_EnterDetailBLL : BaseBLL<HCQ2_Model.T_EnterDetail>,IT_EnterDetailBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_EnterDetailDAL;
		}
    }
	public partial class T_EquipmentBLL : BaseBLL<HCQ2_Model.T_Equipment>,IT_EquipmentBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_EquipmentDAL;
		}
    }
	public partial class T_ExceptionLogBLL : BaseBLL<HCQ2_Model.T_ExceptionLog>,IT_ExceptionLogBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ExceptionLogDAL;
		}
    }
	public partial class T_FilePermissRelationBLL : BaseBLL<HCQ2_Model.T_FilePermissRelation>,IT_FilePermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_FilePermissRelationDAL;
		}
    }
	public partial class T_FolderPermissRelationBLL : BaseBLL<HCQ2_Model.T_FolderPermissRelation>,IT_FolderPermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_FolderPermissRelationDAL;
		}
    }
	public partial class T_FunctionBLL : BaseBLL<HCQ2_Model.T_Function>,IT_FunctionBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_FunctionDAL;
		}
    }
	public partial class T_ImplementBLL : BaseBLL<HCQ2_Model.T_Implement>,IT_ImplementBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ImplementDAL;
		}
    }
	public partial class T_InstructionsBLL : BaseBLL<HCQ2_Model.T_Instructions>,IT_InstructionsBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_InstructionsDAL;
		}
    }
	public partial class T_ItemCodeBLL : BaseBLL<HCQ2_Model.T_ItemCode>,IT_ItemCodeBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ItemCodeDAL;
		}
    }
	public partial class T_ItemCodeMenumBLL : BaseBLL<HCQ2_Model.T_ItemCodeMenum>,IT_ItemCodeMenumBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ItemCodeMenumDAL;
		}
    }
	public partial class T_JobResumeRelationBLL : BaseBLL<HCQ2_Model.T_JobResumeRelation>,IT_JobResumeRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_JobResumeRelationDAL;
		}
    }
	public partial class T_LimitUserBLL : BaseBLL<HCQ2_Model.T_LimitUser>,IT_LimitUserBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_LimitUserDAL;
		}
    }
	public partial class T_LoginBLL : BaseBLL<HCQ2_Model.T_Login>,IT_LoginBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_LoginDAL;
		}
    }
	public partial class T_LogSetingBLL : BaseBLL<HCQ2_Model.T_LogSeting>,IT_LogSetingBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_LogSetingDAL;
		}
    }
	public partial class T_LogSetingDetailBLL : BaseBLL<HCQ2_Model.T_LogSetingDetail>,IT_LogSetingDetailBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_LogSetingDetailDAL;
		}
    }
	public partial class T_MessageNoticeBLL : BaseBLL<HCQ2_Model.T_MessageNotice>,IT_MessageNoticeBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_MessageNoticeDAL;
		}
    }
	public partial class T_ModulePermissRelationBLL : BaseBLL<HCQ2_Model.T_ModulePermissRelation>,IT_ModulePermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_ModulePermissRelationDAL;
		}
    }
	public partial class T_Org_UserBLL : BaseBLL<HCQ2_Model.T_Org_User>,IT_Org_UserBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_Org_UserDAL;
		}
    }
	public partial class T_OrgFolderBLL : BaseBLL<HCQ2_Model.T_OrgFolder>,IT_OrgFolderBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_OrgFolderDAL;
		}
    }
	public partial class T_OrgUserRelationBLL : BaseBLL<HCQ2_Model.T_OrgUserRelation>,IT_OrgUserRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_OrgUserRelationDAL;
		}
    }
	public partial class T_PageElementBLL : BaseBLL<HCQ2_Model.T_PageElement>,IT_PageElementBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PageElementDAL;
		}
    }
	public partial class T_PageFileBLL : BaseBLL<HCQ2_Model.T_PageFile>,IT_PageFileBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PageFileDAL;
		}
    }
	public partial class T_PageFolderBLL : BaseBLL<HCQ2_Model.T_PageFolder>,IT_PageFolderBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PageFolderDAL;
		}
    }
	public partial class T_PayAccountBLL : BaseBLL<HCQ2_Model.T_PayAccount>,IT_PayAccountBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PayAccountDAL;
		}
    }
	public partial class T_PerFuncRelationBLL : BaseBLL<HCQ2_Model.T_PerFuncRelation>,IT_PerFuncRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PerFuncRelationDAL;
		}
    }
	public partial class T_PermissConfigBLL : BaseBLL<HCQ2_Model.T_PermissConfig>,IT_PermissConfigBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PermissConfigDAL;
		}
    }
	public partial class T_PermissionsBLL : BaseBLL<HCQ2_Model.T_Permissions>,IT_PermissionsBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_PermissionsDAL;
		}
    }
	public partial class T_RoleBLL : BaseBLL<HCQ2_Model.T_Role>,IT_RoleBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_RoleDAL;
		}
    }
	public partial class T_RoleGroupRelationBLL : BaseBLL<HCQ2_Model.T_RoleGroupRelation>,IT_RoleGroupRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_RoleGroupRelationDAL;
		}
    }
	public partial class T_RolePermissRelationBLL : BaseBLL<HCQ2_Model.T_RolePermissRelation>,IT_RolePermissRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_RolePermissRelationDAL;
		}
    }
	public partial class T_SetMainPageBLL : BaseBLL<HCQ2_Model.T_SetMainPage>,IT_SetMainPageBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_SetMainPageDAL;
		}
    }
	public partial class T_SynchronousBLL : BaseBLL<HCQ2_Model.T_Synchronous>,IT_SynchronousBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_SynchronousDAL;
		}
    }
	public partial class T_SysModuleBLL : BaseBLL<HCQ2_Model.T_SysModule>,IT_SysModuleBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_SysModuleDAL;
		}
    }
	public partial class T_TableBLL : BaseBLL<HCQ2_Model.T_Table>,IT_TableBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_TableDAL;
		}
    }
	public partial class T_TableFieldBLL : BaseBLL<HCQ2_Model.T_TableField>,IT_TableFieldBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_TableFieldDAL;
		}
    }
	public partial class T_TodoListBLL : BaseBLL<HCQ2_Model.T_TodoList>,IT_TodoListBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_TodoListDAL;
		}
    }
	public partial class T_UserBLL : BaseBLL<HCQ2_Model.T_User>,IT_UserBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UserDAL;
		}
    }
	public partial class T_UserGroupBLL : BaseBLL<HCQ2_Model.T_UserGroup>,IT_UserGroupBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UserGroupDAL;
		}
    }
	public partial class T_UserGroupRelationBLL : BaseBLL<HCQ2_Model.T_UserGroupRelation>,IT_UserGroupRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UserGroupRelationDAL;
		}
    }
	public partial class T_UserRoleRelationBLL : BaseBLL<HCQ2_Model.T_UserRoleRelation>,IT_UserRoleRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UserRoleRelationDAL;
		}
    }
	public partial class T_UserUnitPersonRelationBLL : BaseBLL<HCQ2_Model.T_UserUnitPersonRelation>,IT_UserUnitPersonRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UserUnitPersonRelationDAL;
		}
    }
	public partial class T_UserUnitRelationBLL : BaseBLL<HCQ2_Model.T_UserUnitRelation>,IT_UserUnitRelationBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UserUnitRelationDAL;
		}
    }
	public partial class T_UseWorkerBLL : BaseBLL<HCQ2_Model.T_UseWorker>,IT_UseWorkerBLL
    {
		public override void SetDal()
		{
			Dal = DBSession.IT_UseWorkerDAL;
		}
    }
}