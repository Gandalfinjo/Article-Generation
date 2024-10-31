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
                        var transactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                        var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();

                        var oldtransactions = await transactionRepository.GetAllTransactionsAsync();
                        var newTransactions = await transactionRepository.GetNewOrUpdatedTransactionsAsync(_appStartTime);

                        var allTransactions = oldtransactions.Concat(newTransactions).ToList();

                        if (allTransactions == null || allTransactions.Count == 0)
                        {
                            _logger.LogInformation("No transactions found.");
                        }
                        else
                        {
                            string prompt = $"Based on the following data about a transaction, write an informative article:\n";
                            _logger.LogInformation("Successfully fetched transactions.");
                            _logger.LogInformation("Fetched Transactions:");

                            for (int i = 0; i < allTransactions.Count; i++)
                            {
                                _logger.LogInformation($"Name: {allTransactions[i].Name}");

                                prompt += $"- Transaction Name: {allTransactions[i].Name}" +
                                          $"- Transaction Description: {allTransactions[i].Description ?? ""}" +
                                          $"- Live: {allTransactions[i].Live}" +
                                          $"- Transaction Value: {allTransactions[i].Value ?? 0}" +
                                          $"- Transaction Type: {allTransactions[i].TransactionType?.Name}" +
                                          $"- Transaction Stage: {allTransactions[i].TransactionProcurementStage?.Name}" +
                                          $"- Published On: {allTransactions[i].PublishedOn ?? new DateTime()}" +
                                          $"- Selected Currency: {allTransactions[i].SelectedCurrency ?? ""}" +
                                          $"- Transaction Instrument Type: {allTransactions[i].TransactionInstrumentType?.TransactionInstrumentTypeName}" +
                                          $"- Product Category: {allTransactions[i].ProductCategory?.Name}" +
                                          $"- Market Type: {allTransactions[i].MarketType?.MarketTypeName}" +
                                          $"- Structure: {allTransactions[i].Structure?.StructureName}" +
                                          $"- BankPricing: {allTransactions[i].BankPricing ?? 0}" +
                                          $"- BankOfferPrice: {allTransactions[i].BankOfferPrice ?? 0}" +
                                          $"- LoanReference Name: {allTransactions[i].BankBenchMark?.Name}" +
                                          $"- LoanReference City: {allTransactions[i].BankBenchMark?.City}" +
                                          $"- Product Type: {allTransactions[i].ProductType?.Name}";

                                if (allTransactions[i].Tranches != null)
                                {
                                    prompt += $"- Tranches:\n";

                                    foreach (var tranche in allTransactions[i].Tranches)
                                    {
                                        prompt += $"- Tranche Name: {allTransactions[i].Tranches?.First().Name}" +
                                                  $"- Tranche Value: {allTransactions[i].Tranches?.First().Value}";
                                        
                                        if (tranche.TrancheCompanyRelationships != null)
                                        {
                                            prompt += $"- Companies: \n";

                                            foreach (var trancheCompanyRelationship in allTransactions[i].Tranches.First().TrancheCompanyRelationships)
                                            {
                                                prompt += $"\t- Company Name: {trancheCompanyRelationship.Company.Name}";
                                            }
                                        }
                                    }
                                }

                                var article = await _openAIService.GenerateArticleAsync(prompt);
                                _logger.LogInformation($"{article}");

                                try
                                {
                                    await articleRepository.AddArticleAsync(article, allTransactions[i].TransactionId);
                                    _logger.LogInformation("Successfully added an article to the database!");
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "An error occured while trying to add an article to the database.");
                                }
                                
                                prompt = $"Based on the following data about a transaction, write an informative article:\n";

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
