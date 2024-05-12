using BestStories.Controllers;
using BestStories.Models;
using BestStories.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace NewsControllerTests;

public class NewsControllerTests
{
    readonly Mock<INewsService> _mockNewsService = new();
    readonly Mock<ILogger<NewsController>> _mockLoggerService = new();

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetBestStories_ReturnsOkResult(int storyAmount)
    {
        //Arrange
        _mockNewsService.Setup(n =>  n
                 .GetBestStoriesAsync(storyAmount, It.IsAny<Func<BestStory, object>>(), It.IsAny<bool>()))
                 .ReturnsAsync(GetBestStoryTemplateList(storyAmount).ToList);
        var newsController = new NewsController(_mockLoggerService.Object, _mockNewsService.Object);

        //Act
        var okResult = await newsController.GetBestStoriesAsync(storyAmount);
        
        //Assert
        Assert.IsType<OkObjectResult>(okResult.Result as OkObjectResult);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetBestStories_ReturnsNotFoundResult(int storyAmount)
    {
        //Arrange
        _mockNewsService.Setup(n => n
                .GetBestStoriesAsync(storyAmount, It.IsAny<Func<BestStory, object>>(), It.IsAny<bool>()))
                .ReturnsAsync(GetBestStoryTemplateList(storyAmount).ToList);
        var newsController = new NewsController(_mockLoggerService.Object, _mockNewsService.Object);

        //Act
        var notFoundResult = await newsController.GetBestStoriesAsync(storyAmount);
        
        //Assert
        Assert.IsType<NotFoundObjectResult>(notFoundResult.Result as NotFoundObjectResult);
    }

    private IEnumerable<BestStory> GetBestStoryTemplateList(int storyAmount)
    {
        while (storyAmount > 0)
        {
            yield return new BestStory("", 1,1,[],1,1, "", "", "");
            storyAmount--;
        }
    }
}