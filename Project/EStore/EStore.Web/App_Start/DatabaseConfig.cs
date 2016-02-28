using System.Text.RegularExpressions;
using EStore.DL.Migrator.Common;

namespace EStore.Web
{
    public class DatabaseConfig
    {
        public static void MigrateDatabase()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EStoreEntities"].ConnectionString;
            var unwrappedConnectionString = Regex.Match(connectionString, "\"(.*)\"").ToString().Trim("\"".ToCharArray());
            var migrator = new Migrator(unwrappedConnectionString);
            migrator.Migrate(runner => runner.MigrateUp());
        }
    }
}