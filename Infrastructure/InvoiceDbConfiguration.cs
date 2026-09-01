using System;
using Microsoft.Extensions.Configuration;

namespace InvoiceImporter.Infrastructure
{
    /// <summary>
    /// Builds the importer's runtime configuration and resolves the database
    /// connection string. The value is read (in precedence order) from
    /// environment variables, user-secrets, then appsettings.json — never
    /// baked into source, so no server/credential is committed to the repo.
    /// </summary>
    public static class InvoiceDbConfiguration
    {
        public const string ConnectionStringName = "InvoiceDb";

        public static IConfigurationRoot BuildConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddUserSecrets(typeof(InvoiceDbConfiguration).Assembly, optional: true)
                .AddEnvironmentVariables()
                .Build();

        public static string GetConnectionString()
        {
            var connectionString = BuildConfiguration().GetConnectionString(ConnectionStringName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"No database connection string configured. Set ConnectionStrings:{ConnectionStringName} " +
                    "in appsettings.json, in user-secrets, or via the environment variable " +
                    $"'ConnectionStrings__{ConnectionStringName}'.");
            }

            return connectionString;
        }
    }
}
