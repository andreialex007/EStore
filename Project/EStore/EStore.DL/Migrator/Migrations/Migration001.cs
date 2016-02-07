using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(1)]
    public class Migration001 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150207_update);
        }
    }
}