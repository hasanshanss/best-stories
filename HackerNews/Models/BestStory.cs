namespace BestStories.Models;

public record BestStory(string By, int Descendants, int Id, IEnumerable<int> Kids, int Score, int Time, string Title, string Type, string Url);
