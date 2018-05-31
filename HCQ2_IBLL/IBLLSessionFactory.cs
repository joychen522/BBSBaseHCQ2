using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  业务仓储工厂
    /// </summary>
    public interface IBLLSessionFactory
    {
        IBLLSession GetBLLSession();
    }
}
