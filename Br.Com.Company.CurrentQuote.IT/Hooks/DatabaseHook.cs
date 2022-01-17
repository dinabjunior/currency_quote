using System;
using System.Threading.Tasks;
using Dapper;
using TechTalk.SpecFlow;

namespace Br.Com.Company.CurrentQuote.IT.Hooks
{
    [Binding]
    public class DatabaseHook : BaseHook
    {
        [BeforeTestRun(Order = 2)]
        public static async Task BeforeTestRun()
        {
            try
            {
                await ClearDatabase().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

            }
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await ClearDatabase().ConfigureAwait(false);
        }

        private static async Task ClearDatabase()
        {
            using var dbConnection = CreateConnection();
            await dbConnection.ExecuteAsync("TRUNCATE TABLE dbo.segment_rate").ConfigureAwait(false);
        }
    }
}
