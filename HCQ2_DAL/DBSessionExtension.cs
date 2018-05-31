
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCQ2_IDAL;
namespace HCQ2_DAL_MSSQL
{
	public partial class DBSession
	{
		#region 01 数据接口
		IBane_CriminalRecordDAL iBane_CriminalRecordDAL;
		public IBane_CriminalRecordDAL IBane_CriminalRecordDAL
		{
			get
			{
				if(iBane_CriminalRecordDAL==null)
					iBane_CriminalRecordDAL=new Bane_CriminalRecordDAL();
				return iBane_CriminalRecordDAL;
			}
			set
			{
				iBane_CriminalRecordDAL=value;
			}
		}
		#endregion
			#region 02 数据接口
		IBane_FamilyRecordDAL iBane_FamilyRecordDAL;
		public IBane_FamilyRecordDAL IBane_FamilyRecordDAL
		{
			get
			{
				if(iBane_FamilyRecordDAL==null)
					iBane_FamilyRecordDAL=new Bane_FamilyRecordDAL();
				return iBane_FamilyRecordDAL;
			}
			set
			{
				iBane_FamilyRecordDAL=value;
			}
		}
		#endregion
			#region 03 数据接口
		IBane_HistoryScoreDAL iBane_HistoryScoreDAL;
		public IBane_HistoryScoreDAL IBane_HistoryScoreDAL
		{
			get
			{
				if(iBane_HistoryScoreDAL==null)
					iBane_HistoryScoreDAL=new Bane_HistoryScoreDAL();
				return iBane_HistoryScoreDAL;
			}
			set
			{
				iBane_HistoryScoreDAL=value;
			}
		}
		#endregion
			#region 04 数据接口
		IBane_HistoryScoreDetialDAL iBane_HistoryScoreDetialDAL;
		public IBane_HistoryScoreDetialDAL IBane_HistoryScoreDetialDAL
		{
			get
			{
				if(iBane_HistoryScoreDetialDAL==null)
					iBane_HistoryScoreDetialDAL=new Bane_HistoryScoreDetialDAL();
				return iBane_HistoryScoreDetialDAL;
			}
			set
			{
				iBane_HistoryScoreDetialDAL=value;
			}
		}
		#endregion
			#region 05 数据接口
		IBane_IntegralScoreDetialDAL iBane_IntegralScoreDetialDAL;
		public IBane_IntegralScoreDetialDAL IBane_IntegralScoreDetialDAL
		{
			get
			{
				if(iBane_IntegralScoreDetialDAL==null)
					iBane_IntegralScoreDetialDAL=new Bane_IntegralScoreDetialDAL();
				return iBane_IntegralScoreDetialDAL;
			}
			set
			{
				iBane_IntegralScoreDetialDAL=value;
			}
		}
		#endregion
			#region 06 数据接口
		IBane_LogDetailDAL iBane_LogDetailDAL;
		public IBane_LogDetailDAL IBane_LogDetailDAL
		{
			get
			{
				if(iBane_LogDetailDAL==null)
					iBane_LogDetailDAL=new Bane_LogDetailDAL();
				return iBane_LogDetailDAL;
			}
			set
			{
				iBane_LogDetailDAL=value;
			}
		}
		#endregion
			#region 07 数据接口
		IBane_QuestionInfoDAL iBane_QuestionInfoDAL;
		public IBane_QuestionInfoDAL IBane_QuestionInfoDAL
		{
			get
			{
				if(iBane_QuestionInfoDAL==null)
					iBane_QuestionInfoDAL=new Bane_QuestionInfoDAL();
				return iBane_QuestionInfoDAL;
			}
			set
			{
				iBane_QuestionInfoDAL=value;
			}
		}
		#endregion
			#region 08 数据接口
		IBane_QuestionValueDAL iBane_QuestionValueDAL;
		public IBane_QuestionValueDAL IBane_QuestionValueDAL
		{
			get
			{
				if(iBane_QuestionValueDAL==null)
					iBane_QuestionValueDAL=new Bane_QuestionValueDAL();
				return iBane_QuestionValueDAL;
			}
			set
			{
				iBane_QuestionValueDAL=value;
			}
		}
		#endregion
			#region 09 数据接口
		IBane_RecoveryInfoDAL iBane_RecoveryInfoDAL;
		public IBane_RecoveryInfoDAL IBane_RecoveryInfoDAL
		{
			get
			{
				if(iBane_RecoveryInfoDAL==null)
					iBane_RecoveryInfoDAL=new Bane_RecoveryInfoDAL();
				return iBane_RecoveryInfoDAL;
			}
			set
			{
				iBane_RecoveryInfoDAL=value;
			}
		}
		#endregion
			#region 10 数据接口
		IBane_UrinalysisRecordDAL iBane_UrinalysisRecordDAL;
		public IBane_UrinalysisRecordDAL IBane_UrinalysisRecordDAL
		{
			get
			{
				if(iBane_UrinalysisRecordDAL==null)
					iBane_UrinalysisRecordDAL=new Bane_UrinalysisRecordDAL();
				return iBane_UrinalysisRecordDAL;
			}
			set
			{
				iBane_UrinalysisRecordDAL=value;
			}
		}
		#endregion
			#region 11 数据接口
		IBane_UrinalysisTimeSetDAL iBane_UrinalysisTimeSetDAL;
		public IBane_UrinalysisTimeSetDAL IBane_UrinalysisTimeSetDAL
		{
			get
			{
				if(iBane_UrinalysisTimeSetDAL==null)
					iBane_UrinalysisTimeSetDAL=new Bane_UrinalysisTimeSetDAL();
				return iBane_UrinalysisTimeSetDAL;
			}
			set
			{
				iBane_UrinalysisTimeSetDAL=value;
			}
		}
		#endregion
			#region 12 数据接口
		IBane_UserDAL iBane_UserDAL;
		public IBane_UserDAL IBane_UserDAL
		{
			get
			{
				if(iBane_UserDAL==null)
					iBane_UserDAL=new Bane_UserDAL();
				return iBane_UserDAL;
			}
			set
			{
				iBane_UserDAL=value;
			}
		}
		#endregion
			#region 13 数据接口
		IBane_UserPermissRelationDAL iBane_UserPermissRelationDAL;
		public IBane_UserPermissRelationDAL IBane_UserPermissRelationDAL
		{
			get
			{
				if(iBane_UserPermissRelationDAL==null)
					iBane_UserPermissRelationDAL=new Bane_UserPermissRelationDAL();
				return iBane_UserPermissRelationDAL;
			}
			set
			{
				iBane_UserPermissRelationDAL=value;
			}
		}
		#endregion
			#region 14 数据接口
		IBMQ_DocumentDAL iBMQ_DocumentDAL;
		public IBMQ_DocumentDAL IBMQ_DocumentDAL
		{
			get
			{
				if(iBMQ_DocumentDAL==null)
					iBMQ_DocumentDAL=new BMQ_DocumentDAL();
				return iBMQ_DocumentDAL;
			}
			set
			{
				iBMQ_DocumentDAL=value;
			}
		}
		#endregion
			#region 15 数据接口
		IT_AreaInfoDAL iT_AreaInfoDAL;
		public IT_AreaInfoDAL IT_AreaInfoDAL
		{
			get
			{
				if(iT_AreaInfoDAL==null)
					iT_AreaInfoDAL=new T_AreaInfoDAL();
				return iT_AreaInfoDAL;
			}
			set
			{
				iT_AreaInfoDAL=value;
			}
		}
		#endregion
			#region 16 数据接口
		IT_AreaPermissRelationDAL iT_AreaPermissRelationDAL;
		public IT_AreaPermissRelationDAL IT_AreaPermissRelationDAL
		{
			get
			{
				if(iT_AreaPermissRelationDAL==null)
					iT_AreaPermissRelationDAL=new T_AreaPermissRelationDAL();
				return iT_AreaPermissRelationDAL;
			}
			set
			{
				iT_AreaPermissRelationDAL=value;
			}
		}
		#endregion
			#region 17 数据接口
		IT_AskManagerDAL iT_AskManagerDAL;
		public IT_AskManagerDAL IT_AskManagerDAL
		{
			get
			{
				if(iT_AskManagerDAL==null)
					iT_AskManagerDAL=new T_AskManagerDAL();
				return iT_AskManagerDAL;
			}
			set
			{
				iT_AskManagerDAL=value;
			}
		}
		#endregion
			#region 18 数据接口
		IT_CheckGroupDAL iT_CheckGroupDAL;
		public IT_CheckGroupDAL IT_CheckGroupDAL
		{
			get
			{
				if(iT_CheckGroupDAL==null)
					iT_CheckGroupDAL=new T_CheckGroupDAL();
				return iT_CheckGroupDAL;
			}
			set
			{
				iT_CheckGroupDAL=value;
			}
		}
		#endregion
			#region 19 数据接口
		IT_ComplaintsDAL iT_ComplaintsDAL;
		public IT_ComplaintsDAL IT_ComplaintsDAL
		{
			get
			{
				if(iT_ComplaintsDAL==null)
					iT_ComplaintsDAL=new T_ComplaintsDAL();
				return iT_ComplaintsDAL;
			}
			set
			{
				iT_ComplaintsDAL=value;
			}
		}
		#endregion
			#region 20 数据接口
		IT_CompProInfoDAL iT_CompProInfoDAL;
		public IT_CompProInfoDAL IT_CompProInfoDAL
		{
			get
			{
				if(iT_CompProInfoDAL==null)
					iT_CompProInfoDAL=new T_CompProInfoDAL();
				return iT_CompProInfoDAL;
			}
			set
			{
				iT_CompProInfoDAL=value;
			}
		}
		#endregion
			#region 21 数据接口
		IT_ContractDAL iT_ContractDAL;
		public IT_ContractDAL IT_ContractDAL
		{
			get
			{
				if(iT_ContractDAL==null)
					iT_ContractDAL=new T_ContractDAL();
				return iT_ContractDAL;
			}
			set
			{
				iT_ContractDAL=value;
			}
		}
		#endregion
			#region 22 数据接口
		IT_DocFolderPermissRelationDAL iT_DocFolderPermissRelationDAL;
		public IT_DocFolderPermissRelationDAL IT_DocFolderPermissRelationDAL
		{
			get
			{
				if(iT_DocFolderPermissRelationDAL==null)
					iT_DocFolderPermissRelationDAL=new T_DocFolderPermissRelationDAL();
				return iT_DocFolderPermissRelationDAL;
			}
			set
			{
				iT_DocFolderPermissRelationDAL=value;
			}
		}
		#endregion
			#region 23 数据接口
		IT_DocumentFolderDAL iT_DocumentFolderDAL;
		public IT_DocumentFolderDAL IT_DocumentFolderDAL
		{
			get
			{
				if(iT_DocumentFolderDAL==null)
					iT_DocumentFolderDAL=new T_DocumentFolderDAL();
				return iT_DocumentFolderDAL;
			}
			set
			{
				iT_DocumentFolderDAL=value;
			}
		}
		#endregion
			#region 24 数据接口
		IT_DocumentFolderRelationDAL iT_DocumentFolderRelationDAL;
		public IT_DocumentFolderRelationDAL IT_DocumentFolderRelationDAL
		{
			get
			{
				if(iT_DocumentFolderRelationDAL==null)
					iT_DocumentFolderRelationDAL=new T_DocumentFolderRelationDAL();
				return iT_DocumentFolderRelationDAL;
			}
			set
			{
				iT_DocumentFolderRelationDAL=value;
			}
		}
		#endregion
			#region 25 数据接口
		IT_DocumentInfoDAL iT_DocumentInfoDAL;
		public IT_DocumentInfoDAL IT_DocumentInfoDAL
		{
			get
			{
				if(iT_DocumentInfoDAL==null)
					iT_DocumentInfoDAL=new T_DocumentInfoDAL();
				return iT_DocumentInfoDAL;
			}
			set
			{
				iT_DocumentInfoDAL=value;
			}
		}
		#endregion
			#region 26 数据接口
		IT_DocumentSetTypeDAL iT_DocumentSetTypeDAL;
		public IT_DocumentSetTypeDAL IT_DocumentSetTypeDAL
		{
			get
			{
				if(iT_DocumentSetTypeDAL==null)
					iT_DocumentSetTypeDAL=new T_DocumentSetTypeDAL();
				return iT_DocumentSetTypeDAL;
			}
			set
			{
				iT_DocumentSetTypeDAL=value;
			}
		}
		#endregion
			#region 27 数据接口
		IT_ElementPermissRelationDAL iT_ElementPermissRelationDAL;
		public IT_ElementPermissRelationDAL IT_ElementPermissRelationDAL
		{
			get
			{
				if(iT_ElementPermissRelationDAL==null)
					iT_ElementPermissRelationDAL=new T_ElementPermissRelationDAL();
				return iT_ElementPermissRelationDAL;
			}
			set
			{
				iT_ElementPermissRelationDAL=value;
			}
		}
		#endregion
			#region 28 数据接口
		IT_EnterDetailDAL iT_EnterDetailDAL;
		public IT_EnterDetailDAL IT_EnterDetailDAL
		{
			get
			{
				if(iT_EnterDetailDAL==null)
					iT_EnterDetailDAL=new T_EnterDetailDAL();
				return iT_EnterDetailDAL;
			}
			set
			{
				iT_EnterDetailDAL=value;
			}
		}
		#endregion
			#region 29 数据接口
		IT_EquipmentDAL iT_EquipmentDAL;
		public IT_EquipmentDAL IT_EquipmentDAL
		{
			get
			{
				if(iT_EquipmentDAL==null)
					iT_EquipmentDAL=new T_EquipmentDAL();
				return iT_EquipmentDAL;
			}
			set
			{
				iT_EquipmentDAL=value;
			}
		}
		#endregion
			#region 30 数据接口
		IT_ExceptionLogDAL iT_ExceptionLogDAL;
		public IT_ExceptionLogDAL IT_ExceptionLogDAL
		{
			get
			{
				if(iT_ExceptionLogDAL==null)
					iT_ExceptionLogDAL=new T_ExceptionLogDAL();
				return iT_ExceptionLogDAL;
			}
			set
			{
				iT_ExceptionLogDAL=value;
			}
		}
		#endregion
			#region 31 数据接口
		IT_FilePermissRelationDAL iT_FilePermissRelationDAL;
		public IT_FilePermissRelationDAL IT_FilePermissRelationDAL
		{
			get
			{
				if(iT_FilePermissRelationDAL==null)
					iT_FilePermissRelationDAL=new T_FilePermissRelationDAL();
				return iT_FilePermissRelationDAL;
			}
			set
			{
				iT_FilePermissRelationDAL=value;
			}
		}
		#endregion
			#region 32 数据接口
		IT_FolderPermissRelationDAL iT_FolderPermissRelationDAL;
		public IT_FolderPermissRelationDAL IT_FolderPermissRelationDAL
		{
			get
			{
				if(iT_FolderPermissRelationDAL==null)
					iT_FolderPermissRelationDAL=new T_FolderPermissRelationDAL();
				return iT_FolderPermissRelationDAL;
			}
			set
			{
				iT_FolderPermissRelationDAL=value;
			}
		}
		#endregion
			#region 33 数据接口
		IT_FunctionDAL iT_FunctionDAL;
		public IT_FunctionDAL IT_FunctionDAL
		{
			get
			{
				if(iT_FunctionDAL==null)
					iT_FunctionDAL=new T_FunctionDAL();
				return iT_FunctionDAL;
			}
			set
			{
				iT_FunctionDAL=value;
			}
		}
		#endregion
			#region 34 数据接口
		IT_ImplementDAL iT_ImplementDAL;
		public IT_ImplementDAL IT_ImplementDAL
		{
			get
			{
				if(iT_ImplementDAL==null)
					iT_ImplementDAL=new T_ImplementDAL();
				return iT_ImplementDAL;
			}
			set
			{
				iT_ImplementDAL=value;
			}
		}
		#endregion
			#region 35 数据接口
		IT_InstructionsDAL iT_InstructionsDAL;
		public IT_InstructionsDAL IT_InstructionsDAL
		{
			get
			{
				if(iT_InstructionsDAL==null)
					iT_InstructionsDAL=new T_InstructionsDAL();
				return iT_InstructionsDAL;
			}
			set
			{
				iT_InstructionsDAL=value;
			}
		}
		#endregion
			#region 36 数据接口
		IT_ItemCodeDAL iT_ItemCodeDAL;
		public IT_ItemCodeDAL IT_ItemCodeDAL
		{
			get
			{
				if(iT_ItemCodeDAL==null)
					iT_ItemCodeDAL=new T_ItemCodeDAL();
				return iT_ItemCodeDAL;
			}
			set
			{
				iT_ItemCodeDAL=value;
			}
		}
		#endregion
			#region 37 数据接口
		IT_ItemCodeMenumDAL iT_ItemCodeMenumDAL;
		public IT_ItemCodeMenumDAL IT_ItemCodeMenumDAL
		{
			get
			{
				if(iT_ItemCodeMenumDAL==null)
					iT_ItemCodeMenumDAL=new T_ItemCodeMenumDAL();
				return iT_ItemCodeMenumDAL;
			}
			set
			{
				iT_ItemCodeMenumDAL=value;
			}
		}
		#endregion
			#region 38 数据接口
		IT_JobResumeRelationDAL iT_JobResumeRelationDAL;
		public IT_JobResumeRelationDAL IT_JobResumeRelationDAL
		{
			get
			{
				if(iT_JobResumeRelationDAL==null)
					iT_JobResumeRelationDAL=new T_JobResumeRelationDAL();
				return iT_JobResumeRelationDAL;
			}
			set
			{
				iT_JobResumeRelationDAL=value;
			}
		}
		#endregion
			#region 39 数据接口
		IT_LimitUserDAL iT_LimitUserDAL;
		public IT_LimitUserDAL IT_LimitUserDAL
		{
			get
			{
				if(iT_LimitUserDAL==null)
					iT_LimitUserDAL=new T_LimitUserDAL();
				return iT_LimitUserDAL;
			}
			set
			{
				iT_LimitUserDAL=value;
			}
		}
		#endregion
			#region 40 数据接口
		IT_LoginDAL iT_LoginDAL;
		public IT_LoginDAL IT_LoginDAL
		{
			get
			{
				if(iT_LoginDAL==null)
					iT_LoginDAL=new T_LoginDAL();
				return iT_LoginDAL;
			}
			set
			{
				iT_LoginDAL=value;
			}
		}
		#endregion
			#region 41 数据接口
		IT_LogSetingDAL iT_LogSetingDAL;
		public IT_LogSetingDAL IT_LogSetingDAL
		{
			get
			{
				if(iT_LogSetingDAL==null)
					iT_LogSetingDAL=new T_LogSetingDAL();
				return iT_LogSetingDAL;
			}
			set
			{
				iT_LogSetingDAL=value;
			}
		}
		#endregion
			#region 42 数据接口
		IT_LogSetingDetailDAL iT_LogSetingDetailDAL;
		public IT_LogSetingDetailDAL IT_LogSetingDetailDAL
		{
			get
			{
				if(iT_LogSetingDetailDAL==null)
					iT_LogSetingDetailDAL=new T_LogSetingDetailDAL();
				return iT_LogSetingDetailDAL;
			}
			set
			{
				iT_LogSetingDetailDAL=value;
			}
		}
		#endregion
			#region 43 数据接口
		IT_MessageNoticeDAL iT_MessageNoticeDAL;
		public IT_MessageNoticeDAL IT_MessageNoticeDAL
		{
			get
			{
				if(iT_MessageNoticeDAL==null)
					iT_MessageNoticeDAL=new T_MessageNoticeDAL();
				return iT_MessageNoticeDAL;
			}
			set
			{
				iT_MessageNoticeDAL=value;
			}
		}
		#endregion
			#region 44 数据接口
		IT_ModulePermissRelationDAL iT_ModulePermissRelationDAL;
		public IT_ModulePermissRelationDAL IT_ModulePermissRelationDAL
		{
			get
			{
				if(iT_ModulePermissRelationDAL==null)
					iT_ModulePermissRelationDAL=new T_ModulePermissRelationDAL();
				return iT_ModulePermissRelationDAL;
			}
			set
			{
				iT_ModulePermissRelationDAL=value;
			}
		}
		#endregion
			#region 45 数据接口
		IT_Org_UserDAL iT_Org_UserDAL;
		public IT_Org_UserDAL IT_Org_UserDAL
		{
			get
			{
				if(iT_Org_UserDAL==null)
					iT_Org_UserDAL=new T_Org_UserDAL();
				return iT_Org_UserDAL;
			}
			set
			{
				iT_Org_UserDAL=value;
			}
		}
		#endregion
			#region 46 数据接口
		IT_OrgFolderDAL iT_OrgFolderDAL;
		public IT_OrgFolderDAL IT_OrgFolderDAL
		{
			get
			{
				if(iT_OrgFolderDAL==null)
					iT_OrgFolderDAL=new T_OrgFolderDAL();
				return iT_OrgFolderDAL;
			}
			set
			{
				iT_OrgFolderDAL=value;
			}
		}
		#endregion
			#region 47 数据接口
		IT_OrgUserRelationDAL iT_OrgUserRelationDAL;
		public IT_OrgUserRelationDAL IT_OrgUserRelationDAL
		{
			get
			{
				if(iT_OrgUserRelationDAL==null)
					iT_OrgUserRelationDAL=new T_OrgUserRelationDAL();
				return iT_OrgUserRelationDAL;
			}
			set
			{
				iT_OrgUserRelationDAL=value;
			}
		}
		#endregion
			#region 48 数据接口
		IT_PageElementDAL iT_PageElementDAL;
		public IT_PageElementDAL IT_PageElementDAL
		{
			get
			{
				if(iT_PageElementDAL==null)
					iT_PageElementDAL=new T_PageElementDAL();
				return iT_PageElementDAL;
			}
			set
			{
				iT_PageElementDAL=value;
			}
		}
		#endregion
			#region 49 数据接口
		IT_PageFileDAL iT_PageFileDAL;
		public IT_PageFileDAL IT_PageFileDAL
		{
			get
			{
				if(iT_PageFileDAL==null)
					iT_PageFileDAL=new T_PageFileDAL();
				return iT_PageFileDAL;
			}
			set
			{
				iT_PageFileDAL=value;
			}
		}
		#endregion
			#region 50 数据接口
		IT_PageFolderDAL iT_PageFolderDAL;
		public IT_PageFolderDAL IT_PageFolderDAL
		{
			get
			{
				if(iT_PageFolderDAL==null)
					iT_PageFolderDAL=new T_PageFolderDAL();
				return iT_PageFolderDAL;
			}
			set
			{
				iT_PageFolderDAL=value;
			}
		}
		#endregion
			#region 51 数据接口
		IT_PayAccountDAL iT_PayAccountDAL;
		public IT_PayAccountDAL IT_PayAccountDAL
		{
			get
			{
				if(iT_PayAccountDAL==null)
					iT_PayAccountDAL=new T_PayAccountDAL();
				return iT_PayAccountDAL;
			}
			set
			{
				iT_PayAccountDAL=value;
			}
		}
		#endregion
			#region 52 数据接口
		IT_PerFuncRelationDAL iT_PerFuncRelationDAL;
		public IT_PerFuncRelationDAL IT_PerFuncRelationDAL
		{
			get
			{
				if(iT_PerFuncRelationDAL==null)
					iT_PerFuncRelationDAL=new T_PerFuncRelationDAL();
				return iT_PerFuncRelationDAL;
			}
			set
			{
				iT_PerFuncRelationDAL=value;
			}
		}
		#endregion
			#region 53 数据接口
		IT_PermissConfigDAL iT_PermissConfigDAL;
		public IT_PermissConfigDAL IT_PermissConfigDAL
		{
			get
			{
				if(iT_PermissConfigDAL==null)
					iT_PermissConfigDAL=new T_PermissConfigDAL();
				return iT_PermissConfigDAL;
			}
			set
			{
				iT_PermissConfigDAL=value;
			}
		}
		#endregion
			#region 54 数据接口
		IT_PermissionsDAL iT_PermissionsDAL;
		public IT_PermissionsDAL IT_PermissionsDAL
		{
			get
			{
				if(iT_PermissionsDAL==null)
					iT_PermissionsDAL=new T_PermissionsDAL();
				return iT_PermissionsDAL;
			}
			set
			{
				iT_PermissionsDAL=value;
			}
		}
		#endregion
			#region 55 数据接口
		IT_RoleDAL iT_RoleDAL;
		public IT_RoleDAL IT_RoleDAL
		{
			get
			{
				if(iT_RoleDAL==null)
					iT_RoleDAL=new T_RoleDAL();
				return iT_RoleDAL;
			}
			set
			{
				iT_RoleDAL=value;
			}
		}
		#endregion
			#region 56 数据接口
		IT_RoleGroupRelationDAL iT_RoleGroupRelationDAL;
		public IT_RoleGroupRelationDAL IT_RoleGroupRelationDAL
		{
			get
			{
				if(iT_RoleGroupRelationDAL==null)
					iT_RoleGroupRelationDAL=new T_RoleGroupRelationDAL();
				return iT_RoleGroupRelationDAL;
			}
			set
			{
				iT_RoleGroupRelationDAL=value;
			}
		}
		#endregion
			#region 57 数据接口
		IT_RolePermissRelationDAL iT_RolePermissRelationDAL;
		public IT_RolePermissRelationDAL IT_RolePermissRelationDAL
		{
			get
			{
				if(iT_RolePermissRelationDAL==null)
					iT_RolePermissRelationDAL=new T_RolePermissRelationDAL();
				return iT_RolePermissRelationDAL;
			}
			set
			{
				iT_RolePermissRelationDAL=value;
			}
		}
		#endregion
			#region 58 数据接口
		IT_SetMainPageDAL iT_SetMainPageDAL;
		public IT_SetMainPageDAL IT_SetMainPageDAL
		{
			get
			{
				if(iT_SetMainPageDAL==null)
					iT_SetMainPageDAL=new T_SetMainPageDAL();
				return iT_SetMainPageDAL;
			}
			set
			{
				iT_SetMainPageDAL=value;
			}
		}
		#endregion
			#region 59 数据接口
		IT_SynchronousDAL iT_SynchronousDAL;
		public IT_SynchronousDAL IT_SynchronousDAL
		{
			get
			{
				if(iT_SynchronousDAL==null)
					iT_SynchronousDAL=new T_SynchronousDAL();
				return iT_SynchronousDAL;
			}
			set
			{
				iT_SynchronousDAL=value;
			}
		}
		#endregion
			#region 60 数据接口
		IT_SysModuleDAL iT_SysModuleDAL;
		public IT_SysModuleDAL IT_SysModuleDAL
		{
			get
			{
				if(iT_SysModuleDAL==null)
					iT_SysModuleDAL=new T_SysModuleDAL();
				return iT_SysModuleDAL;
			}
			set
			{
				iT_SysModuleDAL=value;
			}
		}
		#endregion
			#region 61 数据接口
		IT_TableDAL iT_TableDAL;
		public IT_TableDAL IT_TableDAL
		{
			get
			{
				if(iT_TableDAL==null)
					iT_TableDAL=new T_TableDAL();
				return iT_TableDAL;
			}
			set
			{
				iT_TableDAL=value;
			}
		}
		#endregion
			#region 62 数据接口
		IT_TableFieldDAL iT_TableFieldDAL;
		public IT_TableFieldDAL IT_TableFieldDAL
		{
			get
			{
				if(iT_TableFieldDAL==null)
					iT_TableFieldDAL=new T_TableFieldDAL();
				return iT_TableFieldDAL;
			}
			set
			{
				iT_TableFieldDAL=value;
			}
		}
		#endregion
			#region 63 数据接口
		IT_TodoListDAL iT_TodoListDAL;
		public IT_TodoListDAL IT_TodoListDAL
		{
			get
			{
				if(iT_TodoListDAL==null)
					iT_TodoListDAL=new T_TodoListDAL();
				return iT_TodoListDAL;
			}
			set
			{
				iT_TodoListDAL=value;
			}
		}
		#endregion
			#region 64 数据接口
		IT_UserDAL iT_UserDAL;
		public IT_UserDAL IT_UserDAL
		{
			get
			{
				if(iT_UserDAL==null)
					iT_UserDAL=new T_UserDAL();
				return iT_UserDAL;
			}
			set
			{
				iT_UserDAL=value;
			}
		}
		#endregion
			#region 65 数据接口
		IT_UserGroupDAL iT_UserGroupDAL;
		public IT_UserGroupDAL IT_UserGroupDAL
		{
			get
			{
				if(iT_UserGroupDAL==null)
					iT_UserGroupDAL=new T_UserGroupDAL();
				return iT_UserGroupDAL;
			}
			set
			{
				iT_UserGroupDAL=value;
			}
		}
		#endregion
			#region 66 数据接口
		IT_UserGroupRelationDAL iT_UserGroupRelationDAL;
		public IT_UserGroupRelationDAL IT_UserGroupRelationDAL
		{
			get
			{
				if(iT_UserGroupRelationDAL==null)
					iT_UserGroupRelationDAL=new T_UserGroupRelationDAL();
				return iT_UserGroupRelationDAL;
			}
			set
			{
				iT_UserGroupRelationDAL=value;
			}
		}
		#endregion
			#region 67 数据接口
		IT_UserRoleRelationDAL iT_UserRoleRelationDAL;
		public IT_UserRoleRelationDAL IT_UserRoleRelationDAL
		{
			get
			{
				if(iT_UserRoleRelationDAL==null)
					iT_UserRoleRelationDAL=new T_UserRoleRelationDAL();
				return iT_UserRoleRelationDAL;
			}
			set
			{
				iT_UserRoleRelationDAL=value;
			}
		}
		#endregion
			#region 68 数据接口
		IT_UserUnitPersonRelationDAL iT_UserUnitPersonRelationDAL;
		public IT_UserUnitPersonRelationDAL IT_UserUnitPersonRelationDAL
		{
			get
			{
				if(iT_UserUnitPersonRelationDAL==null)
					iT_UserUnitPersonRelationDAL=new T_UserUnitPersonRelationDAL();
				return iT_UserUnitPersonRelationDAL;
			}
			set
			{
				iT_UserUnitPersonRelationDAL=value;
			}
		}
		#endregion
			#region 69 数据接口
		IT_UserUnitRelationDAL iT_UserUnitRelationDAL;
		public IT_UserUnitRelationDAL IT_UserUnitRelationDAL
		{
			get
			{
				if(iT_UserUnitRelationDAL==null)
					iT_UserUnitRelationDAL=new T_UserUnitRelationDAL();
				return iT_UserUnitRelationDAL;
			}
			set
			{
				iT_UserUnitRelationDAL=value;
			}
		}
		#endregion
			#region 70 数据接口
		IT_UseWorkerDAL iT_UseWorkerDAL;
		public IT_UseWorkerDAL IT_UseWorkerDAL
		{
			get
			{
				if(iT_UseWorkerDAL==null)
					iT_UseWorkerDAL=new T_UseWorkerDAL();
				return iT_UseWorkerDAL;
			}
			set
			{
				iT_UseWorkerDAL=value;
			}
		}
		#endregion
	}
}