using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HCQ2_Common
{
    /// <summary>
    ///  合并对象帮助类
    /// </summary>
    public class Rowspan
    {
        /// <summary>
        ///  根据主键合并相同数据
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="list">集合</param>
        /// <param name="keyProperty">作为同一行数据的列名称</param>
        /// <returns></returns>
        public static List<T> rowpanObj<T>(List<T> list,string keyProperty)
        {
            //1.0 获取实体类 类型对象
            Type t = typeof (T);
            //1.1 通过反射获取类的实例，公有属性
            List<PropertyInfo> proInfos = t.GetProperties().ToList();
            //1.2 遍历属性集合
            PropertyInfo keyPro =
                proInfos.FirstOrDefault(s => s.Name.ToLower().ToString().Equals(keyProperty.ToLower()));
            //外层循环：列
            object obj = null;
            object keyObj = null;//获取第一个作为同一行数据的主键值
            proInfos.ForEach(p =>
            {
                //内层循环：行
                for (int i = 0; i < list.Count; i++)
                {
                    if (obj != null && obj.Equals(p.GetValue(list[i], null)) && 
                    (keyPro.GetValue(list[i], null)==null || keyPro.GetValue(list[i], null).Equals(keyObj)))
                        p.SetValue(list[i], null, null);
                    else
                    {
                        obj = p?.GetValue(list[i], null);
                        keyObj = keyPro?.GetValue(list[i], null);
                    }
                }
            });
            return list;
        }
    }
}
