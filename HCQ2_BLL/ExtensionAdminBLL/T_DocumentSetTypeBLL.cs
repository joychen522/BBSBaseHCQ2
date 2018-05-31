using HCQ2_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    public  partial class T_DocumentSetTypeBLL:HCQ2_IBLL.IT_DocumentSetTypeBLL
    {
        /// <summary>
        ///  获取分享人员数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByPerson()
        {
            return DBSession.IT_DocumentSetTypeDAL.GetShareDataByPerson();
        }
        /// <summary>
        ///  获取分享角色数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByRole()
        {
            return DBSession.IT_DocumentSetTypeDAL.GetShareDataByRole();
        }
        /// <summary>
        ///  保存分享人员数据设置
        /// </summary>
        /// <param name="personData"></param>
        /// <returns></returns>
        public bool SaveShareDataByPerson(string personData, int file_id)
        {
            if (string.IsNullOrEmpty(personData) || file_id==0)
                return false;
            string[] str = personData.Trim(',').Split(',');
            if(str.Length<=0)
                return false;
            T_DocumentFolderRelation folder = DBSession.IT_DocumentFolderRelationDAL.Select(s => s.file_id == file_id).FirstOrDefault();
            //T_DocumentFolder folder = DBSession.IT_DocumentFolderDAL.Select(s => s.doc_type == 2 && s.was_share == true).FirstOrDefault();
            if(folder==null)
                return false;
            List<T_DocumentSetType> list = DBSession.IT_DocumentSetTypeDAL.Select(s => s.share_id == HCQ2UI_Helper.OperateContext.Current.Usr.user_id && s.file_id == file_id);
            foreach (string item in str)
            {
                var obj = list.FindAll(s => s.user_id == Helper.ToInt(item));
                if (obj != null && obj.Count > 0)
                    continue;
                Add(new T_DocumentSetType
                {
                    file_id = file_id,
                    folder_id = folder.folder_id,
                    share_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id,
                    user_id = Helper.ToInt(item)
                });
            }
            return true;
        }
        /// <summary>
        ///  保存分享角色数据设置
        /// </summary>
        /// <param name="personData"></param>
        /// <returns></returns>
        public bool SaveShareDataByRole(string personData, int file_id)
        {
            if (string.IsNullOrEmpty(personData) || file_id == 0)
                return false;
            string[] str = personData.Trim(',').Split(',');
            if (str.Length <= 0)
                return false;
            T_DocumentFolder folder = DBSession.IT_DocumentFolderDAL.Select(s => s.doc_type == 2 && s.was_share == true).FirstOrDefault();
            if (folder == null)
                return false;
            List<T_DocumentSetType> list = DBSession.IT_DocumentSetTypeDAL.Select(s => s.share_id == HCQ2UI_Helper.OperateContext.Current.Usr.user_id && s.file_id == file_id);
            foreach (string item in str)
            {
                var obj = list.FindAll(s => s.role_id == Helper.ToInt(item));
                if (obj != null && obj.Count>0)
                    continue;
                Add(new T_DocumentSetType
                {
                    file_id = file_id,
                    folder_id = folder.folder_id,
                    share_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id,
                    role_id = Helper.ToInt(item)
                });
            }
            return true;
        }
    }
}
