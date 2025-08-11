using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace ConnectionTester;
class Program
{
    static async Task Main()
    {
        string endpointUri = "https://localhost:8081/";
        string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        CosmosClient client = new CosmosClient(endpointUri, primaryKey);

        Console.WriteLine("Connected to Cosmos DB Emulator.");

        Database db = await client.CreateDatabaseIfNotExistsAsync("TestDB");
        Container container = await db.CreateContainerIfNotExistsAsync("TestContainer", "/id");

        var sqlQueryText = "SELECT c.id FROM c";

        Console.WriteLine($"Running query: {sqlQueryText}");

        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
        FeedIterator<dynamic> queryResultSetIterator = container.GetItemQueryIterator<dynamic>(queryDefinition);

        while (queryResultSetIterator.HasMoreResults)
        {
            FeedResponse<dynamic> currentResultSet = await queryResultSetIterator.ReadNextAsync();
            foreach (var item in currentResultSet)
            {
                Console.WriteLine(item);
            }
        }


    }
}