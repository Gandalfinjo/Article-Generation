using ArticleGeneration;
using ArticleGeneration.Data;
using ArticleGeneration.Repositories;
using ArticleGeneration.Services;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddHttpClient<IOpenAIService, OpenAIService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
