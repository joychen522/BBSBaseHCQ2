using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;

namespace HCQ2_DI
{
    /// <summary>
    ///  控制反转 Spring容器上下文
    /// </summary>
    public class SpringHelper
    {
        #region 1.0 Spring容器上下文 -IApplicationContext SpringContext
        /// <summary>
        ///  Spring容器上下文
        /// </summary>
        private static IApplicationContext SpringContext
        {
            get { return ContextRegistry.GetContext(); }
        }
        #endregion

        #region 获取配置对象

        public static T GetObject<T>(string objName)
            where T : class
        {
            return (T)SpringContext.GetObject(objName);
        }
        #endregion
    }
}
