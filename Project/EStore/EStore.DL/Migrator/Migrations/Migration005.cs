using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(5)]
    public class Migration005 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150327_update);
        }
    }
}