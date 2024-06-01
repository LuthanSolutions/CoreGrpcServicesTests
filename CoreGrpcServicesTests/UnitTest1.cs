namespace CoreGrpcServicesTests;

using CoreGrpcClient;
using CoreGrpcServicesTests.Models;
using CoreGrpcServicesTests.Services;
using Grpc.Net.Client;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7149");
        var client = new People.PeopleClient(channel);

        var response = await client.GetPeopleAsync(new GetPeopleRequest { Region = "UK" });

        var defaultResponseObject = GrpcResponseConverter.ConvertGrpcResponseTo<PersonsResponseObject, Person>(response);
    }
}