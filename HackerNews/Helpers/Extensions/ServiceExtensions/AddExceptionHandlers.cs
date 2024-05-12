using BestStories.Helpers.ExceptionHandlers;

public static class ServiceExtensions
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
    }
}