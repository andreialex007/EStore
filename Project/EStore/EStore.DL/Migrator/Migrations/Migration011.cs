using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(11)]
    public class Migration011 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150330_update);
        }
    }
}