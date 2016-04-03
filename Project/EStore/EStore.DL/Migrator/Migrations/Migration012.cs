using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(12)]
    public class Migration012 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150404_update);
        }
    }
}