using EStore.DL.Migrator.Common;
using FluentMigrator;

namespace EStore.DL.Migrator.Migrations
{
    [Migration(13)]
    public class Migration013 : NonReversableMigration
    {
        public override void Up()
        {
            Execute.Sql(SqlFiles._20150503_update);
        }
    }
}