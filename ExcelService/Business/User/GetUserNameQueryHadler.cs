namespace ExcelService.Business.User;
public record GetUserNameQuery : IRequest<string>;
public class GetUserNameQueryHadler : IRequestHandler<GetUserNameQuery, string?>
{
    private readonly string? _filePath;
    private readonly ILogger<GetUserNameQueryHadler> _logger;
    public GetUserNameQueryHadler(
        IOptions<Setting> options,
        ILogger<GetUserNameQueryHadler> logger)
    {
        _filePath = options.Value.UserFile;
        _logger = logger;
    }

    public async Task<string?> Handle(GetUserNameQuery request, CancellationToken token)
    {
        if (!File.Exists(_filePath))
        {
            _logger.LogWarning("File not exist");
            return default;
        }
           

        return await File.ReadAllTextAsync(_filePath, token);
    }
}
