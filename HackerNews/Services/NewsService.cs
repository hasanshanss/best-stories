using BestStories.Models;
using BestStories.Services.Abstractions;
using Microsoft.Net.Http.Headers;

namespace BestStories.Services;

public class NewsService : INewsService
{
    private readonly HttpClient _httpClient;

    public NewsService(HttpClient httpClient, IConfiguration configuration)
    {
        var apiUrl = configuration["NewsApi:BaseUrl"];
        var apiVersion = configuration["NewsApi:Version"];
        var baseUrl = $"{apiUrl}/v{apiVersion}/";

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    }
    public async Task<IList<BestStory>> GetBestStoriesAsync(
        int storyAmount,
        Func<BestStory, object>? orderBy = null, 
        bool orderDescending = false)
    {
        if (storyAmount < 1)
        {
            return [];
        }

        List<BestStory> bestStories = new List<BestStory>();
        foreach (var storyTask in await GetBestStoryTasksAsync())
        {
            if (storyTask.Result != null)
            {
                bestStories.Add(storyTask.Result);
            }
        }

        if (orderBy == null)
        {
            return bestStories;
        }
        
        var orderedBestStories = orderDescending 
            ? bestStories.OrderByDescending(orderBy) 
            : bestStories.OrderBy(orderBy);
        
        return orderedBestStories.ToList();

        #region local functions
        
        async Task<Task<BestStory?>[]> GetBestStoryTasksAsync()
        {
            var bestStoryIds = await GetBestStoryIdsAsync(storyAmount);
            var bestStoriesCount = bestStoryIds.Length - 1;
            
            Task<BestStory?>[] bestStoryTasks = new Task<BestStory?>[bestStoriesCount];
        
            for (var i = 0; i < bestStoriesCount; i++)
            {
                bestStoryTasks[i] = _httpClient.GetFromJsonAsync<BestStory>($"item/{bestStoryIds[i]}.json");
            }
        
            await Task.WhenAll(bestStoryTasks);
            return bestStoryTasks;
        }
        
        #endregion
    }
    
    public async Task<int[]> GetBestStoryIdsAsync(int storyAmount = 0)
    {
        if (storyAmount < 1)
        {
            return [];
        }
        
        var bestStoryIds = await _httpClient.GetFromJsonAsync<IEnumerable<int>>("beststories.json");
        if (bestStoryIds != null)
        {
            return bestStoryIds
                .Take(storyAmount)
                .ToArray(); 
        }

        return [];
    }
}