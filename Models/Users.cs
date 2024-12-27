using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotnet_with_MongoDB_and_Docker.Models;

[BsonIgnoreExtraElements]
public class Users
{
    [JsonIgnore]
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; init; }

    [BsonElement("name")]
    public string Name { get; init; } = null!;

    [BsonElement("email")]
    [EmailAddress]
    public string? Email { get; init; } = null!;

    [BsonElement("password")]
    public string? Password { get; init; } = null!;

    public Users()
        => Id = ObjectId.GenerateNewId().ToString();
}