
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCQ2_IBLL;
namespace HCQ2_BLL
{
	public partial class BLLSession:IBLLSession
	{
	#region 01 业务接口
		IBane_CriminalRecordBLL iBane_CriminalRecordBll;
		public IBane_CriminalRecordBLL Bane_CriminalRecord
		{
			get
			{
				if(iBane_CriminalRecordBll==null)
					iBane_CriminalRecordBll=new Bane_CriminalRecordBLL();
				return iBane_CriminalRecordBll;
			}
			set
			{
				iBane_CriminalRecordBll=value;
			}
		}
		#endregion

		#region 02 业务接口
		IBane_FamilyRecordBLL iBane_FamilyRecordBll;
		public IBane_FamilyRecordBLL Bane_FamilyRecord
		{
			get
			{
				if(iBane_FamilyRecordBll==null)
					iBane_FamilyRecordBll=new Bane_FamilyRecordBLL();
				return iBane_FamilyRecordBll;
			}
			set
			{
				iBane_FamilyRecordBll=value;
			}
		}
		#endregion

		#region 03 业务接口
		IBane_HistoryScoreBLL iBane_HistoryScoreBll;
		public IBane_HistoryScoreBLL Bane_HistoryScore
		{
			get
			{
				if(iBane_HistoryScoreBll==null)
					iBane_HistoryScoreBll=new Bane_HistoryScoreBLL();
				return iBane_HistoryScoreBll;
			}
			set
			{
				iBane_HistoryScoreBll=value;
			}
		}
		#endregion

		#region 04 业务接口
		IBane_HistoryScoreDetialBLL iBane_HistoryScoreDetialBll;
		public IBane_HistoryScoreDetialBLL Bane_HistoryScoreDetial
		{
			get
			{
				if(iBane_HistoryScoreDetialBll==null)
					iBane_HistoryScoreDetialBll=new Bane_HistoryScoreDetialBLL();
				return iBane_HistoryScoreDetialBll;
			}
			set
			{
				iBane_HistoryScoreDetialBll=value;
			}
		}
		#endregion

		#region 05 业务接口
		IBane_IntegralScoreDetialBLL iBane_IntegralScoreDetialBll;
		public IBane_IntegralScoreDetialBLL Bane_IntegralScoreDetial
		{
			get
			{
				if(iBane_IntegralScoreDetialBll==null)
					iBane_IntegralScoreDetialBll=new Bane_IntegralScoreDetialBLL();
				return iBane_IntegralScoreDetialBll;
			}
			set
			{
				iBane_IntegralScoreDetialBll=value;
			}
		}
		#endregion

		#region 06 业务接口
		IBane_LogDetailBLL iBane_LogDetailBll;
		public IBane_LogDetailBLL Bane_LogDetail
		{
			get
			{
				if(iBane_LogDetailBll==null)
					iBane_LogDetailBll=new Bane_LogDetailBLL();
				return iBane_LogDetailBll;
			}
			set
			{
				iBane_LogDetailBll=value;
			}
		}
		#endregion

		#region 07 业务接口
		IBane_QuestionInfoBLL iBane_QuestionInfoBll;
		public IBane_QuestionInfoBLL Bane_QuestionInfo
		{
			get
			{
				if(iBane_QuestionInfoBll==null)
					iBane_QuestionInfoBll=new Bane_QuestionInfoBLL();
				return iBane_QuestionInfoBll;
			}
			set
			{
				iBane_QuestionInfoBll=value;
			}
		}
		#endregion

		#region 08 业务接口
		IBane_QuestionValueBLL iBane_QuestionValueBll;
		public IBane_QuestionValueBLL Bane_QuestionValue
		{
			get
			{
				if(iBane_QuestionValueBll==null)
					iBane_QuestionValueBll=new Bane_QuestionValueBLL();
				return iBane_QuestionValueBll;
			}
			set
			{
				iBane_QuestionValueBll=value;
			}
		}
		#endregion

		#region 09 业务接口
		IBane_RecoveryInfoBLL iBane_RecoveryInfoBll;
		public IBane_RecoveryInfoBLL Bane_RecoveryInfo
		{
			get
			{
				if(iBane_RecoveryInfoBll==null)
					iBane_RecoveryInfoBll=new Bane_RecoveryInfoBLL();
				return iBane_RecoveryInfoBll;
			}
			set
			{
				iBane_RecoveryInfoBll=value;
			}
		}
		#endregion

		#region 10 业务接口
		IBane_UrinalysisRecordBLL iBane_UrinalysisRecordBll;
		public IBane_UrinalysisRecordBLL Bane_UrinalysisRecord
		{
			get
			{
				if(iBane_UrinalysisRecordBll==null)
					iBane_UrinalysisRecordBll=new Bane_UrinalysisRecordBLL();
				return iBane_UrinalysisRecordBll;
			}
			set
			{
				iBane_UrinalysisRecordBll=value;
			}
		}
		#endregion

		#region 11 业务接口
		IBane_UrinalysisTimeSetBLL iBane_UrinalysisTimeSetBll;
		public IBane_UrinalysisTimeSetBLL Bane_UrinalysisTimeSet
		{
			get
			{
				if(iBane_UrinalysisTimeSetBll==null)
					iBane_UrinalysisTimeSetBll=new Bane_UrinalysisTimeSetBLL();
				return iBane_UrinalysisTimeSetBll;
			}
			set
			{
				iBane_UrinalysisTimeSetBll=value;
			}
		}
		#endregion

		#region 12 业务接口
		IBane_UserBLL iBane_UserBll;
		public IBane_UserBLL Bane_User
		{
			get
			{
				if(iBane_UserBll==null)
					iBane_UserBll=new Bane_UserBLL();
				return iBane_UserBll;
			}
			set
			{
				iBane_UserBll=value;
			}
		}
		#endregion

		#region 13 业务接口
		IBane_UserPermissRelationBLL iBane_UserPermissRelationBll;
		public IBane_UserPermissRelationBLL Bane_UserPermissRelation
		{
			get
			{
				if(iBane_UserPermissRelationBll==null)
					iBane_UserPermissRelationBll=new Bane_UserPermissRelationBLL();
				return iBane_UserPermissRelationBll;
			}
			set
			{
				iBane_UserPermissRelationBll=value;
			}
		}
		#endregion

		#region 14 业务接口
		IBMQ_DocumentBLL iBMQ_DocumentBll;
		public IBMQ_DocumentBLL BMQ_Document
		{
			get
			{
				if(iBMQ_DocumentBll==null)
					iBMQ_DocumentBll=new BMQ_DocumentBLL();
				return iBMQ_DocumentBll;
			}
			set
			{
				iBMQ_DocumentBll=value;
			}
		}
		#endregion

		#region 15 业务接口
		IT_AreaInfoBLL iT_AreaInfoBll;
		public IT_AreaInfoBLL T_AreaInfo
		{
			get
			{
				if(iT_AreaInfoBll==null)
					iT_AreaInfoBll=new T_AreaInfoBLL();
				return iT_AreaInfoBll;
			}
			set
			{
				iT_AreaInfoBll=value;
			}
		}
		#endregion

		#region 16 业务接口
		IT_AreaPermissRelationBLL iT_AreaPermissRelationBll;
		public IT_AreaPermissRelationBLL T_AreaPermissRelation
		{
			get
			{
				if(iT_AreaPermissRelationBll==null)
					iT_AreaPermissRelationBll=new T_AreaPermissRelationBLL();
				return iT_AreaPermissRelationBll;
			}
			set
			{
				iT_AreaPermissRelationBll=value;
			}
		}
		#endregion

		#region 17 业务接口
		IT_AskManagerBLL iT_AskManagerBll;
		public IT_AskManagerBLL T_AskManager
		{
			get
			{
				if(iT_AskManagerBll==null)
					iT_AskManagerBll=new T_AskManagerBLL();
				return iT_AskManagerBll;
			}
			set
			{
				iT_AskManagerBll=value;
			}
		}
		#endregion

		#region 18 业务接口
		IT_CheckGroupBLL iT_CheckGroupBll;
		public IT_CheckGroupBLL T_CheckGroup
		{
			get
			{
				if(iT_CheckGroupBll==null)
					iT_CheckGroupBll=new T_CheckGroupBLL();
				return iT_CheckGroupBll;
			}
			set
			{
				iT_CheckGroupBll=value;
			}
		}
		#endregion

		#region 19 业务接口
		IT_ComplaintsBLL iT_ComplaintsBll;
		public IT_ComplaintsBLL T_Complaints
		{
			get
			{
				if(iT_ComplaintsBll==null)
					iT_ComplaintsBll=new T_ComplaintsBLL();
				return iT_ComplaintsBll;
			}
			set
			{
				iT_ComplaintsBll=value;
			}
		}
		#endregion

		#region 20 业务接口
		IT_CompProInfoBLL iT_CompProInfoBll;
		public IT_CompProInfoBLL T_CompProInfo
		{
			get
			{
				if(iT_CompProInfoBll==null)
					iT_CompProInfoBll=new T_CompProInfoBLL();
				return iT_CompProInfoBll;
			}
			set
			{
				iT_CompProInfoBll=value;
			}
		}
		#endregion

		#region 21 业务接口
		IT_ContractBLL iT_ContractBll;
		public IT_ContractBLL T_Contract
		{
			get
			{
				if(iT_ContractBll==null)
					iT_ContractBll=new T_ContractBLL();
				return iT_ContractBll;
			}
			set
			{
				iT_ContractBll=value;
			}
		}
		#endregion

		#region 22 业务接口
		IT_DocFolderPermissRelationBLL iT_DocFolderPermissRelationBll;
		public IT_DocFolderPermissRelationBLL T_DocFolderPermissRelation
		{
			get
			{
				if(iT_DocFolderPermissRelationBll==null)
					iT_DocFolderPermissRelationBll=new T_DocFolderPermissRelationBLL();
				return iT_DocFolderPermissRelationBll;
			}
			set
			{
				iT_DocFolderPermissRelationBll=value;
			}
		}
		#endregion

		#region 23 业务接口
		IT_DocumentFolderBLL iT_DocumentFolderBll;
		public IT_DocumentFolderBLL T_DocumentFolder
		{
			get
			{
				if(iT_DocumentFolderBll==null)
					iT_DocumentFolderBll=new T_DocumentFolderBLL();
				return iT_DocumentFolderBll;
			}
			set
			{
				iT_DocumentFolderBll=value;
			}
		}
		#endregion

		#region 24 业务接口
		IT_DocumentFolderRelationBLL iT_DocumentFolderRelationBll;
		public IT_DocumentFolderRelationBLL T_DocumentFolderRelation
		{
			get
			{
				if(iT_DocumentFolderRelationBll==null)
					iT_DocumentFolderRelationBll=new T_DocumentFolderRelationBLL();
				return iT_DocumentFolderRelationBll;
			}
			set
			{
				iT_DocumentFolderRelationBll=value;
			}
		}
		#endregion

		#region 25 业务接口
		IT_DocumentInfoBLL iT_DocumentInfoBll;
		public IT_DocumentInfoBLL T_DocumentInfo
		{
			get
			{
				if(iT_DocumentInfoBll==null)
					iT_DocumentInfoBll=new T_DocumentInfoBLL();
				return iT_DocumentInfoBll;
			}
			set
			{
				iT_DocumentInfoBll=value;
			}
		}
		#endregion

		#region 26 业务接口
		IT_DocumentSetTypeBLL iT_DocumentSetTypeBll;
		public IT_DocumentSetTypeBLL T_DocumentSetType
		{
			get
			{
				if(iT_DocumentSetTypeBll==null)
					iT_DocumentSetTypeBll=new T_DocumentSetTypeBLL();
				return iT_DocumentSetTypeBll;
			}
			set
			{
				iT_DocumentSetTypeBll=value;
			}
		}
		#endregion

		#region 27 业务接口
		IT_ElementPermissRelationBLL iT_ElementPermissRelationBll;
		public IT_ElementPermissRelationBLL T_ElementPermissRelation
		{
			get
			{
				if(iT_ElementPermissRelationBll==null)
					iT_ElementPermissRelationBll=new T_ElementPermissRelationBLL();
				return iT_ElementPermissRelationBll;
			}
			set
			{
				iT_ElementPermissRelationBll=value;
			}
		}
		#endregion

		#region 28 业务接口
		IT_EnterDetailBLL iT_EnterDetailBll;
		public IT_EnterDetailBLL T_EnterDetail
		{
			get
			{
				if(iT_EnterDetailBll==null)
					iT_EnterDetailBll=new T_EnterDetailBLL();
				return iT_EnterDetailBll;
			}
			set
			{
				iT_EnterDetailBll=value;
			}
		}
		#endregion

		#region 29 业务接口
		IT_EquipmentBLL iT_EquipmentBll;
		public IT_EquipmentBLL T_Equipment
		{
			get
			{
				if(iT_EquipmentBll==null)
					iT_EquipmentBll=new T_EquipmentBLL();
				return iT_EquipmentBll;
			}
			set
			{
				iT_EquipmentBll=value;
			}
		}
		#endregion

		#region 30 业务接口
		IT_ExceptionLogBLL iT_ExceptionLogBll;
		public IT_ExceptionLogBLL T_ExceptionLog
		{
			get
			{
				if(iT_ExceptionLogBll==null)
					iT_ExceptionLogBll=new T_ExceptionLogBLL();
				return iT_ExceptionLogBll;
			}
			set
			{
				iT_ExceptionLogBll=value;
			}
		}
		#endregion

		#region 31 业务接口
		IT_FilePermissRelationBLL iT_FilePermissRelationBll;
		public IT_FilePermissRelationBLL T_FilePermissRelation
		{
			get
			{
				if(iT_FilePermissRelationBll==null)
					iT_FilePermissRelationBll=new T_FilePermissRelationBLL();
				return iT_FilePermissRelationBll;
			}
			set
			{
				iT_FilePermissRelationBll=value;
			}
		}
		#endregion

		#region 32 业务接口
		IT_FolderPermissRelationBLL iT_FolderPermissRelationBll;
		public IT_FolderPermissRelationBLL T_FolderPermissRelation
		{
			get
			{
				if(iT_FolderPermissRelationBll==null)
					iT_FolderPermissRelationBll=new T_FolderPermissRelationBLL();
				return iT_FolderPermissRelationBll;
			}
			set
			{
				iT_FolderPermissRelationBll=value;
			}
		}
		#endregion

		#region 33 业务接口
		IT_FunctionBLL iT_FunctionBll;
		public IT_FunctionBLL T_Function
		{
			get
			{
				if(iT_FunctionBll==null)
					iT_FunctionBll=new T_FunctionBLL();
				return iT_FunctionBll;
			}
			set
			{
				iT_FunctionBll=value;
			}
		}
		#endregion

		#region 34 业务接口
		IT_ImplementBLL iT_ImplementBll;
		public IT_ImplementBLL T_Implement
		{
			get
			{
				if(iT_ImplementBll==null)
					iT_ImplementBll=new T_ImplementBLL();
				return iT_ImplementBll;
			}
			set
			{
				iT_ImplementBll=value;
			}
		}
		#endregion

		#region 35 业务接口
		IT_InstructionsBLL iT_InstructionsBll;
		public IT_InstructionsBLL T_Instructions
		{
			get
			{
				if(iT_InstructionsBll==null)
					iT_InstructionsBll=new T_InstructionsBLL();
				return iT_InstructionsBll;
			}
			set
			{
				iT_InstructionsBll=value;
			}
		}
		#endregion

		#region 36 业务接口
		IT_ItemCodeBLL iT_ItemCodeBll;
		public IT_ItemCodeBLL T_ItemCode
		{
			get
			{
				if(iT_ItemCodeBll==null)
					iT_ItemCodeBll=new T_ItemCodeBLL();
				return iT_ItemCodeBll;
			}
			set
			{
				iT_ItemCodeBll=value;
			}
		}
		#endregion

		#region 37 业务接口
		IT_ItemCodeMenumBLL iT_ItemCodeMenumBll;
		public IT_ItemCodeMenumBLL T_ItemCodeMenum
		{
			get
			{
				if(iT_ItemCodeMenumBll==null)
					iT_ItemCodeMenumBll=new T_ItemCodeMenumBLL();
				return iT_ItemCodeMenumBll;
			}
			set
			{
				iT_ItemCodeMenumBll=value;
			}
		}
		#endregion

		#region 38 业务接口
		IT_JobResumeRelationBLL iT_JobResumeRelationBll;
		public IT_JobResumeRelationBLL T_JobResumeRelation
		{
			get
			{
				if(iT_JobResumeRelationBll==null)
					iT_JobResumeRelationBll=new T_JobResumeRelationBLL();
				return iT_JobResumeRelationBll;
			}
			set
			{
				iT_JobResumeRelationBll=value;
			}
		}
		#endregion

		#region 39 业务接口
		IT_LimitUserBLL iT_LimitUserBll;
		public IT_LimitUserBLL T_LimitUser
		{
			get
			{
				if(iT_LimitUserBll==null)
					iT_LimitUserBll=new T_LimitUserBLL();
				return iT_LimitUserBll;
			}
			set
			{
				iT_LimitUserBll=value;
			}
		}
		#endregion

		#region 40 业务接口
		IT_LoginBLL iT_LoginBll;
		public IT_LoginBLL T_Login
		{
			get
			{
				if(iT_LoginBll==null)
					iT_LoginBll=new T_LoginBLL();
				return iT_LoginBll;
			}
			set
			{
				iT_LoginBll=value;
			}
		}
		#endregion

		#region 41 业务接口
		IT_LogSetingBLL iT_LogSetingBll;
		public IT_LogSetingBLL T_LogSeting
		{
			get
			{
				if(iT_LogSetingBll==null)
					iT_LogSetingBll=new T_LogSetingBLL();
				return iT_LogSetingBll;
			}
			set
			{
				iT_LogSetingBll=value;
			}
		}
		#endregion

		#region 42 业务接口
		IT_LogSetingDetailBLL iT_LogSetingDetailBll;
		public IT_LogSetingDetailBLL T_LogSetingDetail
		{
			get
			{
				if(iT_LogSetingDetailBll==null)
					iT_LogSetingDetailBll=new T_LogSetingDetailBLL();
				return iT_LogSetingDetailBll;
			}
			set
			{
				iT_LogSetingDetailBll=value;
			}
		}
		#endregion

		#region 43 业务接口
		IT_MessageNoticeBLL iT_MessageNoticeBll;
		public IT_MessageNoticeBLL T_MessageNotice
		{
			get
			{
				if(iT_MessageNoticeBll==null)
					iT_MessageNoticeBll=new T_MessageNoticeBLL();
				return iT_MessageNoticeBll;
			}
			set
			{
				iT_MessageNoticeBll=value;
			}
		}
		#endregion

		#region 44 业务接口
		IT_ModulePermissRelationBLL iT_ModulePermissRelationBll;
		public IT_ModulePermissRelationBLL T_ModulePermissRelation
		{
			get
			{
				if(iT_ModulePermissRelationBll==null)
					iT_ModulePermissRelationBll=new T_ModulePermissRelationBLL();
				return iT_ModulePermissRelationBll;
			}
			set
			{
				iT_ModulePermissRelationBll=value;
			}
		}
		#endregion

		#region 45 业务接口
		IT_Org_UserBLL iT_Org_UserBll;
		public IT_Org_UserBLL T_Org_User
		{
			get
			{
				if(iT_Org_UserBll==null)
					iT_Org_UserBll=new T_Org_UserBLL();
				return iT_Org_UserBll;
			}
			set
			{
				iT_Org_UserBll=value;
			}
		}
		#endregion

		#region 46 业务接口
		IT_OrgFolderBLL iT_OrgFolderBll;
		public IT_OrgFolderBLL T_OrgFolder
		{
			get
			{
				if(iT_OrgFolderBll==null)
					iT_OrgFolderBll=new T_OrgFolderBLL();
				return iT_OrgFolderBll;
			}
			set
			{
				iT_OrgFolderBll=value;
			}
		}
		#endregion

		#region 47 业务接口
		IT_OrgUserRelationBLL iT_OrgUserRelationBll;
		public IT_OrgUserRelationBLL T_OrgUserRelation
		{
			get
			{
				if(iT_OrgUserRelationBll==null)
					iT_OrgUserRelationBll=new T_OrgUserRelationBLL();
				return iT_OrgUserRelationBll;
			}
			set
			{
				iT_OrgUserRelationBll=value;
			}
		}
		#endregion

		#region 48 业务接口
		IT_PageElementBLL iT_PageElementBll;
		public IT_PageElementBLL T_PageElement
		{
			get
			{
				if(iT_PageElementBll==null)
					iT_PageElementBll=new T_PageElementBLL();
				return iT_PageElementBll;
			}
			set
			{
				iT_PageElementBll=value;
			}
		}
		#endregion

		#region 49 业务接口
		IT_PageFileBLL iT_PageFileBll;
		public IT_PageFileBLL T_PageFile
		{
			get
			{
				if(iT_PageFileBll==null)
					iT_PageFileBll=new T_PageFileBLL();
				return iT_PageFileBll;
			}
			set
			{
				iT_PageFileBll=value;
			}
		}
		#endregion

		#region 50 业务接口
		IT_PageFolderBLL iT_PageFolderBll;
		public IT_PageFolderBLL T_PageFolder
		{
			get
			{
				if(iT_PageFolderBll==null)
					iT_PageFolderBll=new T_PageFolderBLL();
				return iT_PageFolderBll;
			}
			set
			{
				iT_PageFolderBll=value;
			}
		}
		#endregion

		#region 51 业务接口
		IT_PayAccountBLL iT_PayAccountBll;
		public IT_PayAccountBLL T_PayAccount
		{
			get
			{
				if(iT_PayAccountBll==null)
					iT_PayAccountBll=new T_PayAccountBLL();
				return iT_PayAccountBll;
			}
			set
			{
				iT_PayAccountBll=value;
			}
		}
		#endregion

		#region 52 业务接口
		IT_PerFuncRelationBLL iT_PerFuncRelationBll;
		public IT_PerFuncRelationBLL T_PerFuncRelation
		{
			get
			{
				if(iT_PerFuncRelationBll==null)
					iT_PerFuncRelationBll=new T_PerFuncRelationBLL();
				return iT_PerFuncRelationBll;
			}
			set
			{
				iT_PerFuncRelationBll=value;
			}
		}
		#endregion

		#region 53 业务接口
		IT_PermissConfigBLL iT_PermissConfigBll;
		public IT_PermissConfigBLL T_PermissConfig
		{
			get
			{
				if(iT_PermissConfigBll==null)
					iT_PermissConfigBll=new T_PermissConfigBLL();
				return iT_PermissConfigBll;
			}
			set
			{
				iT_PermissConfigBll=value;
			}
		}
		#endregion

		#region 54 业务接口
		IT_PermissionsBLL iT_PermissionsBll;
		public IT_PermissionsBLL T_Permissions
		{
			get
			{
				if(iT_PermissionsBll==null)
					iT_PermissionsBll=new T_PermissionsBLL();
				return iT_PermissionsBll;
			}
			set
			{
				iT_PermissionsBll=value;
			}
		}
		#endregion

		#region 55 业务接口
		IT_RoleBLL iT_RoleBll;
		public IT_RoleBLL T_Role
		{
			get
			{
				if(iT_RoleBll==null)
					iT_RoleBll=new T_RoleBLL();
				return iT_RoleBll;
			}
			set
			{
				iT_RoleBll=value;
			}
		}
		#endregion

		#region 56 业务接口
		IT_RoleGroupRelationBLL iT_RoleGroupRelationBll;
		public IT_RoleGroupRelationBLL T_RoleGroupRelation
		{
			get
			{
				if(iT_RoleGroupRelationBll==null)
					iT_RoleGroupRelationBll=new T_RoleGroupRelationBLL();
				return iT_RoleGroupRelationBll;
			}
			set
			{
				iT_RoleGroupRelationBll=value;
			}
		}
		#endregion

		#region 57 业务接口
		IT_RolePermissRelationBLL iT_RolePermissRelationBll;
		public IT_RolePermissRelationBLL T_RolePermissRelation
		{
			get
			{
				if(iT_RolePermissRelationBll==null)
					iT_RolePermissRelationBll=new T_RolePermissRelationBLL();
				return iT_RolePermissRelationBll;
			}
			set
			{
				iT_RolePermissRelationBll=value;
			}
		}
		#endregion

		#region 58 业务接口
		IT_SetMainPageBLL iT_SetMainPageBll;
		public IT_SetMainPageBLL T_SetMainPage
		{
			get
			{
				if(iT_SetMainPageBll==null)
					iT_SetMainPageBll=new T_SetMainPageBLL();
				return iT_SetMainPageBll;
			}
			set
			{
				iT_SetMainPageBll=value;
			}
		}
		#endregion

		#region 59 业务接口
		IT_SynchronousBLL iT_SynchronousBll;
		public IT_SynchronousBLL T_Synchronous
		{
			get
			{
				if(iT_SynchronousBll==null)
					iT_SynchronousBll=new T_SynchronousBLL();
				return iT_SynchronousBll;
			}
			set
			{
				iT_SynchronousBll=value;
			}
		}
		#endregion

		#region 60 业务接口
		IT_SysModuleBLL iT_SysModuleBll;
		public IT_SysModuleBLL T_SysModule
		{
			get
			{
				if(iT_SysModuleBll==null)
					iT_SysModuleBll=new T_SysModuleBLL();
				return iT_SysModuleBll;
			}
			set
			{
				iT_SysModuleBll=value;
			}
		}
		#endregion

		#region 61 业务接口
		IT_TableBLL iT_TableBll;
		public IT_TableBLL T_Table
		{
			get
			{
				if(iT_TableBll==null)
					iT_TableBll=new T_TableBLL();
				return iT_TableBll;
			}
			set
			{
				iT_TableBll=value;
			}
		}
		#endregion

		#region 62 业务接口
		IT_TableFieldBLL iT_TableFieldBll;
		public IT_TableFieldBLL T_TableField
		{
			get
			{
				if(iT_TableFieldBll==null)
					iT_TableFieldBll=new T_TableFieldBLL();
				return iT_TableFieldBll;
			}
			set
			{
				iT_TableFieldBll=value;
			}
		}
		#endregion

		#region 63 业务接口
		IT_TodoListBLL iT_TodoListBll;
		public IT_TodoListBLL T_TodoList
		{
			get
			{
				if(iT_TodoListBll==null)
					iT_TodoListBll=new T_TodoListBLL();
				return iT_TodoListBll;
			}
			set
			{
				iT_TodoListBll=value;
			}
		}
		#endregion

		#region 64 业务接口
		IT_UserBLL iT_UserBll;
		public IT_UserBLL T_User
		{
			get
			{
				if(iT_UserBll==null)
					iT_UserBll=new T_UserBLL();
				return iT_UserBll;
			}
			set
			{
				iT_UserBll=value;
			}
		}
		#endregion

		#region 65 业务接口
		IT_UserGroupBLL iT_UserGroupBll;
		public IT_UserGroupBLL T_UserGroup
		{
			get
			{
				if(iT_UserGroupBll==null)
					iT_UserGroupBll=new T_UserGroupBLL();
				return iT_UserGroupBll;
			}
			set
			{
				iT_UserGroupBll=value;
			}
		}
		#endregion

		#region 66 业务接口
		IT_UserGroupRelationBLL iT_UserGroupRelationBll;
		public IT_UserGroupRelationBLL T_UserGroupRelation
		{
			get
			{
				if(iT_UserGroupRelationBll==null)
					iT_UserGroupRelationBll=new T_UserGroupRelationBLL();
				return iT_UserGroupRelationBll;
			}
			set
			{
				iT_UserGroupRelationBll=value;
			}
		}
		#endregion

		#region 67 业务接口
		IT_UserRoleRelationBLL iT_UserRoleRelationBll;
		public IT_UserRoleRelationBLL T_UserRoleRelation
		{
			get
			{
				if(iT_UserRoleRelationBll==null)
					iT_UserRoleRelationBll=new T_UserRoleRelationBLL();
				return iT_UserRoleRelationBll;
			}
			set
			{
				iT_UserRoleRelationBll=value;
			}
		}
		#endregion

		#region 68 业务接口
		IT_UserUnitPersonRelationBLL iT_UserUnitPersonRelationBll;
		public IT_UserUnitPersonRelationBLL T_UserUnitPersonRelation
		{
			get
			{
				if(iT_UserUnitPersonRelationBll==null)
					iT_UserUnitPersonRelationBll=new T_UserUnitPersonRelationBLL();
				return iT_UserUnitPersonRelationBll;
			}
			set
			{
				iT_UserUnitPersonRelationBll=value;
			}
		}
		#endregion

		#region 69 业务接口
		IT_UserUnitRelationBLL iT_UserUnitRelationBll;
		public IT_UserUnitRelationBLL T_UserUnitRelation
		{
			get
			{
				if(iT_UserUnitRelationBll==null)
					iT_UserUnitRelationBll=new T_UserUnitRelationBLL();
				return iT_UserUnitRelationBll;
			}
			set
			{
				iT_UserUnitRelationBll=value;
			}
		}
		#endregion

		#region 70 业务接口
		IT_UseWorkerBLL iT_UseWorkerBll;
		public IT_UseWorkerBLL T_UseWorker
		{
			get
			{
				if(iT_UseWorkerBll==null)
					iT_UseWorkerBll=new T_UseWorkerBLL();
				return iT_UseWorkerBll;
			}
			set
			{
				iT_UseWorkerBll=value;
			}
		}
		#endregion

		}
}