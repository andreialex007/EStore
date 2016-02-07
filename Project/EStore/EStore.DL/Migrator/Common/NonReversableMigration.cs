using FluentMigrator;

namespace EStore.DL.Migrator.Common
{
    public abstract class NonReversableMigration : Migration
    {
        public override void Down()
        {
        }
    }
}
