using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Application;
using Okane.Domain;

namespace Okane.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expensesService;

    public ExpensesController(IExpenseService expensesService) => 
        _expensesService = expensesService;
    
    // POST /expenses
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
    public ActionResult<ExpenseResponse> Post(CreateExpenseRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(_expensesService.Register(request));
    }
    
    // PUT /expenses
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    public ActionResult<ExpenseResponse> Put(int id, UpdateExpenseRequest request)
    {
        var response = _expensesService.Update(id, request);
        return Ok(response);
    }

    // GET /expenses
    [HttpGet]
    public IEnumerable<ExpenseResponse> Get(string? category) => 
        _expensesService.Search(category);

    // GET /expenses/:id
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ExpenseResponse> Get(int id)
    {
        var response = _expensesService.ById(id);
        if (response == null)
            return NotFound();
        
        return Ok(response);
    }

    // DELETE /expenses/:id
    [HttpDelete("{id}")]
    public bool Delete(int id) => 
        _expensesService.Delete(id);
}