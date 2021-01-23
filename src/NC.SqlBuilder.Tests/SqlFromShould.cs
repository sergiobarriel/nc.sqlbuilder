using System;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlFromShould
    {
        [Fact]
        public void query_without_tables_throws_exception()
        {
            var builder = SqlBuilder.Create()
                .ToTable(null)
                .AddFields(null)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null);

            Assert.Throws<Exception>(() => builder.Build());
        }

        [Fact]
        public void query_with_table_returns_query()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(Default.DefaultFields)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two], [Three] FROM [dbo].[MyTable]";
            var from = "FROM [dbo].[MyTable]";

            Assert.Equal(builder.Segment.From, from);
            Assert.Equal(builder.Query, query);
        }

    }
}