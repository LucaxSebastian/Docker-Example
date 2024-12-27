using Dotnet_with_MongoDB_and_Docker.Models;
using Dotnet_with_MongoDB_and_Docker.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_with_MongoDB_and_Docker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly MongoDbService _mongoDbService;

    public UsersController(MongoDbService mongoDbService)
        => _mongoDbService = mongoDbService;

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] Users user)
    {
        await _mongoDbService.CreateUserAsync(user);

        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpGet]
    public async Task<List<Users>> GetUsers()
        => await _mongoDbService.GetUsersAsync();

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] string name)
    {
        await _mongoDbService.AddToUsersAsync(id, name);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _mongoDbService.DeleteUserAsync(id);

        return NoContent();
    }
}