using System.Data.Entity;
namespace HCQ2_IDAL
{
    /// <summary>
    ///  EF数据上下文 工厂
    /// </summary>
    public interface IDBContextFactory
    {
        /// <summary>
        ///  获取EF上下文对象
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
    }
}
