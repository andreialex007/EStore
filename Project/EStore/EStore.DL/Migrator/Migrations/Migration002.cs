using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(2)]
    public class Migration002 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150228_update);
        }
    }
}