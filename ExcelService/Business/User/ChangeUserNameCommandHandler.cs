namespace ExcelService.Business.User;
public record ChangeUserNameCommand(string UserName) : IRequest<bool>;
public class ChangeUserNameCommandHandler : IRequestHandler<ChangeUserNameCommand, bool>
{
    private readonly string? _filePath;
    private readonly ILogger<ChangeUserNameCommandHandler> _logger;
    public ChangeUserNameCommandHandler(
        IOptions<Setting> options,
        ILogger<ChangeUserNameCommandHandler> logger)
    {
        _filePath = options.Value.UserFile;
        _logger = logger;
    }
    public async Task<bool> Handle(ChangeUserNameCommand request, CancellationToken token)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        try
        {
            await File.WriteAllTextAsync(_filePath, request.UserName, token);

            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }        
    }
}
