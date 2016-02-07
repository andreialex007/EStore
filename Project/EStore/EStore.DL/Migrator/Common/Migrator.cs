using System;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace EStore.DL.Migrator.Common
{
    public class Migrator
    {
        readonly string _connectionString;

        public Migrator(string connectionString)
        {
            this._connectionString = connectionString;
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; private set; }
        }
        
        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 0 };
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
            var assembly = Assembly.GetExecutingAssembly();
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var migrationContext = new RunnerContext(announcer);
            var processor = factory.Create(this._connectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runnerAction(runner);
        }
    }
}
