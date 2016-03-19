using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(4)]
    public class Migration004 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150319_update);
        }
    }
}