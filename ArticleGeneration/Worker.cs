using ArticleGeneration.Repositories;
using ArticleGeneration.Services;

namespace ArticleGeneration
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Worker> _logger;
        private readonly IOpenAIService _openAIService;
        private DateTime _appStartTime;

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
                        var repository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                        // var transactions = await repository.GetAllTransactionsAsync();
                        var transactions = await repository.GetNewOrUpdatedTransactionsAsync(_appStartTime);

                        if (transactions == null || transactions.Count == 0)
                        {
                            _logger.LogInformation("No transactions found.");
                        }
                        else
                        {
                            string prompt = $"Based on the following data about a transaction, write an informative article:\n";
                            // for (int i = 5127; i < 5128; i++)
                            _logger.LogInformation("Fetched Transactions:");
                            for (int i = 0; i < transactions.Count; i++)
                            {
                                _logger.LogInformation($"Name: {transactions[i].Name}");

                                prompt += $"- Transaction Name: {transactions[i].Name}" +
                                          $"- Transaction Description: {transactions[i].Description ?? ""}" +
                                          $"- Live: {transactions[i].Live}" +
                                          $"- Transaction Value: {transactions[i].Value ?? 0}" +
                                          $"- Transaction Type: {transactions[i].TransactionType?.Name}" +
                                          $"- Transaction Stage: {transactions[i].TransactionProcurementStage?.Name}" +
                                          $"- Published On: {transactions[i].PublishedOn ?? new DateTime()}" +
                                          $"- Selected Currency: {transactions[i].SelectedCurrency ?? ""}" +
                                          $"- Transaction Instrument Type: {transactions[i].TransactionInstrumentType?.TransactionInstrumentTypeName}" +
                                          $"- Product Category: {transactions[i].ProductCategory?.Name}" +
                                          $"- Market Type: {transactions[i].MarketType?.MarketTypeName}" +
                                          $"- Structure: {transactions[i].Structure?.StructureName}" +
                                          $"- BankPricing: {transactions[i].BankPricing ?? 0}" +
                                          $"- BankOfferPrice: {transactions[i].BankOfferPrice ?? 0}" +
                                          $"- LoanReference Name: {transactions[i].BankBenchMark?.Name}" +
                                          $"- LoanReference City: {transactions[i].BankBenchMark?.City}" +
                                          $"- Product Type: {transactions[i].ProductType?.Name}";

                                if (transactions[i].Tranches != null)
                                {
                                    prompt += $"- Tranches:\n";
                                    foreach (var tranche in transactions[i].Tranches)
                                    {
                                        prompt += $"- Tranche Name: {transactions[i].Tranches?.First().Name}" +
                                                  $"- Tranche Value: {transactions[i].Tranches?.First().Value}"; 
                                        
                                        if (tranche.TrancheCompanyRelationships != null)
                                        {
                                            prompt += $"- Companies: \n";
                                            foreach (var trancheCompanyRelationship in transactions[i].Tranches.First().TrancheCompanyRelationships)
                                            {
                                                prompt += $"\t- Company Name: {trancheCompanyRelationship.Company.Name}";
                                            }
                                        }
                                    }
                                }
                            }

                            //for (int i = 0; i < transactions.Count; i++)
                            //{
                            //    // _logger.LogInformation($"Name: {transactions[i].Name}, Tranche: {transactions[i].Tranches?.First().Name}, Company: {transactions[i].Tranches.FirstOrDefault()?.TrancheCompanyRelationships.FirstOrDefault()?.Company.Name}");
                            //    _logger.LogInformation($"Name: {transactions[i].Name}, Tranche: {transactions[i].Tranches?.First().Name}\n");
                            //    _logger.LogInformation($"Companies: \n");
                            //    foreach (var trancheCompanyRelationship in transactions[i].Tranches.First().TrancheCompanyRelationships)
                            //    {
                            //        _logger.LogInformation($"Company Name: {trancheCompanyRelationship.Company.Name}");
                            //    }
                            //}

                            var article = await _openAIService.GenerateArticleAsync(prompt);
                            _logger.LogInformation($"{article}");
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
