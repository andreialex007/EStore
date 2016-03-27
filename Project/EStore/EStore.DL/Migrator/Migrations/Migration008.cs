using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(8)]
    public class Migration008 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150327_update_4);
        }
    }
}