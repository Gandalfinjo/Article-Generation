using ArticleGeneration.Repositories;

namespace ArticleGeneration
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
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
                            _logger.LogInformation("Fetched Transactions:");
                            for (int i = 0; i < 1; i++)
                            {
                                _logger.LogInformation($"Name: {transactions[i].Name}, AlternateName: {transactions[i].AlternateName}, TransactionType: {transactions[i].TransactionType?.Name}, Date: {transactions[i].PublishedOn}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while processing transactions.");
                }
            }
        }
    }
}
