using Microsoft.EntityFrameworkCore;
using PAVops.Core;

namespace PAVops.Infrastructure;

public sealed class ApplicationSetting
{
    public string Key { get; set; } = "";
    public string Value { get; set; } = "";
}

public sealed class PAVopsDbContext(DbContextOptions<PAVopsDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationSetting> Settings => Set<ApplicationSetting>();
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ApplicationSetting>().HasKey(x => x.Key);
}

public sealed class ApplicationStore(string databasePath) : IApplicationStore
{
    private PAVopsDbContext Open()
    {
        var connection = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = databasePath };
        return new(new DbContextOptionsBuilder<PAVopsDbContext>().UseSqlite(connection.ToString()).Options);
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(databasePath))!);
        await using var db = Open();
        await db.Database.EnsureCreatedAsync(cancellationToken);
    }

    public async Task<string?> GetSettingAsync(string key, CancellationToken cancellationToken = default)
    {
        await using var db = Open();
        return (await db.Settings.FindAsync([key], cancellationToken))?.Value;
    }

    public async Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        await using var db = Open();
        var setting = await db.Settings.FindAsync([key], cancellationToken);
        if (setting is null) db.Settings.Add(new() { Key = key, Value = value });
        else setting.Value = value;
        await db.SaveChangesAsync(cancellationToken);
    }
}
