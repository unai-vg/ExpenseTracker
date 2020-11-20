using ExpenseTracker.Domain.Data;
using ExpenseTracker.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        readonly IExpenseService _service;
        readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IExpenseService service, ILogger<ExpenseController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("Ping")]
        public ActionResult Ping()
        {
            return Ok("Expense Tracker API");
        }

        [HttpPost("CreateExpense")]
        public ActionResult CreateExpense([FromBody] Expense e)
        {
            return Ok(_service.AddExpense(e));
        }

        [HttpPost("UpdateExpense")]
        public ActionResult UpdateExpense([FromBody] Expense e)
        {
            return Ok(_service.EditExpense(e));
        }

        [HttpGet("GetExpenses")]
        public ActionResult GetExpenses()
        {

            return Ok(_service.GetExpenses());
        }

        [HttpGet("GetExpense")]
        public ActionResult GetExpense(Guid id)
        {
            return Ok(_service.GetExpense(id));
        }

        [HttpPost("DeleteExpense")]
        public ActionResult DeleteExpense([FromBody] Guid id)
        {
            return Ok(_service.DeleteExpense(id));
        }
    }
}
