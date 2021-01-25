using NC.SqlBuilder.Models;
using System;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlFromShould
    {
        [Fact]
        public void query_without_tables_throws_exception()
        {
            Assert.Throws<Exception>(() => Builder.Create()
                .ToTable("")
                .AddFields(null)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null));
        }

        [Fact]
        public void query_with_table_name_returns_query()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable]";
            var from = "FROM [dbo].[MyTable]";

            Assert.Equal(builder.Segment.From, from);
            Assert.Equal(builder.Query, query);
        }

        [Fact]
        public void query_with_table_name_and_schema_returns_query()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable", "CustomSchema"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two] FROM [CustomSchema].[MyTable]";
            var from = "FROM [CustomSchema].[MyTable]";

            Assert.Equal(builder.Segment.From, from);
            Assert.Equal(builder.Query, query);
        }

    }
}