using System;
using EasyGameFramework.Core;
using EasyGameFramework.Core.DataTable;

namespace EasyGameFramework
{
    public class DefaultDataRowHelperResolver : DataRowHelperResolverBase
    {
        public override IDataRowHelper GetHelper(Type dataRowType)
        {
            return typeof(IDataRow).IsAssignableFrom(dataRowType)
                ? (IDataRowHelper)Activator.CreateInstance(typeof(DefaultDataRowHelper<>).MakeGenericType(dataRowType))
                : throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
        }
    }
}
