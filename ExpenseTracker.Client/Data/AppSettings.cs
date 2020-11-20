using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Client.Data
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string UpdateUrl { get; set; }
        public string CreateUrl { get; set; }
        public string GetExpenseUrl { get; set; }
        public string GetExpensesUrl { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            BaseUrl = configuration.GetSection("AppSettings:BaseUrl").Value;
            DeleteUrl = configuration.GetSection("AppSettings:DeleteUrl").Value;
            UpdateUrl = configuration.GetSection("AppSettings:UpdateUrl").Value;
            CreateUrl = configuration.GetSection("AppSettings:CreateUrl").Value;
            GetExpenseUrl = configuration.GetSection("AppSettings:GetExpenseUrl").Value;
            GetExpensesUrl = configuration.GetSection("AppSettings:GetExpensesUrl").Value;
        }
    }
}
