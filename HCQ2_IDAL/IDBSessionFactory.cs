namespace HCQ2_IDAL
{
    /// <summary>
    ///  数据仓储工厂
    /// </summary>
    public interface IDBSessionFactory
    {
        IDBSession GetDBSession();
    }
}
