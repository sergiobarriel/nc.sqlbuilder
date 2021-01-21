using System;
using Xunit;

namespace SqlBuilder.Tests
{
    public class SqlSelectShould
    {
        [Fact]
        public void query_without_fields_throws_exception()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(null)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null);

            Assert.Throws<Exception>(() =>  builder.Build() );
        }

        [Fact]
        public void query_with_many_fields_returns_query()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(Default.DefaultFields)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two], [Three] FROM [dbo].[MyTable]";
            var select = "SELECT [One], [Two], [Three]";

            Assert.Equal(builder.Segment.Select, select);
            Assert.Equal(builder.Query, query);
        }

        [Fact]
        public void query_with_all_fields_returns_query()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddAllFields()
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT * FROM [dbo].[MyTable]";
            var select = "SELECT *";

            Assert.Equal(builder.Segment.Select, select);
            Assert.Equal(builder.Query, query);
        }
    }
}