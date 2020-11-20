using ExpenseTracker.Domain.Data;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Domain.Services
{
    public interface IExpenseService
    {
        string AddExpense(Expense expense);
        string EditExpense(Expense expense);
        string DeleteExpense(Guid id);
        List<Expense> GetExpenses();
        Expense GetExpense(Guid id);
    }
}
