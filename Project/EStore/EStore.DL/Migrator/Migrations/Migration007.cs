using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(7)]
    public class Migration007 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150327_update_3);
        }
    }
}