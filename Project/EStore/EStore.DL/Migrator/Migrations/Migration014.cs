using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(14)]
    public class Migration014 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150503_update_2);
        }
    }
}