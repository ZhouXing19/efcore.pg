

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

public static class NpgsqlMigrationBuilderExtensions
{
    /// <summary>
    /// Returns true if the active provider in a migration is the Npgsql provider.
    /// </summary>
    /// The migrationBuilder from the parameters on <see cref="Migration.Up(MigrationBuilder)" /> or
    /// <see cref="Migration.Down(MigrationBuilder)" />.
    /// <returns>True if Npgsql is being used; false otherwise.</returns>
    public static bool IsNpgsql(this MigrationBuilder builder)
        => builder.ActiveProvider == typeof(NpgsqlMigrationBuilderExtensions).GetTypeInfo().Assembly.GetName().Name;

    public static MigrationBuilder EnsurePostgresExtension(
        this MigrationBuilder builder,
        string name,
        string? schema = null,
        string? version = null)
    {
        Check.NotEmpty(name, nameof(name));
        Check.NullButNotEmpty(schema, nameof(schema));
        Check.NullButNotEmpty(version, nameof(schema));

        var op = new AlterDatabaseOperation();
        op.GetOrAddPostgresExtension(schema, name, version);
        builder.Operations.Add(op);

        return builder;
    }

    [Obsolete("Use EnsurePostgresExtension instead")]
    public static MigrationBuilder CreatePostgresExtension(
        this MigrationBuilder builder,
        string name,
        string? schema = null,
        string? version = null)
        => EnsurePostgresExtension(builder, name, schema, version);

    [Obsolete("This no longer does anything and should be removed.")]
    public static MigrationBuilder DropPostgresExtension(
        this MigrationBuilder builder,
        string name)
        => builder;
}