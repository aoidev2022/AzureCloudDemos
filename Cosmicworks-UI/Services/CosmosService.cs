using Cosmicworks_UI.Models;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Cosmicworks_UI.Services;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _client;
    private readonly IConfiguration _configuration;

    private Container container
    {
        get => _client.GetDatabase("cosmicworks").GetContainer("products");
    }

    public CosmosService(IConfiguration configuration)
    {
        _configuration = configuration;
        var cosmodb = _configuration.GetConnectionString("cosmodb");
        _client = new CosmosClient(connectionString: cosmodb);
    }

    public async Task<IEnumerable<Product>> RetrieveAllProductsAsync()
    {
        var queryable = container.GetItemLinqQueryable<Product>();

        using FeedIterator<Product> feed = queryable
            .Where(p => p.price < 2000m)
            .OrderByDescending(p => p.price)
            .ToFeedIterator();

        List<Product> results = [];

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                results.Add(item);
            }
        }

        return results;
    }

    public async Task<IEnumerable<Product>> RetrieveActiveProductsAsync()
    {
        var queryable = container.GetItemLinqQueryable<Product>();

        using FeedIterator<Product> feed = queryable.ToFeedIterator();

        List<Product> results = [];

        while (feed.HasMoreResults)
        {
            FeedResponse<Product> response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                results.Add(item);
            }
        }

        return results;
    }
}