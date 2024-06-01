namespace CoreGrpcServicesTests.Models;
public abstract class DefaultResponseObject<T>
{
    public IEnumerable<T>? Content { get; set; }

    public string? ResultType { get; set; }

    public bool HasErrors { get; set; }

    public IEnumerable<Error>? Errors { get; set; }

    public MetaDataInfo? MetaDataInfo { get; set; }
}
