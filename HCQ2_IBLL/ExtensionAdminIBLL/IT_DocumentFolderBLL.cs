using HCQ2_Model;
using HCQ2_Model.DocModel;
using HCQ2_Model.TreeModel;
using System.Collections.Generic;

namespace HCQ2_IBLL
{
    public partial interface IT_DocumentFolderBLL
    {
        /// <summary>
        ///  根据用户获取文档结构树
        /// </summary>
        /// <param name="pageType">页面参数</param>
        /// <param name="doc_type">节点类型</param>
        /// <returns></returns>
        List<DocTreeModel> GetDocTreeData(string pageType, int doc_type);
        /// <summary>
        ///  添加节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddNode(DocTreeModel model);
        /// <summary>
        ///  添加系统节点
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        DocTreeModel AddSysNode(DocTreeModel folder);
        /// <summary>
        ///  编辑节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        int EditNode(int id,string name);
        /// <summary>
        ///  编辑系统节点
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        DocTreeModel EditSysNode(DocTreeModel folder);
        /// <summary>
        ///  删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteNode(T_DocumentFolder model,int id);
        /// <summary>
        ///  获取子目录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<TreeTableAttribute> GetMenuDataByPid(int id, string type);
    }
}
