using Sociam.Domain.Entities;
using System.Text.Json;

namespace Sociam.Infrastructure.Persistence;
public static class DatabaseSeeder
{
    private readonly static JsonSerializerOptions Options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };

    public static async Task SeedDatabaseAsync(this ApplicationDbContext context)
    {
        if (!context.Stories.Any())
        {
            var textStoriesJson = await File.ReadAllTextAsync($"..//Sociam.Infrastructure//Persistence//Seed//text-stories.json");
            var stories = JsonSerializer.Deserialize<IEnumerable<TextStory>>(textStoriesJson, Options);
            if (stories != null)
            {
                await context.Stories.AddRangeAsync(stories);
                await context.SaveChangesAsync();
            }
        }
    }
}
