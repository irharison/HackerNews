using HackerNews.Interfaces;
using HackerNews.NewsReader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddConsole();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<INewsStore, ArticleStore>();
builder.Services.AddSingleton<INewsReader, NewsReader>();
builder.Services.AddSingleton<INewsStoreLock, NewsStoreLock>();
builder.Services.AddSingleton<INewsRefresher, NewsReaderRefresher>();

var app = builder.Build();

app.Logger.LogInformation("HackerNews API Starting");

INewsRefresher refresher = app.Services.GetService<INewsRefresher>();
refresher.StartTask();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

refresher.StopTask();
