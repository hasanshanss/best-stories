using BestStories.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BestStories.Controllers
{
    [ApiController]
    public class NewsController(ILogger<NewsController> logger, INewsService newsService) : ControllerBase
    {
         [HttpGet]   
         [Route(ApiRoutes.News.GetBestStories)]
         public async Task<ActionResult<IEnumerable<string>>> GetBestStoriesAsync(int storyAmount)
         {
             if (storyAmount < 1)
             {
                 return NotFound("Story amount should be more than 0.");
             }
             
             logger.Log(LogLevel.Information, "Fetching best stories...");
             
             var bestStories = await newsService.GetBestStoriesAsync(storyAmount, s=>s.Score, true);
             var storiesReturned = bestStories.Count;
             
             logger.Log(LogLevel.Information, "Elements returned: {0}", storiesReturned);
             
             return storiesReturned > 0 
                 ? Ok(bestStories) 
                 : NotFound("There isn't any best story.");
         }
     
    }
}
