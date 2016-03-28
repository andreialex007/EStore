using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(10)]
    public class Migration010 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150329_update);
        }
    }
}