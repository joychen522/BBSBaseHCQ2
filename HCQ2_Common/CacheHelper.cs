using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Caching;

namespace HCQ2_Common
{
    /// <summary>
    ///  操作缓存帮助类
    ///  说明：缓存属于全局公用，一般用于全局公用
    ///  创建人：陈敏
    ///  创建时间：2015-5-20 14:16
    /// </summary>
    public class CacheHelper
    {
        private static int _cacheAbsoluteExporation;//缓存过期时间，分钟
        /// <summary>
        ///  缓存过期时间：默认20分钟
        ///  返回值为XX秒
        /// </summary>
        protected static int CacheAbsoluteExpiration
        {
            get
            {
                if(_cacheAbsoluteExporation==0)
                {
                    _cacheAbsoluteExporation = CacheConfig.CacheAbsouteExpiration;
                    if (_cacheAbsoluteExporation == 0)
                        _cacheAbsoluteExporation = 20;
                }
                return _cacheAbsoluteExporation;
            }
        }
        /// <summary>
        ///  根据key获取缓存的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCacheValue(string key)
        {
            //判断缓存是否存在
            if (string.IsNullOrEmpty(key)) return null;
            if (CacheConfig.NoCache) return null;
            Cache objCache = HttpRuntime.Cache;
            return objCache[key];
        }
        /// <summary>
        ///  根据缓存名称keyName设置值
        /// </summary>
        /// <param name="keyName">缓存名称</param>
        /// <param name="cacheValue">值</param>
        public static void SetCacheValue(string keyName,object cacheValue)
        {
            if (CacheConfig.NoCache || string.IsNullOrEmpty(keyName)) return;
            //移除缓存
            Cache objCache = HttpRuntime.Cache;
            //处理过期时间
            int CacheTime = CacheAbsoluteExpiration*60;
            objCache.Insert(keyName, cacheValue, null, DateTime.Now.AddSeconds(CacheTime), TimeSpan.Zero);
            //方法二
            //移除缓存
            //HttpContext.Current.Cache.Remove(keyName);
            //添加缓存
            //HttpContext.Current.Cache.Add(keyName, cacheValue, null, DateTime.Now.AddSeconds(CacheAbsoluteExpiration), TimeSpan.Zero, CacheItemPriority.Normal, null);
        }
        /// <summary>
        ///  根据缓存名称keyName依据其他参数设置缓存值
        /// </summary>
        /// <param name="keyName">缓存名称</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="absoluteTime">过期时间</param>
        /// <param name="slidingSpan">类型</param>
        public static void SetCacheValue(string keyName,object cacheValue,DateTime absoluteTime,TimeSpan slidingSpan)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(keyName, cacheValue, null, absoluteTime, slidingSpan);
        }
        /// <summary>
        ///  根据缓存名移除缓存
        /// </summary>
        /// <param name="keyName"></param>
        public static void RemoveCache(string keyName)
        {
            Cache objCache = HttpRuntime.Cache;
            try
            {
                objCache.Remove(keyName);
            }
            catch
            { }
        }
        /// <summary>
        ///  移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            try
            {
                Cache objCache = HttpRuntime.Cache;
                foreach (DictionaryEntry elem in objCache)
                {
                    objCache.Remove(elem.Key.ToString());
                }
            }
            catch
            {
            }
        }
    }
    internal class CacheConfig
    {
        /// <summary>
        ///  根据名称获取换的字符串
        /// </summary>
        /// <param name="key">名称</param>
        /// <returns></returns>
        static string GetString(string key)
        {
            return ConfigurationHelper.AppSetting(key);
        }
        /// <summary>
        ///  根据名称获取缓存的整形数据
        /// </summary>
        /// <param name="key">名称</param>
        /// <returns></returns>
        static int GetInt(string key)
        {
            var val = GetString(key);
            if (string.IsNullOrEmpty(val))
                return 0;
            int result;
            if(!int.TryParse(val,out result))
                result=0;
            return result;
        }
        /// <summary>
        ///  缓存过期时间
        /// </summary>
        public static int CacheAbsouteExpiration
        {
            get
            {
                return GetInt("CacheAbsoluteExpiration");
            }
        }
        public static bool NoCache
        {
            get
            {
                return GetInt("NoCache") == 1;
            }
        }
    }
}
