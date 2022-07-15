namespace ExcelService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("name")]
    public async Task<string> GetName(CancellationToken token = default) =>
         await _mediator.Send(new GetUserNameQuery(), token);


    [HttpPost("name")]
    public async Task<bool> ChangeName(string name, CancellationToken token = default) =>
         await _mediator.Send(new ChangeUserNameCommand(name), token);
}
