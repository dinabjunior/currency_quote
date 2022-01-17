using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions.Equivalency;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace FluentAssertions.Collections
{
    public static class AssertExtensions
    {
        public static void BeEquivalentTo<TEntity>(this Table table, IEnumerable<TEntity> actualItems)
        {
            var expectedItems = table.CreateSet<TEntity>();
            actualItems.Should().BeEquivalentTo(expectedItems, cfg => IncludeTablePropertiesOnly(table, cfg));
        }

        private static EquivalencyAssertionOptions<TExpectation> IncludeTablePropertiesOnly<TExpectation>(Table table, EquivalencyAssertionOptions<TExpectation> cfg)
        {
            var properties = typeof(TExpectation).GetProperties().Select(e => e.Name).ToArray();
            foreach (var propertyName in properties.Where(prop => table.Header.Contains(prop, StringComparer.InvariantCultureIgnoreCase)))
            {
                cfg.Including(e => e.Name == propertyName);
            }

            return cfg;
        }
    }
}
