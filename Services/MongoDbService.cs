using Dotnet_with_MongoDB_and_Docker.Models;
using Dotnet_with_MongoDB_and_Docker.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dotnet_with_MongoDB_and_Docker.Services;

public class MongoDbService
{
    private readonly IMongoCollection<Users> _usersCollection = null!;

    public MongoDbService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _usersCollection = database.GetCollection<Users>(settings.Value.CollectionName);
    }

    public async Task CreateUserAsync(Users user)
        => await _usersCollection.InsertOneAsync(user);

    public async Task<List<Users>> GetUsersAsync()
        => await _usersCollection.Find(new BsonDocument()).ToListAsync();

    public async Task AddToUsersAsync(string id, string name)
    {
        var filter = Builders<Users>.Filter.Eq(u => u.Id, id);
        var update = Builders<Users>.Update.AddToSet("Name", name);

        var result = await _usersCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteUserAsync(string id)
        => await _usersCollection.DeleteOneAsync(u => u.Id == id);
}