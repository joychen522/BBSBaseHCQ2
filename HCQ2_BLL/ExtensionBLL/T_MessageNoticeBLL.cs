using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using System.Web.Mvc;
using System.Web;
using HCQ2_Common;
using static HCQ2_Common.ImageHelper;

namespace HCQ2_BLL
{
    public partial class T_MessageNoticeBLL : HCQ2_IBLL.IT_MessageNoticeBLL
    {
        //********************************************joychen Start**********************************************
        /// <summary>
        ///  获取新闻列表数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public TableModel GetNewsListByParams(string key, int page, int rows)
        {
            TableModel model = new TableModel();
            if (!string.IsNullOrEmpty(key))
            {
                model.total = SelectCount(s => s.m_title.Contains(key));
                model.rows = Select(s => s.m_title.Contains(key), s => s.m_id, page, rows, true).Select(s => new { s.m_id, s.m_title, s.m_content, s.m_type, s.create_date, s.create_user_name, s.focus_imgage, s.messList_imgage, s.messDetail_imgage });
            }
            else
            {
                model.total = SelectCount(null);
                model.rows = Select(s => s.m_id> 0, s => s.m_id, page, rows, true).Select(s => new { s.m_id, s.m_title, s.m_content, s.m_type, s.create_date, s.create_user_name, s.focus_imgage, s.messList_imgage, s.messDetail_imgage });
            }
            return model;                
        }
        /// <summary>
        ///  添加新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNews(T_MessageNotice model)
        {
            if (model == null)
                return false;
            model.create_date = DateTime.Now;
            model.create_user_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name;
            model.create_user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            return Add(model) > 0;
        }
        /// <summary>
        ///  编辑新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditNews(T_MessageNotice model)
        {
            if (model == null)
                return false;
            Modify(model, s => s.m_id == model.m_id, "m_title", "m_content", "m_type", "focus_imgage", "messList_imgage", "messDetail_imgage");
            return true;
        }

        //********************************************joychen End***********************************************
        /// <summary>
        /// 获取所有的新闻公告
        /// </summary>
        /// <returns></returns>
        public List<T_MessageNotice> GetAllMess()
        {
            return base.Select(o => o.m_id > 0).OrderByDescending(o => o.create_date).ToList();
        }
        /// <summary>
        /// 根据用户ID获取自己发布的新闻公告
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<T_MessageNotice> GetByUserId(int user_id)
        {
            return Select(o => o.create_user_id == user_id).ToList();
        }
        /// <summary>
        /// 根据主键ID获取新闻公告
        /// </summary>
        /// <param name="mess_id"></param>
        /// <returns></returns>
        public T_MessageNotice GetByMessId(int mess_id)
        {
            return base.Select(o => o.m_id == mess_id).FirstOrDefault();
        }
        /// <summary>
        /// 获取页面显示的数据源
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<T_MessageNotice> GetPageModle(string keyword,int page,int rows)
        {
            List<T_MessageNotice> list = new List<T_MessageNotice>();
            int user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            if (!string.IsNullOrEmpty(keyword))
                list = Select(s => s.m_title.Contains(keyword) && s.create_user_id == user_id, s => s.m_id, page, rows, true).ToList();
            else
                list = Select(s => s.create_user_id == user_id, s => s.m_id, page, rows, true).ToList();
            return list;
        }
        /// <summary>
        /// 新增或者是编辑新闻公告
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateMess(object obj)
        {
            FormCollection param = (FormCollection)obj;

            T_MessageNotice mes = new T_MessageNotice();

            mes.m_title = param["m_title"];
            mes.m_type = param["m_type"];
            mes.m_content = param["m_content"];
            mes.create_user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            mes.create_user_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name;
            mes.create_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            if (!string.IsNullOrEmpty(param["m_image"]))
            {
                //封面图片
                string imagePath = param["m_image"];
                mes.m_image_src = imagePath;
                byte[] str = ImageToBytes(HttpContext.Current.Server.MapPath(imagePath));
                mes.m_image = str;
                //焦点新闻图片
                string img = imagePath.Substring(imagePath.LastIndexOf("/")),
                    imgName = img.Substring(0, img.LastIndexOf(".")), imgType = img.Substring(img.LastIndexOf("."));
                string tempName = imagePath.Substring(0, imagePath.LastIndexOf("/")) + imgName + "_focus_imgage" + imgType;
                CompressImage(HttpContext.Current.Server.MapPath(imagePath), HttpContext.Current.Server.MapPath(tempName), 300, 720,100, ImageCompressType.Cut);
                mes.focus_imgage = tempName;
                //列表新闻图片
                tempName = imagePath.Substring(0, imagePath.LastIndexOf("/")) + imgName + "_messList_imgage" + imgType;
                CompressImage(HttpContext.Current.Server.MapPath(imagePath), HttpContext.Current.Server.MapPath(tempName), 210, 360, 100, ImageCompressType.Cut);
                mes.messList_imgage = tempName;
                //新闻详情图片
                //tempName = imagePath.Substring(0, imagePath.LastIndexOf("/")) + imgName + "_messDetail_imgage" + imgType;
                //CompressImage(HttpContext.Current.Server.MapPath(imagePath), HttpContext.Current.Server.MapPath(tempName), 210, 360, 1, ImageCompressType.Cut);
                //mes.messDetail_imgage = tempName;
            }
            bool isAccess = false;
            if (!string.IsNullOrEmpty(param["mes_id"]))
            {
                //编辑
                int mes_id = int.Parse(param["mes_id"]);
                isAccess = base.Modify(mes, o => o.m_id == mes_id, "m_title", "m_type", "m_content", "m_image", "focus_imgage", "messDetail_imgage") > 0;
            }
            else
                isAccess = base.Add(mes) > 0;

            return isAccess;
        }
        /// <summary>
        /// 删除新闻公告
        /// </summary>
        /// <param name="mess_id"></param>
        /// <returns></returns>
        public bool DeleteMess(int mess_id)
        {
            return base.Delete(o => o.m_id == mess_id) > 0;
        }

        #region APP接口

        /// <summary>
        /// 分页获取新闻或者通知
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="type">0:新闻  1:通知</param>
        /// <returns></returns>
        public List<HCQ2_Model.AppModel.Message> GetPageRowList(HCQ2_Model.AppModel.NoticeModel notice, string appUrl)
        {
            HCQ2_Model.AppModel.Message mes = new HCQ2_Model.AppModel.Message();
            List<HCQ2_Model.AppModel.Message> rlist = new List<HCQ2_Model.AppModel.Message>();
            List<T_MessageNotice> list = new List<T_MessageNotice>();

            T_ItemCodeBLL codeList = new T_ItemCodeBLL();
            T_ItemCodeMenumBLL codeMenuList = new T_ItemCodeMenumBLL();
            string news_type = codeMenuList.GetByItemId(codeList.GetByItemCode("NewsType").item_id).
                Where(o => o.code_value == notice.type).FirstOrDefault()?.code_name;
            var data = GetAllMess().Where(o => o.m_type == news_type).Skip((notice.rows * notice.page) - notice.rows).Take(notice.rows);
            if (data.Count() > 0)
            {
                list = data.ToList();
                foreach (var item in list)
                {
                    mes = new HCQ2_Model.AppModel.Message();
                    mes.notice_title = item.m_title;
                    mes.notice_content = item.m_content;
                    mes.notice_type = item.m_type;
                    mes.notice_image = item.m_image;
                    if (!string.IsNullOrEmpty(item.m_image_src))
                        mes.notice_image_src = appUrl + "/" + item.m_image_src.Replace("~", "");
                    else
                        mes.notice_image_src = "";
                    mes.release_name = item.create_user_name;
                    if (!string.IsNullOrEmpty(item.create_date.ToString()))
                        mes.release_date = Convert.ToDateTime(item.create_date).ToString("yyyy-MM-dd");
                    rlist.Add(mes);
                }
            }
            return rlist;
        }

        /// <summary>
        /// 根据类别获取新闻通知
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.AppModel.Message> GetNoticeByType(HCQ2_Model.AppModel.NoticeModel notice, string appUrl)
        {
            HCQ2_Model.AppModel.Message mes = new HCQ2_Model.AppModel.Message();
            List<HCQ2_Model.AppModel.Message> rlist = new List<HCQ2_Model.AppModel.Message>();
            List<T_MessageNotice> list = new List<T_MessageNotice>();
            T_ItemCodeBLL codeList = new T_ItemCodeBLL();
            T_ItemCodeMenumBLL codeMenuList = new T_ItemCodeMenumBLL();
            string news_type = codeMenuList.GetByItemId(codeList.GetByItemCode("NewsType").item_id).
                Where(o => o.code_value == notice.type).FirstOrDefault()?.code_name;
            var data = GetAllMess().Where(o => o.m_type == news_type).Skip((notice.rows * notice.page) - notice.rows).Take(notice.rows);
            if (data.Count() > 0)
            {
                list = data.ToList();
                foreach (var item in list)
                {
                    mes = new HCQ2_Model.AppModel.Message();
                    mes.notice_title = item.m_title;
                    mes.notice_content = "";
                    mes.notice_type = item.m_type;
                    mes.notice_image = null;
                    mes.notice_image_src = "";
                    mes.release_name = item.create_user_name;
                    if (!string.IsNullOrEmpty(item.create_date.ToString()))
                        mes.release_date = Convert.ToDateTime(item.create_date).ToString("yyyy-MM-dd");
                    rlist.Add(mes);
                }
            }
            return rlist;
        }

        #endregion
    }
}
