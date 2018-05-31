using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_IDAL;
using System.Runtime.Remoting.Messaging;

namespace HCQ2_DAL_MSSQL
{
    public class DBContextFactory:IDBContextFactory
    {
        #region 创建 EF上下文 对象，在线程中共享 一个 上下文对象 + DbContext GetDbContext()
        /// <summary>
        /// 创建 EF上下文 对象，在线程中共享 一个 上下文对象
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            DbContext dbContext = CallContext.GetData(typeof (DBContextFactory).Name) as DbContext;
            if (dbContext == null)
            {
                dbContext = new HCQ2_Model.HCQ2Entities(); 
                CallContext.SetData(typeof (DBContextFactory).Name, dbContext);
            }
            return dbContext;
        }
        #endregion
    }
}
