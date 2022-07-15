namespace ExcelService.Business.Table;
public record ChangeExpressionValueCommand(Cell Cell) : IRequest<string?>;
public class ChangeExpressionValueCommandHandler : IRequestHandler<ChangeExpressionValueCommand, string?>
{
    private readonly string? _dirName;
    private readonly IParseExpression _parseExpression;
    private readonly ILogger<ChangeExpressionValueCommandHandler> _logger;
    public ChangeExpressionValueCommandHandler(
        IOptions<Setting> options,
        IParseExpression parseExpression,
        ILogger<ChangeExpressionValueCommandHandler> logger)
    {
        _dirName = options.Value.TableDir;
        _parseExpression = parseExpression;
        _logger = logger;
    }
    public async Task<string?> Handle(ChangeExpressionValueCommand request, CancellationToken token)
    {
        if (!Directory.Exists(_dirName))
            Directory.CreateDirectory(_dirName);

        var fileName = Path.Combine(_dirName, request.Cell.Number);

        var value = await _parseExpression.GetValue(request.Cell.Expression);

        try
        {
            await File.WriteAllLinesAsync(fileName, new[] { request.Cell.Expression, value }, token);

            return value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return default;
        }     
    }
}
