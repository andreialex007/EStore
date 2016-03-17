using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(3)]
    public class Migration003 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150318_update);
        }
    }
}