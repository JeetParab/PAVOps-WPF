namespace PAVops.Core;

public interface IApplicationStore
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
    Task<string?> GetSettingAsync(string key, CancellationToken cancellationToken = default);
    Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default);
}
