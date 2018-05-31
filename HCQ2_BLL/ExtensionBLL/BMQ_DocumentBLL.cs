using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class BMQ_DocumentBLL : HCQ2_IBLL.IBMQ_DocumentBLL
    {
        /// <summary>
        /// 获取所有的政策
        /// </summary>
        /// <returns></returns>
        public List<BMQ_Document> GetDocumentInfo()
        {
            return base.Select(o => o.DocID != "").OrderBy(k => k.DispOrder).ToList();
        }

        /// <summary>
        /// 根据政策ID获取一条政策
        /// </summary>
        /// <returns></returns>
        public BMQ_Document GetByDocumentID(string documentID)
        {
            return base.Select(o => o.DocID == documentID).FirstOrDefault();
        }

        /// <summary>
        /// 处理字符串长度超过20将改写
        /// </summary>
        /// <returns></returns>
        public List<BMQ_Document> GetDocumentSortName(List<BMQ_Document> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].DocTitle.Length > 20)
                        list[i].DocTitle = list[i].DocTitle.Substring(0, 20) + "...";
                }
            }
            return list;
        }

        /// <summary>
        /// 处理新闻政策分页的数据
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public List<BMQ_Document> GetApiPageRowDoc(HCQ2_Model.WebApiModel.ParamModel.BmqModle modle)
        {
            List<BMQ_Document> listBmq = GetDocumentInfo();
            var data = listBmq.AsEnumerable();
            if (!string.IsNullOrEmpty(modle.search))
                data = data.Where(o => o.DocTitle.Contains(modle.search) || o.DocContent.Contains(modle.search));
            data = data.Skip((modle.page * modle.rows) - modle.rows).Take(modle.rows);
            return data.ToList();
        }
    }
}
