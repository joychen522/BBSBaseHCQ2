using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    public partial interface IBMQ_DocumentBLL
    {
        /// <summary>
        /// 获取所有的政策
        /// </summary>
        /// <returns></returns>
        List<BMQ_Document> GetDocumentInfo();

        /// <summary>
        /// 根据政策ID获取一条政策
        /// </summary>
        /// <returns></returns>
        BMQ_Document GetByDocumentID(string documentID);

        /// <summary>
        /// 处理字符串长度超过20将改写
        /// </summary>
        /// <returns></returns>
        List<BMQ_Document> GetDocumentSortName(List<BMQ_Document> list);

        /// <summary>
        /// 处理新闻政策分页的数据
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        List<BMQ_Document> GetApiPageRowDoc(HCQ2_Model.WebApiModel.ParamModel.BmqModle modle);
    }
}
