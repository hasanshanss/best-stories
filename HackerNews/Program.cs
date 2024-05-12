using BestStories.Services;
using BestStories.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
    .ClearProviders()
    .AddConsole();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandlers();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddHttpClient<INewsService, NewsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.MapControllers();

app.Run();
