namespace ExcelService.Services;

public interface IParseExpression
{
    Task<string?> GetValue(string? expression, CancellationToken token = default);
}
