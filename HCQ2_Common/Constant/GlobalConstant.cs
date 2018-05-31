using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common.Constant
{
    /// <summary>
    ///  控制器返回值
    /// </summary>
    public enum GlobalConstant
    {
        参数异常 = 1,
        数据获取成功 = 201,
        数据获取失败 = 301,
        数据获取异常 = 401,
        数据为空 = 501,
        保存成功 = 202,
        保存失败 = 302,
        保存异常 = 402,
        操作成功 = 203,
        操作失败 = 303,
        操作异常 = 403
    }
}
