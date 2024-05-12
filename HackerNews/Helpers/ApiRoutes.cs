namespace BestStories;

public static class ApiRoutes
{
    public static class News
    {
        public const string Root = "api";
        public const string Base = Root + "/News";

        public const string GetBestStories = Base + "/BestStories/{storyAmount}";

    }
}