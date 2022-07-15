namespace ExcelService.Controllers;

[ApiController]
[Route("[controller]")]
public class TableController : Controller
{
    private readonly IMediator _mediator;

    public TableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("expression")]
    public async Task<Cell> GetExpressionValue(string? number, CancellationToken token = default) =>
         await _mediator.Send(new GetExpressionValueQuery(number), token);


    [HttpPost("expression")]
    public async Task<string?> ChangeName(Cell cell, CancellationToken token = default) =>
         await _mediator.Send(new ChangeExpressionValueCommand(cell), token);
}
