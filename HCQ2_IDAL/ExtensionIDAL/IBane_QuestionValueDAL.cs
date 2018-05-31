using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  答题选项库数据层接口
    /// </summary>
    public partial interface IBane_QuestionValueDAL
    {
        /// <summary>
        ///  根据指定试题ID 获取答题选项
        /// </summary>
        /// <param name="sub_ids"></param>
        /// <returns></returns>
        List<Bane_QuestionValue> GetOptionsById(List<int> sub_ids);
    }
}
