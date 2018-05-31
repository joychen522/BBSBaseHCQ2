using HCQ2_Model;
using HCQ2_Model.DocModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    public partial interface IT_DocumentInfoDAL
    {
        /// <summary>
        ///  我的文档
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTableByOwnDoc(DocTableParamModel model, int user_id);
        /// <summary>
        ///  我的文档 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        int GetTableByOwnDocCount(DocTableParamModel model, int user_id);
        /// <summary>
        ///  我的分享
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTableByOwnShareDoc(DocTableParamModel model, int user_id);
        /// <summary>
        ///  我的分享 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetTableByOwnShareDocCount(DocTableParamModel model, int user_id);
        /// <summary>
        ///  分享文档
        /// </summary>
        /// <param name="model">参数</param>
        /// <param name="user_id">用户id</param>
        /// <param name="roles">角色id集合</param>
        /// <param name="groups">组id集合</param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTableShareByOwnDoc(DocTableParamModel model, int user_id, List<int> roles);
        /// <summary>
        ///  分享文档 数量统计
        /// </summary>
        /// <param name="model">参数</param>
        /// <param name="user_id">用户id</param>
        /// <param name="roles">角色id集合</param>
        /// <param name="groups">组id集合</param>
        /// <returns></returns>
        int GetTableShareByOwnDocCount(DocTableParamModel model, int user_id, List<int> roles);
        /// <summary>
        ///  公用文档
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTablePublicDoc(DocTableParamModel model, int user_id);
        /// <summary>
        ///  公用文档 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        int GetTablePublicDocCount(DocTableParamModel model, int user_id);
        /// <summary>
        ///  统计自己目录下的文档数量
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetTablePublicDocCountByOwn(DocTableParamModel model, int user_id);
        /// <summary>
        ///  回收站
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTableRemoveDoc(DocTableParamModel model, int user_id);
        /// <summary>
        ///  回收站 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        int GetTableRemoveDocCount(DocTableParamModel model, int user_id);
        /// <summary>
        ///  5：待审核资源
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        List<DocTreeResultModel> GetTableApproveDoc(DocTableParamModel model, int user_id);
        /// <summary>
        ///  5：待审核资源 数量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        int GetTableApproveDocCount(DocTableParamModel model, int user_id);
    }
}
