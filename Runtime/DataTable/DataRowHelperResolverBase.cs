using System;
using EasyGameFramework.Core.DataTable;
using UnityEngine;

namespace EasyGameFramework
{
    public abstract class DataRowHelperResolverBase : MonoBehaviour, IDataRowHelperResolver
    {
        public abstract IDataRowHelper GetHelper(Type dataRowType);

        public virtual IDataRowHelper<T> GetHelper<T>()
        {
            return GetHelper(typeof(T)) as IDataRowHelper<T>;
        }
    }
}
