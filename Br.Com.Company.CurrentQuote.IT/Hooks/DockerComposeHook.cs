using System;
using System.IO;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TechTalk.SpecFlow;

namespace Br.Com.Company.CurrentQuote.IT.Hooks
{
    [Binding]
    public class DockerComposeHook : BaseHook
    {
        private static readonly ICompositeService DockerCompose = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(Path.Combine(Directory.GetCurrentDirectory(), "Support", "docker-compose-test.yml"))
            .RemoveOrphans()
            .WaitForPort("postgres", "5432/tcp", 10000)
            .Wait("postgres", (service, count) => CheckConnection(count))
            .Build();

        [BeforeTestRun(Order = 1)]
        public static void DockerComposeUp()
        {
            DockerCompose.Start();
        }

        private static int CheckConnection(int qtdAtempts)
        {
            if (qtdAtempts > 10)
                throw new FluentDockerException("Unable to establish a connection to the database");

            using var connection = new NpgsqlConnection(Configuration.GetConnectionString("global"));
            try
            {
                connection.Open();
                return 0;
            }
            catch (Exception ex) when ((ex as PostgresException)?.SqlState == "3D000") // 3D000: Database does not exist
            {
                return 0; // Successfully established the connection
            }
            catch (Exception)
            {
                return 500;
            }
        }
    }
}
