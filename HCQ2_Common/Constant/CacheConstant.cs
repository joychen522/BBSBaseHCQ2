using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common.Constant
{
    /// <summary>
    ///  缓存常量类
    /// </summary>
    public class CacheConstant
    {
        /// <summary>
        ///  当前登录用户 角色缓存Key
        /// </summary>
        public const string loginUserCacheRoles = "loginUserCacheRoles";
        /// <summary>
        ///  当前登录用户 权限缓存Key
        /// </summary>
        public const string loginUserCachePermiss = "loginUserCachePermiss";
        /// <summary>
        ///  当前登录用户 菜单缓存Key
        /// </summary>
        public const string loginUserCacheMenus = "loginUserCacheMenus";
        /// <summary>
        ///  所有元素 缓存Key
        /// </summary>
        public const string allCacheElements = "allCacheElements";
        /// <summary>
        ///  当前登录用户 代管单位缓存Key
        /// </summary>
        public const string loginUserPerminssUnitData = "loginUserPerminssUnitData";
        /// <summary>
        ///  模块权限设置 缓存Key
        /// </summary>
        public const string modulePerminss = "modulePerminss";
        /// <summary>
        ///  当前登录用 授权区域集合
        /// </summary>
        public const string loginUserPerminssAreaData = "loginUserPerminssAreaData";
        /// <summary>
        ///  APP登录用户guid 对于身份证Key
        /// </summary>
        public const string baneUserID = "baneIdentify";
    }
}
