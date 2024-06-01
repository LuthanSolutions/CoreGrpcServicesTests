namespace CoreGrpcServicesTests.Services;

using CoreGrpcClient;
using CoreGrpcServicesTests.Models;
using Google.Protobuf;

public static class GrpcResponseConverter
{
    public static T ConvertGrpcResponseTo<T, TContent>(DefaultResponseObject response)
        where T : DefaultResponseObject<TContent>
        where TContent : IMessage, new()
    {
        var defaultResponseObject = InstantiateDefaultResponseObject<T, TContent>(response);
        PopulateMetaDataInfo<T, TContent>(response, defaultResponseObject);
        PopulateErrors<T, TContent>(response, defaultResponseObject);
        PopulateContent<T, TContent>(response, defaultResponseObject);
        return defaultResponseObject;
    }

    private static T InstantiateDefaultResponseObject<T, TContent>(DefaultResponseObject response)
        where T : DefaultResponseObject<TContent>
        where TContent : new()
    {
        var defaultResponseObject = Activator.CreateInstance<T>();
        defaultResponseObject.HasErrors = response.HasErrors;
        defaultResponseObject.ResultType = response.ResultType;
        return defaultResponseObject;
    }

    private static void PopulateMetaDataInfo<T, TContent>(DefaultResponseObject response, T defaultResponseObject)
        where T : DefaultResponseObject<TContent>
        where TContent : new() =>
        defaultResponseObject.MetaDataInfo = new Models.MetaDataInfo
        {
            CacheEvent = response.MetaDataInfo.CacheEvent,
            Count = response.MetaDataInfo.Count,
            DataSource = response.MetaDataInfo.DataSource,
        };

    private static void PopulateErrors<T, TContent>(DefaultResponseObject response, T defaultResponseObject)
        where T : DefaultResponseObject<TContent>
        where TContent : new() =>
        defaultResponseObject.Errors = response.Errors
            .Select(err =>
                new Models.Error
                {
                    Code = err.Code,
                    Description = err.Description
                });

    private static void PopulateContent<T, TContent>(DefaultResponseObject response, T defaultResponseObject)
        where T : DefaultResponseObject<TContent>
        where TContent : IMessage, new() =>
        defaultResponseObject.Content = response.Content.Select(cnt => cnt.Unpack<TContent>());
}
