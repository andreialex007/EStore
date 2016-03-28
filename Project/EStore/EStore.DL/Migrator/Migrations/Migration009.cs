using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(9)]
    public class Migration009 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150328_update);
        }
    }
}