using System;

namespace EasyGameFramework.Core.DataTable
{
    /// <summary>
    /// 数据表行辅助器解析器接口。
    /// </summary>
    public interface IDataRowHelperResolver
    {
        /// <summary>
        /// 获取数据表行辅助器。
        /// </summary>
        /// <param name="dataRowType">数据表行类型。</param>
        /// <returns>数据表行辅助器。</returns>
        IDataRowHelper GetHelper(Type dataRowType);

        /// <summary>
        /// 获取数据表行辅助器。
        /// </summary>
        /// <typeparam name="T">数据表行类型。</typeparam>
        /// <returns>数据表行辅助器。</returns>
        IDataRowHelper<T> GetHelper<T>();
    }
}
