using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleGeneration.Models;
using ArticleGeneration.Repositories;

namespace ArticleGeneration.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository repository)
        {
            _transactionRepository = repository;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }
        public async Task<List<Transaction>> GetNewOrUpdatedTransactionsAsync(DateTime lastChecked)
        {
            return await _transactionRepository.GetNewOrUpdatedTransactionsAsync(lastChecked);
        }

        public string GeneratePrompt(Transaction transaction)
        {
            string prompt = $"Based on the following data about a transaction, write an informative article:\n";

            prompt += $"- Transaction Name: {transaction.Name}" +
                                      $"- Transaction Description: {transaction.Description ?? ""}" +
                                      $"- Live: {transaction.Live}" +
                                      $"- Transaction Value: {transaction.Value ?? 0}" +
                                      $"- Transaction Type: {transaction.TransactionType?.Name}" +
                                      $"- Transaction Stage: {transaction.TransactionProcurementStage?.Name}" +
                                      $"- Published On: {transaction.PublishedOn ?? null}" +
                                      $"- Selected Currency: {transaction.SelectedCurrency ?? ""}" +
                                      $"- Transaction Instrument Type: {transaction.TransactionInstrumentType?.TransactionInstrumentTypeName}" +
                                      $"- Product Category: {transaction.ProductCategory?.Name}" +
                                      $"- Market Type: {transaction.MarketType?.MarketTypeName}" +
                                      $"- Structure: {transaction.Structure?.StructureName}" +
                                      $"- BankPricing: {transaction.BankPricing ?? 0}" +
                                      $"- BankOfferPrice: {transaction.BankOfferPrice ?? 0}" +
                                      $"- LoanReference Name: {transaction.BankBenchMark?.Name}" +
                                      $"- LoanReference City: {transaction.BankBenchMark?.City}" +
                                      $"- Product Type: {transaction.ProductType?.Name}";

            if (transaction.Tranches != null && transaction.Tranches.Count > 0)
            {
                prompt += $"- Tranches:\n";

                foreach (var tranche in transaction.Tranches)
                {
                    prompt += $"- Tranche Name: {transaction.Tranches!.First().Name}" +
                              $"- Tranche Value: {transaction.Tranches!.First().Value}";

                    if (tranche.TrancheCompanyRelationships != null)
                    {
                        prompt += $"- Companies: \n";

                        foreach (var trancheCompanyRelationship in transaction.Tranches!.First().TrancheCompanyRelationships)
                        {
                            prompt += $"\t- Company Name: {trancheCompanyRelationship.Company.Name}";
                        }
                    }
                }
            }

            return prompt;
        }
    }
}
