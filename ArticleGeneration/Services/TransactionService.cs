using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleGeneration.Models;
using ArticleGeneration.Repositories;

namespace ArticleGeneration.Services
{
    /// <summary>
    /// Provides the implementation for business logic related to transactions.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository repository)
        {
            _transactionRepository = repository;
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously retrieves all transactions from the database by delegating to the repository layer.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Asynchronously retrieves new or updated transactions from the database by delegating to the repository layer.
        /// </summary>
        /// <param name="lastChecked">The date and time when the last check for new or updated transactions was made. The method will retrieve transactions that have been created or modified since this timestamp.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a list of <see cref="Transaction"/> objects.</returns>
        public async Task<List<Transaction>> GetNewOrUpdatedTransactionsAsync(DateTime lastChecked)
        {
            return await _transactionRepository.GetNewOrUpdatedTransactionsAsync(lastChecked);
        }

        /// <inheritdoc />
        /// <summary>
        /// Generates a prompt string based on the provided transaction data, to be used in an API request.
        /// </summary>
        /// <param name="transaction">The transaction object containing the data used to generate the prompt.</param>
        /// <returns>A string representing the generated prompt that will be sent to the OpenAI API.</returns>
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
