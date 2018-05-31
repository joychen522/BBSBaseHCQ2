using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    /// <summary>
    ///  业务仓储工厂
    /// </summary>
    public class BLLSessionFactory: HCQ2_IBLL.IBLLSessionFactory
    {
        /// <summary>
        ///  创建业务仓储对象
        /// </summary>
        /// <returns></returns>
        public HCQ2_IBLL.IBLLSession GetBLLSession()
        {
            BLLSession bllSession = CallContext.GetData(typeof (BLLSessionFactory).Name) as BLLSession;
            if (bllSession == null)
            {
                bllSession = new BLLSession();
                CallContext.SetData(typeof (BLLSessionFactory).Name, bllSession);
            }
            return bllSession;
        }
    }
}
