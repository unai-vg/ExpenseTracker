using ExpenseTracker.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Domain.Services
{
    public class ExpenseService : IExpenseService
    {
        readonly ILogger _logger;
        readonly ExpenseDbContext _dbContext;

        public ExpenseService(
            ILogger<ExpenseService> logger,
            ExpenseDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            _dbContext = dbContext;
            _logger = logger;
        }

        public string AddExpense(Expense expense)
        {
            try
            {
                expense.Id = Guid.NewGuid();
                _dbContext.Expenses.Add(expense);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"[AddExpense]: Could not add a new entry: {e.Message} - {e.InnerException}");

                return "Could not add the specified expense";
            }
            _logger.LogInformation($"[AddExpense]: Expense '{expense.Id}' added successfully");

            return null;
        }

        public string EditExpense(Expense expense)
        {
            try
            {
                var expenseToUpdate = _dbContext.Expenses.FirstOrDefault(e => e.Id == expense.Id);
                if (expenseToUpdate == null)
                {
                    _logger.LogInformation($"[EditExpense]: Expense '{expense.Id}' could not be found");

                    return "Could not find an expense with the specified id";
                }
                _dbContext.Entry(expenseToUpdate).State = EntityState.Detached;
                _dbContext.Entry(expense).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"[EditExpense]: Could not edit the specified entry: {e.Message} - {e.InnerException}");

                return "Could not edit the expense";
            }
            _logger.LogInformation($"[EditExpense]: Expense '{expense.Id}' edited successfully");

            return null;
        }

        public List<Expense> GetExpenses()
        {
            try
            {
                List<Expense> result = _dbContext.Expenses.ToList();
                _logger.LogInformation($"[GetExpenses]: Expense list retrieved successfully");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"[GetExpenses]: An error happened while retrieving the expense list: {e.Message} - {e.InnerException}");

                return null;
            }
        }

        public string DeleteExpense(Guid id)
        {
            try
            {
                var expense = _dbContext.Expenses.FirstOrDefault(e => e.Id == id);
                if (expense == null)
                {
                    _logger.LogInformation($"[DeleteExpense]: Expense '{id}' could not be found");

                    return "Could not find an expense with the specified id";
                }
                _dbContext.Expenses.Remove(expense);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"[DeleteExpense]: Could not delete the specified entry: {e.Message} - {e.InnerException}");

                return "Could not delete the specified expense";
            }
            _logger.LogInformation($"[DeleteExpense]: Expense '{id}' removed successfully");

            return null;
        }

        public Expense GetExpense(Guid id)
        {
            try
            {
                Expense result = _dbContext.Expenses.FirstOrDefault(e => e.Id == id);
                if (result == null)
                {
                    _logger.LogInformation($"[GetExpense]: Could not find expense '{id}'");
                }
                else
                {
                    _logger.LogInformation($"[GetExpense]: Expense '{id}' retrieved successfully");
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"[GetExpense]: An error happened while retrieving expense '{id}': {e.Message} - {e.InnerException}");

                return null;
            }
        }
    }
}
