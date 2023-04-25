namespace Notino.Data.SQLite.TypeHandlers;

using Dapper;
using System.Data;

internal sealed class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        return Guid.Parse(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value.ToString();
    }
}
