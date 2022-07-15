namespace ExcelService.Business.Table;
public record GetExpressionValueQuery(string? Number) : IRequest<Cell>;
public class GetExpressionValueQueryHandler : IRequestHandler<GetExpressionValueQuery, Cell>
{
    private readonly string? _dirName;
    private readonly ILogger<GetExpressionValueQueryHandler> _logger;
    public GetExpressionValueQueryHandler(
        IOptions<Setting> options,
        ILogger<GetExpressionValueQueryHandler> logger)
    {
        _dirName = options.Value.TableDir;
        _logger = logger;
    }
    public async Task<Cell> Handle(GetExpressionValueQuery request, CancellationToken token)
    {
        var fileName = Path.Combine(_dirName, request.Number);
        if (!File.Exists(fileName))
        {
            _logger.LogWarning("File not exist");
            return new();
        }

        var fileLines = await File.ReadAllLinesAsync(fileName, token);

        return new()
        {
            Number = request.Number,
            Expression = fileLines[0],
            Value = fileLines[1]
        };
    }
}
