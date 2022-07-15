namespace ExcelService.Services;

public class ParseExpression : IParseExpression
{
    private const string pattern = "^=(Sum|Minus|Multi|Div)\\(([A-Za-z0-9\\,]+),([A-Za-z0-9\\,]+)\\)$";
    private readonly IMediator _mediator;

    public ParseExpression(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<string?> GetValue(string expression, CancellationToken token)
    {
        if (!expression.StartsWith("="))
            return expression;

        if (Regex.IsMatch(expression, pattern))
        {
            var match = Regex.Match(expression, pattern);

            var left = await ConvertValue(match.Groups[2].Value, token);
            var right = await ConvertValue(match.Groups[3].Value, token);

            if (expression.Contains("Sum"))
                return (left + right).ToString();

            if (expression.Contains("Minus"))
                return (left - right).ToString();

            if (expression.Contains("Multi"))
                return (left * right).ToString();

            if (expression.Contains("Div") && right != 0)
                return (left / right).ToString();

        }

        return default;
    }



    private async Task<decimal> ConvertValue(string value, CancellationToken token)
    {
        if (decimal.TryParse(value, out decimal resultValue))
            return resultValue;

        var resultGetExpr = await _mediator.Send(new GetExpressionValueQuery(value), token);

        if (decimal.TryParse(resultGetExpr.Value, out decimal resultValueFromExpr))
            return resultValueFromExpr;

        return default;
    }
}
