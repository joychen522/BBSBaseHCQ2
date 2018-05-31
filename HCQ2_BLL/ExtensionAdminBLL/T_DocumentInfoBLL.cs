using HCQ2_IDAL;
using HCQ2_Model;
using HCQ2_Model.DocModel;
using System;
using System.Collections.Generic;
using System.Web;

namespace HCQ2_BLL
{
    public partial class T_DocumentInfoBLL:HCQ2_IBLL.IT_DocumentInfoBLL
    {
        /// <summary>
        ///  添加文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddDocInfo(T_DocumentInfo model)
        {
            if (null == model)
                return 0;
            model.create_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            model.create_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name;
            model.create_time = DateTime.Now;
            int mark = Add(model);
            if (mark > 0)
                return model.file_id;
            return 0;
        }
        /// <summary>
        ///  获取Table数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DocTreeResultModel> GetTableData(DocTableParamModel model,out int total)
        {
            total = 0;
            if (null == model)
                return null;
            List<DocTreeResultModel> list = new List<DocTreeResultModel>();
            int user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            IT_DocumentInfoDAL dal = DBSession.IT_DocumentInfoDAL;
            switch (model.doc_type)
            {
                case 1: {
                        list = dal.GetTableByOwnShareDoc(model, user_id);
                        total = dal.GetTableByOwnShareDocCount(model, user_id);
                    } break;
                case 2: {
                        List<int> roles = HCQ2UI_Helper.Session.SysPermissSession.RolesList;
                        list = dal.GetTableShareByOwnDoc(model, user_id,roles);
                        total = dal.GetTableShareByOwnDocCount(model, user_id, roles);
                    }  break;
                case 3: {
                        list = dal.GetTablePublicDoc(model, user_id);
                        total = dal.GetTablePublicDocCount(model, user_id);
                    } break;
                case 4: {
                        list = dal.GetTableRemoveDoc(model, user_id);
                        total = dal.GetTableRemoveDocCount(model, user_id);
                    } break;
                case 5: {
                        //待审核资源
                        list=dal.GetTableApproveDoc(model, user_id);
                        total = dal.GetTableApproveDocCount(model, user_id);
                    } break;
                default: {
                        list = dal.GetTableByOwnDoc(model, user_id);
                        total = dal.GetTableByOwnDocCount(model, user_id);
                    } break;
            }
            if (null == list)
                return null;
            return list;
        }
    }
}
