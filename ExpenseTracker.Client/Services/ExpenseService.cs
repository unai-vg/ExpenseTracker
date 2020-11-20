using ExpenseTracker.Client.Data;
using ExpenseTracker.Domain.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Client.Services
{
    public class ExpenseService
    {
        readonly AppSettings _settings;
        public ExpenseService(AppSettings settings)
        {
            _settings = settings;
        }
        public async Task<Bread> AddExpense(Expense e)
        {
            await Task.Yield();
            var error = await GetResponseFromPost(_settings.CreateUrl, e);

            return string.IsNullOrEmpty(error) ? Bread.Success("Expense added successfully") : Bread.Error("Could not add expense", error);
        }

        public async Task<Bread> UpdateExpense(Expense e)
        {
            await Task.Yield();
            var error = await GetResponseFromPost(_settings.UpdateUrl, e);

            return string.IsNullOrEmpty(error) ? Bread.Success("Expense edited successfully") : Bread.Error("Could not edit expense", error);
        }

        public async Task<Bread> RemoveExpense(Expense e)
        {
            await Task.Yield();
            var error = await GetResponseFromPost(_settings.DeleteUrl, e.Id);

            return string.IsNullOrEmpty(error) ? Bread.Success("Expense deleted successfully") : Bread.Error("Could not delete expense", error);
        }

        public async Task<(Bread, Expense)> GetExpense(Guid Id)
        {
            await Task.Yield();
            var expense = await GetResponseFromGet<Expense>($"{_settings.GetExpenseUrl}?id={Id}");
            var bread = expense == null ? Bread.Error("Could not retrieve expense") : Bread.Success("Expense retrieved successfully");

            return (bread, expense);
        }

        public async Task<(Bread, List<Expense>)> GetExpenses()
        {
            var result = await GetResponseFromGet<List<Expense>>(_settings.GetExpensesUrl);
            var bread = result == null ? Bread.Error("Could not retrieve expenses") : Bread.Success("Expenses retrieved successfully");

            return (bread, result);
        }

        private async Task<T> GetResponseFromGet<T>(string url)
        {
            try
            {
                var client = new HttpClient();
                var uri = new Uri(_settings.BaseUrl + url);
                var response = await client.GetAsync(uri.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);

                return result;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private async Task<string> GetResponseFromPost(string url, object objectToPost)
        {
            try
            {
                var contentString = Newtonsoft.Json.JsonConvert.SerializeObject(objectToPost);
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var uri = new Uri(_settings.BaseUrl + url);
                var response = await client.PostAsync(uri.ToString(), content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseContent);

                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
