using HCQ2_Model;
using HCQ2_Model.DocModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HCQ2_IBLL
{
    public partial interface IT_DocumentInfoBLL
    {
        /// <summary>
        ///  添加文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddDocInfo(T_DocumentInfo model);
        /// <summary>
        ///  获取Table数据
        /// </summary>
        /// <param name="model">参数</param>
        /// <param name="Server">服务</param>
        /// <param name="total">根据条件返回记录条数</param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTableData(DocTableParamModel model, out int total);
    }
}
