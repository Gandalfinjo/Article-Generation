using ArticleGeneration.Repositories;
using ArticleGeneration.Services;

namespace ArticleGeneration
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Worker> _logger;
        private readonly IOpenAIService _openAIService;
        private readonly DateTime _appStartTime;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger, IOpenAIService openAIService)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _openAIService = openAIService;
            _appStartTime = DateTime.UtcNow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();
                        var articleService = scope.ServiceProvider.GetRequiredService<IArticleService>();

                        var oldtransactions = await transactionService.GetAllTransactionsAsync();
                        var newTransactions = await transactionService.GetNewOrUpdatedTransactionsAsync(_appStartTime);

                        var allTransactions = oldtransactions.Concat(newTransactions).ToList();

                        if (allTransactions == null || allTransactions.Count == 0)
                        {
                            _logger.LogInformation("No transactions found.");
                        }
                        else
                        {
                            _logger.LogInformation("Successfully fetched transactions.");
                            _logger.LogInformation("Fetched Transactions:");

                            for (int i = 0; i < allTransactions.Count; i++)
                            {
                                _logger.LogInformation($"Name: {allTransactions[i].Name}");

                                string prompt = transactionService.GeneratePrompt(allTransactions[i]);

                                var article = await _openAIService.GenerateArticleAsync(prompt);
                                _logger.LogInformation($"{article}");

                                try
                                {
                                    await articleService.AddArticleAsync(article, allTransactions[i].TransactionId);
                                    _logger.LogInformation("Successfully added an article to the database!");
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "An error occured while trying to add an article to the database.");
                                }

                                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while processing transactions.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
