using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities.Base;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Br.Com.Company.CurrentQuote.IT.Support.Helpers
{
    public class DatabaseHelper
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json", false, false).Build();

        public static async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query) where TEntity : EntityBase
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<TEntity>(query).ConfigureAwait(false);
        }

        public static async Task InsertTableIntoDatabaseAsync<TEntity>(Table table, string tableName)
        {
            using var connection = CreateConnection();

            var properties = GetPropertiesByTable<TEntity>(table);
            var parameters = properties.Select(e => $"@{e}");

            var itemsToInsert = table.CreateSet<TEntity>();

            var insertQuery = $"INSERT INTO {tableName} ({string.Join(",", properties)}) VALUES ({string.Join(",", parameters)})";
            await connection.ExecuteAsync(insertQuery, itemsToInsert).ConfigureAwait(false);
        }

        private static NpgsqlConnection CreateConnection() => new NpgsqlConnection(Configuration.GetConnectionString("global"));

        private static IEnumerable<string> GetPropertiesByTable<TEntity>(Table table)
        {
            var properties = typeof(TEntity).GetProperties().Select(e => e.Name);
            foreach (var propertyName in properties.Where(prop => table.Header.Contains(prop, StringComparer.InvariantCultureIgnoreCase)))
            {
                yield return propertyName;
            }
        }
    }
}
