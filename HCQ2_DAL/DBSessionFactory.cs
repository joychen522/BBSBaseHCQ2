using HCQ2_IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HCQ2_DAL_MSSQL;
using Spring.Data.Core;

namespace HCQ2_DAL_MSSQL
{
     public class DBSessionFactory: AdoDaoSupport, IDBSessionFactory
    {
         /// <summary>
         ///  此方法作用：提高效率 在线程中 公用一个DBSession数据仓储对象
         /// </summary>
         /// <returns></returns>
        public IDBSession GetDBSession()
        {
            //从当前线程中 获取DBSession数据仓储对象
            IDBSession dbSession = CallContext.GetData(typeof (DBSessionFactory).Name) as IDBSession;
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData(typeof (DBSessionFactory).Name, dbSession);
            }
            return dbSession;
        }
    }
}
