using System.Globalization;
using TechTalk.SpecFlow;
using BookService.Testing.Core;
using System.Data.SqlClient;
using System.Configuration;


namespace BookService.Testing.Resources
{

        [Binding]
        public class TestInitializer : ApiIntegrationTestBase
        {
            [BeforeScenario]
            public static void BeforeScenario()
            {
            //DbFunction.DeleteAllData("currency");
            //DbPawnDeficit.InsertAllPawnDeficit();
        }

            [BeforeTestRun]
            public static void BeforeTestRun()
            {

            SetupLocalDb();

            SetupInMemoryServer();

                //CultureInfo culture;
                //culture = CultureInfo.CreateSpecificCulture("en");
                //System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                //System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            }

            [AfterScenario]
            public static void AfterScenario()
            {
            }

            [AfterTestRun]
            public static void AfterTestRun()
            {
                CleanupLocalDb();
                StopInMemoryServer();
            }
        }
}
