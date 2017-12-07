using System;
using System.Configuration;
//using System.Data.EntityClient;
//using Microsoft.Practices.Unity;
//using System.Data.Entity;
//using Microsoft.Practices.Unity;
//using Unity;
//using Unity.Injection;
using Microsoft.SqlServer.Management.Smo;

namespace BookService.Testing.Core
{
    public class IntegrationTestBase
    {
        // ReSharper disable once InconsistentNaming
        private const string DBUPDATEFOLDER = "DBUpdate";

        private static IntegrationTestLocalDbManager _databaseManager;

        //public static IUnityContainer InMemoryUnityContainer { get; private set; }

        public static IntegrationTestLocalDbManager DatabaseManager
        {
            get
            {
                return _databaseManager ?? (_databaseManager = new IntegrationTestLocalDbManager(CreateUniqueDatabaseName()));
            }
        }

        public static void SetupLocalDb()
        {
            _databaseManager = new IntegrationTestLocalDbManager(CreateUniqueDatabaseName());

            //var updateRunner = new UpdateRunner(_databaseManager.ConnectionString, DBUPDATEFOLDER);
            //updateRunner.RunDatabaseUpdate();
        }

        public static void CleanupLocalDb()
        {
            DatabaseManager.Dispose();
        }

        public static void SetInMemoryUnityContainer()
        {
            //var container = new UnityContainer();
            //container.RegisterType<ITestContext, IntegrationTestContext>(
            //    new InjectionProperty("PooledCoverFundDbConnection", GetDiagnosticsEntityConnectionString()),
            //    new InjectionProperty("BusinessDbConnection", GetBusinessConnectionString()),
            //    new InjectionProperty("ServerDbConnection", GetServerCurrencyConnectionString()));

            //SetupTranslationConnectionString();

            //InMemoryUnityContainer = container;

            GetDiagnosticsEntityConnectionString();
        }

        protected static string GetDiagnosticsEntityConnectionString()
        {
            //var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //var connectionStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            //connectionStringsSection.ConnectionStrings["PooledCoverFundModel"].ConnectionString =
            //    new EntityConnectionStringBuilder
            //    {
            //        ProviderConnectionString = DatabaseManager.ConnectionString,
            //        Provider = "System.Data.SqlClient",
            //        Metadata = "res://*/PooledCoverFundModel.csdl|res://*/PooledCoverFundModel.ssdl|res://*/PooledCoverFundModel.msl"
            //    }.ConnectionString;

            //configuration.Save();
            //ConfigurationManager.RefreshSection("connectionStrings");
            //return connectionStringsSection.ConnectionStrings["PooledCoverFundModel"].ConnectionString;

     

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["BookServiceContext"].ConnectionString = DatabaseManager.ConnectionString;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            return ConfigurationManager.ConnectionStrings["BookServiceContext"].ConnectionString;
        }

        //protected static string GetServerCurrencyConnectionString()
        //{
        //    var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    var connectionStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

        //    connectionStringsSection.ConnectionStrings["ServerModel"].ConnectionString =
        //        new EntityConnectionStringBuilder
        //        {
        //            ProviderConnectionString = DatabaseManager.ConnectionString,
        //            Provider = "System.Data.SqlClient",
        //            Metadata = "res://*/ServerModel.csdl|res://*/ServerModel.ssdl|res://*/ServerModel.msl"
        //        }.ConnectionString;

        //    configuration.Save();
        //    ConfigurationManager.RefreshSection("connectionStrings");
        //    return connectionStringsSection.ConnectionStrings["ServerModel"].ConnectionString;
        //}

        //protected static void SetupTranslationConnectionString()
        //{
        //    var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    var connectionStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

        //    connectionStringsSection.ConnectionStrings["TranslationServiceModel"].ConnectionString =
        //        new EntityConnectionStringBuilder
        //        {
        //            ProviderConnectionString = DatabaseManager.ConnectionString,
        //            Provider = "System.Data.SqlClient",
        //            Metadata = "res://*/TranslationServiceModel.csdl|res://*/TranslationServiceModel.ssdl|res://*/TranslationServiceModel.msl"
        //        }.ConnectionString;

        //    configuration.Save();
        //    ConfigurationManager.RefreshSection("connectionStrings");
        //}

        protected static string GetBusinessConnectionString()
        {
            return DatabaseManager.ConnectionString;
        }

        private static string CreateUniqueDatabaseName()
        {
            var suffix = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return string.Format("BookServiceContext_{0}", suffix);
        }
    }
}
