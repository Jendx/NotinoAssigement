namespace Notino.Domain.Attributes;

internal sealed class QueryAttribute : Attribute
{
    public string InsertQuery { get; set; }

    public string UpdateQuery { get; set; }

    public string GetQuery { get; set; }
}
