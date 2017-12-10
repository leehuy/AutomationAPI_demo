using System.Globalization;
using TechTalk.SpecFlow;
using BookService.Testing.Core;
using System.Data.SqlClient;
using System.Configuration;
using BookService.Testing.DatabaseConfiguration;

namespace BookService.Testing.Resources
{

        [Binding]
        public class TestInitializer : ApiIntegrationTestBase
        {
            [BeforeScenario]
            public static void BeforeScenario()
            {
            //DbBook.InsertDataInBookTable("1", "Pride and Prejudice", "1813", "9.99", "Comedy of manners", "1");
            //DbBook.InsertDataInBookTable("2", "Northanger Abbey", "1817", "12.95", "Gothic parody", "1");
            //DbBook.InsertDataInBookTable("3", "David Copperfield", "1850", "15.00", "Bildungsroman", "2");
            //DbBook.InsertDataInBookTable("4", "Don Quixote", "1617", "8.95", "Picaresque", "3");
            //DbAuthors.InsertDataInAutherTable("1", "Jane Austen");
            //DbAuthors.InsertDataInAutherTable("2", "Charles Dickens");
            //DbAuthors.InsertDataInAutherTable("3", "Miguel de Cervantes");
        }

            [BeforeTestRun]
            public static void BeforeTestRun()
            {

            SetupLocalDb();
            //DbFunction.CreateAllTable();

            SetupInMemoryServer();



           

            //CultureInfo culture;
            //culture = CultureInfo.CreateSpecificCulture("en");
            //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            DbFunction.DeleteAllData("Books");
            DbFunction.DeleteAllData("Authors");
            //DbPawnDeficit.InsertAllPawnDeficit();
        }

        [AfterTestRun]
            public static void AfterTestRun()
            {
                CleanupLocalDb();
                StopInMemoryServer();
            }
        }
}
