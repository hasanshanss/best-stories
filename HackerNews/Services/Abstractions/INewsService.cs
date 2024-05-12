using System.Collections.ObjectModel;
using System.Linq.Expressions;
using BestStories.Models;

namespace BestStories.Services.Abstractions;

public interface INewsService
{
    public Task<int[]> GetBestStoryIdsAsync(int storyAmount);
    public Task<IList<BestStory>> GetBestStoriesAsync(
        int storyAmount,
        Func<BestStory, object>? orderBy = null, 
        bool orderDescending = false);
}