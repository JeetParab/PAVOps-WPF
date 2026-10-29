using Microsoft.Data.Sqlite;
using PAVops.Infrastructure;
namespace PAVops.Tests;
public sealed class StoreTests
{
    [Fact]
    public async Task SettingsSurviveReopeningAndInitializationWithoutDuplicateKeys()
    {
        var directory = Path.Combine(Path.GetTempPath(), "PAVops-tests", Guid.NewGuid().ToString("N"));
        var path = Path.Combine(directory, "pavops.db");
        try
        {
            var store = new ApplicationStore(path);
            await store.InitializeAsync();
            Assert.Null(await store.GetSettingAsync("theme"));
            await store.SetSettingAsync("theme", "light");
            var reopened = new ApplicationStore(path);
            await reopened.InitializeAsync();
            Assert.Equal("light", await reopened.GetSettingAsync("theme"));
            await reopened.SetSettingAsync("theme", "dark");
            Assert.Equal("dark", await store.GetSettingAsync("theme"));
            await Assert.ThrowsAsync<ArgumentException>(() => store.SetSettingAsync(" ", "value"));
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory)) Directory.Delete(directory, true);
        }
    }
}
