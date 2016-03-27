using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(6)]
    public class Migration006 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150327_update_2);
        }
    }
}