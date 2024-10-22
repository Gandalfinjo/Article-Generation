using ArticleGeneration.Repositories;
using ArticleGeneration.Services;

namespace ArticleGeneration
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Worker> _logger;
        private readonly IOpenAIService _openAIService;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger, IOpenAIService openAIService)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _openAIService = openAIService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string prompt = "What is the capital of Serbia?";

                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var repository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                        var transactions = await repository.GetAllTransactionsAsync();

                        if (transactions == null || transactions.Count == 0)
                        {
                            _logger.LogInformation("No transactions found.");
                        }
                        else
                        {
                            var article = await _openAIService.GenerateArticleAsync(prompt);
                            _logger.LogInformation($"{article}");
                            //_logger.LogInformation("Fetched Transactions:");
                            //for (int i = 5127; i < 5128; i++)
                            //{
                            //    // _logger.LogInformation($"Name: {transactions[i].Name}, Tranche: {transactions[i].Tranches?.First().Name}, Company: {transactions[i].Tranches.FirstOrDefault()?.TrancheCompanyRelationships.FirstOrDefault()?.Company.Name}");
                            //    _logger.LogInformation($"Name: {transactions[i].Name}, Tranche: {transactions[i].Tranches?.First().Name}\n");
                            //    _logger.LogInformation($"Companies: \n");
                            //    foreach (var trancheCompanyRelationship in transactions[i].Tranches.First().TrancheCompanyRelationships)
                            //    {
                            //        _logger.LogInformation($"Company Name: {trancheCompanyRelationship.Company.Name}");
                            //    }
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while processing transactions.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
