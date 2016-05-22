using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(15)]
    public class Migration015 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150522_update);
        }
    }
}